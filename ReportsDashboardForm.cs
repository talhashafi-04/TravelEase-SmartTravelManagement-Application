using System;
using System.Drawing;
using System.Windows.Forms;
using TripBookingReportApp;
using TravelAnalyticsApp;

namespace DatabaseProject
{
    public class ReportsDashboardForm : Form
    {
        private Button btnDestinationPopularity;
        private Button btnAbandonedBooking;
        private Button btnPlatformGrowth;
        private Button btnPaymentFraud;
        private Button btnTravelerDemographics;
        private Button btnTripBookingRevenue;

        public ReportsDashboardForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Reports Dashboard";
            this.ClientSize = new Size(400, 470); // Increased height to accommodate new buttons
            this.StartPosition = FormStartPosition.CenterParent;

            // Destination Popularity Report
            btnDestinationPopularity = new Button
            {
                Text = "Destination Popularity",
                Location = new Point(50, 50),
                Size = new Size(300, 40)
            };
            btnDestinationPopularity.Click += (s, e) =>
            {
                using (var form = new DestinationPopularityReportForm())
                    form.ShowDialog(this);
            };

            // Abandoned Booking Analysis Report
            btnAbandonedBooking = new Button
            {
                Text = "Abandoned Booking Analysis",
                Location = new Point(50, 110),
                Size = new Size(300, 40)
            };
            btnAbandonedBooking.Click += (s, e) =>
            {
                //using (var form = new AbandonedBookingReportForm())
                   // form.ShowDialog(this);
            };

            // Platform Growth Report
            btnPlatformGrowth = new Button
            {
                Text = "Platform Growth Report",
                Location = new Point(50, 170),
                Size = new Size(300, 40)
            };
            btnPlatformGrowth.Click += (s, e) =>
            {
                using (var form = new PlatformGrowthReportForm())
                    form.ShowDialog(this);
            };

            // Payment & Fraud Report
            btnPaymentFraud = new Button
            {
                Text = "Payment & Fraud Report",
                Location = new Point(50, 230),
                Size = new Size(300, 40)
            };
            btnPaymentFraud.Click += (s, e) =>
            {
                //using (var form = new PaymentFraudReportForm())
                  //  form.ShowDialog(this);
            };

            // Traveler Demographics and Preferences Report
            btnTravelerDemographics = new Button
            {
                Text = "Traveler Demographics and Preferences",
                Location = new Point(50, 290),
                Size = new Size(300, 40)
            };
            btnTravelerDemographics.Click += (s, e) =>
            {
                using (var form = new TravelerDemographicsForm())
                    form.ShowDialog(this);
            };

            // Trip Booking and Revenue Report
            btnTripBookingRevenue = new Button
            {
                Text = "Trip Booking and Revenue",
                Location = new Point(50, 350),
                Size = new Size(300, 40)
            };
            btnTripBookingRevenue.Click += (s, e) =>
            {
                using (var form = new TripBookingReportForm())
                    form.ShowDialog(this);
            };

            // Add controls to the form
            this.Controls.AddRange(new Control[]
            {
                btnDestinationPopularity,
                btnAbandonedBooking,
                btnPlatformGrowth,
                btnPaymentFraud,
                btnTravelerDemographics,
                btnTripBookingRevenue
            });
        }
    }
}