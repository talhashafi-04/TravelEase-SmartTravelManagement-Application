using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace Service_Provider_Section
{
    public partial class EditServices : Form
    {
        private string connectionString = @"Data Source=LIVERPOOL\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private string serviceProviderID;

        private Label lblServiceID;
        private TextBox txtServiceID;
        private Button btnLoadService;
        private Label lblFields;
        private Panel pnlFields;
        private Button btnUpdate;
        private Button btnBack;

        private string currentServiceType = "";
        private TextBox[] currentTextBoxes;


        public EditServices(string providerID)
        {
            serviceProviderID = providerID;

            // Initialize form components
            this.Text = "Edit Services";
            this.Size = new System.Drawing.Size(600, 500);

            lblServiceID = new Label { Text = "Enter Service ID:", Location = new System.Drawing.Point(30, 20) };
            txtServiceID = new TextBox { Location = new System.Drawing.Point(160, 20), Width = 100 };
            btnLoadService = new Button { Text = "Load", Location = new System.Drawing.Point(270, 18) };
            btnLoadService.Click += BtnLoadService_Click;

            pnlFields = new Panel { Location = new System.Drawing.Point(30, 60), Size = new System.Drawing.Size(520, 300), AutoScroll = true };
            btnUpdate = new Button { Text = "Update", Location = new System.Drawing.Point(250, 370) };
            btnUpdate.Click += BtnUpdate_Click;
            btnUpdate.Enabled = false;

            btnBack = new Button { Text = "Back", Location = new System.Drawing.Point(30, 370) };
            btnBack.Click += (s, e) =>
            {
                new ServiceProviderDashboard(serviceProviderID).Show();
                this.Close();
            };

            this.Controls.Add(lblServiceID);
            this.Controls.Add(txtServiceID);
            this.Controls.Add(btnLoadService);
            this.Controls.Add(pnlFields);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnBack);
        }

        private void BtnLoadService_Click(object sender, EventArgs e)
        {
            pnlFields.Controls.Clear();

            string serviceID = txtServiceID.Text.Trim();

            if (string.IsNullOrEmpty(serviceID))
            {
                MessageBox.Show("Please enter a valid Service ID.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT S.ServiceID, S.ServiceType, G.GuideID, G.Specializations, G.Languages, G.Certifications, H.HotelID, H.Name, H.Capacity, H.Amenities, T.TransportID, T.Specializations, T.ServiceAreas
                                    FROM SERVICES S
                                    LEFT JOIN GUIDE G ON S.ServiceID = G.GuideID AND G.ProviderID = @ProviderID
                                    LEFT JOIN HOTEL H ON S.ServiceID = H.HotelID AND H.ProviderID = @ProviderID
                                    LEFT JOIN TRANSPORT_PROVIDER T ON S.ServiceID = T.TransportID AND T.ProviderID = @ProviderID
                                    WHERE S.ServiceID = @ServiceID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    command.Parameters.AddWithValue("@ProviderID", serviceProviderID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string serviceType = reader["ServiceType"].ToString();

                        if (serviceType == "Guide")
                        {
                            // Create UI fields for Guide service
                            CreateGuideFields(reader);
                        }
                        else if (serviceType == "Hotel")
                        {
                            // Create UI fields for Hotel service
                            CreateHotelFields(reader);
                        }
                        else if (serviceType == "Transport")
                        {
                            // Create UI fields for Transport service
                            CreateTransportFields(reader);
                        }

                        btnUpdate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No service found for the given Service ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading the service: " + ex.Message);
                }
            }
        }

        private void CreateGuideFields(SqlDataReader reader)
        {
            lblFields = new Label { Text = "Guide Details", Location = new System.Drawing.Point(20, 60) };
            pnlFields.Controls.Add(lblFields);

            var txtSpecializations = new TextBox { Text = reader["Specializations"].ToString(), Location = new System.Drawing.Point(20, 90), Width = 200 };
            var txtLanguages = new TextBox { Text = reader["Languages"].ToString(), Location = new System.Drawing.Point(20, 120), Width = 200 };
            var txtCertifications = new TextBox { Text = reader["Certifications"].ToString(), Location = new System.Drawing.Point(20, 150), Width = 200 };

            pnlFields.Controls.Add(txtSpecializations);
            pnlFields.Controls.Add(txtLanguages);
            pnlFields.Controls.Add(txtCertifications);

            currentServiceType = "Guide";
            currentTextBoxes = new[] { txtSpecializations, txtLanguages, txtCertifications };
        }


        private void CreateHotelFields(SqlDataReader reader)
        {
            lblFields = new Label { Text = "Hotel Details", Location = new System.Drawing.Point(20, 60) };
            pnlFields.Controls.Add(lblFields);

            var txtName = new TextBox { Text = reader["Name"].ToString(), Location = new System.Drawing.Point(20, 90), Width = 200 };
            var txtCapacity = new TextBox { Text = reader["Capacity"].ToString(), Location = new System.Drawing.Point(20, 120), Width = 200 };
            var txtAmenities = new TextBox { Text = reader["Amenities"].ToString(), Location = new System.Drawing.Point(20, 150), Width = 200 };

            pnlFields.Controls.Add(txtName);
            pnlFields.Controls.Add(txtCapacity);
            pnlFields.Controls.Add(txtAmenities);
        }

        private void CreateTransportFields(SqlDataReader reader)
        {
            lblFields = new Label { Text = "Transport Details", Location = new System.Drawing.Point(20, 60) };
            pnlFields.Controls.Add(lblFields);

            var txtSpecializations = new TextBox { Text = reader["Specializations"].ToString(), Location = new System.Drawing.Point(20, 90), Width = 200 };
            var txtServiceAreas = new TextBox { Text = reader["ServiceAreas"].ToString(), Location = new System.Drawing.Point(20, 120), Width = 200 };

            pnlFields.Controls.Add(txtSpecializations);
            pnlFields.Controls.Add(txtServiceAreas);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand updateCommand = null;

                    if (currentServiceType == "Guide")
                    {
                        updateCommand = new SqlCommand(
                            "UPDATE GUIDE SET Specializations = @Spec, Languages = @Lang, Certifications = @Cert WHERE GuideID = @ID AND ProviderID = @ProviderID",
                            connection);
                        updateCommand.Parameters.AddWithValue("@Spec", currentTextBoxes[0].Text);
                        updateCommand.Parameters.AddWithValue("@Lang", currentTextBoxes[1].Text);
                        updateCommand.Parameters.AddWithValue("@Cert", currentTextBoxes[2].Text);
                    }
                    else if (currentServiceType == "Hotel")
                    {
                        updateCommand = new SqlCommand(
                            "UPDATE HOTEL SET Name = @Name, Capacity = @Cap, Amenities = @Amen WHERE HotelID = @ID AND ProviderID = @ProviderID",
                            connection);
                        updateCommand.Parameters.AddWithValue("@Name", currentTextBoxes[0].Text);
                        updateCommand.Parameters.AddWithValue("@Cap", currentTextBoxes[1].Text);
                        updateCommand.Parameters.AddWithValue("@Amen", currentTextBoxes[2].Text);
                    }
                    else if (currentServiceType == "Transport")
                    {
                        updateCommand = new SqlCommand(
                            "UPDATE TRANSPORT_PROVIDER SET Specializations = @Spec, ServiceAreas = @Area WHERE TransportID = @ID AND ProviderID = @ProviderID",
                            connection);
                        updateCommand.Parameters.AddWithValue("@Spec", currentTextBoxes[0].Text);
                        updateCommand.Parameters.AddWithValue("@Area", currentTextBoxes[1].Text);
                    }
                    else
                    {
                        MessageBox.Show("Unknown service type.");
                        return;
                    }

                    updateCommand.Parameters.AddWithValue("@ID", txtServiceID.Text.Trim());
                    updateCommand.Parameters.AddWithValue("@ProviderID", serviceProviderID);

                    int rows = updateCommand.ExecuteNonQuery();
                    if (rows > 0)
                        MessageBox.Show("Service updated successfully.");
                    else
                        MessageBox.Show("Update failed. Please ensure the service belongs to you.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating the service: " + ex.Message);
                }
            }
        }

    }
}
