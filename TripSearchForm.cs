using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class TripSearchForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private string travelerId;
        private DataTable searchResults;
        private bool isCardView = false;

        public TripSearchForm(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            SetupForm();
        }

        private void SetupForm()
        {
            // Load destinations for dropdown
            LoadDestinations();

            // Load trip categories
            LoadTripCategories();

            // Set datepicker default values
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(30);

            // Set price range default values
            numMaxPrice.Minimum = 0;
            numMaxPrice.Maximum = 9999999;      // Set limit first
            numMaxPrice.Value = 9999999;        // Then set value


            // Set default difficulty
            cmbDifficulty.SelectedIndex = 0;

            // Setup view toggle button
            SetupViewToggle();

            // Configure results grid with initial empty datatable
            searchResults = new DataTable();
            searchResults.Columns.Add("TripID", typeof(int));
            searchResults.Columns.Add("Title", typeof(string));
            searchResults.Columns.Add("Destination", typeof(string));
            searchResults.Columns.Add("StartDate", typeof(DateTime));
            searchResults.Columns.Add("EndDate", typeof(DateTime));
            searchResults.Columns.Add("Duration", typeof(int));
            searchResults.Columns.Add("Price", typeof(decimal));
            searchResults.Columns.Add("Difficulty", typeof(string));
            searchResults.Columns.Add("CategoryName", typeof(string));
            searchResults.Columns.Add("Capacity", typeof(int));
            searchResults.Columns.Add("AvailableSpots", typeof(int));

            dgvSearchResults.DataSource = searchResults;
            FormatDataGridView();

            // Hide card view panel initially
            flpCardView.Visible = false;
        }

        private void LoadDestinations()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DestinationID, Name FROM DESTINATION ORDER BY Name";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Add blank item
                    cmbDestination.Items.Add("-- All Destinations --");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbDestination.Items.Add(reader["Name"].ToString());
                        }
                    }

                    cmbDestination.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading destinations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTripCategories()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CategoryID, Name FROM TRIP_CATEGORY ORDER BY Name";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Add blank item
                    cmbCategory.Items.Add("-- All Categories --");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(reader["Name"].ToString());
                        }
                    }

                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupViewToggle()
        {
            // Initially set to grid view
            btnToggleView.Text = "Switch to Card View";
            isCardView = false;
        }

        private void FormatDataGridView()
        {
            // Set column headers and formatting
            if (dgvSearchResults.Columns.Count > 0)
            {
                dgvSearchResults.Columns["TripID"].Visible = false;

                dgvSearchResults.Columns["Title"].HeaderText = "Trip Name";
                dgvSearchResults.Columns["Title"].Width = 200;

                dgvSearchResults.Columns["Destination"].HeaderText = "Destination";
                dgvSearchResults.Columns["Destination"].Width = 150;

                dgvSearchResults.Columns["StartDate"].HeaderText = "Start Date";
                dgvSearchResults.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dgvSearchResults.Columns["StartDate"].Width = 120;

                dgvSearchResults.Columns["EndDate"].HeaderText = "End Date";
                dgvSearchResults.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dgvSearchResults.Columns["EndDate"].Width = 120;

                dgvSearchResults.Columns["Duration"].HeaderText = "Days";
                dgvSearchResults.Columns["Duration"].Width = 60;

                dgvSearchResults.Columns["Price"].HeaderText = "Price";
                dgvSearchResults.Columns["Price"].DefaultCellStyle.Format = "c2";
                dgvSearchResults.Columns["Price"].Width = 100;

                dgvSearchResults.Columns["Difficulty"].HeaderText = "Difficulty";
                dgvSearchResults.Columns["Difficulty"].Width = 100;

                dgvSearchResults.Columns["CategoryName"].HeaderText = "Category";
                dgvSearchResults.Columns["CategoryName"].Width = 120;

                dgvSearchResults.Columns["Capacity"].HeaderText = "Capacity";
                dgvSearchResults.Columns["Capacity"].Width = 80;

                dgvSearchResults.Columns["AvailableSpots"].HeaderText = "Available";
                dgvSearchResults.Columns["AvailableSpots"].Width = 80;

                // Add buttons
                if (!dgvSearchResults.Columns.Contains("Details"))
                {
                    DataGridViewButtonColumn detailsButton = new DataGridViewButtonColumn();
                    detailsButton.HeaderText = "Details";
                    detailsButton.Name = "Details";
                    detailsButton.Text = "View";
                    detailsButton.UseColumnTextForButtonValue = true;
                    detailsButton.FlatStyle = FlatStyle.Flat;
                    dgvSearchResults.Columns.Add(detailsButton);
                }

                if (!dgvSearchResults.Columns.Contains("Wishlist"))
                {
                    DataGridViewButtonColumn wishlistButton = new DataGridViewButtonColumn();
                    wishlistButton.HeaderText = "Wishlist";
                    wishlistButton.Name = "Wishlist";
                    wishlistButton.Text = "Add";
                    wishlistButton.UseColumnTextForButtonValue = true;
                    wishlistButton.FlatStyle = FlatStyle.Flat;
                    dgvSearchResults.Columns.Add(wishlistButton);
                }

                if (!dgvSearchResults.Columns.Contains("Book"))
                {
                    DataGridViewButtonColumn bookButton = new DataGridViewButtonColumn();
                    bookButton.HeaderText = "Book";
                    bookButton.Name = "Book";
                    bookButton.Text = "Book";
                    bookButton.UseColumnTextForButtonValue = true;
                    bookButton.FlatStyle = FlatStyle.Flat;
                    dgvSearchResults.Columns.Add(bookButton);
                }
            }

            // Set row formatting
            dgvSearchResults.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvSearchResults.EnableHeadersVisualStyles = false;
            dgvSearchResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvSearchResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSearchResults.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvSearchResults.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvSearchResults.ReadOnly = true;
            dgvSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSearchResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvSearchResults.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Set cell click event handler
            dgvSearchResults.CellContentClick += DgvSearchResults_CellContentClick;
        }

        private void SearchTrips()
        {
            try
            {
                string destination = cmbDestination.SelectedIndex == 0 ? string.Empty : cmbDestination.SelectedItem.ToString();
                string category = cmbCategory.SelectedIndex == 0 ? string.Empty : cmbCategory.SelectedItem.ToString();
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;
                decimal minPrice = numMinPrice.Value;
                decimal maxPrice = numMaxPrice.Value;
                string difficulty = cmbDifficulty.SelectedIndex == 0 ? string.Empty : cmbDifficulty.SelectedItem.ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    T.TripID, T.Title, D.Name AS Destination, 
                    T.StartDate, T.EndDate, T.Duration_Days AS Duration, 
                    T.Price, T.Difficulty, TC.Name AS CategoryName,
                    T.Capacity, 
                    (T.Capacity - ISNULL((
                        SELECT SUM(B.NoOfTravelers) 
                        FROM BOOKING B 
                        WHERE B.TripID = T.TripID AND B.Status IN ('Confirmed', 'Pending')
                    ), 0)) AS AvailableSpots
                FROM TRIP T
                INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                INNER JOIN TRIP_CATEGORY TC ON T.CategoryID = TC.CategoryID
                WHERE 
                    T.Status = 'Active'
                    AND (@StartDate IS NULL OR T.StartDate >= @StartDate)
                    AND (@EndDate IS NULL OR T.EndDate <= @EndDate)
                    AND (@MinPrice IS NULL OR T.Price >= @MinPrice)
                    AND (@MaxPrice IS NULL OR T.Price <= @MaxPrice)
                    AND (@Destination = '' OR D.Name = @Destination)
                    AND (@Category = '' OR TC.Name = @Category)
                    AND (@Difficulty = '' OR T.Difficulty = @Difficulty)
                ORDER BY T.StartDate";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StartDate", startDate == DateTime.MinValue ? DBNull.Value : (object)startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate == DateTime.MaxValue ? DBNull.Value : (object)endDate);
                    command.Parameters.AddWithValue("@MinPrice", minPrice == 0 ? DBNull.Value : (object)minPrice);
                    command.Parameters.AddWithValue("@MaxPrice", maxPrice == 0 ? DBNull.Value : (object)maxPrice);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Difficulty", difficulty);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    searchResults = new DataTable();
                    adapter.Fill(searchResults);

                    if (searchResults.Rows.Count == 0)
                    {
                        MessageBox.Show("No trips found matching your search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Update UI based on view mode
                    if (isCardView)
                    {
                        dgvSearchResults.Visible = false;
                        flpCardView.Visible = true;
                        GenerateCardView();
                    }
                    else
                    {
                        dgvSearchResults.DataSource = searchResults;
                        dgvSearchResults.Visible = true;
                        flpCardView.Visible = false;
                    }

                    lblResultsCount.Text = $"Found {searchResults.Rows.Count} trips matching your criteria";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching trips: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateCardView()
        {
            // Clear existing cards
            flpCardView.Controls.Clear();

            // Generate a card for each trip
            foreach (DataRow row in searchResults.Rows)
            {
                // Create card panel
                Panel card = new Panel();
                card.Width = 300;
                card.Height = 250;
                card.Margin = new Padding(10);
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;

                // Create trip title
                Label lblTitle = new Label();
                lblTitle.Text = row["Title"].ToString();
                lblTitle.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                lblTitle.ForeColor = Color.FromArgb(41, 128, 185);
                lblTitle.Location = new Point(10, 10);
                lblTitle.Size = new Size(280, 25);
                card.Controls.Add(lblTitle);

                // Create destination
                Label lblDestination = new Label();
                lblDestination.Text = "Destination: " + row["Destination"].ToString();
                lblDestination.Font = new Font("Century Gothic", 10);
                lblDestination.Location = new Point(10, 40);
                lblDestination.Size = new Size(280, 20);
                card.Controls.Add(lblDestination);

                // Create dates
                Label lblDates = new Label();
                lblDates.Text = "Dates: " + Convert.ToDateTime(row["StartDate"]).ToString("dd MMM yyyy") +
                                " - " + Convert.ToDateTime(row["EndDate"]).ToString("dd MMM yyyy");
                lblDates.Font = new Font("Century Gothic", 9);
                lblDates.Location = new Point(10, 65);
                lblDates.Size = new Size(280, 20);
                card.Controls.Add(lblDates);

                // Create price
                Label lblPrice = new Label();
                lblPrice.Text = "Price: $" + Convert.ToDecimal(row["Price"]).ToString("N2");
                lblPrice.Font = new Font("Century Gothic", 11, FontStyle.Bold);
                lblPrice.ForeColor = Color.ForestGreen;
                lblPrice.Location = new Point(10, 90);
                lblPrice.Size = new Size(280, 20);
                card.Controls.Add(lblPrice);

                // Create category and difficulty
                Label lblCategoryDifficulty = new Label();
                lblCategoryDifficulty.Text = "Category: " + row["CategoryName"].ToString() +
                                            " | Difficulty: " + row["Difficulty"].ToString();
                lblCategoryDifficulty.Font = new Font("Century Gothic", 9);
                lblCategoryDifficulty.Location = new Point(10, 115);
                lblCategoryDifficulty.Size = new Size(280, 20);
                card.Controls.Add(lblCategoryDifficulty);

                // Create availability
                Label lblAvailability = new Label();
                lblAvailability.Text = "Available Spots: " + row["AvailableSpots"].ToString() +
                                      " of " + row["Capacity"].ToString();
                lblAvailability.Font = new Font("Century Gothic", 9);
                lblAvailability.Location = new Point(10, 140);
                lblAvailability.Size = new Size(280, 20);
                card.Controls.Add(lblAvailability);

                // Create buttons
                Button btnDetails = new Button();
                btnDetails.Text = "View Details";
                btnDetails.FlatStyle = FlatStyle.Flat;
                btnDetails.BackColor = Color.FromArgb(41, 128, 185);
                btnDetails.ForeColor = Color.White;
                btnDetails.Location = new Point(10, 170);
                btnDetails.Size = new Size(90, 30);
                btnDetails.Tag = row["TripID"];
                btnDetails.Click += (s, e) => ViewTripDetails(Convert.ToInt32((s as Button).Tag));
                card.Controls.Add(btnDetails);

                Button btnAddWishlist = new Button();
                btnAddWishlist.Text = "Add to Wishlist";
                btnAddWishlist.FlatStyle = FlatStyle.Flat;
                btnAddWishlist.BackColor = Color.FromArgb(155, 89, 182);
                btnAddWishlist.ForeColor = Color.White;
                btnAddWishlist.Location = new Point(105, 170);
                btnAddWishlist.Size = new Size(90, 30);
                btnAddWishlist.Tag = row["TripID"];
                btnAddWishlist.Click += (s, e) => AddToWishlist(Convert.ToInt32((s as Button).Tag));
                card.Controls.Add(btnAddWishlist);

                Button btnBook = new Button();
                btnBook.Text = "Book Now";
                btnBook.FlatStyle = FlatStyle.Flat;
                btnBook.BackColor = Color.FromArgb(46, 204, 113);
                btnBook.ForeColor = Color.White;
                btnBook.Location = new Point(200, 170);
                btnBook.Size = new Size(90, 30);
                btnBook.Tag = row["TripID"];
                btnBook.Click += (s, e) => BookTrip(Convert.ToInt32((s as Button).Tag));
                card.Controls.Add(btnBook);

                // Add the card to the FlowLayoutPanel
                flpCardView.Controls.Add(card);
            }
        }

        private void DgvSearchResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int tripId = Convert.ToInt32(dgvSearchResults.Rows[e.RowIndex].Cells["TripID"].Value);

                // Check which button was clicked
                if (e.ColumnIndex == dgvSearchResults.Columns["Details"].Index)
                {
                    ViewTripDetails(tripId);
                }
                else if (e.ColumnIndex == dgvSearchResults.Columns["Wishlist"].Index)
                {
                    AddToWishlist(tripId);
                }
                else if (e.ColumnIndex == dgvSearchResults.Columns["Book"].Index)
                {
                    BookTrip(tripId);
                }
            }
        }

        private void ViewTripDetails(int tripId)
        {
            // Open trip details form
            TripDetailsForm detailsForm = new TripDetailsForm(tripId, 0, travelerId);
            detailsForm.ShowDialog();
        }

        private void AddToWishlist(int tripId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if trip is already in wishlist
                    string checkQuery = "SELECT COUNT(*) FROM WISHLIST WHERE TravelerID = @TravelerID AND TripID = @TripID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@TravelerID", travelerId);
                    checkCmd.Parameters.AddWithValue("@TripID", tripId);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("This trip is already in your wishlist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Get trip price for price at adding field
                    string priceQuery = "SELECT Price FROM TRIP WHERE TripID = @TripID";
                    SqlCommand priceCmd = new SqlCommand(priceQuery, connection);
                    priceCmd.Parameters.AddWithValue("@TripID", tripId);

                    decimal price = (decimal)priceCmd.ExecuteScalar();

                    // Add to wishlist
                    string insertQuery = @"INSERT INTO WISHLIST (TravelerID, TripID, DateAdded, PriceAtAdding, PriceAlert, TargetPrice)
                                          VALUES (@TravelerID, @TripID, GETDATE(), @Price, 0, 0)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@TravelerID", travelerId);
                    insertCmd.Parameters.AddWithValue("@TripID", tripId);
                    insertCmd.Parameters.AddWithValue("@Price", price);

                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Trip added to your wishlist successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding to wishlist: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookTrip(int tripId)
        {
            //Open booking form
            BookingsForm bookingForm = new BookingsForm(travelerId, tripId);
            bookingForm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTrips();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset all search fields
            cmbDestination.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(30);
            numMinPrice.Value = 0;
            numMaxPrice.Value = 9999999;
            cmbDifficulty.SelectedIndex = 0;

            // Clear results
            searchResults.Clear();
            dgvSearchResults.DataSource = searchResults;
            flpCardView.Controls.Clear();
            lblResultsCount.Text = "Use the search filters to find your perfect trip";
        }

        private void btnToggleView_Click(object sender, EventArgs e)
        {
            isCardView = !isCardView;

            if (isCardView)
            {
                btnToggleView.Text = "Switch to Grid View";
                if (searchResults.Rows.Count > 0)
                {
                    dgvSearchResults.Visible = false;
                    flpCardView.Visible = true;
                    GenerateCardView();
                }
            }
            else
            {
                btnToggleView.Text = "Switch to Card View";
                dgvSearchResults.Visible = true;
                flpCardView.Visible = false;
            }
        }

    }
}