using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DatabaseProject;
using Service_Provider_Section;


namespace TravelEase
{
    public partial class LoginForm : Form
    {
        // Database connection string - replace with your actual connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True";

        public LoginForm()
        {
            InitializeComponent();
            ConfigureUIElements();
        }

        private void ConfigureUIElements()
        {
            // Set rounded corners for buttons and panels
            btnLogin.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnLogin.Width, btnLogin.Height, 10, 10));
            btnForgotPassword.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnForgotPassword.Width, btnForgotPassword.Height, 10, 10));
            panelLeft.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelLeft.Width, panelLeft.Height, 20, 20));
        }

        // P/Invoke method for creating rounded rectangles
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT UserID, UserRole, Status FROM [USER] WHERE Email = @Email AND Password = @Password", connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password); // In a real app, use hashed passwords!

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string userID = reader["UserID"].ToString();
                        string userRole = reader["UserRole"].ToString();
                        string status = reader["Status"].ToString();

                        if (status == "Inactive" || status == "Banned")
                        {
                            MessageBox.Show("Your account is " + status.ToLower() + ". Please contact an administrator.", "Account Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Update last login timestamp
                        UpdateLastLoginTime(userID);

                        // Redirect to appropriate dashboard based on user role
                        switch (userRole)
                        {
                            case "Traveler":
                                TravelerDashboard travelerForm = new TravelerDashboard(userID);
                                travelerForm.Show();
                                break;
                            case "ServiceProvider":
                                ServiceProviderDashboard providerForm = new ServiceProviderDashboard(userID);
                                providerForm.Show();
                                break;
                            case "TourOperator":
                                Form1 operatorForm = new Form1(userID);
                                operatorForm.Show();
                                break;
                            case "Admin":
                                AdminDashboardForm adminForm = new AdminDashboardForm(userID);
                                adminForm.Show();
                                break;
                            default:
                                MessageBox.Show("Unknown user role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                        }
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLastLoginTime(string userID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE [USER] SET LastLogIn = GETDATE() WHERE UserID = @UserID", connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Just log this error, don't stop the login process
                Console.WriteLine("Error updating last login time: " + ex.Message);
            }
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm();
            forgotPasswordForm.Show();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}