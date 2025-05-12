using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Service_Provider_Section
{
    public partial class Service_Listing : Form
    {
        private string _serviceProviderId;
        private DataGridView dgvServices;
        private DataGridView dgvTrips;
        private Label lblTitle;
        private Button btnExportServices;
        private Button btnExportTrips;
        private Button btnBack;
        private string ProviderId;


        // Connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        public Service_Listing(string serviceProviderId)
        {
            if (string.IsNullOrWhiteSpace(serviceProviderId))
            {
                MessageBox.Show("Invalid Service Provider ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _serviceProviderId = serviceProviderId;
            ProviderId = serviceProviderId;
            InitializeComponents();
            LoadServiceListings();
        }

        private void InitializeComponents()
        {
            this.Text = "Service Provider Services & Trips";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title Label
            lblTitle = new Label
            {
                Text = $"Services and Trips for Provider: {_serviceProviderId}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Services DataGridView
            dgvServices = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(960, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Export Services Button
            btnExportServices = new Button
            {
                Text = "Export Services",
                Location = new Point(880, 320),
                Size = new Size(100, 30)
            };
            btnExportServices.Click += BtnExportServices_Click;

            // Trips DataGridView
            dgvTrips = new DataGridView
            {
                Location = new Point(20, 360),
                Size = new Size(960, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Export Trips Button
            btnExportTrips = new Button
            {
                Text = "Export Trips",
                Location = new Point(880, 620),
                Size = new Size(100, 30)
            };
            btnExportTrips.Click += BtnExportTrips_Click;

            // Back Button
            btnBack = new Button
            {
                Text = "Back",
                Location = new Point(20, 620),
                Size = new Size(100, 30)
            };
            btnBack.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                dgvServices,
                btnExportServices,
                dgvTrips,
                btnExportTrips,
                btnBack
            });
        }

        private void LoadServiceListings()
        {
            try
            {
                // Load Services
                string servicesQuery = @"
                SELECT 
                    s.ServiceID,
                    s.ServiceType,
                    CASE 
                        WHEN s.ServiceType = 'Guide' THEN g.Specializations
                        WHEN s.ServiceType = 'Hotel' THEN h.Name
                        WHEN s.ServiceType = 'Transport' THEN tp.Specializations
                    END AS ServiceDetails,
                    CASE 
                        WHEN s.ServiceType = 'Guide' THEN g.Languages
                        WHEN s.ServiceType = 'Hotel' THEN h.Amenities
                        WHEN s.ServiceType = 'Transport' THEN tp.ServiceAreas
                    END AS AdditionalInfo
                FROM SERVICES s
                LEFT JOIN GUIDE g ON s.ServiceID = g.GuideID AND s.ServiceType = 'Guide'
                LEFT JOIN HOTEL h ON s.ServiceID = h.HotelID AND s.ServiceType = 'Hotel'
                LEFT JOIN TRANSPORT_PROVIDER tp ON s.ServiceID = tp.TransportID AND s.ServiceType = 'Transport'
                WHERE 
                    (g.ProviderID = @ProviderId OR 
                     h.ProviderID = @ProviderId OR 
                     tp.ProviderID = @ProviderId)";

                // Load Trips
                string tripsQuery = @"
                SELECT 
                    t.TripID,
                    t.Title,
                    t.StartDate,
                    t.EndDate,
                    t.Price,
                    d.Name AS Destination,
                    tc.Name AS TripCategory
                FROM TRIP t
                JOIN TRIP_SERVICES_Renrollment tsr ON t.TripID = tsr.TripID
                JOIN SERVICES s ON tsr.ServiceID = s.ServiceID
                LEFT JOIN DESTINATION d ON t.DestinationID = d.DestinationID
                LEFT JOIN TRIP_CATEGORY tc ON t.CategoryID = tc.CategoryID
                LEFT JOIN GUIDE g ON s.ServiceID = g.GuideID
                LEFT JOIN HOTEL h ON s.ServiceID = h.HotelID
                LEFT JOIN TRANSPORT_PROVIDER tp ON s.ServiceID = tp.TransportID
                WHERE 
                    (g.ProviderID = @ProviderId OR 
                     h.ProviderID = @ProviderId OR 
                     tp.ProviderID = @ProviderId)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load Services
                    using (SqlCommand cmd = new SqlCommand(servicesQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProviderId", _serviceProviderId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dtServices = new DataTable();
                            adapter.Fill(dtServices);
                            dgvServices.DataSource = dtServices;

                            // Customize Services GridView headers
                            dgvServices.Columns["ServiceID"].HeaderText = "Service ID";
                            dgvServices.Columns["ServiceType"].HeaderText = "Service Type";
                            dgvServices.Columns["ServiceDetails"].HeaderText = "Service Details";
                            dgvServices.Columns["AdditionalInfo"].HeaderText = "Additional Info";
                        }
                    }

                    // Load Trips
                    using (SqlCommand cmd = new SqlCommand(tripsQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProviderId", _serviceProviderId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTrips = new DataTable();
                            adapter.Fill(dtTrips);
                            dgvTrips.DataSource = dtTrips;

                            // Customize Trips GridView headers and formatting
                            dgvTrips.Columns["TripID"].HeaderText = "Trip ID";
                            dgvTrips.Columns["Title"].HeaderText = "Trip Title";
                            dgvTrips.Columns["StartDate"].HeaderText = "Start Date";
                            dgvTrips.Columns["EndDate"].HeaderText = "End Date";
                            dgvTrips.Columns["Price"].HeaderText = "Price";
                            dgvTrips.Columns["Destination"].HeaderText = "Destination";
                            dgvTrips.Columns["TripCategory"].HeaderText = "Category";

                            // Format date columns
                            dgvTrips.Columns["StartDate"].DefaultCellStyle.Format = "dd MMM yyyy";
                            dgvTrips.Columns["EndDate"].DefaultCellStyle.Format = "dd MMM yyyy";
                            dgvTrips.Columns["Price"].DefaultCellStyle.Format = "C2";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading services and trips: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportServices_Click(object sender, EventArgs e)
        {
            ExportToCSV(dgvServices, "ServiceProviderServices");
        }

        private void BtnExportTrips_Click(object sender, EventArgs e)
        {
            ExportToCSV(dgvTrips, "ServiceProviderTrips");
        }

        private void ExportToCSV(DataGridView dgv, string fileNamePrefix)
        {
            if (dgv.DataSource == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = $"{fileNamePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable dt = (DataTable)dgv.DataSource;
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            // Write headers
                            sw.WriteLine(string.Join(",",
                                dgv.Columns.Cast<DataGridViewColumn>()
                                .Select(column => column.HeaderText)));

                            // Write data
                            foreach (DataRow row in dt.Rows)
                            {
                                sw.WriteLine(string.Join(",",
                                    row.ItemArray.Select(field =>
                                        field?.ToString().Replace(",", ";") ?? string.Empty)));
                            }
                        }

                        MessageBox.Show("CSV exported successfully!",
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting CSV: {ex.Message}",
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void BtnBack_Click(object sender, EventArgs e)
        {
            var form = new ServiceProviderDashboard(ProviderId); // ✅ pass ID back
            form.Show();
            this.Close();
        }
    }
}