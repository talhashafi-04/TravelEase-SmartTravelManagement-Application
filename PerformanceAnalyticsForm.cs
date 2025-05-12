//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace DatabaseProject
//{
//    public partial class PerformanceAnalyticsForm : Form
//    {
//        private Chart chartRevenue;
//        private Chart chartBookings;
//        private Chart chartRatings;
//        private Button btnRevenueDetails;
//        private Button btnBookingDetails;
//        private Button btnRatingDetails;

//        public PerformanceAnalyticsForm()
//        {
//            InitializeComponent();
//            LoadAnalytics();
//        }

//        private void InitializeComponent()
//        {
//            this.Text = "Performance Analytics";
//            this.ClientSize = new Size(1000, 700);
//            this.StartPosition = FormStartPosition.CenterParent;

//            // Revenue trend chart
//            chartRevenue = new Chart { Location = new Point(20, 20), Size = new Size(450, 300) };
//            chartRevenue.ChartAreas.Add(new ChartArea("RevenueArea"));
//            chartRevenue.Series.Add(new Series("Revenue")
//            {
//                ChartType = SeriesChartType.Line,
//                XValueType = ChartValueType.Date
//            });
//            chartRevenue.Titles.Add("Revenue Trends");

//            // Booking pattern chart
//            chartBookings = new Chart { Location = new Point(520, 20), Size = new Size(450, 300) };
//            chartBookings.ChartAreas.Add(new ChartArea("BookingArea"));
//            chartBookings.Series.Add(new Series("Bookings")
//            {
//                ChartType = SeriesChartType.Column,
//                XValueType = ChartValueType.Date
//            });
//            chartBookings.Titles.Add("Booking Patterns");

//            // Rating chart
//            chartRatings = new Chart { Location = new Point(20, 350), Size = new Size(450, 300) };
//            chartRatings.ChartAreas.Add(new ChartArea("RatingArea"));
//            chartRatings.Series.Add(new Series("Ratings")
//            {
//                ChartType = SeriesChartType.Bar,
//                XValueType = ChartValueType.String
//            });
//            chartRatings.Titles.Add("Average Ratings");

//            // Detail buttons
//            btnRevenueDetails = new Button { Text = "Revenue Report", Location = new Point(520, 340), Size = new Size(150, 30) };
//            btnBookingDetails = new Button { Text = "Booking Report", Location = new Point(520, 380), Size = new Size(150, 30) };
//            btnRatingDetails = new Button { Text = "Rating Report", Location = new Point(520, 420), Size = new Size(150, 30) };

//            this.Controls.AddRange(new Control[]
//            {
//                chartRevenue, chartBookings, chartRatings,
//                btnRevenueDetails, btnBookingDetails, btnRatingDetails
//            });

//            btnRevenueDetails.Click += (s, e) => OpenDetailedReport("Revenue");
//            btnBookingDetails.Click += (s, e) => OpenDetailedReport("Booking");
//            btnRatingDetails.Click += (s, e) => OpenDetailedReport("Rating");
//        }

//        private void LoadAnalytics()
//        {
//            // TODO: Fetch and bind data for revenue trends
//            // Example:
//            // var revData = AnalyticsRepository.GetRevenueTrend(currentOperatorId);
//            // chartRevenue.Series["Revenue"].Points.DataBindXY(revData.Dates, revData.Amounts);

//            // TODO: Fetch and bind data for booking patterns
//            // var bookData = AnalyticsRepository.GetBookingPattern(currentOperatorId);
//            // chartBookings.Series["Bookings"].Points.DataBindXY(bookData.Dates, bookData.Counts);

//            // TODO: Fetch and bind data for average ratings
//            // var rateData = AnalyticsRepository.GetRatingsSummary(currentOperatorId);
//            // chartRatings.Series["Ratings"].Points.DataBindXY(rateData.Categories, rateData.Averages);
//        }

//        private void OpenDetailedReport(string reportType)
//        {
//            // TODO: Open specific DetailedReportForm, passing reportType if needed
//            MessageBox.Show($"Open detailed {reportType} report here.");
//        }
//    }
//}
