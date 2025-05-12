using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Data.SqlClient;

namespace TravelApplication
{
    public partial class TourOperatorPerformanceReportForm : Form
    {
        private Chart chartAvgRating;
        private Chart chartRevenue;
        private Button btnGenerate;
        private Button btnExportCsv;

        private static string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        private DataTable dtRatings;
        private DataTable dtRevenue;
        private string operatorID;

        public TourOperatorPerformanceReportForm(string ID)
        {
            operatorID = ID;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tour Operator Performance Report";
            this.ClientSize = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnGenerate = new Button
            {
                Text = "Generate Report",
                Location = new Point(20, 20),
                Size = new Size(150, 30)
            };
            btnGenerate.Click += (s, e) => LoadReportData();

            btnExportCsv = new Button
            {
                Text = "Export to CSV",
                Location = new Point(200, 20),
                Size = new Size(150, 30),
                Enabled = false
            };
            btnExportCsv.Click += (s, e) => ExportCsvFiles();

            chartAvgRating = CreateChart("Average Operator Rating", new Point(20, 70));
            chartRevenue = CreateChart("Revenue per Operator", new Point(520, 70));

            this.Controls.AddRange(new Control[]
            {
                btnGenerate,
                btnExportCsv,
                chartAvgRating,
                chartRevenue
            });
        }

        private Chart CreateChart(string title, Point location)
        {
            var chart = new Chart
            {
                Location = location,
                Size = new Size(450, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            var chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            chart.Titles.Add(new Title
            {
                Text = title,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Blue
            });

            chartArea.AxisX.LabelStyle.Font = new Font("Arial", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Arial", 8);
            chartArea.AxisY.Minimum = 0;

            return chart;
        }

        private void LoadReportData()
        {
            try
            {
                Application.UseWaitCursor = true;
                this.Cursor = Cursors.WaitCursor;

                LoadAvgRatingChart(operatorID);
                LoadRevenueChart(operatorID);

                btnExportCsv.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report data: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Application.UseWaitCursor = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void LoadAvgRatingChart(string operatorID)
        {
            chartAvgRating.Series.Clear();
            var series = new Series("AvgRating")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue,
                IsValueShownAsLabel = true,
                LabelFormat = "0.0",
                Font = new Font("Arial", 8, FontStyle.Bold)
            };
            chartAvgRating.Series.Add(series);

            string sql = @"
                SELECT O.OperatorID, O.AgencyName,
                       AVG(R.Rating) AS AvgRating
                FROM REVIEW R
                JOIN Trip_REVIEW TR ON R.ReviewID = TR.ReviewID
                JOIN TRIP T ON TR.TripID = T.TripID
                JOIN TOUR_OPERATOR O ON T.OperatorID = O.OperatorID
                WHERE O.OperatorID = @operatorID
                GROUP BY O.OperatorID, O.AgencyName
                ORDER BY AvgRating DESC";

            dtRatings = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@operatorID", operatorID);
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtRatings);
                }
            }

            if (dtRatings.Rows.Count == 0)
            {
                MessageBox.Show("No rating data found for this operator.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataRow row in dtRatings.Rows)
            {
                double rating = Convert.ToDouble(row["AvgRating"]);
                string agencyName = row["AgencyName"].ToString();

                int index = series.Points.AddY(rating);
                var point = series.Points[index];
                point.AxisLabel = agencyName;
                point.ToolTip = $"{agencyName}: {rating:0.0}";
            }

            chartAvgRating.ChartAreas[0].AxisY.Maximum = 5;
            chartAvgRating.ChartAreas[0].AxisY.Interval = 1;
        }

        private void LoadRevenueChart(string operatorID)
        {
            chartRevenue.Series.Clear();
            var series = new Series("Revenue")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.Green,
                IsValueShownAsLabel = true,
                LabelFormat = "C0",
                Font = new Font("Arial", 8, FontStyle.Bold)
            };
            chartRevenue.Series.Add(series);

            string sql = @"
                SELECT O.OperatorID, O.AgencyName,
                       SUM(P.Amount) AS Revenue
                FROM PAYMENT P
                JOIN BOOKING B ON P.BookingID = B.BookingID
                JOIN TRIP T ON B.TripID = T.TripID
                JOIN TOUR_OPERATOR O ON T.OperatorID = O.OperatorID
                WHERE O.OperatorID = @operatorID
                GROUP BY O.OperatorID, O.AgencyName
                ORDER BY Revenue DESC";

            dtRevenue = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@operatorID", operatorID);
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtRevenue);
                }
            }

            if (dtRevenue.Rows.Count == 0)
            {
                MessageBox.Show("No revenue data found for this operator.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double maxRevenue = 0;
            foreach (DataRow row in dtRevenue.Rows)
            {
                double revenue = Convert.ToDouble(row["Revenue"]);
                if (revenue > maxRevenue) maxRevenue = revenue;
            }

            foreach (DataRow row in dtRevenue.Rows)
            {
                double revenue = Convert.ToDouble(row["Revenue"]);
                string agencyName = row["AgencyName"].ToString();

                int index = series.Points.AddY(revenue);
                var point = series.Points[index];
                point.AxisLabel = agencyName;
                point.ToolTip = $"{agencyName}: {revenue:C0}";
            }

            chartRevenue.ChartAreas[0].AxisY.Maximum = Math.Ceiling(maxRevenue * 1.1);
        }

        private void ExportCsvFiles()
        {
            using (var dlg = new FolderBrowserDialog
            {
                Description = "Select folder to save CSV reports",
                ShowNewFolderButton = true
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string folder = dlg.SelectedPath;
                try
                {
                    Application.UseWaitCursor = true;
                    this.Cursor = Cursors.WaitCursor;

                    if (dtRatings != null && dtRatings.Rows.Count > 0)
                    {
                        string ratingsPath = Path.Combine(folder, $"OperatorRatings_{DateTime.Now:yyyyMMddHHmmss}.csv");
                        WriteDataTableToCsv(dtRatings, ratingsPath);
                    }

                    if (dtRevenue != null && dtRevenue.Rows.Count > 0)
                    {
                        string revenuePath = Path.Combine(folder, $"OperatorRevenue_{DateTime.Now:yyyyMMddHHmmss}.csv");
                        WriteDataTableToCsv(dtRevenue, revenuePath);
                    }

                    MessageBox.Show("CSV files exported successfully!", "Export Complete",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting CSV files: {ex.Message}", "Export Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Application.UseWaitCursor = false;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void WriteDataTableToCsv(DataTable table, string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i > 0) sw.Write(",");
                    sw.Write(table.Columns[i].ColumnName);
                }
                sw.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i > 0) sw.Write(",");
                        string val = row[i]?.ToString().Replace(',', ';');
                        sw.Write(val);
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}