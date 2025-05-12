using System;
using System.Drawing;
using System.Windows.Forms;
using Service_Provider_Section;

namespace DatabaseProject
{
    public partial class PerformanceReportsForm : Form
    {
        private string _serviceProviderId;

        // Main buttons for report types
        private Button btnHotelOccupancy;
        private Button btnGuideRatings;
        private Button btnTransportPerformance;
        private Button btnServiceUtilization;
        private Button btnBack;

        private string serviceProviderId;


        public PerformanceReportsForm(string serviceProviderId)
        {
            this._serviceProviderId = serviceProviderId; // ✅ correct usage
            InitializeComponents();
        }


        private void InitializeComponents()
        {
            this.Text = "Performance Reports";
            this.ClientSize = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title label
            Label lblTitle = new Label
            {
                Text = "Performance Reports",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Description label
            Label lblDescription = new Label
            {
                Text = "Select a report type to view performance metrics",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 60)
            };

            // Report type buttons
            btnHotelOccupancy = new Button
            {
                Text = "Hotel Occupancy Rate",
                Size = new Size(250, 50),
                Location = new Point(50, 100),
                Font = new Font("Segoe UI", 10)
            };
            btnHotelOccupancy.Click += BtnHotelOccupancy_Click;

            btnGuideRatings = new Button
            {
                Text = "Guide Ratings",
                Size = new Size(250, 50),
                Location = new Point(300, 100),
                Font = new Font("Segoe UI", 10)
            };
            btnGuideRatings.Click += BtnGuideRatings_Click;

            btnTransportPerformance = new Button
            {
                Text = "Transport On-Time Performance",
                Size = new Size(250, 50),
                Location = new Point(50, 170),
                Font = new Font("Segoe UI", 10)
            };
            btnTransportPerformance.Click += BtnTransportPerformance_Click;

            btnServiceUtilization = new Button
            {
                Text = "Service Utilization",
                Size = new Size(250, 50),
                Location = new Point(300, 170),
                Font = new Font("Segoe UI", 10)
            };
            btnServiceUtilization.Click += BtnServiceUtilization_Click;

            btnBack = new Button
            {
                Text = "Back",
                Size = new Size(100, 40),
                Location = new Point(250, 320),
                Font = new Font("Segoe UI", 10)
            };
            btnBack.Click += btnBack_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblDescription,
                btnHotelOccupancy,
                btnGuideRatings,
                btnTransportPerformance,
                btnServiceUtilization,
                btnBack
            });
        }

        private void BtnHotelOccupancy_Click(object sender, EventArgs e)
        {
            using (HotelOccupancyReportForm form = new HotelOccupancyReportForm(_serviceProviderId))
            {
                form.ShowDialog(this);
            }
        }

        private void BtnGuideRatings_Click(object sender, EventArgs e)
        {
            using (GuideRatingsReportForm form = new GuideRatingsReportForm(_serviceProviderId))
            {
                form.ShowDialog(this);
            }
        }

        private void BtnTransportPerformance_Click(object sender, EventArgs e)
        {
            using (TransportPerformanceReportForm form = new TransportPerformanceReportForm(_serviceProviderId))
            {
                form.ShowDialog(this);
            }
        }

        private void BtnServiceUtilization_Click(object sender, EventArgs e)
        {
            using (ServiceUtilizationReportForm form = new ServiceUtilizationReportForm(_serviceProviderId))
            {
                form.ShowDialog(this);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            var form = new ServiceProviderDashboard(this._serviceProviderId); 
            form.Show();
            this.Close();
        }


    }
}