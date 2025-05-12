//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.IO;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace DatabaseProject
//{
//    public partial class PaymentFraudReportForm : Form
//    {
//        private Chart chartSuccessFailure;
//        private Chart chartChargebackRate;
//        private Button btnGenerateReport;
//        private Button btnExportCsv;
//        private DateTimePicker dtpStartDate;
//        private DateTimePicker dtpEndDate;
//        private Label lblDateRange;
//        private readonly SqlConnection con = new SqlConnection(
//            @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;");

//        // DataTables to hold report data
//        private DataTable dtSuccessFailure;
//        private DataTable dtChargeback;

//        public PaymentFraudReportForm()
//        {
//            InitializeComponents();
//        }

//        private void InitializeComponents()
//        {
//            this.Text = "Payment Transaction & Fraud Report";
//            this.ClientSize = new Size(900, 600);
//            this.StartPosition = FormStartPosition.CenterScreen;

//            // Date range controls
//            lblDateRange = new Label
//            {
//                Text = "Date Range:",
//                Location = new Point(20, 20),
//                Size = new Size(80, 20)
//            };

//            dtpStartDate = new DateTimePicker
//            {
//                Location = new Point(100, 20),
//                Size = new Size(120, 20),
//                Format = DateTimePickerFormat.Short,
//                Value = DateTime.Now.AddMonths(-6)
//            };

//            Label lblTo = new Label
//            {
//                Text = "to",
//                Location = new Point(230, 20),
//                Size = new Size(20, 20),
//                TextAlign = ContentAlignment.MiddleCenter
//            };

//            dtpEndDate = new DateTimePicker
//            {
//                Location = new Point(260, 20),
//                Size = new Size(120, 20),
//                Format = DateTimePickerFormat.Short,
//                Value = DateTime.Now
//            };

//            btnGenerateReport = new Button
//            {
//                Text = "Generate Report",
//                Location = new Point(400, 18),
//                Size = new Size(150, 25)
//            };
//            btnGenerateReport.Click += (s, e) => LoadReportData();

//            btnExportCsv = new Button
//            {
//                Text = "Export to CSV",
//                Location = new Point(560, 18),
//                Size = new Size(150, 25),
//                Enabled = false
//            };
//            btnExportCsv.Click += (s, e) => ExportCsvFiles();

//            // Create charts
//            chartSuccessFailure = CreateChart("Payment Success vs Failure Rate", new Point(20, 60));
//            chartChargebackRate = CreateChart("Monthly Chargeback Rate (% of Total Transactions)", new Point(460, 60));

//            // Add controls to form
//            this.Controls.AddRange(new Control[]
//            {
//                lblDateRange,
//                dtpStartDate,
//                lblTo,
//                dtpEndDate,
//                btnGenerateReport,
//                btnExportCsv,
//                chartSuccessFailure,
//                chartChargebackRate
//            });
//        }

//        private Chart CreateChart(string title, Point location)
//        {
//            var chart = new Chart
//            {
//                Location = location,
//                Size = new Size(400, 450),
//                Anchor = AnchorStyles.Top | AnchorStyles.Left
//            };

//            var chartArea = new ChartArea();
//            chartArea.AxisX.LabelStyle.Angle = -45;
//            chartArea.AxisX.MajorGrid.Enabled = false;
//            chartArea.AxisY.Title = "Percentage (%)";
//            chartArea.AxisY.LabelStyle.Format = "P0";
//            chart.ChartAreas.Add(chartArea);

//            var title1 = new Title(title)
//            {
//                Font = new Font("Arial", 12, FontStyle.Bold)
//            };
//            chart.Titles.Add(title1);

//            return chart;
//        }

//        private void LoadReportData()
//        {
//            try
//            {
//                Cursor = Cursors.WaitCursor;

//                if (dtpEndDate.Value < dtpStartDate.Value)
//                {
//                    MessageBox.Show("End date must be after start date.", "Invalid Date Range",
//                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                LoadSuccessFailureChart();
//                LoadChargebackRateChart();
//                btnExportCsv.Enabled = true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading report data: {ex.Message}", "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                Cursor = Cursors.Default;
//            }
//        }

//        private void LoadSuccessFailureChart()
//        {
//            chartSuccessFailure.Series.Clear();

//            var series = new Series("Transactions")
//            {
//                ChartType = SeriesChartType.Pie,
//                IsValueShownAsLabel = true,
//                LabelFormat = "{0:P1}",
//                Font = new Font("Arial", 10, FontStyle.Regular)
//            };

//            chartSuccessFailure.Series.Add(series);

//            string sql = @"
//SELECT
//  SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) AS SuccessCount,
//  SUM(CASE WHEN Status = 'Failed' THEN 1 ELSE 0 END) AS FailureCount,
//  COUNT(*) AS TotalCount
//FROM PAYMENT
//WHERE Date BETWEEN @StartDate AND @EndDate";

//            dtSuccessFailure = new DataTable();
//            using (var adapter = new SqlDataAdapter(sql, con))
//            {
//                adapter.SelectCommand.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
//                adapter.SelectCommand.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
//                adapter.Fill(dtSuccessFailure);
//            }

//            if (dtSuccessFailure.Rows.Count > 0 && !Convert.IsDBNull(dtSuccessFailure.Rows[0]["TotalCount"]))
//            {
//                var row = dtSuccessFailure.Rows[0];
//                int success = Convert.ToInt32(row["SuccessCount"]);
//                int failure = Convert.ToInt32(row["FailureCount"]);
//                int total = Convert.ToInt32(row["TotalCount"]);

//                if (total > 0)
//                {
//                    double successRate = (double)success / total;
//                    double failureRate = (double)failure / total;

//                    var successPoint = series.Points.AddXY("Success", successRate);
//                    successPoint.Color = Color.Green;
//                    successPoint.LegendText = $"Success ({success:N0} transactions)";

//                    var failurePoint = series.Points.AddXY("Failure", failureRate);
//                    failurePoint.Color = Color.Red;
//                    failurePoint.LegendText = $"Failure ({failure:N0} transactions)";

//                    // Add legend
//                    var legend = new Legend
//                    {
//                        Alignment = StringAlignment.Center,
//                        Docking = Docking.Bottom,
//                        LegendStyle = LegendStyle.Row
//                    };
//                    chartSuccessFailure.Legends.Add(legend);
//                }
//                else
//                {
//                    // No data
//                    AddNoDataLabel(chartSuccessFailure);
//                }
//            }
//            else
//            {
//                // No data
//                AddNoDataLabel(chartSuccessFailure);
//            }
//        }

//        private void LoadChargebackRateChart()
//        {
//            chartChargebackRate.Series.Clear();
//            chartChargebackRate.Legends.Clear();

//            var series = new Series("Chargeback Rate")
//            {
//                ChartType = SeriesChartType.Column,
//                Color = Color.DarkOrange,
//                IsValueShownAsLabel = true,
//                LabelFormat = "{0:P1}",
//                BorderWidth = 1,
//                BorderColor = Color.Black
//            };

//            chartChargebackRate.Series.Add(series);

//            string sql = @"
//WITH MonthlyTransactions AS (
//    SELECT 
//        YEAR(Date) AS Year,
//        MONTH(Date) AS MonthNum,
//        DATENAME(MONTH, Date) AS MonthName,
//        COUNT(*) AS TotalTransactions,
//        SUM(CASE WHEN Status = 'Disputed' OR Status = 'Chargeback' THEN 1 ELSE 0 END) AS ChargebackCount
//    FROM PAYMENT
//    WHERE Date BETWEEN @StartDate AND @EndDate
//    GROUP BY YEAR(Date), MONTH(Date), DATENAME(MONTH, Date)
//)
//SELECT 
//    Year,
//    MonthNum,
//    MonthName,
//    TotalTransactions,
//    ChargebackCount,
//    CAST(CASE WHEN TotalTransactions > 0 THEN ChargebackCount * 1.0 / TotalTransactions ELSE 0 END AS DECIMAL(5,4)) AS ChargebackRate
//FROM MonthlyTransactions
//ORDER BY Year, MonthNum";

//            dtChargeback = new DataTable();
//            using (var adapter = new SqlDataAdapter(sql, con))
//            {
//                adapter.SelectCommand.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
//                adapter.SelectCommand.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
//                adapter.Fill(dtChargeback);
//            }

//            if (dtChargeback.Rows.Count > 0)
//            {
//                double highestRate = 0;

//                foreach (DataRow row in dtChargeback.Rows)
//                {
//                    string month = row["MonthName"].ToString();
//                    int year = Convert.ToInt32(row["Year"]);
//                    string label = $"{month}\n{year}";
//                    double rate = Convert.ToDouble(row["ChargebackRate"]);

//                    if (rate > highestRate) highestRate = rate;

//                    // Add data point with custom tooltip
//                    int index = series.Points.AddXY(label, rate);
//                    int total = Convert.ToInt32(row["TotalTransactions"]);
//                    int chargebacks = Convert.ToInt32(row["ChargebackCount"]);
//                    series.Points[index].ToolTip = $"{month} {year}: {rate:P2}\n({chargebacks:N0} of {total:N0} transactions)";
//                }

//                // Set y-axis maximum to make the chart more readable
//                // Set max to either 10% or slightly above the highest value
//                double axisMax = Math.Max(0.1, Math.Ceiling(highestRate * 10) / 10);
//                chartChargebackRate.ChartAreas[0].AxisY.Maximum = axisMax;

//                // Add reference line for industry average (example: 0.9%)
//                var avgLine = chartChargebackRate.ChartAreas[0].AxisY.StripLines.Add(0.009);
//                avgLine.Text = "Industry Avg (0.9%)";
//                avgLine.BorderColor = Color.Red;
//                avgLine.BorderWidth = 2;
//                avgLine.BorderDashStyle = ChartDashStyle.Dash;
//                avgLine.IntervalOffset = 0.009;
//                avgLine.Font = new Font("Arial", 8);
//                avgLine.TextAlignment = StringAlignment.Far;
//            }
//            else
//            {
//                // No data
//                AddNoDataLabel(chartChargebackRate);
//            }
//        }

//        private void AddNoDataLabel(Chart chart)
//        {
//            chart.Titles.Add(new Title("No data available for selected date range")
//            {
//                Docking = Docking.Top,
//                Font = new Font("Arial", 10, FontStyle.Italic),
//                ForeColor = Color.Gray
//            });
//        }

//        private void ExportCsvFiles()
//        {
//            using (var dlg = new FolderBrowserDialog { Description = "Select folder to save CSV reports" })
//            {
//                if (dlg.ShowDialog() != DialogResult.OK) return;

//                string folder = dlg.SelectedPath;
//                try
//                {
//                    // Create timestamp for filenames
//                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
//                    string dateRange = $"{dtpStartDate.Value:yyyyMMdd}to{dtpEndDate.Value:yyyyMMdd}";

//                    // Export both reports
//                    WriteDataTableToCsv(dtSuccessFailure,
//                        Path.Combine(folder, $"PaymentSuccessFailureRate_{dateRange}_{timestamp}.csv"));

//                    WriteDataTableToCsv(dtChargeback,
//                        Path.Combine(folder, $"MonthlyChargebackRate_{dateRange}_{timestamp}.csv"));

//                    MessageBox.Show("CSV files exported successfully.", "Export Complete",
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Error exporting CSV: " + ex.Message,
//                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
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

//                        // Handle null values and ensure proper CSV escaping
//                        if (row[i] == DBNull.Value)
//                        {
//                            sw.Write("");
//                        }
//                        else
//                        {
//                            string val = row[i].ToString();
//                            // Escape quotes by doubling them and enclose in quotes
//                            val = $"\"{val.Replace("\"", "\"\"")}\"";
//                            sw.Write(val);
//                        }
//                    }
//                    sw.WriteLine();
//                }
//            }
//        }
//    }
//}