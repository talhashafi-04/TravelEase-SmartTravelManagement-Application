
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Service_Provider_Section
{
    public partial class AddServices : Form
    {
        private readonly string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";
        private readonly string providerId;

        private ComboBox cmbServiceType;
        private Button btnSubmit;
        private Button btnBack;
        private Panel panelFields;

        public AddServices(string providerId)
        {
            if (string.IsNullOrWhiteSpace(providerId))
                throw new ArgumentException("Provider ID cannot be null or empty", nameof(providerId));

            this.providerId = providerId;
            InitializeDynamicComponents();
        }

        private void InitializeDynamicComponents()
        {
            // Form setup
            this.Text = "Add New Service";
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Service Type Dropdown
            cmbServiceType = new ComboBox
            {
                Location = new System.Drawing.Point(30, 30),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList // Prevent manual typing
            };
            cmbServiceType.Items.AddRange(new string[] { "Hotel", "Guide", "Transport" });
            cmbServiceType.SelectedIndexChanged += CmbServiceType_SelectedIndexChanged;
            this.Controls.Add(cmbServiceType);

            // Panel for dynamic fields
            panelFields = new Panel
            {
                Location = new System.Drawing.Point(30, 70),
                Size = new System.Drawing.Size(500, 350),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelFields);

            // Submit button
            btnSubmit = new Button
            {
                Text = "Add Service",
                Location = new System.Drawing.Point(30, 430),
                Width = 150
            };
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            // Back button
            btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(200, 430),
                Width = 100
            };
            btnBack.Click += (s, e) =>
            {
                new ServiceProviderDashboard(providerId).Show();
                this.Close();
            };
            this.Controls.Add(btnBack);
        }

        private void CmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelFields.Controls.Clear();

            if (cmbServiceType.SelectedItem == null)
                return;

            string selectedType = cmbServiceType.SelectedItem.ToString();

            switch (selectedType)
            {
                case "Hotel":
                    AddInput("Name", required: true);
                    AddInput("Capacity", required: true, numeric: true);
                    AddInput("Amenities", required: true);
                    AddInput("Description", required: false);
                    AddDateInput("StartDate", required: true);
                    break;
                case "Guide":
                    AddInput("Specializations", required: true);
                    AddInput("Languages", required: true);
                    AddInput("Certifications", required: false);
                    AddDateInput("StartDate", required: true);
                    break;
                case "Transport":
                    AddInput("Specializations", required: true);
                    AddInput("Vehicles", required: true);
                    AddInput("LicenseDetails", required: true);
                    AddInput("ServiceAreas", required: true);
                    break;
            }
        }

        private void AddInput(string label, bool required, bool numeric = false)
        {
            int y = panelFields.Controls.Count * 30;

            // Add asterisk for required fields
            var lbl = new Label
            {
                Text = label + (required ? " *" : ""),
                Location = new System.Drawing.Point(0, y + 5),
                Width = 120
            };

            var txt = new TextBox
            {
                Name = "txt" + label,
                Location = new System.Drawing.Point(130, y),
                Width = 250,
                Tag = new { Required = required, Numeric = numeric } // Store validation info
            };

            panelFields.Controls.Add(lbl);
            panelFields.Controls.Add(txt);
        }

        private void AddDateInput(string label, bool required)
        {
            int y = panelFields.Controls.Count * 30;

            var lbl = new Label
            {
                Text = label + (required ? " *" : ""),
                Location = new System.Drawing.Point(0, y + 5),
                Width = 120
            };

            var dtp = new DateTimePicker
            {
                Name = "txt" + label,
                Location = new System.Drawing.Point(130, y),
                Width = 250,
                Format = DateTimePickerFormat.Short,
                Tag = new { Required = required } // Store validation info
            };

            panelFields.Controls.Add(lbl);
            panelFields.Controls.Add(dtp);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate service type selection
                if (cmbServiceType.SelectedItem == null)
                {
                    MessageBox.Show("Please select a service type.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbServiceType.Focus();
                    return;
                }

                string serviceType = cmbServiceType.SelectedItem.ToString();

                // Validate all input fields
                if (!ValidateInputs(serviceType))
                    return;

                // Proceed with database operations
                AddServiceToDatabase(serviceType);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs(string serviceType)
        {
            bool isValid = true;
            string errorMessage = "";

            foreach (Control control in panelFields.Controls)
            {
                if (control is TextBox textBox)
                {
                    var validationInfo = (dynamic)textBox.Tag;
                    bool required = validationInfo?.Required ?? false;
                    bool numeric = validationInfo?.Numeric ?? false;

                    if (required && string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        errorMessage += $"- {textBox.Name.Replace("txt", "")} is required\n";
                        isValid = false;
                    }
                    else if (numeric && !int.TryParse(textBox.Text, out _))
                    {
                        errorMessage += $"- {textBox.Name.Replace("txt", "")} must be a number\n";
                        isValid = false;
                    }
                }
                else if (control is DateTimePicker datePicker)
                {
                    var validationInfo = (dynamic)datePicker.Tag;
                    bool required = validationInfo?.Required ?? false;

                    if (required && datePicker.Value == DateTime.MinValue)
                    {
                        errorMessage += $"- {datePicker.Name.Replace("txt", "")} is required\n";
                        isValid = false;
                    }
                }
            }

            // Additional service-specific validation
            switch (serviceType)
            {
                case "Hotel":
                    if (int.TryParse(GetInput("Capacity"), out int capacity) && capacity <= 0)
                    {
                        errorMessage += "- Capacity must be greater than 0\n";
                        isValid = false;
                    }
                    break;
            }

            if (!isValid)
            {
                MessageBox.Show("Please fix the following errors:\n\n" + errorMessage,
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void AddServiceToDatabase(string serviceType)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Insert into SERVICES and get ServiceID
                    string insertServiceQuery = @"
                        INSERT INTO SERVICES (ServiceType)
                        OUTPUT INSERTED.ServiceID
                        VALUES (@ServiceType)";

                    SqlCommand cmd = new SqlCommand(insertServiceQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@ServiceType", serviceType);
                    int newServiceId = (int)cmd.ExecuteScalar();

                    // 2. Insert into subtype table
                    switch (serviceType)
                    {
                        case "Hotel":
                            InsertHotel(conn, transaction, newServiceId);
                            break;
                        case "Guide":
                            InsertGuide(conn, transaction, newServiceId);
                            break;
                        case "Transport":
                            InsertTransport(conn, transaction, newServiceId);
                            break;
                    }

                    transaction.Commit();
                    MessageBox.Show($"{serviceType} service added successfully with ID: {newServiceId}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset form for new entry
                    cmbServiceType.SelectedIndex = -1;
                    panelFields.Controls.Clear();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Failed to add service: " + ex.Message, ex);
                }
            }
        }

        private void InsertHotel(SqlConnection conn, SqlTransaction transaction, int serviceId)
        {
            string query = @"
                INSERT INTO HOTEL (
                    HotelID, Name, Capacity, Amenities,
                    Description, StartDate, ProviderID
                ) VALUES (
                    @ID, @Name, @Capacity, @Amenities,
                    @Description, @StartDate, @ProviderID
                )";

            SqlCommand cmd = new SqlCommand(query, conn, transaction);
            cmd.Parameters.AddWithValue("@ID", serviceId);
            cmd.Parameters.AddWithValue("@Name", GetInput("Name"));
            cmd.Parameters.AddWithValue("@Capacity", int.Parse(GetInput("Capacity")));
            cmd.Parameters.AddWithValue("@Amenities", GetInput("Amenities"));
            cmd.Parameters.AddWithValue("@Description", GetInput("Description"));
            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(GetInput("StartDate")));
            cmd.Parameters.AddWithValue("@ProviderID", providerId);
            cmd.ExecuteNonQuery();
        }

        private void InsertGuide(SqlConnection conn, SqlTransaction transaction, int serviceId)
        {
            string query = @"
                INSERT INTO GUIDE (
                    GuideID, Specializations, Languages,
                    Certifications, StartDate, ProviderID
                ) VALUES (
                    @ID, @Specializations, @Languages,
                    @Certifications, @StartDate, @ProviderID
                )";

            SqlCommand cmd = new SqlCommand(query, conn, transaction);
            cmd.Parameters.AddWithValue("@ID", serviceId);
            cmd.Parameters.AddWithValue("@Specializations", GetInput("Specializations"));
            cmd.Parameters.AddWithValue("@Languages", GetInput("Languages"));
            cmd.Parameters.AddWithValue("@Certifications", GetInput("Certifications"));
            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(GetInput("StartDate")));
            cmd.Parameters.AddWithValue("@ProviderID", providerId);
            cmd.ExecuteNonQuery();
        }

        private void InsertTransport(SqlConnection conn, SqlTransaction transaction, int serviceId)
        {
            string query = @"
                INSERT INTO TRANSPORT_PROVIDER (
                    TransportID, Specializations, Vehicles,
                    LicenseDetails, ServiceAreas, ProviderID
                ) VALUES (
                    @ID, @Specializations, @Vehicles,
                    @LicenseDetails, @ServiceAreas, @ProviderID
                )";

            SqlCommand cmd = new SqlCommand(query, conn, transaction);
            cmd.Parameters.AddWithValue("@ID", serviceId);
            cmd.Parameters.AddWithValue("@Specializations", GetInput("Specializations"));
            cmd.Parameters.AddWithValue("@Vehicles", GetInput("Vehicles"));
            cmd.Parameters.AddWithValue("@LicenseDetails", GetInput("LicenseDetails"));
            cmd.Parameters.AddWithValue("@ServiceAreas", GetInput("ServiceAreas"));
            cmd.Parameters.AddWithValue("@ProviderID", providerId);
            cmd.ExecuteNonQuery();
        }

        private string GetInput(string fieldName)
        {
            var control = panelFields.Controls.Find("txt" + fieldName, true).FirstOrDefault();

            if (control is TextBox textBox)
                return textBox.Text;

            if (control is DateTimePicker datePicker)
                return datePicker.Value.ToString("yyyy-MM-dd");

            return string.Empty;
        }
    }
}