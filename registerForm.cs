using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class RegisterForm : Form
    {
        // Database connection string - update with your actual connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";

        public RegisterForm()
        {
            InitializeComponent();
            SetupFormControls();
        }

        private void SetupFormControls()
        {
            // Configure user role dropdown
            cmbUserRole.Items.Add("Traveler");
            cmbUserRole.Items.Add("ServiceProvider");
            cmbUserRole.Items.Add("TourOperator");
            cmbUserRole.SelectedIndex = 0; // Default to Traveler

            // Configure service type dropdown
            cmbServiceType.Items.Add("Guide");
            cmbServiceType.Items.Add("Hotel");
            cmbServiceType.Items.Add("Transport");
            cmbServiceType.SelectedIndex = 0;

            // Setup languages combobox
            string[] languages = { "English", "Spanish", "French", "Urdu", "Arabic", "Chinese" };
            cmbLanguage.Items.AddRange(languages);
            cmbLanguage.SelectedIndex = 0;

            // Setup nationality combobox
            string[] countries = { "Pakistan", "India", "USA", "UK", "Canada", "Australia", "China", "Japan" };
            cmbNationality.Items.AddRange(countries);
            cmbNationality.SelectedIndex = 0;

            // Initial visibility setup
            UpdateFieldsVisibility();
        }

        private void UpdateFieldsVisibility()
        {
            // Hide all role-specific fields first
            HideAllRoleSpecificFields();

            // Show fields based on selected role
            string selectedRole = cmbUserRole.SelectedItem.ToString();

            switch (selectedRole)
            {
                case "Traveler":
                    ShowTravelerFields();
                    break;
                case "ServiceProvider":
                    ShowServiceProviderFields();
                    break;
                case "TourOperator":
                    ShowTourOperatorFields();
                    break;
            }
        }

        private void HideAllRoleSpecificFields()
        {
            // Traveler fields
            lblCNIC.Visible = txtCNIC.Visible = false;
            lblDOB.Visible = dtpDateOfBirth.Visible = false;
            lblNationality.Visible = cmbNationality.Visible = false;
            lblLanguage.Visible = cmbLanguage.Visible = false;
            lblEmergency.Visible = txtEmergencyContact.Visible = false;

            // Service provider fields
            lblCompanyName.Visible = txtCompanyName.Visible = false;
            lblServiceType.Visible = cmbServiceType.Visible = false;

            // Tour operator fields
            lblBusinessNumber.Visible = txtBusinessNumber.Visible = false;
            lblEstablishedDate.Visible = dtpEstablishedDate.Visible = false;
            lblCompanyDesc.Visible = txtCompanyDesc.Visible = false;
            lblWebsite.Visible = txtWebsite.Visible = false;
        }

        private void ShowTravelerFields()
        {
            lblCNIC.Visible = txtCNIC.Visible = true;
            lblDOB.Visible = dtpDateOfBirth.Visible = true;
            lblNationality.Visible = cmbNationality.Visible = true;
            lblLanguage.Visible = cmbLanguage.Visible = true;
            lblEmergency.Visible = txtEmergencyContact.Visible = true;
        }

        private void ShowServiceProviderFields()
        {
            lblCompanyName.Visible = txtCompanyName.Visible = true;
            lblServiceType.Visible = cmbServiceType.Visible = true;
        }

        private void ShowTourOperatorFields()
        {
            lblCompanyName.Visible = txtCompanyName.Visible = true;
            lblBusinessNumber.Visible = txtBusinessNumber.Visible = true;
            lblEstablishedDate.Visible = dtpEstablishedDate.Visible = true;
            lblCompanyDesc.Visible = txtCompanyDesc.Visible = true;
            lblWebsite.Visible = txtWebsite.Visible = true;
        }

        private void cmbUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFieldsVisibility();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    // Generate a unique user ID
                    string userID = GenerateUserID();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert into USER table first (parent table)
                        string userInsertQuery = @"INSERT INTO [USER] (UserID, FirstName, LastName, Email, Password, PhoneNumber, Address, Status, RegistrationDate, UserRole)
                                                VALUES (@UserID, @FirstName, @LastName, @Email, @Password, @PhoneNumber, @Address, 'Active', GETDATE(), @UserRole)";

                        SqlCommand userCmd = new SqlCommand(userInsertQuery, connection);
                        userCmd.Parameters.AddWithValue("@UserID", userID);
                        userCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                        userCmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                        userCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        userCmd.Parameters.AddWithValue("@Password", txtPassword.Text); // In production, use hashing
                        userCmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text.Trim());
                        userCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        userCmd.Parameters.AddWithValue("@UserRole", cmbUserRole.SelectedItem.ToString());
                        userCmd.ExecuteNonQuery();

                        // Insert into role-specific table
                        string role = cmbUserRole.SelectedItem.ToString();
                        switch (role)
                        {
                            case "Traveler":
                                InsertTraveler(userID, connection);
                                break;
                            case "ServiceProvider":
                                InsertServiceProvider(userID, connection);
                                break;
                            case "TourOperator":
                                InsertTourOperator(userID, connection);
                                break;
                        }

                        MessageBox.Show("Registration successful! Please login to continue.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Return to login form
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Registration failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertTraveler(string userID, SqlConnection connection)
        {
            // Calculate age based on DOB
            int age = DateTime.Now.Year - dtpDateOfBirth.Value.Year;
            if (DateTime.Now.DayOfYear < dtpDateOfBirth.Value.DayOfYear)
                age--;

            string query = @"INSERT INTO TRAVELER (TravelerID, CNIC, DateOfBirth, Age, Nationality, PreferredLanguage, LoyaltyPoints, EmergencyContactNumber)
                           VALUES (@TravelerID, @CNIC, @DateOfBirth, @Age, @Nationality, @PreferredLanguage, 0, @EmergencyContactNumber)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TravelerID", userID);
            cmd.Parameters.AddWithValue("@CNIC", txtCNIC.Text.Trim());
            cmd.Parameters.AddWithValue("@DateOfBirth", dtpDateOfBirth.Value);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@Nationality", cmbNationality.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@PreferredLanguage", cmbLanguage.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@EmergencyContactNumber", txtEmergencyContact.Text.Trim());

            cmd.ExecuteNonQuery();
        }

        private void InsertServiceProvider(string userID, SqlConnection connection)
        {
            string serviceType = cmbServiceType.SelectedItem.ToString();

            string query = @"INSERT INTO SERVICE_PROVIDER (ProviderID, CompanyName, ServiceType, Rating, IsGuide, IsHotel, IsTransport)
                           VALUES (@ProviderID, @CompanyName, @ServiceType, 0, @IsGuide, @IsHotel, @IsTransport)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProviderID", userID);
            cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
            cmd.Parameters.AddWithValue("@ServiceType", serviceType);
            cmd.Parameters.AddWithValue("@IsGuide", serviceType == "Guide" ? 1 : 0);
            cmd.Parameters.AddWithValue("@IsHotel", serviceType == "Hotel" ? 1 : 0);
            cmd.Parameters.AddWithValue("@IsTransport", serviceType == "Transport" ? 1 : 0);

            cmd.ExecuteNonQuery();
        }

        private void InsertTourOperator(string userID, SqlConnection connection)
        {
            string query = @"INSERT INTO TOUR_OPERATOR (OperatorID, AgencyName, BusinessInsertionNumber, EstablishedDate, URL, CompanyDesc, Rating)
                           VALUES (@OperatorID, @AgencyName, @BusinessInsertionNumber, @EstablishedDate, @URL, @CompanyDesc, 0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@OperatorID", userID);
            cmd.Parameters.AddWithValue("@AgencyName", txtCompanyName.Text.Trim());
            cmd.Parameters.AddWithValue("@BusinessInsertionNumber", txtBusinessNumber.Text.Trim());
            cmd.Parameters.AddWithValue("@EstablishedDate", dtpEstablishedDate.Value);
            cmd.Parameters.AddWithValue("@URL", txtWebsite.Text.Trim());
            cmd.Parameters.AddWithValue("@CompanyDesc", txtCompanyDesc.Text.Trim());

            cmd.ExecuteNonQuery();
        }

        private string GenerateUserID()
        {
            string prefix;
            switch (cmbUserRole.SelectedItem.ToString())
            {
                case "Traveler":
                    prefix = "TR";
                    break;
                case "ServiceProvider":
                    prefix = "SP";
                    break;
                case "TourOperator":
                    prefix = "TO";
                    break;
                default:
                    prefix = "US";
                    break;
            }

            // Generate a random 6-digit number
            Random rand = new Random();
            int randomNumber = rand.Next(100000, 999999);

            return prefix + randomNumber.ToString();
        }

        private bool ValidateInputs()
        {
            // Basic validation for all users
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate email format
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate password match
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Role-specific validation
            switch (cmbUserRole.SelectedItem.ToString())
            {
                case "Traveler":
                    if (string.IsNullOrWhiteSpace(txtCNIC.Text) ||
                        string.IsNullOrWhiteSpace(txtEmergencyContact.Text))
                    {
                        MessageBox.Show("Please fill all traveler-specific fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Validate CNIC format (customize as needed)
                    if (!IsValidCNIC(txtCNIC.Text))
                    {
                        MessageBox.Show("Please enter a valid CNIC number (format: xxxxx-xxxxxxx-x).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Validate age (must be at least 18)
                    int age = DateTime.Now.Year - dtpDateOfBirth.Value.Year;
                    if (DateTime.Now.DayOfYear < dtpDateOfBirth.Value.DayOfYear)
                        age--;

                    if (age < 18)
                    {
                        MessageBox.Show("You must be at least 18 years old to register.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;

                case "ServiceProvider":
                    if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                    {
                        MessageBox.Show("Please enter company name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;

                case "TourOperator":
                    if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                        string.IsNullOrWhiteSpace(txtBusinessNumber.Text) ||
                        string.IsNullOrWhiteSpace(txtCompanyDesc.Text))
                    {
                        MessageBox.Show("Please fill all tour operator-specific fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    break;
            }

            return true;
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

        private bool IsValidCNIC(string cnic)
        {
            try
            {
                // Format: xxxxx-xxxxxxx-x
                return Regex.IsMatch(cnic, @"^\d{5}-\d{7}-\d{1}$");
            }
            catch
            {
                return false;
            }
        }

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}