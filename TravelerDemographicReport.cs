using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelAnalyticsApp
{
    public partial class TravelerDemographicsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        public TravelerDemographicsForm()
        {
            InitializeComponent();

            // Set default dates - use a wider range to ensure we catch data
            dtpStartDate.Value = DateTime.Now.AddYears(-5);  // 5 years ago
            dtpEndDate.Value = DateTime.Now.AddYears(5);     // 5 years in the future
        }

        private void TravelerDemographicsForm_Load(object sender, EventArgs e)
        {
            // Form load event - show ready status
            lblStatus.Text = "Ready";

            // Test database connection
            TestDatabaseConnection();

            // Load trip categories for filter dropdown
            LoadTripCategories();
        }

        private void TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    lblStatus.Text = "Database connection successful";

                    // Check table counts - let's see how many records exist in key tables
                    DisplayTableCount(connection, "[USER]");
                    DisplayTableCount(connection, "TRAVELER");
                    DisplayTableCount(connection, "BOOKING");
                    DisplayTableCount(connection, "TRIP");
                    DisplayTableCount(connection, "TRIP_CATEGORY");
                    DisplayTableCount(connection, "DESTINATION");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayTableCount(SqlConnection connection, string tableName)
        {
            try
            {
                // Get count of records in the table
                string query = $"SELECT COUNT(*) FROM {tableName}";
                SqlCommand cmd = new SqlCommand(query, connection);
                int count = (int)cmd.ExecuteScalar();

                // Display result (use console or add a label to the form)
                Console.WriteLine($"Table {tableName} has {count} records");

                // Add a label to show counts on the form
                Label countLabel = new Label
                {
                    Text = $"Table {tableName}: {count} records",
                    AutoSize = true,
                    Location = new System.Drawing.Point(10, 500 + (Controls.Count * 20)),
                    ForeColor = System.Drawing.Color.Blue
                };

                this.Controls.Add(countLabel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking {tableName}: {ex.Message}");

                // Add error label
                Label errorLabel = new Label
                {
                    Text = $"Error checking {tableName}: {ex.Message}",
                    AutoSize = true,
                    Location = new System.Drawing.Point(10, 500 + (Controls.Count * 20)),
                    ForeColor = System.Drawing.Color.Red
                };

                this.Controls.Add(errorLabel);
            }
        }

        private void LoadTripCategories()
        {
            try
            {
                string query = "SELECT CategoryID, Name FROM TRIP_CATEGORY ORDER BY Name";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable categories = new DataTable();
                    adapter.Fill(categories);

                    if (categories.Rows.Count > 0)
                    {
                        // Add "All Categories" option
                        DataRow allRow = categories.NewRow();
                        allRow["CategoryID"] = 0;
                        allRow["Name"] = "All Categories";
                        categories.Rows.InsertAt(allRow, 0);

                        cboCategoryFilter.DataSource = categories;
                        cboCategoryFilter.DisplayMember = "Name";
                        cboCategoryFilter.ValueMember = "CategoryID";
                    }
                    else
                    {
                        // If no categories exist, create a simple default option
                        cboCategoryFilter.Items.Add("All Categories");
                        cboCategoryFilter.SelectedIndex = 0;

                        // Show warning message
                        MessageBox.Show("No trip categories found in the database.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip categories: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Update status
                lblStatus.Text = "Generating report...";
                Application.DoEvents();

                // Set report period label
                lblReportPeriod.Text = $"Report Period: {dtpStartDate.Value.ToShortDateString()} to {dtpEndDate.Value.ToShortDateString()}";

                // Try a very simple query first to see if we can get ANY data
                TestDataQuery();

                // Now try the full reports
                LoadSummaryStatistics();
                LoadTravelerDetails(); // Start with a simple data table

                // Only proceed with charts if data exists
                if (lblTotalTravelers.Text != "0")
                {
                    LoadDemographics();
                    LoadPreferences();
                }

                // Update status
                lblStatus.Text = "Report generation complete. If no data appears, your database may be empty.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error generating report";
                MessageBox.Show("Error generating report: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestDataQuery()
        {
            // Simple test query to check if ANY data exists that matches our criteria
            string query = @"
                SELECT COUNT(*) AS BookingCount
                FROM BOOKING B
                WHERE B.Date BETWEEN @StartDate AND @EndDate";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show($"No booking data found for the selected date range: {dtpStartDate.Value.ToShortDateString()} to {dtpEndDate.Value.ToShortDateString()}.\n\nTry selecting a wider date range or check if your database has booking data.",
                        "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Success - there is at least some data
                    lblStatus.Text = $"Found {count} bookings in the selected date range.";
                }
            }
        }

        private void LoadSummaryStatistics()
        {
            try
            {
                string filter = GetCategoryFilterClause();

                string query = $@"
                    SELECT 
                        COUNT(DISTINCT T.TravelerID) AS TotalTravelers,
                        COUNT(B.BookingID) AS TotalBookings,
                        AVG(B.TotalAmount) AS AvgSpending,
                        MIN(B.TotalAmount) AS MinSpending,
                        MAX(B.TotalAmount) AS MaxSpending
                    FROM TRAVELER T
                    JOIN BOOKING B ON T.TravelerID = B.TravelerID
                    JOIN TRIP TR ON B.TripID = TR.TripID
                    WHERE B.Date BETWEEN @StartDate AND @EndDate
                    {filter}";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                    cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblTotalTravelers.Text = reader["TotalTravelers"].ToString();
                        lblTotalBookings.Text = reader["TotalBookings"].ToString();

                        if (reader["AvgSpending"] != DBNull.Value)
                        {
                            decimal avgSpending = Convert.ToDecimal(reader["AvgSpending"]);
                            decimal minSpending = Convert.ToDecimal(reader["MinSpending"]);
                            decimal maxSpending = Convert.ToDecimal(reader["MaxSpending"]);

                            lblAvgSpending.Text = avgSpending.ToString("C2");
                            lblSpendingRange.Text = $"{minSpending.ToString("C2")} - {maxSpending.ToString("C2")}";
                        }
                        else
                        {
                            lblAvgSpending.Text = "$0.00";
                            lblSpendingRange.Text = "$0.00 - $0.00";
                        }
                    }
                    else
                    {
                        // No data found
                        lblTotalTravelers.Text = "0";
                        lblTotalBookings.Text = "0";
                        lblAvgSpending.Text = "$0.00";
                        lblSpendingRange.Text = "$0.00 - $0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading summary statistics: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTravelerDetails()
        {
            try
            {
                string filter = GetCategoryFilterClause();

                // Using JOINs with the actual DB structure from the SQL script
                string query = $@"
                    SELECT TOP 100
                        T.TravelerID,
                        U.FirstName,
                        U.LastName,
                        T.Age,
                        T.Nationality,
                        U.Email,
                        COUNT(B.BookingID) AS TotalBookings,
                        SUM(B.TotalAmount) AS TotalSpending,
                        AVG(B.TotalAmount) AS AvgSpending
                    FROM TRAVELER T
                    JOIN [USER] U ON T.TravelerID = U.UserID
                    JOIN BOOKING B ON T.TravelerID = B.TravelerID
                    JOIN TRIP TR ON B.TripID = TR.TripID
                    WHERE B.Date BETWEEN @StartDate AND @EndDate
                    {filter}
                    GROUP BY T.TravelerID, U.FirstName, U.LastName, T.Age, T.Nationality, U.Email
                    ORDER BY SUM(B.TotalAmount) DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                    cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable travelerData = new DataTable();
                    adapter.Fill(travelerData);

                    dgvTravelerData.DataSource = travelerData;

                    // Format data grid
                    if (dgvTravelerData.Columns.Contains("TotalSpending"))
                        dgvTravelerData.Columns["TotalSpending"].DefaultCellStyle.Format = "C2";

                    if (dgvTravelerData.Columns.Contains("AvgSpending"))
                        dgvTravelerData.Columns["AvgSpending"].DefaultCellStyle.Format = "C2";

                    if (travelerData.Rows.Count == 0)
                    {
                        MessageBox.Show("No traveler booking data found for the selected date range and filters.",
                            "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading traveler details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Include the chart-loading methods from the previous code, but only call them if data exists

        private void LoadDemographics()
        {
            try
            {
                LoadAgeDistribution();
                LoadNationalityDistribution();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading demographics: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAgeDistribution()
        {
            string filter = GetCategoryFilterClause();

            string query = $@"
                SELECT 
                    CASE 
                        WHEN T.Age BETWEEN 18 AND 24 THEN '18-24'
                        WHEN T.Age BETWEEN 25 AND 34 THEN '25-34'
                        WHEN T.Age BETWEEN 35 AND 44 THEN '35-44'
                        WHEN T.Age BETWEEN 45 AND 54 THEN '45-54'
                        WHEN T.Age BETWEEN 55 AND 64 THEN '55-64'
                        WHEN T.Age >= 65 THEN '65+'
                        ELSE 'Unknown'
                    END AS AgeGroup,
                    COUNT(*) AS Count
                FROM TRAVELER T
                JOIN BOOKING B ON T.TravelerID = B.TravelerID
                JOIN TRIP TR ON B.TripID = TR.TripID
                WHERE B.Date BETWEEN @StartDate AND @EndDate
                {filter}
                GROUP BY 
                    CASE 
                        WHEN T.Age BETWEEN 18 AND 24 THEN '18-24'
                        WHEN T.Age BETWEEN 25 AND 34 THEN '25-34'
                        WHEN T.Age BETWEEN 35 AND 44 THEN '35-44'
                        WHEN T.Age BETWEEN 45 AND 54 THEN '45-54'
                        WHEN T.Age BETWEEN 55 AND 64 THEN '55-64'
                        WHEN T.Age >= 65 THEN '65+'
                        ELSE 'Unknown'
                    END
                ORDER BY AgeGroup";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable ageData = new DataTable();
                adapter.Fill(ageData);

                // Configure age distribution chart
                chartAgeDistribution.Series.Clear();
                Series ageSeries = new Series("Age Distribution");
                ageSeries.ChartType = SeriesChartType.Column;

                foreach (DataRow row in ageData.Rows)
                {
                    ageSeries.Points.AddXY(row["AgeGroup"].ToString(), Convert.ToInt32(row["Count"]));
                }

                chartAgeDistribution.Series.Add(ageSeries);

                // Format chart
                chartAgeDistribution.ChartAreas[0].AxisX.Title = "Age Group";
                chartAgeDistribution.ChartAreas[0].AxisY.Title = "Number of Travelers";
                chartAgeDistribution.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0";
            }
        }

        private void LoadNationalityDistribution()
        {
            string filter = GetCategoryFilterClause();

            string query = $@"
                SELECT TOP 10
                    T.Nationality,
                    COUNT(*) AS Count
                FROM TRAVELER T
                JOIN BOOKING B ON T.TravelerID = B.TravelerID
                JOIN TRIP TR ON B.TripID = TR.TripID
                WHERE B.Date BETWEEN @StartDate AND @EndDate
                {filter}
                GROUP BY T.Nationality
                ORDER BY COUNT(*) DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable nationalityData = new DataTable();
                adapter.Fill(nationalityData);

                // Configure nationality chart
                chartNationality.Series.Clear();
                Series nationalitySeries = new Series("Nationality");
                nationalitySeries.ChartType = SeriesChartType.Pie;

                foreach (DataRow row in nationalityData.Rows)
                {
                    int count = Convert.ToInt32(row["Count"]);
                    string nationality = row["Nationality"].ToString();

                    nationalitySeries.Points.AddXY(nationality, count);
                }

                chartNationality.Series.Add(nationalitySeries);

                // Format chart
                chartNationality.Series[0].Label = "#PERCENT{P0}";
                chartNationality.Series[0].LegendText = "#VALX";
            }
        }

        private void LoadPreferences()
        {
            try
            {
                LoadTripTypes();
                LoadTopDestinations();
                LoadSpendingByTripType();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading preferences: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTripTypes()
        {
            string query = $@"
                SELECT 
                    C.Name AS TripType,
                    COUNT(*) AS Count
                FROM BOOKING B
                JOIN TRIP T ON B.TripID = T.TripID
                JOIN TRIP_CATEGORY C ON T.CategoryID = C.CategoryID
                WHERE B.Date BETWEEN @StartDate AND @EndDate
                GROUP BY C.Name
                ORDER BY COUNT(*) DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tripTypeData = new DataTable();
                adapter.Fill(tripTypeData);

                // Configure trip types chart
                chartTripTypes.Series.Clear();
                Series tripTypeSeries = new Series("Trip Types");
                tripTypeSeries.ChartType = SeriesChartType.Doughnut;

                foreach (DataRow row in tripTypeData.Rows)
                {
                    tripTypeSeries.Points.AddXY(row["TripType"].ToString(), Convert.ToInt32(row["Count"]));
                }

                chartTripTypes.Series.Add(tripTypeSeries);

                // Format chart
                chartTripTypes.Series[0].Label = "#PERCENT{P0}";
                chartTripTypes.Series[0].LegendText = "#VALX";
            }
        }

        private void LoadTopDestinations()
        {
            string filter = GetCategoryFilterClause();

            string query = $@"
                SELECT TOP 10
                    D.Name AS Destination,
                    COUNT(*) AS Count
                FROM BOOKING B
                JOIN TRIP T ON B.TripID = T.TripID
                JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                WHERE B.Date BETWEEN @StartDate AND @EndDate
                {filter}
                GROUP BY D.Name
                ORDER BY COUNT(*) DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable destinationData = new DataTable();
                adapter.Fill(destinationData);

                // Configure destinations chart
                chartDestinations.Series.Clear();
                Series destinationSeries = new Series("Destinations");
                destinationSeries.ChartType = SeriesChartType.Bar;

                foreach (DataRow row in destinationData.Rows)
                {
                    destinationSeries.Points.AddXY(row["Destination"].ToString(), Convert.ToInt32(row["Count"]));
                }

                chartDestinations.Series.Add(destinationSeries);

                // Format chart
                chartDestinations.ChartAreas[0].AxisX.Title = "Destination";
                chartDestinations.ChartAreas[0].AxisY.Title = "Number of Bookings";
                chartDestinations.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0";
                chartDestinations.ChartAreas[0].AxisX.Interval = 1;
            }
        }

        private void LoadSpendingByTripType()
        {
            string query = $@"
                SELECT 
                    C.Name AS TripType,
                    AVG(B.TotalAmount) AS AvgSpending
                FROM BOOKING B
                JOIN TRIP T ON B.TripID = T.TripID
                JOIN TRIP_CATEGORY C ON T.CategoryID = C.CategoryID
                WHERE B.Date BETWEEN @StartDate AND @EndDate
                GROUP BY C.Name
                ORDER BY AVG(B.TotalAmount) DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable spendingData = new DataTable();
                adapter.Fill(spendingData);

                // Configure spending chart
                chartSpending.Series.Clear();
                Series spendingSeries = new Series("Average Spending");
                spendingSeries.ChartType = SeriesChartType.Column;

                foreach (DataRow row in spendingData.Rows)
                {
                    spendingSeries.Points.AddXY(row["TripType"].ToString(), Convert.ToDecimal(row["AvgSpending"]));
                }

                chartSpending.Series.Add(spendingSeries);

                // Format chart
                chartSpending.ChartAreas[0].AxisX.Title = "Trip Type";
                chartSpending.ChartAreas[0].AxisY.Title = "Average Spending ($)";
                chartSpending.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
                chartSpending.Series[0].IsValueShownAsLabel = true;
                chartSpending.Series[0].LabelFormat = "C0";
            }
        }

        private string GetCategoryFilterClause()
        {
            // Get selected category ID from combo box
            if (cboCategoryFilter.SelectedValue != null)
            {
                // Try to safely convert the value
                try
                {
                    int categoryId = Convert.ToInt32(cboCategoryFilter.SelectedValue);

                    if (categoryId > 0)
                        return $"AND TR.CategoryID = {categoryId}";
                }
                catch
                {
                    // If conversion fails, just return empty string (no filter)
                }
            }

            return string.Empty;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export to PDF functionality is not implemented in this diagnostic version.",
                "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export to Excel functionality is not implemented in this diagnostic version.",
                "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}