using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace DatabaseProject
{
    public partial class TransportPerformanceReportForm : Form
    {
        private string _serviceProviderId;
        private DataGridView dgvTransports;
        private DataGridView dgvPerformanceReport;
        private Label lblSelectTransport;
        private Label lblNoTransports;
        private Label lblReportTitle;
        private Label lblSelectedTransport;
        private Button btnBack;
        private Button btnExportCSV;
         
        public TransportPerformanceReportForm(string serviceProviderId)
        {
            _serviceProviderId = serviceProviderId;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Transport Performance Report";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title label
            Label lblTitle = new Label
            {
                Text = "Transport Performance Report",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Instructions label
            lblSelectTransport = new Label
            {
                Text = "Select a transport service to view its performance:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 60)
            };

            // No transports message (initially hidden)
            lblNoTransports = new Label
            {
                Text = "You don't have any transport services registered in the system.",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(250, 200),
                Visible = false
            };

            // Transports DataGridView
            dgvTransports = new DataGridView
            {
                Location = new Point(20, 90),
                Size = new Size(860, 150),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
            };
            dgvTransports.CellClick += DgvTransports_CellClick;

            // Report section
            lblReportTitle = new Label
            {
                Text = "Performance Report",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 260),
                Visible = false
            };

            lblSelectedTransport = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(20, 290),
                Visible = false
            };

            // Performance report DataGridView
            dgvPerformanceReport = new DataGridView
            {
                Location = new Point(20, 320),
                Size = new Size(860, 220),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                Visible = false
            };

            // Back button
            btnBack = new Button
            {
                Text = "Back",
                Size = new Size(100, 30),
                Location = new Point(780, 550),
                Font = new Font("Segoe UI", 10)
            };
            btnBack.Click += (s, e) => this.Close();

            // Export to CSV button
            btnExportCSV = new Button
            {
                Text = "Export to CSV",
                Size = new Size(120, 30),
                Location = new Point(640, 550),
                Font = new Font("Segoe UI", 10),
                Visible = false // initially hidden
            };
            btnExportCSV.Click += BtnExportCSV_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblSelectTransport,
                lblNoTransports,
                dgvTransports,
                lblReportTitle,
                lblSelectedTransport,
                dgvPerformanceReport,
                btnBack,
                btnExportCSV
            });

            // Load transports when form loads
            this.Load += TransportPerformanceReportForm_Load;
        }

        private void TransportPerformanceReportForm_Load(object sender, EventArgs e)
        {
            LoadTransports();
        }

        private void LoadTransports()
        {
            DataTable dtTransports = ServiceProviderRepository.GetTransportsByServiceProvider(_serviceProviderId);

            if (dtTransports.Rows.Count > 0)
            {
                dgvTransports.DataSource = dtTransports;
                dgvTransports.Columns["TransportID"].Visible = false;
                dgvTransports.Columns["Specializations"].HeaderText = "Specializations";
                dgvTransports.Columns["LicenseDetails"].HeaderText = "License Details";
                dgvTransports.Columns["ServiceAreas"].HeaderText = "Service Areas";

                lblNoTransports.Visible = false;
                dgvTransports.Visible = true;
            }
            else
            {
                lblNoTransports.Visible = true;
                dgvTransports.Visible = false;
                btnExportCSV.Visible = false;
            }
        }

        private void DgvTransports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int transportId = Convert.ToInt32(dgvTransports.Rows[e.RowIndex].Cells["TransportID"].Value);
                string transportInfo = $"Transport Service #{transportId}";

                // Show report title and selected transport
                lblReportTitle.Visible = true;
                lblSelectedTransport.Text = $"For: {transportInfo}";
                lblSelectedTransport.Visible = true;

                // Load and display performance report
                LoadPerformanceReport(transportId);
            }
        }

        private void LoadPerformanceReport(int transportId)
        {
            DataTable dtPerformance = ServiceProviderRepository.GetTransportPerformance(transportId);

            if (dtPerformance.Rows.Count > 0)
            {
                btnExportCSV.Visible = true;
                btnExportCSV.Tag = dtPerformance; // store the datatable in the button's tag for exporting
                dgvPerformanceReport.DataSource = dtPerformance;
                dgvPerformanceReport.Columns["TripID"].Visible = false;
                dgvPerformanceReport.Columns["TripName"].HeaderText = "Trip Name";
                dgvPerformanceReport.Columns["StartDate"].HeaderText = "Start Date";
                dgvPerformanceReport.Columns["EndDate"].HeaderText = "End Date";
                dgvPerformanceReport.Columns["OverallRating"].HeaderText = "Overall Rating";
                dgvPerformanceReport.Columns["PunctualityRating"].HeaderText = "Punctuality";
                dgvPerformanceReport.Columns["SafetyRating"].HeaderText = "Safety";
                dgvPerformanceReport.Columns["DriverRating"].HeaderText = "Driver Rating";

                // Format the date columns
                dgvPerformanceReport.Columns["StartDate"].DefaultCellStyle.Format = "MMM dd, yyyy";
                dgvPerformanceReport.Columns["EndDate"].DefaultCellStyle.Format = "MMM dd, yyyy";

                // Format the rating columns to show 1 decimal place
                dgvPerformanceReport.Columns["OverallRating"].DefaultCellStyle.Format = "0.0";
                dgvPerformanceReport.Columns["PunctualityRating"].DefaultCellStyle.Format = "0.0";
                dgvPerformanceReport.Columns["SafetyRating"].DefaultCellStyle.Format = "0.0";
                dgvPerformanceReport.Columns["DriverRating"].DefaultCellStyle.Format = "0.0";

                // Add color coding for ratings
                dgvPerformanceReport.CellFormatting += (s, e) => {
                    if (e.Value != null && (e.ColumnIndex == dgvPerformanceReport.Columns["OverallRating"].Index ||
                                          e.ColumnIndex == dgvPerformanceReport.Columns["PunctualityRating"].Index ||
                                          e.ColumnIndex == dgvPerformanceReport.Columns["SafetyRating"].Index ||
                                          e.ColumnIndex == dgvPerformanceReport.Columns["DriverRating"].Index))
                    {
                        if (decimal.TryParse(e.Value.ToString(), out decimal rating))
                        {
                            if (rating < 2)
                                e.CellStyle.BackColor = Color.LightCoral;
                            else if (rating >= 2 && rating < 4)
                                e.CellStyle.BackColor = Color.LightYellow;
                            else
                                e.CellStyle.BackColor = Color.LightGreen;
                        }
                    }
                };

                dgvPerformanceReport.Visible = true;
            }
            else
            {
                btnExportCSV.Visible = false;
                dgvPerformanceReport.DataSource = null;
                MessageBox.Show("No performance data available for this transport service.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            if (btnExportCSV.Tag is DataTable dt && dt.Rows.Count > 0)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv";
                    sfd.FileName = "TransportPerformanceReport.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                // Write headers
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    sw.Write(dt.Columns[i]);
                                    if (i < dt.Columns.Count - 1)
                                        sw.Write(",");
                                }
                                sw.WriteLine();

                                // Write data
                                foreach (DataRow row in dt.Rows)
                                {
                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        string value = row[i]?.ToString().Replace(",", ""); // remove commas from values
                                        sw.Write(value);
                                        if (i < dt.Columns.Count - 1)
                                            sw.Write(",");
                                    }
                                    sw.WriteLine();
                                }
                            }
                            MessageBox.Show("CSV file has been saved successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while saving CSV:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}