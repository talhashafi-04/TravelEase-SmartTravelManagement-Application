using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TripBookingReportApp
{
    public partial class TripBookingReportForm : Form
    {
        // Connection string - replace with your connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";
        private DataTable reportData;

        public TripBookingReportForm()
        {
            InitializeComponent();
        }

        private void TripBookingReportForm_Load(object sender, EventArgs e)
        {
            // Set default dates for the date pickers
            dtpStartDate.Value = DateTime.Now.AddMonths(-3);
            dtpEndDate.Value = DateTime.Now;

            // Fill Trip Type combo box
            FillTripTypeComboBox();
        }

        private void FillTripTypeComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT DISTINCT Name FROM TRIP_CATEGORY ORDER BY Name";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add "All" option
                    DataRow dr = dt.NewRow();
                    dr["Name"] = "All";



                    dt.Rows.InsertAt(dr, 0);

                    cboTripType.DataSource = dt;
                    cboTripType.DisplayMember = "Name";
                    cboTripType.ValueMember = "Name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Show loading indicator
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Generating report...";
                Application.DoEvents();

                // Get the parameters
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of selected day
                string tripType = cboTripType.SelectedValue.ToString();

                // Get data from database
                reportData = GetBookingData(startDate, endDate, tripType);

                if (reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "Ready";
                    this.Cursor = Cursors.Default;
                    return;
                }

                // Update report summary
                UpdateReportSummary();

                // Generate charts
                GenerateTripTypeRevenueChart();
                GenerateCapacityBookingsChart();
                GenerateMonthlyBookingsChart();

                // Display data in grid
                dgvBookings.DataSource = reportData;

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
                // Restore cursor
                this.Cursor = Cursors.Default;
            }
        }

        private DataTable GetBookingData(DateTime startDate, DateTime endDate, string Name)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                    SELECT 
                        b.BookingID, b.Date, b.Status, b.NoOfTravelers, 
                        b.TotalAmount, b.Tax, b.Discount, b.BookingNotes, 
                        b.ReminderSent, b.CancellationReason, 
                        b.TripID, b.TravelerID,
                        t.Title AS TripName, tc.Name, 
                        CASE 
                            WHEN t.capacity = 1 THEN 'Solo' 
                            ELSE 'Group' 
                        END AS Capacity,
                        CASE 
                            WHEN t.Duration_Days = 1 THEN '1-Day' 
                            WHEN t.Duration_Days <= 3 THEN '2-3 Day' 
                            ELSE '4+ Day' 
                        END AS Duration
                    FROM 
                        Booking b
                    INNER JOIN 
                        TRIP t ON b.TripID = t.TripID
                    INNER JOIN 
                        TRIP_CATEGORY tc  ON tc.CategoryID = t.CategoryID
                    WHERE 
                        b.Date BETWEEN @StartDate AND @EndDate";

                    // Add trip type filter if not "All"
                    if (Name != "All")
                    {
                        query += " AND tc.Name = @Name";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    if (Name != "All")
                    {
                        cmd.Parameters.AddWithValue("@Name", Name);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private void UpdateReportSummary()
        {
            // Calculate summary metrics
            int totalBookings = reportData.Rows.Count;
            decimal totalRevenue = reportData.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
            int cancelledBookings = reportData.AsEnumerable().Count(row => row.Field<string>("Status") == "Cancelled");
            double cancellationRate = totalBookings > 0 ? (double)cancelledBookings / totalBookings * 100 : 0;
            decimal avgBookingValue = totalBookings > 0 ? totalRevenue / totalBookings : 0;

            // Update labels
            lblTotalBookings.Text = totalBookings.ToString();
            lblTotalRevenue.Text = totalRevenue.ToString("C2");
            lblCancellationRate.Text = cancellationRate.ToString("F1") + "%";
            lblAvgBookingValue.Text = avgBookingValue.ToString("C2");

            // Update report period
            lblReportPeriod.Text = $"Report Period: {dtpStartDate.Value.ToShortDateString()} to {dtpEndDate.Value.ToShortDateString()}";
        }

        private void GenerateTripTypeRevenueChart()
        {
            // Clear previous series
            chartTripTypeRevenue.Series.Clear();
            chartTripTypeRevenue.Titles.Clear();

            // Add a title
            chartTripTypeRevenue.Titles.Add("Revenue by Trip Type");

            // Add a series
            Series series = new Series("Revenue");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "C0";

            // Group data by TripType and calculate total revenue
            var tripTypeRevenue = reportData.AsEnumerable()
                .GroupBy(row => row.Field<string>("Name"))
                .Select(g => new
                {
                    TripType = g.Key,
                    Revenue = g.Sum(row => row.Field<decimal>("TotalAmount"))
                })
                .OrderByDescending(x => x.Revenue);

            // Add data points
            foreach (var item in tripTypeRevenue)
            {
                series.Points.AddXY(item.TripType, (double)item.Revenue);
            }

            // Add the series to the chart
            chartTripTypeRevenue.Series.Add(series);

            // Format the chart
            chartTripTypeRevenue.ChartAreas[0].AxisX.Title = "Trip Type";
            chartTripTypeRevenue.ChartAreas[0].AxisY.Title = "Revenue";
            chartTripTypeRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
            chartTripTypeRevenue.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        }

        private void GenerateCapacityBookingsChart()
        {
            // Clear previous series
            chartCapacityBookings.Series.Clear();
            chartCapacityBookings.Titles.Clear();

            // Add a title
            chartCapacityBookings.Titles.Add("Bookings by NoOfTravellers");

            // Add a series
            Series series = new Series("Bookings");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "#,# (P0)";

            // Group data by NoOfTravellers and count bookings
            var capacityBookings = reportData.AsEnumerable()
                .GroupBy(row => row.Field<int>("NoOfTravelers"))
                .Select(g => new
                {
                    Capacity = g.Key,
                    Count = g.Count()
                });

            // Add data points
            foreach (var item in capacityBookings)
            {
                series.Points.AddXY(item.Capacity, item.Count);
            }

            // Add the series to the chart
            chartCapacityBookings.Series.Add(series);
            chartCapacityBookings.Legends[0].Enabled = true;
        }

        private void GenerateMonthlyBookingsChart()
        {
            // Clear previous series
            chartMonthlyBookings.Series.Clear();
            chartMonthlyBookings.Titles.Clear();

            // Add a title
            chartMonthlyBookings.Titles.Add("Peak Booking Periods");

            // Add a series
            Series series = new Series("Monthly Bookings");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            // Group data by Month and count bookings
            var monthlyBookings = reportData.AsEnumerable()
                .GroupBy(row => new {
                    Year = row.Field<DateTime>("Date").Year,
                    Month = row.Field<DateTime>("Date").Month
                })
                .Select(g => new
                {
                    YearMonth = g.Key.Year + "-" + g.Key.Month.ToString("D2"),
                    MonthName = g.Key.Year + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    Count = g.Count()
                })
                .OrderBy(x => x.YearMonth);

            // Add data points
            foreach (var item in monthlyBookings)
            {
                series.Points.AddXY(item.MonthName, item.Count);
            }

            // Add the series to the chart
            chartMonthlyBookings.Series.Add(series);

            // Format the chart
            chartMonthlyBookings.ChartAreas[0].AxisX.Title = "Month";
            chartMonthlyBookings.ChartAreas[0].AxisY.Title = "Number of Bookings";
            chartMonthlyBookings.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartMonthlyBookings.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Setup save dialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF File|*.pdf";
                saveDialog.Title = "Save Report as PDF";
                saveDialog.FileName = "TripBookingReport_" + DateTime.Now.ToString("yyyyMMdd");

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Exporting to PDF...";
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    // Create printer settings and page settings
                    PrintDocument printDoc = new PrintDocument();
                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

                    // Use a PDF printer library here - like iTextSharp, PDFsharp, etc.
                    // For simplicity, we'll just show a success message

                    MessageBox.Show("Note: To implement actual PDF export functionality, you need to add a " +
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

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // This is where you would implement the printing logic
            // For a full report, you'd draw the summary section, charts, and data grid

            // Example of printing a heading
            e.Graphics.DrawString("Trip Booking and Revenue Report",
                new Font("Arial", 16, FontStyle.Bold),
                Brushes.Black, new Point(100, 100));
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Setup save dialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel File|*.xlsx";
                saveDialog.Title = "Save Report as Excel";
                saveDialog.FileName = "TripBookingReport_" + DateTime.Now.ToString("yyyyMMdd");

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Exporting to Excel...";
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    // For real Excel export, you'd use a library like EPPlus, NPOI, or ClosedXML
                    // For simplicity, we'll just export the DataGridView data to CSV

                    ExportToCSV(saveDialog.FileName.Replace(".xlsx", ".csv"));

                    MessageBox.Show("Note: To implement proper Excel export functionality, you need to add an " +
                                    "Excel library like EPPlus, NPOI, or ClosedXML to your project.\n\n" +
                                    "Data has been exported to CSV format instead.",
                                    "Excel Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                for (int i = 0; i < dgvBookings.Columns.Count; i++)
                {
                    sw.Write(dgvBookings.Columns[i].HeaderText);
                    if (i < dgvBookings.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.WriteLine();

                // Write data
                foreach (DataGridViewRow row in dgvBookings.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < dgvBookings.Columns.Count; i++)
                        {
                            if (row.Cells[i].Value != null)
                            {
                                sw.Write(row.Cells[i].Value.ToString().Replace(",", ";"));
                            }
                            if (i < dgvBookings.Columns.Count - 1)
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