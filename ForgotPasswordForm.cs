using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class ForgotPasswordForm : Form
    {
        // Database connection string - update with your actual connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if email exists in database
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [USER] WHERE Email = @Email", connection);
                    command.Parameters.AddWithValue("@Email", email);

                    int count = (int)command.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("No account found with this email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // In a real application, you would:
                    // 1. Generate a reset token
                    // 2. Save it to database with expiry
                    // 3. Send email with reset link to user

                    // For this demo, we'll simulate reset by allowing password change directly
                    panelResetPassword.Visible = true;
                    lblEmail.Visible = false;
                    txtEmail.Visible = false;
                    btnResetPassword.Visible = false;

                    // Store email in label
                    lblCurrentEmail.Text = email;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter and confirm your new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("UPDATE [USER] SET Password = @Password WHERE Email = @Email", connection);
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@Email", lblCurrentEmail.Text);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Your password has been reset successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Return to login form
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to reset password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            catch
            {
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to first step
            panelResetPassword.Visible = false;
            lblEmail.Visible = true;
            txtEmail.Visible = true;
            btnResetPassword.Visible = true;

            // Clear password fields
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}