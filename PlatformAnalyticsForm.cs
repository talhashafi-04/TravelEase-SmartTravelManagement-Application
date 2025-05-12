using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace DatabaseProject
{
    public partial class PlatformAnalyticsForm : Form
    {
        private Chart chartUsers;
        private Chart chartTrips;
        private Chart chartBookings;
        private Chart chartRevenue;
        private Button btnUsersReport, btnTripsReport, btnBookingsReport, btnRevenueReport;
        SqlConnection con = new SqlConnection(
    @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");


        public PlatformAnalyticsForm()
        {
            InitializeComponents();
            LoadAnalytics();
        }

        private void InitializeComponents()
        {
            this.Text = "Platform Analytics";
            this.ClientSize = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterParent;

            // Charts
            chartUsers = new Chart { Location = new Point(20, 20), Size = new Size(450, 300) };
            var areaU = new ChartArea("Users"); chartUsers.ChartAreas.Add(areaU);
            chartUsers.Series.Add(new Series("NewUsers") { ChartType = SeriesChartType.Column });
            chartUsers.Titles.Add("New User Registrations");

            chartTrips = new Chart { Location = new Point(520, 20), Size = new Size(450, 300) };
            var areaT = new ChartArea("Trips"); chartTrips.ChartAreas.Add(areaT);
            chartTrips.Series.Add(new Series("Trips") { ChartType = SeriesChartType.Column });
            chartTrips.Titles.Add("New Trips Created");

            chartBookings = new Chart { Location = new Point(20, 350), Size = new Size(450, 300) };
            var areaB = new ChartArea("Bookings"); chartBookings.ChartAreas.Add(areaB);
            chartBookings.Series.Add(new Series("Bookings") { ChartType = SeriesChartType.Line });
            chartBookings.Titles.Add("Booking Volume");

            chartRevenue = new Chart { Location = new Point(520, 350), Size = new Size(450, 300) };
            var areaR = new ChartArea("Revenue"); chartRevenue.ChartAreas.Add(areaR);
            chartRevenue.Series.Add(new Series("Revenue") { ChartType = SeriesChartType.Line });
            chartRevenue.Titles.Add("Revenue Trend");

            // Report buttons
            btnUsersReport = new Button { Text = "Detailed Users Report", Location = new Point(20, 670), Size = new Size(200, 30) };
            btnTripsReport = new Button { Text = "Detailed Trips Report", Location = new Point(240, 670), Size = new Size(200, 30) };
            btnBookingsReport = new Button { Text = "Detailed Bookings Report", Location = new Point(460, 670), Size = new Size(200, 30) };
            btnRevenueReport = new Button { Text = "Detailed Revenue Report", Location = new Point(680, 670), Size = new Size(200, 30) };

            this.Controls.AddRange(new Control[]
            {
                chartUsers, chartTrips, chartBookings, chartRevenue,
                btnUsersReport, btnTripsReport, btnBookingsReport, btnRevenueReport
            });
        }

        private void LoadAnalytics()
        {
            DataTable dtUsers = new DataTable();
            DataTable dtTrips = new DataTable();
            DataTable dtBookings = new DataTable();
            DataTable dtRevenue = new DataTable();

            try
            {
                con.Open();

                // 1) New User Registrations by Date
                using (var cmd = new SqlCommand(@"
            SELECT 
              CAST(RegistrationDate AS DATE) AS [Date],
              COUNT(*)                        AS [Count]
            FROM [USER]
            GROUP BY CAST(RegistrationDate AS DATE)
            ORDER BY [Date]", con))
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dtUsers);

                // 2) New Trips Created by Date
                using (var cmd = new SqlCommand(@"
            SELECT 
              CAST(CreationDate AS DATE) AS [Date],
              COUNT(*)                    AS [Count]
            FROM TRIP
            GROUP BY CAST(CreationDate AS DATE)
            ORDER BY [Date]", con))
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dtTrips);

                // 3) Booking Volume by Date
                using (var cmd = new SqlCommand(@"
            SELECT 
              CAST(Date AS DATE) AS [Date],
              COUNT(*)           AS [Count]
            FROM BOOKING
            GROUP BY CAST(Date AS DATE)
            ORDER BY [Date]", con))
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dtBookings);

                // 4) Revenue Trend by Date
                using (var cmd = new SqlCommand(@"
            SELECT 
              CAST(p.Date AS DATE)   AS [Date],
              SUM(p.Amount)          AS [Count]
            FROM PAYMENT p
            JOIN BOOKING b ON p.BookingID = b.BookingID
            GROUP BY CAST(p.Date AS DATE)
            ORDER BY [Date]", con))
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dtRevenue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading analytics: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

            // Bind each DataTable to its chart series
            chartUsers.Series["NewUsers"].Points.DataBindXY(dtUsers.DefaultView, "Date", dtUsers.DefaultView, "Count");
            chartTrips.Series["Trips"].Points.DataBindXY(dtTrips.DefaultView, "Date", dtTrips.DefaultView, "Count");
            chartBookings.Series["Bookings"].Points.DataBindXY(dtBookings.DefaultView, "Date", dtBookings.DefaultView, "Count");
            chartRevenue.Series["Revenue"].Points.DataBindXY(dtRevenue.DefaultView, "Date", dtRevenue.DefaultView, "Count");
        }
    }
}
