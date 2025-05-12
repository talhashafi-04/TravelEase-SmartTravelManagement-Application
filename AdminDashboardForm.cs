using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class AdminDashboardForm : Form
    {
        private readonly string _adminUserId;

        // Database connection
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

        // Welcome label
        private Label lblWelcome;

        // Summary labels
        private Label lblTotalUsers;
        private Label lblTotalOperators;
        private Label lblTotalTrips;
        private Label lblTotalBookings;

        // Alerts grid
        private DataGridView dgvAlerts;

        // Navigation buttons
        private Button btnUserMgmt;
        private Button btnOperatorMgmt;
        private Button btnCategoryMgmt;
        private Button btnPlatformAnalytics;
        private Button btnReviewModeration;

        public AdminDashboardForm(string adminUserId)
        {
            _adminUserId = adminUserId;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = $"Admin Dashboard - {_adminUserId}";
            this.ClientSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            var labelFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Welcome Label
            lblWelcome = new Label
            {
                Text = $"Welcome, Admin {_adminUserId}",
                Location = new Point(20, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };
            this.Controls.Add(lblWelcome);

            // Summary Labels
            lblTotalUsers = new Label { Text = "Total Users: 0", Location = new Point(20, 40), AutoSize = true, Font = labelFont };
            lblTotalOperators = new Label { Text = "Total Operators: 0", Location = new Point(20, 80), AutoSize = true, Font = labelFont };
            lblTotalTrips = new Label { Text = "Total Trips: 0", Location = new Point(300, 40), AutoSize = true, Font = labelFont };
            lblTotalBookings = new Label { Text = "Total Bookings: 0", Location = new Point(300, 80), AutoSize = true, Font = labelFont };

            // Alerts DataGridView
            dgvAlerts = new DataGridView
            {
                Location = new Point(20, 120),
                Size = new Size(860, 350),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
            };
            dgvAlerts.Columns.Add("AlertType", "Alert Type");
            dgvAlerts.Columns.Add("Description", "Description");
            dgvAlerts.Columns.Add("Date", "Date");

            // Navigation Buttons
            btnUserMgmt = new Button { Text = "User Management", Location = new Point(20, 490), Size = new Size(160, 40) };
            btnOperatorMgmt = new Button { Text = "Operator Management", Location = new Point(200, 490), Size = new Size(160, 40) };
            btnCategoryMgmt = new Button { Text = "Category Management", Location = new Point(380, 490), Size = new Size(160, 40) };
            btnPlatformAnalytics = new Button { Text = "Platform Analytics", Location = new Point(560, 490), Size = new Size(160, 40) };
            btnReviewModeration = new Button { Text = "Review Moderation", Location = new Point(740, 490), Size = new Size(160, 40) };

            this.Controls.AddRange(new Control[]
            {
                lblTotalUsers, lblTotalOperators, lblTotalTrips, lblTotalBookings,
                dgvAlerts,
                btnUserMgmt, btnOperatorMgmt, btnCategoryMgmt, btnPlatformAnalytics, btnReviewModeration
            });

            // Event wiring
            this.Load += AdminDashboardForm_Load;
            btnUserMgmt.Click += (s, e) => new UserManagementForm().ShowDialog(this);
            btnOperatorMgmt.Click += (s, e) => new OperatorManagementForm().ShowDialog(this);
            btnCategoryMgmt.Click += (s, e) => new TourCategoriesManagementForm().ShowDialog(this);
            btnPlatformAnalytics.Click += (s, e) => new PlatformAnalyticsForm().ShowDialog(this);
            btnReviewModeration.Click += (s, e) => new ReviewModerationForm().ShowDialog(this);
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            RefreshStatistics();
            LoadAlerts();
        }

        private void RefreshStatistics()
        {
            try
            {
                con.Open();

                // Total Users
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM [USER]", con))
                    lblTotalUsers.Text = "Total Users: " + cmd.ExecuteScalar();

                // Total Tour Operators
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM TOUR_OPERATOR", con))
                    lblTotalOperators.Text = "Total Operators: " + cmd.ExecuteScalar();

                // Total Trips
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM TRIP", con))
                    lblTotalTrips.Text = "Total Trips: " + cmd.ExecuteScalar();

                // Total Bookings
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM BOOKING", con))
                    lblTotalBookings.Text = "Total Bookings: " + cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading statistics: " + ex.Message,
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

                // Fetch flagged reviews as alerts
                const string sql = @"
                    SELECT Comment AS Description, Date
                      FROM REVIEW
                     WHERE ReportedFlag = 1 OR ApprovalStatus = 'Pending'";
                using (var cmd = new SqlCommand(sql, con))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string desc = rd["Description"].ToString();
                        DateTime dt = Convert.ToDateTime(rd["Date"]);
                        dgvAlerts.Rows.Add("Flagged Review", desc, dt.ToShortDateString());
                    }
                }
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
    }
}
