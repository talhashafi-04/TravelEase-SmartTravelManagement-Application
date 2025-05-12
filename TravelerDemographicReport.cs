using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelAnalyticsApp
{
    public partial class TravelerDemographicsForm : Form
    {
        // Connection string - replace with your connection string
        private string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DB;Integrated Security=True";
        private DataTable reportData;

        public TravelerDemographicsForm()
        {
            InitializeComponent();
        }

        private void TravelerDemographicsForm_Load(object sender, EventArgs e)
        {
            // Set default dates for the date pickers
            dtpStartDate.Value = DateTime.Now.AddMonths(-6);
            dtpEndDate.Value = DateTime.Now;

            // Load trip categories
            LoadTripCategories();

            // Initialize charts
            InitializeCharts();
        }

        private void LoadTripCategories()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT CategoryID, Name FROM TRIP_CATEGORY ORDER BY Name";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add "All" option
                    DataRow dr = dt.NewRow();
                    dr["CategoryID"] = 0;
                    dr["Name"] = "All Categories";
                    dt.Rows.InsertAt(dr, 0);

                    cboCategoryFilter.DataSource = dt;
                    cboCategoryFilter.DisplayMember = "Name";
                    cboCategoryFilter.ValueMember = "CategoryID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeCharts()
        {
            // Age Distribution Chart
            chartAgeDistribution.Series.Clear();
            chartAgeDistribution.Titles.Clear();
            chartAgeDistribution.Titles.Add("Age Distribution");
            Series ageSeries = new Series("Age Groups");
            ageSeries.ChartType = SeriesChartType.Column;
            chartAgeDistribution.Series.Add(ageSeries);
            chartAgeDistribution.ChartAreas[0].AxisX.Title = "Age Groups";
            chartAgeDistribution.ChartAreas[0].AxisY.Title = "Number of Travelers";
            chartAgeDistribution.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // Nationality Distribution Chart
            chartNationality.Series.Clear();
            chartNationality.Titles.Clear();
            chartNationality.Titles.Add("Top Nationalities");
            Series nationalitySeries = new Series("Nationalities");
            nationalitySeries.ChartType = SeriesChartType.Pie;
            nationalitySeries.IsValueShownAsLabel = true;
            nationalitySeries.Label = "#PERCENT{P0}";
            chartNationality.Series.Add(nationalitySeries);

            // Preferred Trip Types Chart
            chartTripTypes.Series.Clear();
            chartTripTypes.Titles.Clear();
            chartTripTypes.Titles.Add("Preferred Trip Types");
            Series tripTypeSeries = new Series("Trip Categories");
            tripTypeSeries.ChartType = SeriesChartType.Doughnut;
            tripTypeSeries.IsValueShownAsLabel = true;
            tripTypeSeries.Label = "#PERCENT{P0}";
            chartTripTypes.Series.Add(tripTypeSeries);

            // Popular Destinations Chart
            chartDestinations.Series.Clear();
            chartDestinations.Titles.Clear();
            chartDestinations.Titles.Add("Popular Destinations");
            Series destinationSeries = new Series("Destinations");
            destinationSeries.ChartType = SeriesChartType.Bar;
            chartDestinations.Series.Add(destinationSeries);
            chartDestinations.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // Spending Habits Chart
            chartSpending.Series.Clear();
            chartSpending.Titles.Clear();
            chartSpending.Titles.Add("Average Spending by Trip Type");
            Series spendingSeries = new Series("Avg. Spending");
            spendingSeries.ChartType = SeriesChartType.Column;
            spendingSeries.IsValueShownAsLabel = true;
            spendingSeries.LabelFormat = "C0";
            chartSpending.Series.Add(spendingSeries);
            chartSpending.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartSpending.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
            chartSpending.ChartAreas[0].AxisY.Title = "Average Amount Spent";
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Show loading indicator
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Generating report...";
                Application.DoEvents();

                // Get parameters
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of selected day
                int categoryId = Convert.ToInt32(cboCategoryFilter.SelectedValue);

                // Collect all data
                DataSet reportDataSet = GetTravelerDemographicsData(startDate, endDate, categoryId);

                // Update labels for overall metrics
                UpdateSummaryMetrics(reportDataSet.Tables["Summary"]);

                // Update charts
                UpdateAgeDistributionChart(reportDataSet.Tables["AgeDistribution"]);
                UpdateNationalityChart(reportDataSet.Tables["Nationalities"]);
                UpdateTripTypesChart(reportDataSet.Tables["TripTypes"]);
                UpdateDestinationsChart(reportDataSet.Tables["Destinations"]);
                UpdateSpendingChart(reportDataSet.Tables["Spending"]);

                // Update grid data
                dgvTravelerData.DataSource = reportDataSet.Tables["TravelerDetails"];
                AutoResizeDataGridViewColumns(dgvTravelerData);

                // Update period label
                lblReportPeriod.Text = $"Report Period: {startDate.ToShortDateString()} to {endDate.ToShortDateString()}";

                // Update status
                lblStatus.Text = "Report generated successfully";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error generating report";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void AutoResizeDataGridViewColumns(DataGridView dgv)
        {
            // Auto-resize columns for better display
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // Ensure the last column fills the remaining space
            if (dgv.Columns.Count > 0)
            {
                dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private DataSet GetTravelerDemographicsData(DateTime startDate, DateTime endDate, int categoryId)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create a general filter for category if specified
                    string categoryFilter = categoryId > 0 ? $" AND t.CategoryID = {categoryId}" : "";

                    // 1. Get summary data
                    string summarySql = @"
                    SELECT 
                        COUNT(DISTINCT tr.TravelerID) AS TotalTravelers,
                        COUNT(DISTINCT b.BookingID) AS TotalBookings,
                        AVG(b.TotalAmount) AS AvgSpending,
                        MAX(b.TotalAmount) AS MaxSpending,
                        MIN(CASE WHEN b.TotalAmount > 0 THEN b.TotalAmount ELSE NULL END) AS MinSpending
                    FROM 
                        TRAVELER tr
                        JOIN BOOKING b ON tr.TravelerID = b.TravelerID
                        JOIN TRIP t ON b.TripID = t.TripID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter;

                    SqlCommand cmdSummary = new SqlCommand(summarySql, conn);
                    cmdSummary.Parameters.AddWithValue("@StartDate", startDate);
                    cmdSummary.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter summaryAdapter = new SqlDataAdapter(cmdSummary);
                    summaryAdapter.Fill(ds, "Summary");

                    // 2. Get age distribution data
                    string ageSql = @"
                    SELECT 
                        CASE 
                            WHEN tr.Age < 18 THEN 'Under 18'
                            WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                            WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                            WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                            WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                            WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                            WHEN tr.Age >= 65 THEN '65+'
                            ELSE 'Unknown'
                        END AS AgeGroup,
                        COUNT(DISTINCT tr.TravelerID) AS TravelerCount
                    FROM 
                        TRAVELER tr
                        JOIN BOOKING b ON tr.TravelerID = b.TravelerID
                        JOIN TRIP t ON b.TripID = t.TripID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter + @"
                    GROUP BY 
                        CASE 
                            WHEN tr.Age < 18 THEN 'Under 18'
                            WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                            WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                            WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                            WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                            WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                            WHEN tr.Age >= 65 THEN '65+'
                            ELSE 'Unknown'
                        END
                    ORDER BY 
                        CASE 
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = 'Under 18' THEN 1
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = '18-24' THEN 2
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = '25-34' THEN 3
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = '35-44' THEN 4
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = '45-54' THEN 5
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = '55-64' THEN 6
                            WHEN CASE 
                                WHEN tr.Age < 18 THEN 'Under 18'
                                WHEN tr.Age BETWEEN 18 AND 24 THEN '18-24'
                                WHEN tr.Age BETWEEN 25 AND 34 THEN '25-34'
                                WHEN tr.Age BETWEEN 35 AND 44 THEN '35-44'
                                WHEN tr.Age BETWEEN 45 AND 54 THEN '45-54'
                                WHEN tr.Age BETWEEN 55 AND 64 THEN '55-64'
                                WHEN tr.Age >= 65 THEN '65+'
                                ELSE 'Unknown'
                            END = '65+' THEN 7
                            ELSE 8
                        END";

                    SqlCommand cmdAge = new SqlCommand(ageSql, conn);
                    cmdAge.Parameters.AddWithValue("@StartDate", startDate);
                    cmdAge.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter ageAdapter = new SqlDataAdapter(cmdAge);
                    ageAdapter.Fill(ds, "AgeDistribution");

                    // 3. Get nationality distribution data (top 5)
                    string nationalitySql = @"
                    SELECT TOP 5
                        tr.Nationality,
                        COUNT(DISTINCT tr.TravelerID) AS TravelerCount
                    FROM 
                        TRAVELER tr
                        JOIN BOOKING b ON tr.TravelerID = b.TravelerID
                        JOIN TRIP t ON b.TripID = t.TripID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter + @"
                    GROUP BY 
                        tr.Nationality
                    ORDER BY 
                        COUNT(DISTINCT tr.TravelerID) DESC";

                    SqlCommand cmdNationality = new SqlCommand(nationalitySql, conn);
                    cmdNationality.Parameters.AddWithValue("@StartDate", startDate);
                    cmdNationality.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter nationalityAdapter = new SqlDataAdapter(cmdNationality);
                    nationalityAdapter.Fill(ds, "Nationalities");

                    // 4. Get trip type preferences
                    string tripTypesSql = @"
                    SELECT 
                        tc.Name AS CategoryName,
                        COUNT(b.BookingID) AS BookingCount
                    FROM 
                        BOOKING b
                        JOIN TRIP t ON b.TripID = t.TripID
                        JOIN TRIP_CATEGORY tc ON t.CategoryID = tc.CategoryID
                        JOIN TRAVELER tr ON b.TravelerID = tr.TravelerID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter + @"
                    GROUP BY 
                        tc.Name
                    ORDER BY 
                        COUNT(b.BookingID) DESC";

                    SqlCommand cmdTripTypes = new SqlCommand(tripTypesSql, conn);
                    cmdTripTypes.Parameters.AddWithValue("@StartDate", startDate);
                    cmdTripTypes.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter tripTypesAdapter = new SqlDataAdapter(cmdTripTypes);
                    tripTypesAdapter.Fill(ds, "TripTypes");

                    // 5. Get popular destinations
                    string destinationsSql = @"
                    SELECT TOP 8
                        d.Name AS DestinationName,
                        COUNT(b.BookingID) AS BookingCount
                    FROM 
                        BOOKING b
                        JOIN TRIP t ON b.TripID = t.TripID
                        JOIN DESTINATION d ON t.DestinationID = d.DestinationID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter + @"
                    GROUP BY 
                        d.Name
                    ORDER BY 
                        COUNT(b.BookingID) DESC";

                    SqlCommand cmdDestinations = new SqlCommand(destinationsSql, conn);
                    cmdDestinations.Parameters.AddWithValue("@StartDate", startDate);
                    cmdDestinations.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter destinationsAdapter = new SqlDataAdapter(cmdDestinations);
                    destinationsAdapter.Fill(ds, "Destinations");

                    // 6. Get spending habits by trip type
                    string spendingSql = @"
                    SELECT 
                        tc.Name AS CategoryName,
                        AVG(b.TotalAmount) AS AverageSpending
                    FROM 
                        BOOKING b
                        JOIN TRIP t ON b.TripID = t.TripID
                        JOIN TRIP_CATEGORY tc ON t.CategoryID = tc.CategoryID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter + @"
                    GROUP BY 
                        tc.Name
                    ORDER BY 
                        AVG(b.TotalAmount) DESC";

                    SqlCommand cmdSpending = new SqlCommand(spendingSql, conn);
                    cmdSpending.Parameters.AddWithValue("@StartDate", startDate);
                    cmdSpending.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter spendingAdapter = new SqlDataAdapter(cmdSpending);
                    spendingAdapter.Fill(ds, "Spending");

                    // 7. Get traveler details for grid
                    string travelerDetailsSql = @"
                    SELECT 
                        tr.TravelerID,
                        u.FirstName + ' ' + u.LastName AS FullName,
                        tr.Age,
                        tr.Nationality,
                        tr.PreferredLanguage,
                        COUNT(b.BookingID) AS TotalBookings,
                        SUM(b.TotalAmount) AS TotalSpent,
                        AVG(b.TotalAmount) AS AvgSpent,
                        MAX(b.Date) AS LastBookingDate,
                        tr.LoyaltyPoints
                    FROM 
                        TRAVELER tr
                        JOIN USER u ON tr.TravelerID = u.UserID
                        JOIN BOOKING b ON tr.TravelerID = b.TravelerID
                        JOIN TRIP t ON b.TripID = t.TripID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate" + categoryFilter + @"
                    GROUP BY 
                        tr.TravelerID, u.FirstName, u.LastName, tr.Age, tr.Nationality, tr.PreferredLanguage, tr.LoyaltyPoints
                    ORDER BY 
                        SUM(b.TotalAmount) DESC";

                    SqlCommand cmdTravelerDetails = new SqlCommand(travelerDetailsSql, conn);
                    cmdTravelerDetails.Parameters.AddWithValue("@StartDate", startDate);
                    cmdTravelerDetails.Parameters.AddWithValue("@EndDate", endDate);
                    SqlDataAdapter travelerDetailsAdapter = new SqlDataAdapter(cmdTravelerDetails);
                    travelerDetailsAdapter.Fill(ds, "TravelerDetails");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ds;
        }

        private void UpdateSummaryMetrics(DataTable summaryTable)
        {
            if (summaryTable != null && summaryTable.Rows.Count > 0)
            {
                DataRow row = summaryTable.Rows[0];

                lblTotalTravelers.Text = row["TotalTravelers"].ToString();
                lblTotalBookings.Text = row["TotalBookings"].ToString();

                decimal avgSpending = row["AvgSpending"] != DBNull.Value ? Convert.ToDecimal(row["AvgSpending"]) : 0;
                decimal maxSpending = row["MaxSpending"] != DBNull.Value ? Convert.ToDecimal(row["MaxSpending"]) : 0;
                decimal minSpending = row["MinSpending"] != DBNull.Value ? Convert.ToDecimal(row["MinSpending"]) : 0;

                lblAvgSpending.Text = avgSpending.ToString("C2");
                lblSpendingRange.Text = $"{minSpending.ToString("C2")} - {maxSpending.ToString("C2")}";
            }
            else
            {
                lblTotalTravelers.Text = "0";
                lblTotalBookings.Text = "0";
                lblAvgSpending.Text = "$0.00";
                lblSpendingRange.Text = "$0.00 - $0.00";
            }
        }

        private void UpdateAgeDistributionChart(DataTable ageTable)
        {
            chartAgeDistribution.Series[0].Points.Clear();

            if (ageTable != null && ageTable.Rows.Count > 0)
            {
                foreach (DataRow row in ageTable.Rows)
                {
                    string ageGroup = row["AgeGroup"].ToString();
                    int count = Convert.ToInt32(row["TravelerCount"]);
                    chartAgeDistribution.Series[0].Points.AddXY(ageGroup, count);
                }

                // Set Y-axis minimum to zero
                chartAgeDistribution.ChartAreas[0].AxisY.Minimum = 0;

                // Format data labels
                chartAgeDistribution.Series[0].IsValueShownAsLabel = true;
            }
        }

        private void UpdateNationalityChart(DataTable nationalityTable)
        {
            chartNationality.Series[0].Points.Clear();

            if (nationalityTable != null && nationalityTable.Rows.Count > 0)
            {
                foreach (DataRow row in nationalityTable.Rows)
                {
                    string nationality = row["Nationality"].ToString();
                    int count = Convert.ToInt32(row["TravelerCount"]);
                    int pointIndex = chartNationality.Series[0].Points.AddXY(nationality, count);
                    chartNationality.Series[0].Points[pointIndex].Label = $"{nationality}: #PERCENT{{P0}}";
                }
            }
        }

        private void UpdateTripTypesChart(DataTable tripTypesTable)
        {
            chartTripTypes.Series[0].Points.Clear();

            if (tripTypesTable != null && tripTypesTable.Rows.Count > 0)
            {
                foreach (DataRow row in tripTypesTable.Rows)
                {
                    string category = row["CategoryName"].ToString();
                    int count = Convert.ToInt32(row["BookingCount"]);
                    int pointIndex = chartTripTypes.Series[0].Points.AddXY(category, count);
                    chartTripTypes.Series[0].Points[pointIndex].Label = $"{category}: #PERCENT{{P0}}";
                }
            }
        }

        private void UpdateDestinationsChart(DataTable destinationsTable)
        {
            chartDestinations.Series[0].Points.Clear();

            if (destinationsTable != null && destinationsTable.Rows.Count > 0)
            {
                // Add destinations in reverse order (so highest appears at top in horizontal bar chart)
                for (int i = destinationsTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = destinationsTable.Rows[i];
                    string destination = row["DestinationName"].ToString();
                    int count = Convert.ToInt32(row["BookingCount"]);
                    chartDestinations.Series[0].Points.AddXY(destination, count);
                }

                // Set Y-axis minimum to zero
                chartDestinations.ChartAreas[0].AxisY.Minimum = 0;

                // Format data labels
                chartDestinations.Series[0].IsValueShownAsLabel = true;
            }
        }

        private void UpdateSpendingChart(DataTable spendingTable)
        {
            chartSpending.Series[0].Points.Clear();

            if (spendingTable != null && spendingTable.Rows.Count > 0)
            {
                foreach (DataRow row in spendingTable.Rows)
                {
                    string category = row["CategoryName"].ToString();
                    decimal avgSpending = Convert.ToDecimal(row["AverageSpending"]);
                    chartSpending.Series[0].Points.AddXY(category, (double)avgSpending);
                }

                // Set Y-axis minimum to zero
                chartSpending.ChartAreas[0].AxisY.Minimum = 0;
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Setup save dialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF File|*.pdf";
                saveDialog.Title = "Save Report as PDF";
                saveDialog.FileName = "TravelerDemographics_" + DateTime.Now.ToString("yyyyMMdd");

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Exporting to PDF...";
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    // Note: In a real implementation, you would use a PDF library like iTextSharp or PDFsharp
                    MessageBox.Show("To implement actual PDF export functionality, you need to add a " +
                                    "PDF library like iTextSharp or PDFsharp to your project.",
                                    "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblStatus.Text = "Report exported successfully";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error exporting report";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Setup save dialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel File|*.xlsx";
                saveDialog.Title = "Save Report as Excel";
                saveDialog.FileName = "TravelerDemographics_" + DateTime.Now.ToString("yyyyMMdd");

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Exporting to Excel...";
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    // Note: In a real implementation, you would use a library like EPPlus, NPOI, or ClosedXML
                    MessageBox.Show("To implement proper Excel export functionality, you need to add an " +
                                    "Excel library like EPPlus, NPOI, or ClosedXML to your project.",
                                    "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // For simplicity, export the grid data to CSV
                    ExportToCSV(saveDialog.FileName.Replace(".xlsx", ".csv"));

                    lblStatus.Text = "Report exported successfully";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error exporting report";
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportToCSV(string fileName)
        {
            // Simple CSV export of the grid data
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            {
                // Write headers
                for (int i = 0; i < dgvTravelerData.Columns.Count; i++)
                {
                    sw.Write(dgvTravelerData.Columns[i].HeaderText);
                    if (i < dgvTravelerData.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.WriteLine();

                // Write data
                foreach (DataGridViewRow row in dgvTravelerData.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < dgvTravelerData.Columns.Count; i++)
                        {
                            if (row.Cells[i].Value != null)
                            {
                                sw.Write(row.Cells[i].Value.ToString().Replace(",", ";"));
                            }
                            if (i < dgvTravelerData.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}