using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace DatabaseProject
{
    public partial class Form1 : Form
    {
        private string _operatorId;
        private SqlConnection con = new SqlConnection(@"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");

        // Dashboard controls
        private Label lblActiveTrips;
        private Label lblTotalBookings;
        private Label lblRevenue;
        private Button btnTripManagement;
        private Button btnBookingManagement;
        private Button btnAnalytics;
        private Button btnViewReports;
        private DataGridView dgvAlerts;

        public Form1(string operatorId)
        {
            _operatorId = operatorId;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = $"Tour Operator Dashboard — {_operatorId}";
            this.ClientSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            var boldFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Summary Labels
            lblActiveTrips = new Label { Text = "Active Trips: 0", Location = new Point(20, 20), AutoSize = true, Font = boldFont };
            lblTotalBookings = new Label { Text = "Total Bookings: 0", Location = new Point(20, 50), AutoSize = true, Font = boldFont };
            lblRevenue = new Label { Text = "Revenue: $0.00", Location = new Point(20, 80), AutoSize = true, Font = boldFont };

            // Navigation Buttons
            btnTripManagement = new Button { Text = "Trip Management", Location = new Point(20, 120), Size = new Size(150, 35) };
            btnBookingManagement = new Button { Text = "Booking Management", Location = new Point(190, 120), Size = new Size(150, 35) };
            btnAnalytics = new Button { Text = "Analytics", Location = new Point(360, 120), Size = new Size(150, 35) };
            btnViewReports = new Button { Text = "View Reports", Location = new Point(530, 120), Size = new Size(150, 35) };

            // Alerts DataGridView
            dgvAlerts = new DataGridView
            {
                Location = new Point(20, 170),
                Size = new Size(740, 380),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvAlerts.Columns.Add("AlertType", "Alert Type");
            dgvAlerts.Columns.Add("Message", "Message");
            dgvAlerts.Columns.Add("Date", "Date");

            this.Controls.AddRange(new Control[]
            {
                lblActiveTrips, lblTotalBookings, lblRevenue,
                btnTripManagement, btnBookingManagement, btnAnalytics, btnViewReports,
                dgvAlerts
            });

            // Event handlers
            this.Load += Form1_Load;
            btnTripManagement.Click += BtnTripManagement_Click;
            btnBookingManagement.Click += BtnBookingManagement_Click;
            btnAnalytics.Click += BtnAnalytics_Click;
            btnViewReports.Click += BtnViewReports_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshDashboard();
            LoadAlerts();
        }

        private void RefreshDashboard()
        {
            try
            {
                con.Open();

                // Active Trips
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM TRIP WHERE Status='Active' AND OperatorID=@OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    lblActiveTrips.Text = "Active Trips: " + cmd.ExecuteScalar();
                }

                // Total Bookings
                using (var cmd = new SqlCommand(
                    @"SELECT COUNT(*) 
                        FROM BOOKING b
                        JOIN TRIP t ON b.TripID = t.TripID
                       WHERE t.OperatorID = @OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    lblTotalBookings.Text = "Total Bookings: " + cmd.ExecuteScalar();
                }

                // Corrected Revenue Query
                using (var cmd = new SqlCommand(
                    @"SELECT ISNULL(SUM(p.Amount),0)
        FROM PAYMENT p
        JOIN BOOKING b ON p.BookingID = b.BookingID
        JOIN TRIP t ON b.TripID = t.TripID
       WHERE t.OperatorID = @OpID
         AND p.Status = 'Completed'", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    decimal rev = Convert.ToDecimal(cmd.ExecuteScalar());
                    lblRevenue.Text = "Revenue: $" + rev.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadAlerts()
        {
            dgvAlerts.Rows.Clear();

            try
            {
                con.Open();

                // Example: new pending bookings as alerts
                using (var cmd = new SqlCommand(
                    @"SELECT TOP 10 b.BookingID, 'Pending Booking' as Msg, b.Date
                        FROM BOOKING b
                        JOIN TRIP t ON b.TripID = t.TripID
                       WHERE t.OperatorID = @OpID
                         AND b.Status = 'Pending'
                    ORDER BY b.Date DESC", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            dgvAlerts.Rows.Add(
                                rd["Msg"].ToString(),
                                $"Booking #{rd["BookingID"]}",
                                Convert.ToDateTime(rd["Date"]).ToShortDateString()
                            );
                        }
                    }
                }

                // You could also fetch recent reviews, etc.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading alerts: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnTripManagement_Click(object sender, EventArgs e)
        {
            using (var form = new Form2(_operatorId))  // Form2 is your TripManagementForm
                form.ShowDialog(this);

            RefreshDashboard();
            LoadAlerts();
        }

        private void BtnBookingManagement_Click(object sender, EventArgs e)
        {
            using (var form = new TourBookingManagementForm(_operatorId))
                form.ShowDialog(this);

            RefreshDashboard();
            LoadAlerts();
        }

        private void BtnAnalytics_Click(object sender, EventArgs e)
        {
            using (var form = new PerformanceReportsForm(_operatorId))
                form.ShowDialog(this);

            RefreshDashboard();
            LoadAlerts();
        }

        private void BtnViewReports_Click(object sender, EventArgs e)
        {
            using (var form = new ReportsDashboardForm())
                form.ShowDialog(this);

            RefreshDashboard();
            LoadAlerts();
        }
    }
}