using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class WishlistForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private string travelerId;
        private int selectedTripId = 0;
        private bool sortByPrice = false;
        private bool sortByDate = false;
        private bool showPriceAlerts = false;

        public WishlistForm(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            ConfigureUIElements();
            LoadWishlistItems();
        }

        private void ConfigureUIElements()
        {
            // Set colors
            panelHeader.BackColor = Color.FromArgb(41, 128, 185);
            panelFilter.BackColor = Color.FromArgb(245, 245, 245);
            btnRemove.BackColor = Color.FromArgb(231, 76, 60);
            btnViewDetails.BackColor = Color.FromArgb(52, 152, 219);
            btnBook.BackColor = Color.FromArgb(46, 204, 113);
            btnSetPriceAlert.BackColor = Color.FromArgb(255, 152, 0);

            // Apply rounded corners
            btnRemove.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRemove.Width, btnRemove.Height, 10, 10));
            btnViewDetails.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnViewDetails.Width, btnViewDetails.Height, 10, 10));
            btnBook.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnBook.Width, btnBook.Height, 10, 10));
            btnSetPriceAlert.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSetPriceAlert.Width, btnSetPriceAlert.Height, 10, 10));

            // Configure DataGridView
            dgvWishlist.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvWishlist.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvWishlist.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvWishlist.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvWishlist.EnableHeadersVisualStyles = false;
            dgvWishlist.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvWishlist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWishlist.ReadOnly = true;

            // Set up buttons
            btnRemove.Enabled = false;
            btnViewDetails.Enabled = false;
            btnBook.Enabled = false;
            btnSetPriceAlert.Enabled = false;

            // Set up sorting buttons
            btnSortByPrice.Click += BtnSortByPrice_Click;
            btnSortByDate.Click += BtnSortByDate_Click;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void LoadWishlistItems()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            W.TripID,
                            T.Title,
                            D.Name AS Destination,
                            T.StartDate,
                            T.EndDate,
                            T.Price AS CurrentPrice,
                            W.PriceAtAdding,
                            W.DateAdded,
                            W.Notes,
                            W.PriceAlert,
                            W.TargetPrice,
                            CASE 
                                WHEN T.Price < W.PriceAtAdding THEN 'Price Dropped'
                                WHEN T.Price > W.PriceAtAdding THEN 'Price Increased'
                                ELSE 'Same Price'
                            END AS PriceStatus,
                            T.Price - W.PriceAtAdding AS PriceChange
                        FROM WISHLIST W
                        INNER JOIN TRIP T ON W.TripID = T.TripID
                        INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                        WHERE W.TravelerID = @TravelerID";

                    if (showPriceAlerts)
                    {
                        query += " AND W.PriceAlert = 1";
                    }

                    if (sortByPrice)
                    {
                        query += " ORDER BY T.Price ASC";
                    }
                    else if (sortByDate)
                    {
                        query += " ORDER BY T.StartDate ASC";
                    }
                    else
                    {
                        query += " ORDER BY W.DateAdded DESC";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable wishlistTable = new DataTable();
                    adapter.Fill(wishlistTable);

                    dgvWishlist.DataSource = wishlistTable;
                    FormatWishlistGrid();
                    UpdateStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading wishlist: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatWishlistGrid()
        {
            if (dgvWishlist.Columns.Count > 0)
            {
                dgvWishlist.Columns["TripID"].Visible = false;

                dgvWishlist.Columns["Title"].HeaderText = "Trip Name";
                dgvWishlist.Columns["Title"].Width = 200;

                dgvWishlist.Columns["Destination"].HeaderText = "Destination";
                dgvWishlist.Columns["Destination"].Width = 120;

                dgvWishlist.Columns["StartDate"].HeaderText = "Start Date";
                dgvWishlist.Columns["StartDate"].Width = 100;
                dgvWishlist.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                dgvWishlist.Columns["EndDate"].HeaderText = "End Date";
                dgvWishlist.Columns["EndDate"].Width = 100;
                dgvWishlist.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                dgvWishlist.Columns["CurrentPrice"].HeaderText = "Current Price";
                dgvWishlist.Columns["CurrentPrice"].Width = 100;
                dgvWishlist.Columns["CurrentPrice"].DefaultCellStyle.Format = "c2";

                dgvWishlist.Columns["PriceAtAdding"].HeaderText = "Original Price";
                dgvWishlist.Columns["PriceAtAdding"].Width = 100;
                dgvWishlist.Columns["PriceAtAdding"].DefaultCellStyle.Format = "c2";

                dgvWishlist.Columns["PriceChange"].HeaderText = "Price Change";
                dgvWishlist.Columns["PriceChange"].Width = 100;
                dgvWishlist.Columns["PriceChange"].DefaultCellStyle.Format = "c2";

                dgvWishlist.Columns["PriceStatus"].HeaderText = "Status";
                dgvWishlist.Columns["PriceStatus"].Width = 100;

                dgvWishlist.Columns["DateAdded"].HeaderText = "Added On";
                dgvWishlist.Columns["DateAdded"].Width = 100;
                dgvWishlist.Columns["DateAdded"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                dgvWishlist.Columns["Notes"].HeaderText = "Notes";
                dgvWishlist.Columns["Notes"].Width = 150;

                dgvWishlist.Columns["PriceAlert"].HeaderText = "Price Alert";
                dgvWishlist.Columns["PriceAlert"].Width = 80;

                dgvWishlist.Columns["TargetPrice"].HeaderText = "Target Price";
                dgvWishlist.Columns["TargetPrice"].Width = 100;
                dgvWishlist.Columns["TargetPrice"].DefaultCellStyle.Format = "c2";

                // Color code price changes
                foreach (DataGridViewRow row in dgvWishlist.Rows)
                {
                    string status = row.Cells["PriceStatus"].Value.ToString();
                    if (status == "Price Dropped")
                    {
                        row.Cells["PriceStatus"].Style.ForeColor = Color.Green;
                        row.Cells["PriceChange"].Style.ForeColor = Color.Green;
                    }
                    else if (status == "Price Increased")
                    {
                        row.Cells["PriceStatus"].Style.ForeColor = Color.Red;
                        row.Cells["PriceChange"].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get total wishlist items
                    string countQuery = "SELECT COUNT(*) FROM WISHLIST WHERE TravelerID = @TravelerID";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@TravelerID", travelerId);
                    int totalItems = (int)countCommand.ExecuteScalar();
                    lblTotalItems.Text = totalItems.ToString();

                    // Get price dropped items count
                    string droppedQuery = @"
                        SELECT COUNT(*) FROM WISHLIST W
                        INNER JOIN TRIP T ON W.TripID = T.TripID
                        WHERE W.TravelerID = @TravelerID 
                        AND T.Price < W.PriceAtAdding";
                    SqlCommand droppedCommand = new SqlCommand(droppedQuery, connection);
                    droppedCommand.Parameters.AddWithValue("@TravelerID", travelerId);
                    int priceDropped = (int)droppedCommand.ExecuteScalar();
                    lblPriceDropped.Text = priceDropped.ToString();

                    // Get price alerts count
                    string alertsQuery = @"
                        SELECT COUNT(*) FROM WISHLIST 
                        WHERE TravelerID = @TravelerID 
                        AND PriceAlert = 1";
                    SqlCommand alertsCommand = new SqlCommand(alertsQuery, connection);
                    alertsCommand.Parameters.AddWithValue("@TravelerID", travelerId);
                    int priceAlerts = (int)alertsCommand.ExecuteScalar();
                    lblPriceAlerts.Text = priceAlerts.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating statistics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvWishlist_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvWishlist.SelectedRows.Count > 0)
            {
                selectedTripId = Convert.ToInt32(dgvWishlist.SelectedRows[0].Cells["TripID"].Value);
                btnRemove.Enabled = true;
                btnViewDetails.Enabled = true;
                btnBook.Enabled = true;
                btnSetPriceAlert.Enabled = true;
            }
            else
            {
                selectedTripId = 0;
                btnRemove.Enabled = false;
                btnViewDetails.Enabled = false;
                btnBook.Enabled = false;
                btnSetPriceAlert.Enabled = false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (selectedTripId > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to remove this trip from your wishlist?",
                                                    "Confirm Remove",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = @"DELETE FROM WISHLIST 
                                           WHERE TravelerID = @TravelerID 
                                           AND TripID = @TripID";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@TravelerID", travelerId);
                            command.Parameters.AddWithValue("@TripID", selectedTripId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Trip removed from wishlist successfully!", "Success",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadWishlistItems();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error removing trip from wishlist: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (selectedTripId > 0)
            {
                TripDetailsForm detailsForm = new TripDetailsForm(selectedTripId, 0, travelerId);
                detailsForm.ShowDialog();

                // Refresh wishlist in case user booked from details
                LoadWishlistItems();
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (selectedTripId > 0)
            {
                BookingsForm bookingForm = new BookingsForm(travelerId, selectedTripId);
                if (bookingForm.ShowDialog() == DialogResult.OK)
                {
                    // Remove from wishlist after successful booking
                    RemoveFromWishlistAfterBooking();
                }
            }
        }

        private void RemoveFromWishlistAfterBooking()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"DELETE FROM WISHLIST 
                                   WHERE TravelerID = @TravelerID 
                                   AND TripID = @TripID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);
                    command.Parameters.AddWithValue("@TripID", selectedTripId);

                    command.ExecuteNonQuery();
                    LoadWishlistItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing from wishlist: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetPriceAlert_Click(object sender, EventArgs e)
        {
            if (selectedTripId > 0)
            {
                //PriceAlertForm alertForm = new PriceAlertForm(travelerId, selectedTripId);
                //if (alertForm.ShowDialog() == DialogResult.OK)
                {
                    LoadWishlistItems();
                }
            }
        }

        private void BtnSortByPrice_Click(object sender, EventArgs e)
        {
            sortByPrice = true;
            sortByDate = false;
            LoadWishlistItems();
        }

        private void BtnSortByDate_Click(object sender, EventArgs e)
        {
            sortByPrice = false;
            sortByDate = true;
            LoadWishlistItems();
        }

        private void chkShowPriceAlerts_CheckedChanged(object sender, EventArgs e)
        {
            showPriceAlerts = chkShowPriceAlerts.Checked;
            LoadWishlistItems();
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (selectedTripId > 0)
            {
                //WishlistNoteForm noteForm = new WishlistNoteForm(travelerId, selectedTripId);
                //if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    LoadWishlistItems();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}