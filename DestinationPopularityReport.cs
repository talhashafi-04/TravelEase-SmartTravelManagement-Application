using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DatabaseProject
{
    public partial class DestinationPopularityReportForm : Form
    {
        private Chart chartBookings, chartSeasonal, chartRatings, chartEmerging;
        private Button btnGenerateReport;
        private Label lblStatus;
        private ComboBox cmbTimeFrame;

        // Update with your actual connection string
        SqlConnection con = new SqlConnection(@"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;");

        public DestinationPopularityReportForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Destination Popularity Report";
            this.ClientSize = new Size(1050, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 9F);

            // Time frame selection
            Label lblTimeFrame = new Label
            {
                Text = "Time Frame:",
                Location = new Point(20, 20),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            cmbTimeFrame = new ComboBox
            {
                Location = new Point(100, 20),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTimeFrame.Items.AddRange(new object[] { "Last 6 Months", "Last Year", "All Time" });
            cmbTimeFrame.SelectedIndex = 2; // Default to All Time

            btnGenerateReport = new Button
            {
                Text = "Generate Report",
                Location = new Point(270, 20),
                Size = new Size(150, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Click += BtnGenerateReport_Click;

            lblStatus = new Label
            {
                Text = "Ready to generate report",
                Location = new Point(440, 20),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DarkGray
            };

            // Create charts
            chartBookings = CreateChart("Most Booked Destinations", new Point(20, 70), "Destinations", "Number of Bookings");
            chartSeasonal = CreateChart("Seasonal Booking Trends", new Point(535, 70), "Month", "Number of Bookings");
            chartRatings = CreateChart("Traveler Satisfaction Scores", new Point(20, 430), "Destinations", "Average Rating");
            chartEmerging = CreateChart("Emerging Destinations (Growth Rate)", new Point(535, 430), "Destinations", "Booking Growth");

            // Add all controls to the form
            this.Controls.AddRange(new Control[] {
                lblTimeFrame, cmbTimeFrame, btnGenerateReport, lblStatus,
                chartBookings, chartSeasonal, chartRatings, chartEmerging
            });
        }

        private Chart CreateChart(string title, Point location, string xAxisTitle, string yAxisTitle)
        {
            Chart chart = new Chart
            {
                Location = location,
                Size = new Size(500, 330),
                BackColor = Color.White,
                BorderlineColor = Color.LightGray,
                BorderlineDashStyle = ChartDashStyle.Solid,
                BorderlineWidth = 1
            };

            // Add chart area
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Title = xAxisTitle;
            chartArea.AxisY.Title = yAxisTitle;
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 8F);
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 8F);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.BackColor = Color.White;
            chartArea.BorderColor = Color.LightGray;
            chartArea.BorderWidth = 1;
            chart.ChartAreas.Add(chartArea);

            // Add title
            Title chartTitle = new Title();
            chartTitle.Text = title;
            chartTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chartTitle.ForeColor = Color.FromArgb(0, 99, 177);
            chart.Titles.Add(chartTitle);

            // Add legend
            Legend legend = new Legend();
            legend.Font = new Font("Segoe UI", 8F);
            chart.Legends.Add(legend);

            return chart;
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Generating report...";
                lblStatus.ForeColor = Color.DarkBlue;

                // Test the connection before proceeding
                con.Open();
                con.Close();

                LoadReportData();

                lblStatus.Text = "Report generated successfully!";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Error generating report: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void LoadReportData()
        {
            string timeFilter = GetTimeFilter();
            LoadMostBookedChart(timeFilter);
            LoadSeasonalTrendsChart(timeFilter);
            LoadSatisfactionChart(timeFilter);
            LoadEmergingChart(timeFilter);
        }

        private string GetTimeFilter()
        {
            switch (cmbTimeFrame.SelectedIndex)
            {
                case 0: // Last 6 Months
                    return " AND B.Date >= DATEADD(MONTH, -6, GETDATE())";
                case 1: // Last Year
                    return " AND B.Date >= DATEADD(YEAR, -1, GETDATE())";
                default: // All Time
                    return "";
            }
        }

        private void LoadMostBookedChart(string timeFilter)
        {
            chartBookings.Series.Clear();
            Series series = new Series("Bookings");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(65, 140, 240);
            chartBookings.Series.Add(series);

            string query = @"SELECT TOP 10 D.Name, COUNT(*) AS BookingCount
                             FROM BOOKING B
                             JOIN TRIP T ON B.TripID = T.TripID
                             JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                             WHERE 1=1" + timeFilter + @"
                             GROUP BY D.Name
                             ORDER BY BookingCount DESC";

            FillColumnChart(chartBookings.Series["Bookings"], query, "Name", "BookingCount");

            // Format the chart
            chartBookings.ChartAreas[0].AxisX.Interval = 1;
            chartBookings.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartBookings.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartBookings.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Enable data points values
            foreach (DataPoint point in chartBookings.Series["Bookings"].Points)
            {
                point.IsValueShownAsLabel = true;
                point.LabelFormat = "0";
            }
        }

        private void LoadSeasonalTrendsChart(string timeFilter)
        {
            chartSeasonal.Series.Clear();
            Series series = new Series("Monthly Trend");
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.FromArgb(192, 80, 77);
            series.BorderWidth = 3;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;
            chartSeasonal.Series.Add(series);

            // Adjusted query to handle the month name ordering properly
            string query = @"SELECT
                                DATENAME(MONTH, B.Date) AS MonthName,
                                MONTH(B.Date) AS MonthNumber,
                                COUNT(*) AS Bookings
                             FROM BOOKING B
                             WHERE 1=1" + timeFilter + @"
                             GROUP BY DATENAME(MONTH, B.Date), MONTH(B.Date)
                             ORDER BY MonthNumber";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                int pointIndex = series.Points.AddXY(row["MonthName"].ToString(), Convert.ToDouble(row["Bookings"]));
                series.Points[pointIndex].Label = row["Bookings"].ToString();
            }

            // Format the chart
            chartSeasonal.ChartAreas[0].AxisX.Interval = 1;
            chartSeasonal.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartSeasonal.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void LoadSatisfactionChart(string timeFilter)
        {
            chartRatings.Series.Clear();
            Series series = new Series("Average Rating");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(112, 173, 71);
            chartRatings.Series.Add(series);

            // Use modified query with Trip_REVIEW table based on your SQL schema
            string query = @"SELECT TOP 10 D.Name, AVG(R.Rating) AS AvgRating
                             FROM REVIEW R
                             JOIN Trip_REVIEW TR ON R.ReviewID = TR.ReviewID
                             JOIN TRIP T ON TR.TripID = T.TripID
                             JOIN BOOKING B ON B.TripID = T.TripID
                             JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                             WHERE 1=1" + timeFilter + @"
                             GROUP BY D.Name
                             ORDER BY AvgRating DESC";

            FillColumnChart(chartRatings.Series["Average Rating"], query, "Name", "AvgRating");

            // Format the chart
            chartRatings.ChartAreas[0].AxisX.Interval = 1;
            chartRatings.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartRatings.ChartAreas[0].AxisY.Maximum = 5;
            chartRatings.ChartAreas[0].AxisY.Minimum = 0;
            chartRatings.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartRatings.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            // Enable data points values
            foreach (DataPoint point in chartRatings.Series["Average Rating"].Points)
            {
                point.IsValueShownAsLabel = true;
                point.LabelFormat = "0.00";
            }
        }

        private void LoadEmergingChart(string timeFilter)
        {
            chartEmerging.Series.Clear();
            Series series = new Series("Growth Rate");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(255, 192, 0);
            chartEmerging.Series.Add(series);

            // Query for emerging destinations - comparing current month vs previous month
            string query = @"SELECT TOP 10 D.Name, 
                            COUNT(CASE WHEN B.Date >= DATEADD(MONTH, -1, GETDATE()) THEN 1 END) AS CurrentPeriod,
                            COUNT(CASE WHEN B.Date < DATEADD(MONTH, -1, GETDATE()) AND B.Date >= DATEADD(MONTH, -2, GETDATE()) THEN 1 END) AS PreviousPeriod
                            FROM BOOKING B
                            JOIN TRIP T ON B.TripID = T.TripID
                            JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                            WHERE B.Date >= DATEADD(MONTH, -2, GETDATE())" + @"
                            GROUP BY D.Name
                            HAVING 
                                COUNT(CASE WHEN B.Date >= DATEADD(MONTH, -1, GETDATE()) THEN 1 END) > 0
                                AND
                                COUNT(CASE WHEN B.Date < DATEADD(MONTH, -1, GETDATE()) AND B.Date >= DATEADD(MONTH, -2, GETDATE()) THEN 1 END) > 0
                            ORDER BY (
                                (CAST(COUNT(CASE WHEN B.Date >= DATEADD(MONTH, -1, GETDATE()) THEN 1 END) AS FLOAT) / 
                                NULLIF(CAST(COUNT(CASE WHEN B.Date < DATEADD(MONTH, -1, GETDATE()) AND B.Date >= DATEADD(MONTH, -2, GETDATE()) THEN 1 END) AS FLOAT), 0)) - 1
                            ) DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                string name = row["Name"].ToString();
                double currentPeriod = Convert.ToDouble(row["CurrentPeriod"]);
                double previousPeriod = Convert.ToDouble(row["PreviousPeriod"]);

                // Calculate growth percentage
                double growth = previousPeriod > 0 ? ((currentPeriod / previousPeriod) - 1) * 100 : 0;

                int pointIndex = series.Points.AddXY(name, growth);
                series.Points[pointIndex].Label = growth.ToString("0.0") + "%";

                // Color-code based on growth rate
                if (growth > 50)
                    series.Points[pointIndex].Color = Color.FromArgb(112, 173, 71); // Strong growth
                else if (growth > 20)
                    series.Points[pointIndex].Color = Color.FromArgb(255, 192, 0); // Moderate growth
                else
                    series.Points[pointIndex].Color = Color.FromArgb(192, 80, 77); // Low growth
            }

            // Format the chart
            chartEmerging.ChartAreas[0].AxisX.Interval = 1;
            chartEmerging.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartEmerging.ChartAreas[0].AxisY.Title = "Growth Rate (%)";
            chartEmerging.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartEmerging.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void FillColumnChart(Series series, string query, string xField, string yField)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(row[xField].ToString(), Convert.ToDouble(row[yField]));
            }
        }

      
    }
}