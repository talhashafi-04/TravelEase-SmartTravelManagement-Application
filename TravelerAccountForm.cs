using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class TravelerAccountForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private string travelerId;
        private bool isEditMode = false;

        public TravelerAccountForm(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            ConfigureUIElements();
            LoadTravelerData();
            
        }

        private void ConfigureUIElements()
        {
            // Set rounded corners for panels and buttons
            panelHeader.BackColor = Color.FromArgb(41, 128, 185);
            btnSave.BackColor = Color.FromArgb(41, 128, 185);
            btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            btnEdit.BackColor = Color.FromArgb(52, 152, 219);

            // Apply rounded corners
            btnSave.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSave.Width, btnSave.Height, 10, 10));
            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 10, 10));
            btnEdit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnEdit.Width, btnEdit.Height, 10, 10));

            // Group boxes styling
            gbPersonalInfo.ForeColor = Color.FromArgb(41, 128, 185);
            gbContactInfo.ForeColor = Color.FromArgb(41, 128, 185);
            gbPreferences.ForeColor = Color.FromArgb(41, 128, 185);
            gbSecurity.ForeColor = Color.FromArgb(41, 128, 185);

            // Set all fields to readonly initially
            SetFieldsReadOnly(true);
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void LoadTravelerData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT U.FirstName, U.LastName, U.Email, U.PhoneNumber, U.Address, 
                                           T.CNIC, T.DateOfBirth, T.Age, T.Nationality, T.PreferredLanguage, 
                                           T.LoyaltyPoints, T.EmergencyContactNumber
                                    FROM [USER] U 
                                    INNER JOIN TRAVELER T ON U.UserID = T.TravelerID
                                    WHERE U.UserID = @TravelerID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Personal Information
                            txtFirstName.Text = reader["FirstName"].ToString();
                            txtLastName.Text = reader["LastName"].ToString();
                            txtCNIC.Text = reader["CNIC"].ToString();
                            dtpDateOfBirth.Value = Convert.ToDateTime(reader["DateOfBirth"]);
                            lblAgeValue.Text = reader["Age"].ToString();
                            txtNationality.Text = reader["Nationality"].ToString();

                            // Contact Information
                            txtEmail.Text = reader["Email"].ToString();
                            txtPhoneNumber.Text = reader["PhoneNumber"].ToString();
                            txtAddress.Text = reader["Address"].ToString();
                            txtEmergencyContact.Text = reader["EmergencyContactNumber"].ToString();

                            // Preferences
                            cmbPreferredLanguage.Text = reader["PreferredLanguage"].ToString();
                            lblLoyaltyPointsValue.Text = reader["LoyaltyPoints"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading traveler data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFieldsReadOnly(bool readOnly)
        {
            // Personal Information
            txtFirstName.ReadOnly = readOnly;
            txtLastName.ReadOnly = readOnly;
            txtCNIC.ReadOnly = readOnly;
            dtpDateOfBirth.Enabled = !readOnly;
            txtNationality.ReadOnly = readOnly;

            // Contact Information
            txtEmail.ReadOnly = readOnly;
            txtPhoneNumber.ReadOnly = readOnly;
            txtAddress.ReadOnly = readOnly;
            txtEmergencyContact.ReadOnly = readOnly;

            // Preferences
            cmbPreferredLanguage.Enabled = !readOnly;

            // Security (Password fields remain always editable in a different flow)
            txtCurrentPassword.ReadOnly = false;
            txtNewPassword.ReadOnly = false;
            txtConfirmPassword.ReadOnly = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEditMode = true;
            SetFieldsReadOnly(false);
            btnEdit.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            SetFieldsReadOnly(true);
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            LoadTravelerData(); // Reload original data
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update USER table
                    string userQuery = @"UPDATE [USER] 
                                       SET FirstName = @FirstName, 
                                           LastName = @LastName, 
                                           Email = @Email, 
                                           PhoneNumber = @PhoneNumber, 
                                           Address = @Address
                                       WHERE UserID = @UserID";

                    SqlCommand userCommand = new SqlCommand(userQuery, connection);
                    userCommand.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    userCommand.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    userCommand.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    userCommand.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                    userCommand.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    userCommand.Parameters.AddWithValue("@UserID", travelerId);

                    userCommand.ExecuteNonQuery();

                    // Update TRAVELER table
                    string travelerQuery = @"UPDATE TRAVELER 
                                           SET CNIC = @CNIC, 
                                               DateOfBirth = @DateOfBirth, 
                                               Age = @Age, 
                                               Nationality = @Nationality, 
                                               PreferredLanguage = @PreferredLanguage, 
                                               EmergencyContactNumber = @EmergencyContactNumber
                                           WHERE TravelerID = @TravelerID";

                    SqlCommand travelerCommand = new SqlCommand(travelerQuery, connection);
                    travelerCommand.Parameters.AddWithValue("@CNIC", txtCNIC.Text.Trim());
                    travelerCommand.Parameters.AddWithValue("@DateOfBirth", dtpDateOfBirth.Value);
                    travelerCommand.Parameters.AddWithValue("@Age", CalculateAge(dtpDateOfBirth.Value));
                    travelerCommand.Parameters.AddWithValue("@Nationality", txtNationality.Text.Trim());
                    travelerCommand.Parameters.AddWithValue("@PreferredLanguage", cmbPreferredLanguage.Text);
                    travelerCommand.Parameters.AddWithValue("@EmergencyContactNumber", txtEmergencyContact.Text.Trim());
                    travelerCommand.Parameters.AddWithValue("@TravelerID", travelerId);

                    travelerCommand.ExecuteNonQuery();

                    MessageBox.Show("Account information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Switch back to view mode
                    isEditMode = false;
                    SetFieldsReadOnly(true);
                    btnEdit.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating account information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all password fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Verify current password
                    string verifyQuery = "SELECT COUNT(*) FROM [USER] WHERE UserID = @UserID AND Password = @Password";
                    SqlCommand verifyCommand = new SqlCommand(verifyQuery, connection);
                    verifyCommand.Parameters.AddWithValue("@UserID", travelerId);
                    verifyCommand.Parameters.AddWithValue("@Password", currentPassword); // In a real app, use hashed passwords!

                    int count = (int)verifyCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Current password is incorrect.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update password
                    string updateQuery = "UPDATE [USER] SET Password = @Password WHERE UserID = @UserID";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Password", newPassword); // In a real app, use hashed passwords!
                    updateCommand.Parameters.AddWithValue("@UserID", travelerId);

                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear password fields
                    txtCurrentPassword.Clear();
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateFields()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First name and last name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate email
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate CNIC
            if (!string.IsNullOrWhiteSpace(txtCNIC.Text) && txtCNIC.Text.Length != 13)
            {
                MessageBox.Show("CNIC must be 13 digits long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate phone number (basic validation)
            if (!string.IsNullOrWhiteSpace(txtPhoneNumber.Text) && !Regex.IsMatch(txtPhoneNumber.Text, @"^[\d\-\+\s\(\)]+$"))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                age--;
            return age;
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            lblAgeValue.Text = CalculateAge(dtpDateOfBirth.Value).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}