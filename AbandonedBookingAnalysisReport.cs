//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace DatabaseProject
//{
//    public partial class AbandonedBookingReportForm : Form
//    {
//        private Chart chartAbandonment, chartReasons, chartRecovery, chartLossTrend;
//        private ComboBox cboTimeRange;
//        private Label lblTimeRange;
//        private DateTimePicker dtpStartDate, dtpEndDate;
//        private Label lblStartDate, lblEndDate;
//        private RadioButton radPreset, radCustom;
//        private Button btnGenerate, btnExportCsv;
//        private SqlConnection con = new SqlConnection(
//            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

//        // DataTables for CSV export
//        private DataTable dtAbandonment, dtReasons, dtRecovery, dtLossTrend;

//        public AbandonedBookingReportForm()
//        {
//            InitializeComponents();
//        }

//        private void InitializeComponents()
//        {
//            this.Text = "Abandoned Booking Analysis";
//            this.ClientSize = new Size(1000, 850);
//            this.StartPosition = FormStartPosition.CenterScreen;

//            // Date range selection controls
//            radPreset = new RadioButton { Text = "Preset Range:", Location = new Point(20, 20), Size = new Size(110, 20), Checked = true };
//            radCustom = new RadioButton { Text = "Custom Range:", Location = new Point(20, 50), Size = new Size(110, 20) };

//            radPreset.CheckedChanged += (s, e) => ToggleDateRangeControls();
//            radCustom.CheckedChanged += (s, e) => ToggleDateRangeControls();

//            lblTimeRange = new Label { Text = "Time Range:", Location = new Point(140, 20), Size = new Size(80, 20) };
//            cboTimeRange = new ComboBox
//            {
//                Location = new Point(230, 20),
//                Size = new Size(150, 30),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            cboTimeRange.Items.AddRange(new object[] { "Last Month", "Last 3 Months", "Last 6 Months", "Last Year", "All Time" });
//            cboTimeRange.SelectedIndex = 2; // Default to Last 6 Months

//            lblStartDate = new Label { Text = "Start Date:", Location = new Point(140, 50), Size = new Size(80, 20) };
//            dtpStartDate = new DateTimePicker
//            {
//                Location = new Point(230, 50),
//                Size = new Size(150, 30),
//                Format = DateTimePickerFormat.Short,
//                Value = DateTime.Now.AddMonths(-1)
//            };

//            lblEndDate = new Label { Text = "End Date:", Location = new Point(400, 50), Size = new Size(80, 20) };
//            dtpEndDate = new DateTimePicker
//            {
//                Location = new Point(480, 50),
//                Size = new Size(150, 30),
//                Format = DateTimePickerFormat.Short,
//                Value = DateTime.Now
//            };

//            btnGenerate = new Button { Text = "Generate Report", Location = new Point(400, 20), Size = new Size(150, 30) };
//            btnGenerate.Click += (s, e) => LoadReportData();

//            btnExportCsv = new Button { Text = "Export to CSV", Location = new Point(570, 20), Size = new Size(150, 30), Enabled = false };
//            btnExportCsv.Click += (s, e) => ExportCsvFiles();

//            chartAbandonment = CreateChart("Abandonment Rate", new Point(20, 90));
//            chartReasons = CreateChart("Top Cancellation Reasons", new Point(520, 90));
//            chartRecovery = CreateChart("Recovery Rate", new Point(20, 450));
//            chartLossTrend = CreateChart("Potential Revenue Loss (Monthly)", new Point(520, 450));

//            this.Controls.AddRange(new Control[] {
//                radPreset, radCustom, lblTimeRange, cboTimeRange,
//                lblStartDate, dtpStartDate, lblEndDate, dtpEndDate,
//                btnGenerate, btnExportCsv,
//                chartAbandonment, chartReasons,
//                chartRecovery, chartLossTrend
//            });

//            // Initialize date controls visibility
//            ToggleDateRangeControls();
//        }

//        private void ToggleDateRangeControls()
//        {
//            bool usePreset = radPreset.Checked;

//            lblTimeRange.Visible = usePreset;
//            cboTimeRange.Visible = usePreset;

//            lblStartDate.Visible = !usePreset;
//            dtpStartDate.Visible = !usePreset;
//            lblEndDate.Visible = !usePreset;
//            dtpEndDate.Visible = !usePreset;
//        }

//        private Chart CreateChart(string title, Point loc)
//        {
//            var chart = new Chart { Location = loc, Size = new Size(450, 330) };
//            chart.ChartAreas.Add(new ChartArea());

//            chart.Titles.Add(new Title(title, Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black));
//            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
//            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
//            chart.ChartAreas[0].BackColor = Color.White;
//            chart.BorderlineDashStyle = ChartDashStyle.Solid;
//            chart.BorderlineColor = Color.LightGray;
//            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

//            return chart;
//        }

//        private void LoadReportData()
//        {
//            try
//            {
//                Cursor = Cursors.WaitCursor;

//                string dateFilter = GetDateFilterClause();

//                LoadAbandonmentChart(dateFilter);
//                LoadReasonsChart(dateFilter);
//                LoadRecoveryChart(dateFilter);
//                LoadLossTrendChart(dateFilter);

//                btnExportCsv.Enabled = true;
//                Cursor = Cursors.Default;
//            }
//            catch (Exception ex)
//            {
//                Cursor = Cursors.Default;
//                MessageBox.Show("Error loading report data: " + ex.Message, "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private string GetDateFilterClause()
//        {
//            string clause;
//            DateTime now = DateTime.Now;

//            if (radPreset.Checked)
//            {
//                switch (cboTimeRange.SelectedIndex)
//                {
//                    case 0: // Last Month
//                        clause = $"AND Date >= '{now.AddMonths(-1).ToString("yyyy-MM-dd")}'";
//                        break;
//                    case 1: // Last 3 Months
//                        clause = $"AND Date >= '{now.AddMonths(-3).ToString("yyyy-MM-dd")}'";
//                        break;
//                    case 2: // Last 6 Months
//                        clause = $"AND Date >= '{now.AddMonths(-6).ToString("yyyy-MM-dd")}'";
//                        break;
//                    case 3: // Last Year
//                        clause = $"AND Date >= '{now.AddYears(-1).ToString("yyyy-MM-dd")}'";
//                        break;
//                    default: // All Time
//                        clause = "";
//                        break;
//                }
//            }
//            else
//            {
//                clause = $"AND Date BETWEEN '{dtpStartDate.Value.ToString("yyyy-MM-dd")}' AND '{dtpEndDate.Value.ToString("yyyy-MM-dd")}'";
//            }

//            return clause;
//        }

//        private void LoadAbandonmentChart(string dateFilter)
//        {
//            chartAbandonment.Series.Clear();
//            chartAbandonment.Legends.Clear();

//            var series = new Series("Status") { ChartType = SeriesChartType.Doughnut };
//            chartAbandonment.Series.Add(series);

//            var legend = new Legend("BookingStatus");
//            chartAbandonment.Legends.Add(legend);

//            string sql = @"
//SELECT
//    Status,
//    COUNT(*) AS Count,
//    CAST(COUNT() * 100.0 / (SELECT COUNT() FROM BOOKING WHERE 1=1 " + dateFilter + @") AS DECIMAL(5,2)) AS Percentage
//FROM 
//    BOOKING
//WHERE 
//    1=1 " + dateFilter + @"
//GROUP BY 
//    Status";

//            dtAbandonment = new DataTable();
//            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
//            {
//                connection.Open();
//                using (SqlCommand cmd = new SqlCommand(sql, connection))
//                {
//                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
//                    {
//                        adapter.Fill(dtAbandonment);
//                    }
//                }
//            }

//            // Define colors for different statuses
//            Dictionary<string, Color> statusColors = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase)
//            {
//                { "Cancelled", Color.Tomato },
//                { "Completed", Color.MediumSeaGreen },
//                { "Pending", Color.SkyBlue },
//                { "Processing", Color.Gold }
//            };

//            int index = 0;
//            foreach (DataRow row in dtAbandonment.Rows)
//            {
//                string status = row["Status"].ToString();
//                int count = Convert.ToInt32(row["Count"]);
//                decimal percentage = Convert.ToDecimal(row["Percentage"]);

//                DataPoint point = new DataPoint();
//                point.SetValueXY(status, count);
//                point.LegendText = $"{status} ({percentage}%)";

//                if (statusColors.ContainsKey(status))
//                {
//                    point.Color = statusColors[status];
//                }

//                series.Points.Add(point);
//                index++;
//            }

//            // Add labels to show percentages
//            series.Label = "#PERCENT{P1}";
//            series.LabelForeColor = Color.White;
//            series.LabelFont = new Font("Arial", 9, FontStyle.Bold);
//        }

//        private void LoadReasonsChart(string dateFilter)
//        {
//            chartReasons.Series.Clear();
//            chartReasons.Legends.Clear();

//            var series = new Series("Reasons") { ChartType = SeriesChartType.Column };
//            chartReasons.Series.Add(series);

//            chartReasons.ChartAreas[0].AxisX.Interval = 1;
//            chartReasons.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
//            chartReasons.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);

//            chartReasons.ChartAreas[0].AxisY.Title = "Number of Cancellations";
//            chartReasons.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10);

//            string sql = @"
//SELECT TOP 5 
//    CancellationReason AS Reason, 
//    COUNT(*) AS Count
//FROM 
//    BOOKING
//WHERE 
//    Status = 'Cancelled' 
//    AND CancellationReason IS NOT NULL " + dateFilter + @"
//GROUP BY 
//    CancellationReason
//ORDER BY 
//    Count DESC";

//            dtReasons = new DataTable();
//            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
//            {
//                connection.Open();
//                using (SqlCommand cmd = new SqlCommand(sql, connection))
//                {
//                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
//                    {
//                        adapter.Fill(dtReasons);
//                    }
//                }
//            }

//            // Color array for different reasons
//            Color[] reasonColors = new Color[] {
//                Color.SteelBlue, Color.Crimson, Color.ForestGreen,
//                Color.DarkOrange, Color.DarkViolet
//            };

//            int index = 0;
//            foreach (DataRow row in dtReasons.Rows)
//            {
//                string reason = row["Reason"].ToString();
//                int count = Convert.ToInt32(row["Count"]);

//                DataPoint point = series.Points.AddXY(reason, count);

//                // Use a specific color based on index (cycling if needed)
//                point.Color = reasonColors[index % reasonColors.Length];

//                // Add data label on top of each column
//                point.Label = count.ToString();
//                point.LabelForeColor = Color.Black;

//                index++;
//            }

//            // If we have fewer than 5 reasons, add a message
//            if (dtReasons.Rows.Count == 0)
//            {
//                chartReasons.Titles.Add(new Title("No cancellation data available", Docking.Bottom,
//                    new Font("Arial", 10), Color.Gray));
//            }
//        }

//        private void LoadRecoveryChart(string dateFilter)
//        {
//            chartRecovery.Series.Clear();
//            chartRecovery.Legends.Clear();

//            var series = new Series("Recovery") { ChartType = SeriesChartType.Pie };
//            chartRecovery.Series.Add(series);

//            var legend = new Legend("RecoveryStatus");
//            chartRecovery.Legends.Add(legend);

//            string sql = @"
//WITH CancelledBookings AS (
//    SELECT 
//        DISTINCT TravelerID 
//    FROM 
//        BOOKING 
//    WHERE 
//        Status = 'Cancelled' " + dateFilter + @"
//),
//RecoveredBookings AS (
//    SELECT 
//        DISTINCT b.TravelerID
//    FROM 
//        BOOKING b
//        JOIN CancelledBookings cb ON b.TravelerID = cb.TravelerID
//    WHERE 
//        b.Status = 'Completed' 
//        AND b.Date > (
//            SELECT MIN(b2.Date) 
//            FROM BOOKING b2 
//            WHERE b2.TravelerID = b.TravelerID AND b2.Status = 'Cancelled'
//        ) " + dateFilter + @"
//)
//SELECT
//    (SELECT COUNT(*) FROM RecoveredBookings) AS Recovered,
//    (SELECT COUNT() FROM CancelledBookings) - (SELECT COUNT() FROM RecoveredBookings) AS NotRecovered";

//            dtRecovery = new DataTable();
//            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
//            {
//                connection.Open();
//                using (SqlCommand cmd = new SqlCommand(sql, connection))
//                {
//                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
//                    {
//                        adapter.Fill(dtRecovery);
//                    }
//                }
//            }

//            if (dtRecovery.Rows.Count > 0)
//            {
//                DataRow row = dtRecovery.Rows[0];
//                int recovered = Convert.ToInt32(row["Recovered"]);
//                int notRecovered = Convert.ToInt32(row["NotRecovered"]);
//                int total = recovered + notRecovered;

//                if (total > 0)
//                {
//                    decimal recoveredPct = Math.Round((decimal)recovered / total * 100, 1);
//                    decimal notRecoveredPct = Math.Round((decimal)notRecovered / total * 100, 1);

//                    DataPoint pointRecovered = new DataPoint();
//                    pointRecovered.SetValueXY("Recovered", recovered);
//                    pointRecovered.LegendText = $"Recovered ({recoveredPct}%)";
//                    pointRecovered.Color = Color.MediumSeaGreen;
//                    series.Points.Add(pointRecovered);

//                    DataPoint pointNotRecovered = new DataPoint();
//                    pointNotRecovered.SetValueXY("Not Recovered", notRecovered);
//                    pointNotRecovered.LegendText = $"Not Recovered ({notRecoveredPct}%)";
//                    pointNotRecovered.Color = Color.IndianRed;
//                    series.Points.Add(pointNotRecovered);

//                    // Add labels
//                    series.Label = "#PERCENT{P1}";
//                    series.LabelForeColor = Color.White;
//                    series.LabelFont = new Font("Arial", 9, FontStyle.Bold);

//                    // Add recovery rate as a subtitle
//                    chartRecovery.Titles.Add(new Title($"Recovery Rate: {recoveredPct}%", Docking.Bottom,
//                        new Font("Arial", 10, FontStyle.Bold), Color.DarkGreen));
//                }
//                else
//                {
//                    chartRecovery.Titles.Add(new Title("No recovery data available", Docking.Bottom,
//                        new Font("Arial", 10), Color.Gray));
//                }
//            }
//        }

//        private void LoadLossTrendChart(string dateFilter)
//        {
//            chartLossTrend.Series.Clear();
//            chartLossTrend.Legends.Clear();

//            var lostSeries = new Series("Lost Revenue")
//            {
//                ChartType = SeriesChartType.Column,
//                Color = Color.Salmon
//            };

//            var countSeries = new Series("Cancelled Count")
//            {
//                ChartType = SeriesChartType.Line,
//                BorderWidth = 3,
//                Color = Color.DarkBlue,
//                YAxisType = AxisType.Secondary
//            };

//            chartLossTrend.Series.Add(lostSeries);
//            chartLossTrend.Series.Add(countSeries);

//            // Configure chart area for dual Y-axis
//            chartLossTrend.ChartAreas[0].AxisY.Title = "Lost Revenue ($)";
//            chartLossTrend.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 9);
//            chartLossTrend.ChartAreas[0].AxisY.LabelStyle.Format = "${0:N0}";

//            chartLossTrend.ChartAreas[0].AxisY2.Title = "Number of Cancellations";
//            chartLossTrend.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 9);
//            chartLossTrend.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

//            chartLossTrend.ChartAreas[0].AxisX.Interval = 1;
//            chartLossTrend.ChartAreas[0].AxisX.Title = "Month";
//            chartLossTrend.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 9);

//            var legend = new Legend("LossTrend");
//            chartLossTrend.Legends.Add(legend);

//            string sql = @"
//SELECT 
//    MONTH(Date) AS MonthNum,
//    YEAR(Date) AS YearNum,
//    FORMAT(Date, 'MMM yyyy') AS MonthYear,
//    COUNT(*) AS CancelCount,
//    SUM(TotalAmount) AS LostRevenue
//FROM 
//    BOOKING
//WHERE 
//    Status = 'Cancelled' " + dateFilter + @"
//GROUP BY 
//    MONTH(Date),
//    YEAR(Date),
//    FORMAT(Date, 'MMM yyyy')
//ORDER BY 
//    YearNum, MonthNum";

//            dtLossTrend = new DataTable();
//            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
//            {
//                connection.Open();
//                using (SqlCommand cmd = new SqlCommand(sql, connection))
//                {
//                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
//                    {
//                        adapter.Fill(dtLossTrend);
//                    }
//                }
//            }

//            // Calculate cumulative loss for the title
//            decimal totalLoss = 0;
//            if (dtLossTrend.Rows.Count > 0)
//            {
//                totalLoss = dtLossTrend.AsEnumerable()
//                    .Sum(row => Convert.ToDecimal(row["LostRevenue"]));
//            }

//            // Add subtitle showing total loss
//            chartLossTrend.Titles.Add(new Title($"Total Potential Loss: ${totalLoss:N2}", Docking.Bottom,
//                new Font("Arial", 10, FontStyle.Bold), Color.Crimson));

//            foreach (DataRow row in dtLossTrend.Rows)
//            {
//                string monthYear = row["MonthYear"].ToString();
//                double lostRevenue = Convert.ToDouble(row["LostRevenue"]);
//                int cancelCount = Convert.ToInt32(row["CancelCount"]);

//                lostSeries.Points.AddXY(monthYear, lostRevenue);
//                countSeries.Points.AddXY(monthYear, cancelCount);
//            }
//        }

//        private void ExportCsvFiles()
//        {
//            using (var dlg = new FolderBrowserDialog { Description = "Select folder to save CSV reports" })
//            {
//                if (dlg.ShowDialog() != DialogResult.OK) return;
//                string folder = dlg.SelectedPath;
//                try
//                {
//                    Cursor = Cursors.WaitCursor;

//                    WriteDataTableToCsv(dtAbandonment, Path.Combine(folder, "AbandonmentRate.csv"));
//                    WriteDataTableToCsv(dtReasons, Path.Combine(folder, "CancellationReasons.csv"));
//                    WriteDataTableToCsv(dtRecovery, Path.Combine(folder, "RecoveryRates.csv"));
//                    WriteDataTableToCsv(dtLossTrend, Path.Combine(folder, "PotentialRevenueLoss.csv"));

//                    // Create a summary report
//                    CreateSummaryReport(folder);

//                    Cursor = Cursors.Default;
//                    MessageBox.Show("CSV files exported successfully.", "Export Complete",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    Cursor = Cursors.Default;
//                    MessageBox.Show("Error exporting CSV: " + ex.Message,
//                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void CreateSummaryReport(string folder)
//        {
//            using (var sw = new StreamWriter(Path.Combine(folder, "BookingAbandonmentSummary.csv")))
//            {
//                sw.WriteLine("Abandoned Booking Analysis Summary");
//                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                sw.WriteLine();

//                // Abandonment rate summary
//                sw.WriteLine("Abandonment Rate Summary");
//                sw.WriteLine("Status,Count,Percentage");
//                foreach (DataRow row in dtAbandonment.Rows)
//                {
//                    sw.WriteLine($"\"{row["Status"]}\",{row["Count"]},{row["Percentage"]}%");
//                }
//                sw.WriteLine();

//                // Recovery rate summary
//                if (dtRecovery.Rows.Count > 0)
//                {
//                    sw.WriteLine("Recovery Rate Summary");
//                    DataRow r = dtRecovery.Rows[0];
//                    int recovered = Convert.ToInt32(r["Recovered"]);
//                    int notRecovered = Convert.ToInt32(r["NotRecovered"]);
//                    int total = recovered + notRecovered;
//                    decimal recoveryRate = total > 0 ? Math.Round((decimal)recovered / total * 100, 2) : 0;

//                    sw.WriteLine($"Recovered,{recovered}");
//                    sw.WriteLine($"Not Recovered,{notRecovered}");
//                    sw.WriteLine($"Total,{total}");
//                    sw.WriteLine($"Recovery Rate,{recoveryRate}%");
//                    sw.WriteLine();
//                }

//                // Top cancellation reasons
//                sw.WriteLine("Top Cancellation Reasons");
//                sw.WriteLine("Reason,Count");
//                foreach (DataRow row in dtReasons.Rows)
//                {
//                    sw.WriteLine($"\"{row["Reason"]}\",{row["Count"]}");
//                }
//                sw.WriteLine();

//                // Revenue loss summary
//                sw.WriteLine("Revenue Loss Summary");
//                sw.WriteLine("Month,Cancellations,Lost Revenue");
//                decimal totalLost = 0;
//                int totalCancellations = 0;

//                foreach (DataRow row in dtLossTrend.Rows)
//                {
//                    string month = row["MonthYear"].ToString();
//                    int count = Convert.ToInt32(row["CancelCount"]);
//                    decimal revenue = Convert.ToDecimal(row["LostRevenue"]);

//                    totalCancellations += count;
//                    totalLost += revenue;

//                    sw.WriteLine($"\"{month}\",{count},${revenue:N2}");
//                }

//                sw.WriteLine($"Total,{totalCancellations},${totalLost:N2}");
//            }
//        }

//        private void WriteDataTableToCsv(DataTable table, string filePath)
//        {
//            using (var sw = new StreamWriter(filePath))
//            {
//                // Header
//                for (int i = 0; i < table.Columns.Count; i++)
//                {
//                    if (i > 0) sw.Write(",");
//                    sw.Write($"\"{table.Columns[i].ColumnName}\"");
//                }
//                sw.WriteLine();

//                // Rows
//                foreach (DataRow row in table.Rows)
//                {
//                    for (int i = 0; i < table.Columns.Count; i++)
//                    {
//                        if (i > 0) sw.Write(",");
//                        string val = row[i]?.ToString() ?? "";
//                        // Escape quotes and special characters
//                        val = val.Replace("\"", "\"\"");
//                        sw.Write($"\"{val}\"");
//                    }
//                    sw.WriteLine();
//                }
//            }
//        }
//    }
//}