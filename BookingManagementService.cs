using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Service_Provider_Section
{
    public partial class Booking_Management : Form
    {
        private Label lblHeading;
        private Label lblServiceId;
        private Label lblTripId;
        private ComboBox cmbServiceId;
        private ComboBox cmbTripId;
        private Button btnSeeBookings;
        private Button btnBack;
        private DataGridView dgvBookings;
        private Button btnCancelBooking;

        private readonly string serviceProviderID;

        // Connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";

        public Booking_Management(string serviceProviderID)
        {
            this.serviceProviderID = serviceProviderID;
            InitializeComponent();
            LoadServices();
        }

        private void InitializeComponent()
        {
            this.lblHeading = new Label();
            this.lblServiceId = new Label();
            this.lblTripId = new Label();

            this.cmbServiceId = new ComboBox();
            this.cmbTripId = new ComboBox();
            this.btnSeeBookings = new Button();
            this.btnBack = new Button();
            this.dgvBookings = new DataGridView();
            this.btnCancelBooking = new Button();

            this.SuspendLayout();

            // Heading Label
            lblHeading.Text = "Booking Management";
            lblHeading.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblHeading.AutoSize = true;
            lblHeading.Location = new System.Drawing.Point(70, 20);

            // Service ID
            lblServiceId.Text = "Service:";
            lblServiceId.Location = new System.Drawing.Point(50, 80);
            lblServiceId.AutoSize = true;
            cmbServiceId.Location = new System.Drawing.Point(150, 75);
            cmbServiceId.Size = new System.Drawing.Size(200, 26);
            cmbServiceId.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbServiceId.SelectedIndexChanged += new EventHandler(this.CmbServiceId_SelectedIndexChanged);

            // Trip ID
            lblTripId.Text = "Trip:";
            lblTripId.Location = new System.Drawing.Point(50, 120);
            lblTripId.AutoSize = true;
            cmbTripId.Location = new System.Drawing.Point(150, 115);
            cmbTripId.Size = new System.Drawing.Size(200, 26);
            cmbTripId.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTripId.SelectedIndexChanged += new EventHandler(this.CmbTripId_SelectedIndexChanged);

            // See Bookings
            btnSeeBookings.Text = "See the Bookings";
            btnSeeBookings.Location = new System.Drawing.Point(120, 170);
            btnSeeBookings.Size = new System.Drawing.Size(160, 40);
            btnSeeBookings.Click += new EventHandler(this.BtnSeeBookings_Click);

            // Back
            btnBack.Text = "Back";
            btnBack.Location = new System.Drawing.Point(10, 500);
            btnBack.Size = new System.Drawing.Size(75, 30);
            btnBack.Click += new EventHandler(this.BtnBack_Click);

            // DataGridView
            dgvBookings.Location = new System.Drawing.Point(20, 230);
            dgvBookings.Size = new System.Drawing.Size(560, 200);  // Made wider to show more data
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.MultiSelect = false;
            dgvBookings.ReadOnly = true;
            dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Cancel Booking
            btnCancelBooking.Text = "Cancel Selected Booking";
            btnCancelBooking.Location = new System.Drawing.Point(120, 450);
            btnCancelBooking.Size = new System.Drawing.Size(160, 40);
            btnCancelBooking.Click += new EventHandler(this.BtnCancelBooking_Click);

            // Form Settings
            this.ClientSize = new System.Drawing.Size(600, 550);  // Made wider to accommodate the DataGridView
            this.Controls.Add(lblHeading);
            this.Controls.Add(lblServiceId);
            this.Controls.Add(cmbServiceId);
            this.Controls.Add(lblTripId);
            this.Controls.Add(cmbTripId);
            this.Controls.Add(btnSeeBookings);
            this.Controls.Add(btnBack);
            this.Controls.Add(dgvBookings);
            this.Controls.Add(btnCancelBooking);
            this.Name = "Booking_Management";
            this.Text = "Booking Management";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadServices()
        {
            if (string.IsNullOrEmpty(serviceProviderID)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query based on your schema - Get services for this provider
                    string query = @"
                        SELECT s.ServiceID, s.ServiceType, 
                            CASE 
                                WHEN g.GuideID IS NOT NULL THEN 'Guide (ID: ' + CAST(g.GuideID AS VARCHAR) + ')'
                                WHEN h.HotelID IS NOT NULL THEN h.Name + ' (Hotel ID: ' + CAST(h.HotelID AS VARCHAR) + ')'
                                WHEN t.TransportID IS NOT NULL THEN 'Transport (ID: ' + CAST(t.TransportID AS VARCHAR) + ')'
                                ELSE 'Unknown Service'
                            END AS ServiceName
                        FROM SERVICES s
                        LEFT JOIN GUIDE g ON s.ServiceID = g.GuideID AND g.ProviderID = @ServiceProviderID
                        LEFT JOIN HOTEL h ON s.ServiceID = h.HotelID AND h.ProviderID = @ServiceProviderID
                        LEFT JOIN TRANSPORT_PROVIDER t ON s.ServiceID = t.TransportID AND t.ProviderID = @ServiceProviderID
                        WHERE g.ProviderID = @ServiceProviderID 
                           OR h.ProviderID = @ServiceProviderID 
                           OR t.ProviderID = @ServiceProviderID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceProviderID", serviceProviderID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbServiceId.Items.Clear();

                            // Create a dictionary to store service information
                            Dictionary<string, int> serviceDict = new Dictionary<string, int>();

                            while (reader.Read())
                            {
                                int serviceId = Convert.ToInt32(reader["ServiceID"]);
                                string serviceType = reader["ServiceType"].ToString();
                                string serviceName = reader["ServiceName"].ToString();

                                // Format: ServiceType - ServiceName
                                string displayText = $"{serviceType} - {serviceName}";

                                // Add to ComboBox
                                cmbServiceId.Items.Add(displayText);

                                // Store for reference
                                serviceDict[displayText] = serviceId;
                            }

                            // Store the dictionary as a tag on the ComboBox for later reference
                            cmbServiceId.Tag = serviceDict;

                            if (cmbServiceId.Items.Count == 0)
                            {
                                MessageBox.Show("No services found for this provider.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading services: " + ex.Message);
                }
            }
        }

        private void CmbServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceId.SelectedItem == null) return;

            string selectedService = cmbServiceId.SelectedItem.ToString();

            // Extract the service ID from the selected item
            Dictionary<string, int> serviceDict = cmbServiceId.Tag as Dictionary<string, int>;
            if (serviceDict == null || !serviceDict.ContainsKey(selectedService)) return;

            int serviceId = serviceDict[selectedService];

            // Load trips for this service
            LoadTripsForService(serviceId);
        }

        private void LoadTripsForService(int serviceId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to get trips that include this service
                    string query = @"
                        SELECT t.TripID, t.Title, t.Price, t.Duration_Days, d.Name AS Destination,
                               c.Name AS Category
                        FROM TRIP t
                        JOIN TRIP_SERVICES_Renrollment ts ON t.TripID = ts.TripID
                        LEFT JOIN DESTINATION d ON t.DestinationID = d.DestinationID
                        LEFT JOIN TRIP_CATEGORY c ON t.CategoryID = c.CategoryID
                        WHERE ts.ServiceID = @ServiceID
                        ORDER BY t.TripID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ServiceID", serviceId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbTripId.Items.Clear();

                            // Create a dictionary to store trip information
                            Dictionary<string, int> tripDict = new Dictionary<string, int>();

                            while (reader.Read())
                            {
                                int tripId = Convert.ToInt32(reader["TripID"]);
                                string tripTitle = reader["Title"].ToString();
                                decimal price = Convert.ToDecimal(reader["Price"]);
                                string destination = reader["Destination"]?.ToString() ?? "Unknown";

                                // Format: TripID - Title - Destination ($Price)
                                string displayText = $"Trip #{tripId} - {tripTitle} - {destination} (${price:F2})";

                                // Add to ComboBox
                                cmbTripId.Items.Add(displayText);

                                // Store for reference
                                tripDict[displayText] = tripId;
                            }

                            // Store the dictionary as a tag on the ComboBox for later reference
                            cmbTripId.Tag = tripDict;

                            if (cmbTripId.Items.Count == 0)
                            {
                                MessageBox.Show("No trips found for this service.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading trips: " + ex.Message);
                }
            }
        }

        private void CmbTripId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTripId.SelectedItem != null)
            {
                // Auto-load bookings when both service and trip are selected
                BtnSeeBookings_Click(sender, e);
            }
        }

        private void BtnSeeBookings_Click(object sender, EventArgs e)
        {
            if (cmbServiceId.SelectedItem == null || cmbTripId.SelectedItem == null)
            {
                MessageBox.Show("Please select both Service and Trip.");
                return;
            }

            // Extract the Service ID and Trip ID from the selected items
            Dictionary<string, int> serviceDict = cmbServiceId.Tag as Dictionary<string, int>;
            Dictionary<string, int> tripDict = cmbTripId.Tag as Dictionary<string, int>;

            if (serviceDict == null || tripDict == null) return;

            string selectedService = cmbServiceId.SelectedItem.ToString();
            string selectedTrip = cmbTripId.SelectedItem.ToString();

            if (!serviceDict.ContainsKey(selectedService) || !tripDict.ContainsKey(selectedTrip)) return;

            int serviceId = serviceDict[selectedService];
            int tripId = tripDict[selectedTrip];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            B.BookingID, 
                            B.Date, 
                            B.Status, 
                            B.NoOfTravelers, 
                            B.TotalAmount,
                            U.FirstName + ' ' + U.LastName AS TravelerName,
                            B.BookingNotes
                        FROM BOOKING B
                        JOIN TRAVELER T ON B.TravelerID = T.TravelerID
                        JOIN [USER] U ON T.TravelerID = U.UserID
                        WHERE B.TripID = @TripID
                          AND B.Status = 'Confirmed'
                          AND EXISTS (
                              SELECT 1 FROM TRIP_SERVICES_Renrollment TS
                              WHERE TS.TripID = B.TripID AND TS.ServiceID = @ServiceID
                          )";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TripID", tripId);
                    cmd.Parameters.AddWithValue("@ServiceID", serviceId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvBookings.DataSource = dt;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("No confirmed bookings found for this trip.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading bookings: " + ex.Message);
                }
            }
        }

        private void BtnCancelBooking_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to cancel.");
                return;
            }

            var bookingId = dgvBookings.SelectedRows[0].Cells["BookingID"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this booking?\nThis will delete the booking record.",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Using a transaction to ensure data integrity
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // First update the booking status to 'Cancelled'
                            string updateQuery = @"
                                UPDATE BOOKING 
                                SET Status = 'Cancelled', 
                                    CancellationReason = 'Cancelled by service provider'
                                WHERE BookingID = @BookingID";

                            SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                            updateCmd.Parameters.AddWithValue("@BookingID", bookingId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                transaction.Commit();
                                MessageBox.Show("Booking cancelled successfully.");
                                BtnSeeBookings_Click(null, null); // Refresh the list
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Failed to cancel the booking. No rows were affected.");
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Transaction failed: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error cancelling booking: " + ex.Message);
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            var dashboard = new ServiceProviderDashboard(serviceProviderID);
            dashboard.Show();
            this.Close();
        }
    }
}