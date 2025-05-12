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
        SqlConnection con = new SqlConnection(@"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;");

        public DestinationPopularityReportForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Destination Popularity Report";
            this.ClientSize = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnGenerateReport = new Button
            {
                Text = "Generate Report",
                Location = new Point(20, 20),
                Size = new Size(150, 30)
            };
            btnGenerateReport.Click += (s, e) => LoadReportData();

            chartBookings = CreateChart("Most Booked Destinations", new Point(20, 70));
            chartSeasonal = CreateChart("Seasonal Trends", new Point(500, 70));
            chartRatings = CreateChart("Satisfaction Scores", new Point(20, 400));
            chartEmerging = CreateChart("Emerging Destinations", new Point(500, 400));

            this.Controls.AddRange(new Control[] { btnGenerateReport, chartBookings, chartSeasonal, chartRatings, chartEmerging });
        }

        private Chart CreateChart(string title, Point location)
        {
            Chart chart = new Chart { Location = location, Size = new Size(450, 300) };
            chart.ChartAreas.Add(new ChartArea());
            chart.Titles.Add(title);
            return chart;
        }

        private void LoadReportData()
        {
            LoadMostBookedChart();
            LoadSeasonalTrendsChart();
            LoadSatisfactionChart();
            LoadEmergingChart();
        }

        private void LoadMostBookedChart()
        {
            chartBookings.Series.Clear();
            chartBookings.Series.Add("Bookings");
            chartBookings.Series["Bookings"].ChartType = SeriesChartType.Column;

            string query = @"SELECT D.City, COUNT(*) AS BookingCount
                             FROM Booking B
                             JOIN Trip T ON B.TripID = T.TripID
                             JOIN Destination D ON T.DestinationID = D.DestinationID
                             GROUP BY D.City
                             ORDER BY BookingCount DESC";
            FillChart(chartBookings.Series["Bookings"], query, "City", "BookingCount");
        }

        private void LoadSeasonalTrendsChart()
        {
            chartSeasonal.Series.Clear();
            chartSeasonal.Series.Add("Seasonal");
            chartSeasonal.Series["Seasonal"].ChartType = SeriesChartType.Line;

            string query = @"SELECT DATENAME(MONTH, BookingDate) AS MonthName, COUNT(*) AS Bookings
                             FROM Booking
                             GROUP BY DATENAME(MONTH, BookingDate), MONTH(BookingDate)
                             ORDER BY MONTH(BookingDate)";
            FillChart(chartSeasonal.Series["Seasonal"], query, "MonthName", "Bookings");
        }

        private void LoadSatisfactionChart()
        {
            chartRatings.Series.Clear();
            chartRatings.Series.Add("Ratings");
            chartRatings.Series["Ratings"].ChartType = SeriesChartType.Column;

            string query = @"SELECT D.City, AVG(R.Rating) AS AvgRating
                             FROM Review R
                             JOIN Booking B ON R.BookingID = B.BookingID
                             JOIN Trip T ON B.TripID = T.TripID
                             JOIN Destination D ON T.DestinationID = D.DestinationID
                             GROUP BY D.City
                             ORDER BY AvgRating DESC";
            FillChart(chartRatings.Series["Ratings"], query, "City", "AvgRating");
        }

        private void LoadEmergingChart()
        {
            chartEmerging.Series.Clear();
            chartEmerging.Series.Add("Emerging");
            chartEmerging.Series["Emerging"].ChartType = SeriesChartType.Column;

            string query = @"SELECT D.City, 
                            COUNT(CASE WHEN MONTH(B.BookingDate) = MONTH(GETDATE()) THEN 1 END) AS CurrentMonth,
                            COUNT(CASE WHEN MONTH(B.BookingDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) THEN 1 END) AS LastMonth
                            FROM Booking B
                            JOIN Trip T ON B.TripID = T.TripID
                            JOIN Destination D ON T.DestinationID = D.DestinationID
                            GROUP BY D.City
                            HAVING COUNT(CASE WHEN MONTH(B.BookingDate) = MONTH(GETDATE()) THEN 1 END) >
                                   COUNT(CASE WHEN MONTH(B.BookingDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) THEN 1 END)";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                string city = row["City"].ToString();
                int growth = Convert.ToInt32(row["CurrentMonth"]) - Convert.ToInt32(row["LastMonth"]);
                chartEmerging.Series["Emerging"].Points.AddXY(city, growth);
            }
        }

        private void FillChart(Series series, string query, string xField, string yField)
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
