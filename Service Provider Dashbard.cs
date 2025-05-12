using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseProject;
using Microsoft.Data.SqlClient;
using TravelEase;

using TripBookingReportApp;

namespace Service_Provider_Section
{
    public partial class ServiceProviderDashboard : Form
    {
        private string serviceProviderId;
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        public ServiceProviderDashboard(string id)
        {
            InitializeComponent();
            serviceProviderId = id; // Store for future use
            LoadProviderDetails(); // Load provider details on form initialization
        }

        private void LoadProviderDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT SP.ProviderID, U.FirstName, U.LastName, SP.CompanyName, SP.Rating
                                    FROM SERVICE_PROVIDER SP
                                    JOIN [USER] U ON SP.ProviderID = U.UserID
                                    WHERE SP.ProviderID = @ProviderID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProviderID", serviceProviderId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update the UI with provider information
                                lblProviderID.Text = reader["ProviderID"].ToString();
                                string fullName = $"{reader["FirstName"]} {reader["LastName"]}";
                                lblProviderName.Text = fullName;

                                if (reader["CompanyName"] != DBNull.Value)
                                    lblCompanyName.Text = reader["CompanyName"].ToString();
                                else
                                    lblCompanyName.Text = "Not specified";

                                if (reader["Rating"] != DBNull.Value)
                                    lblRating.Text = reader["Rating"].ToString();
                                else
                                    lblRating.Text = "Not yet rated";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading provider details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.lblHeading = new System.Windows.Forms.Label();
            this.btnListServices = new System.Windows.Forms.Button();
            this.btnServiceIntegration = new System.Windows.Forms.Button();
            this.btnBookingManagement = new System.Windows.Forms.Button();
            this.btnPerformanceReports = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();

            // New components for provider details
            this.panelProviderInfo = new System.Windows.Forms.Panel();
            this.lblProviderIDTitle = new System.Windows.Forms.Label();
            this.lblProviderID = new System.Windows.Forms.Label();
            this.lblProviderNameTitle = new System.Windows.Forms.Label();
            this.lblProviderName = new System.Windows.Forms.Label();
            this.lblCompanyNameTitle = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblRatingTitle = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblServiceTypeTitle = new System.Windows.Forms.Label();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.panelProviderInfo.SuspendLayout();

            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeading.Location = new System.Drawing.Point(80, 20);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(438, 45);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Service Provider Dashboard";
            // 
            // panelProviderInfo
            // 
            this.panelProviderInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelProviderInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProviderInfo.Controls.Add(this.lblServiceType);
            this.panelProviderInfo.Controls.Add(this.lblServiceTypeTitle);
            this.panelProviderInfo.Controls.Add(this.lblRating);
            this.panelProviderInfo.Controls.Add(this.lblRatingTitle);
            this.panelProviderInfo.Controls.Add(this.lblCompanyName);
            this.panelProviderInfo.Controls.Add(this.lblCompanyNameTitle);
            this.panelProviderInfo.Controls.Add(this.lblProviderName);
            this.panelProviderInfo.Controls.Add(this.lblProviderNameTitle);
            this.panelProviderInfo.Controls.Add(this.lblProviderID);
            this.panelProviderInfo.Controls.Add(this.lblProviderIDTitle);
            this.panelProviderInfo.Location = new System.Drawing.Point(360, 80);
            this.panelProviderInfo.Name = "panelProviderInfo";
            this.panelProviderInfo.Size = new System.Drawing.Size(300, 190);
            this.panelProviderInfo.TabIndex = 6;
            // 
            // lblProviderIDTitle
            // 
            this.lblProviderIDTitle.AutoSize = true;
            this.lblProviderIDTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProviderIDTitle.Location = new System.Drawing.Point(10, 10);
            this.lblProviderIDTitle.Name = "lblProviderIDTitle";
            this.lblProviderIDTitle.Size = new System.Drawing.Size(100, 20);
            this.lblProviderIDTitle.TabIndex = 0;
            this.lblProviderIDTitle.Text = "Provider ID:";
            // 
            // lblProviderID
            // 
            this.lblProviderID.AutoSize = true;
            this.lblProviderID.Location = new System.Drawing.Point(120, 10);
            this.lblProviderID.Name = "lblProviderID";
            this.lblProviderID.Size = new System.Drawing.Size(50, 20);
            this.lblProviderID.TabIndex = 1;
            this.lblProviderID.Text = "...";
            // 
            // lblProviderNameTitle
            // 
            this.lblProviderNameTitle.AutoSize = true;
            this.lblProviderNameTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProviderNameTitle.Location = new System.Drawing.Point(10, 40);
            this.lblProviderNameTitle.Name = "lblProviderNameTitle";
            this.lblProviderNameTitle.Size = new System.Drawing.Size(100, 20);
            this.lblProviderNameTitle.TabIndex = 2;
            this.lblProviderNameTitle.Text = "Name:";
            // 
            // lblProviderName
            // 
            this.lblProviderName.AutoSize = true;
            this.lblProviderName.Location = new System.Drawing.Point(120, 40);
            this.lblProviderName.Name = "lblProviderName";
            this.lblProviderName.Size = new System.Drawing.Size(50, 20);
            this.lblProviderName.TabIndex = 3;
            this.lblProviderName.Text = "...";
            // 
            // lblCompanyNameTitle
            // 
            this.lblCompanyNameTitle.AutoSize = true;
            this.lblCompanyNameTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompanyNameTitle.Location = new System.Drawing.Point(10, 70);
            this.lblCompanyNameTitle.Name = "lblCompanyNameTitle";
            this.lblCompanyNameTitle.Size = new System.Drawing.Size(100, 20);
            this.lblCompanyNameTitle.TabIndex = 4;
            this.lblCompanyNameTitle.Text = "Company:";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(120, 70);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(50, 20);
            this.lblCompanyName.TabIndex = 5;
            this.lblCompanyName.Text = "...";
            // 
            // lblRatingTitle
            // 
            this.lblRatingTitle.AutoSize = true;
            this.lblRatingTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRatingTitle.Location = new System.Drawing.Point(10, 100);
            this.lblRatingTitle.Name = "lblRatingTitle";
            this.lblRatingTitle.Size = new System.Drawing.Size(100, 20);
            this.lblRatingTitle.TabIndex = 6;
            this.lblRatingTitle.Text = "Rating:";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(120, 100);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(50, 20);
            this.lblRating.TabIndex = 7;
            this.lblRating.Text = "...";
            // 

            // 
            // btnListServices
            // 
            this.btnListServices.Location = new System.Drawing.Point(100, 80);
            this.btnListServices.Name = "btnListServices";
            this.btnListServices.Size = new System.Drawing.Size(200, 40);
            this.btnListServices.TabIndex = 1;
            this.btnListServices.Text = "List Services";
            this.btnListServices.Click += new System.EventHandler(this.BtnListServices_Click);
            // 
            // btnServiceIntegration
            // 
            this.btnServiceIntegration.Location = new System.Drawing.Point(100, 130);
            this.btnServiceIntegration.Name = "btnServiceIntegration";
            this.btnServiceIntegration.Size = new System.Drawing.Size(200, 40);
            this.btnServiceIntegration.TabIndex = 2;
            this.btnServiceIntegration.Text = "Service Integration";
            this.btnServiceIntegration.Click += new System.EventHandler(this.BtnServiceIntegration_Click);
            // 
            // btnBookingManagement
            // 
            this.btnBookingManagement.Location = new System.Drawing.Point(100, 180);
            this.btnBookingManagement.Name = "btnBookingManagement";
            this.btnBookingManagement.Size = new System.Drawing.Size(200, 40);
            this.btnBookingManagement.TabIndex = 3;
            this.btnBookingManagement.Text = "Booking Management";
            this.btnBookingManagement.Click += new System.EventHandler(this.BtnBookingManagement_Click);
            // 
            // btnPerformanceReports
            // 
            this.btnPerformanceReports.Location = new System.Drawing.Point(100, 230);
            this.btnPerformanceReports.Name = "btnPerformanceReports";
            this.btnPerformanceReports.Size = new System.Drawing.Size(200, 40);
            this.btnPerformanceReports.TabIndex = 4;
            this.btnPerformanceReports.Text = "Performance Reports";
            this.btnPerformanceReports.Click += new System.EventHandler(this.BtnPerformanceReports_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(10, 300);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // ServiceProviderDashboard
            // 
            this.ClientSize = new System.Drawing.Size(705, 350);
            this.Controls.Add(this.panelProviderInfo);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btnListServices);
            this.Controls.Add(this.btnServiceIntegration);
            this.Controls.Add(this.btnBookingManagement);
            this.Controls.Add(this.btnPerformanceReports);
            this.Controls.Add(this.btnBack);
            this.Name = "ServiceProviderDashboard";
            this.Text = "Service Provider Dashboard";
            this.panelProviderInfo.ResumeLayout(false);
            this.panelProviderInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnListServices_Click(object sender, EventArgs e)
        {
            var form = new Service_Listing(serviceProviderId);// Pass the ID to the next form
            form.Show();
        }

        private void BtnServiceIntegration_Click(object sender, EventArgs e)
        {
            // Navigate to another form (to be created) ServiceIntegration
            var form = new Service_Integration(serviceProviderId); // Pass the ID to the next form
            form.Show();
            this.Hide();
        }

        private void BtnBookingManagement_Click(object sender, EventArgs e)
        {
            // Navigate to another form (to be created) Booking Management
            var form = new Booking_Management(serviceProviderId); // Pass the ID to the next form
            form.Show();
            this.Hide();
        }

        private void BtnPerformanceReports_Click(object sender, EventArgs e)
        {
            // Navigate to another form (to be created) Performance Report
            var form = new PerformanceReportsForm(serviceProviderId); // Pass the ID to the next form
            form.Show();
            this.Hide();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Navigate back to login form
            // Assuming you have a login form class
            var form = new LoginForm();
            form.Show();
            this.Close();
        }

        // Form controls declaration
        private Label lblHeading;
        private Button btnListServices;
        private Button btnServiceIntegration;
        private Button btnBookingManagement;
        private Button btnPerformanceReports;
        private Button btnBack;

        // New controls for provider details
        private Panel panelProviderInfo;
        private Label lblProviderIDTitle;
        private Label lblProviderID;
        private Label lblProviderNameTitle;
        private Label lblProviderName;
        private Label lblCompanyNameTitle;
        private Label lblCompanyName;
        private Label lblRatingTitle;
        private Label lblRating;
        private Label lblServiceTypeTitle;
        private Label lblServiceType;
    }
}