using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class TravelerDashboard : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private string travelerId;
        private string fullName;

        public TravelerDashboard(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            this.WindowState = FormWindowState.Maximized;
            LoadTravelerInfo();
            LoadUpcomingTrips();
            LoadWishlist();
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            // Set welcome message
            lblWelcome.Text = $"Welcome, {fullName}!";

            // Set form control events
            btnLogout.Click += BtnLogout_Click;
            btnMyAccount.Click += BtnMyAccount_Click;
            btnSearchTrips.Click += BtnSearchTrips_Click;
            btnBookings.Click += BtnBookings_Click;
            btnReviews.Click += BtnReviews_Click;
            btnWishlist.Click += BtnWishlist_Click;

            // Set dashboard time
            timer1.Start();
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void LoadTravelerInfo()
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
                            fullName = $"{reader["FirstName"]} {reader["LastName"]}";

                            // Store other user details as needed
                            lblName.Text = fullName;
                            lblEmail.Text = reader["Email"].ToString();
                            lblLoyaltyPoints.Text = reader["LoyaltyPoints"].ToString();
                            lblPhoneNumber.Text = reader["PhoneNumber"].ToString();
                            lblNationality.Text = reader["Nationality"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading traveler information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUpcomingTrips()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT T.TripID, T.Title, T.StartDate, T.EndDate, T.Price, 
                                           B.BookingID, B.Status, D.Name AS Destination
                                    FROM TRIP T
                                    INNER JOIN BOOKING B ON T.TripID = B.TripID
                                    INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                                    WHERE B.TravelerID = @TravelerID 
                                    AND B.Status IN ('Confirmed', 'Pending')
                                    AND T.StartDate >= GETDATE()
                                    ORDER BY T.StartDate ASC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable tripsTable = new DataTable();
                    adapter.Fill(tripsTable);

                    // Bind to upcoming trips grid
                    dgvUpcomingTrips.DataSource = tripsTable;

                    // Format grid
                    FormatTripGrid(dgvUpcomingTrips);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading upcoming trips: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWishlist()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT T.TripID, T.Title, T.Price, T.StartDate, T.EndDate, 
                                           D.Name AS Destination, W.DateAdded
                                    FROM TRIP T
                                    INNER JOIN WISHLIST W ON T.TripID = W.TripID
                                    INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                                    WHERE W.TravelerID = @TravelerID
                                    ORDER BY W.DateAdded DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable wishlistTable = new DataTable();
                    adapter.Fill(wishlistTable);

                    // Bind to wishlist grid
                    dgvWishlist.DataSource = wishlistTable;

                    // Format grid
                    FormatTripGrid(dgvWishlist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading wishlist: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatTripGrid(DataGridView grid)
        {
            // Set column headers and formatting
            if (grid.Columns.Count > 0)
            {
                grid.Columns["TripID"].Visible = false;

                if (grid.Columns.Contains("BookingID"))
                    grid.Columns["BookingID"].Visible = false;

                if (grid.Columns.Contains("Title"))
                {
                    grid.Columns["Title"].HeaderText = "Trip Name";
                    grid.Columns["Title"].Width = 200;
                }

                if (grid.Columns.Contains("Destination"))
                {
                    grid.Columns["Destination"].HeaderText = "Destination";
                    grid.Columns["Destination"].Width = 150;
                }

                if (grid.Columns.Contains("StartDate"))
                {
                    grid.Columns["StartDate"].HeaderText = "Start Date";
                    grid.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                    grid.Columns["StartDate"].Width = 100;
                }

                if (grid.Columns.Contains("EndDate"))
                {
                    grid.Columns["EndDate"].HeaderText = "End Date";
                    grid.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                    grid.Columns["EndDate"].Width = 100;
                }

                if (grid.Columns.Contains("Price"))
                {
                    grid.Columns["Price"].HeaderText = "Price";
                    grid.Columns["Price"].DefaultCellStyle.Format = "c2";
                    grid.Columns["Price"].Width = 100;
                }

                if (grid.Columns.Contains("Status"))
                {
                    grid.Columns["Status"].HeaderText = "Status";
                    grid.Columns["Status"].Width = 100;
                }

                if (grid.Columns.Contains("DateAdded"))
                {
                    grid.Columns["DateAdded"].HeaderText = "Added On";
                    grid.Columns["DateAdded"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                    grid.Columns["DateAdded"].Width = 100;
                }
            }

            // Set row formatting
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            grid.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        #region Button Click Handlers
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        private void BtnMyAccount_Click(object sender, EventArgs e)
        {
            // Open account details form
            TravelerAccountForm accountForm = new TravelerAccountForm(travelerId);
            accountForm.ShowDialog();

            // Refresh data after account details may have been updated
            LoadTravelerInfo();
        }

        private void BtnSearchTrips_Click(object sender, EventArgs e)
        {
            TripSearchForm searchForm = new TripSearchForm(travelerId);
            searchForm.ShowDialog();

            // Refresh data after possible new bookings or wishlist items
            LoadUpcomingTrips();
            LoadWishlist();
        }

        private void BtnBookings_Click(object sender, EventArgs e)
        {
            TravelerBookingsForm bookingsForm = new TravelerBookingsForm(travelerId);
            bookingsForm.ShowDialog();

            // Refresh data after bookings may have been updated
            LoadUpcomingTrips();
        }

        private void BtnReviews_Click(object sender, EventArgs e)
        {
            ReviewsForm reviewsForm = new ReviewsForm(travelerId);
            reviewsForm.ShowDialog();
        }

        private void BtnWishlist_Click(object sender, EventArgs e)
        {
            WishlistForm wishlistForm = new WishlistForm(travelerId);
            wishlistForm.ShowDialog();

            // Refresh wishlist data
            LoadWishlist();
        }
        #endregion

        private void dgvUpcomingTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get selected trip info
                int tripId = Convert.ToInt32(dgvUpcomingTrips.Rows[e.RowIndex].Cells["TripID"].Value);
                int bookingId = Convert.ToInt32(dgvUpcomingTrips.Rows[e.RowIndex].Cells["BookingID"].Value);

                // Open trip details form
                TripDetailsForm detailsForm = new TripDetailsForm(tripId, bookingId, travelerId);
                detailsForm.ShowDialog();

                // Refresh data after possible changes
                LoadUpcomingTrips();
            }
        }

        private void dgvWishlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get selected trip info
                int tripId = Convert.ToInt32(dgvWishlist.Rows[e.RowIndex].Cells["TripID"].Value);

                // Open trip details form (with no booking)
                TripDetailsForm detailsForm = new TripDetailsForm(tripId, 0, travelerId);
                detailsForm.ShowDialog();

                // Refresh data after possible booking or wishlist removal
                LoadUpcomingTrips();
                LoadWishlist();
            }
        }

        private void TravelerDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        
    }
}