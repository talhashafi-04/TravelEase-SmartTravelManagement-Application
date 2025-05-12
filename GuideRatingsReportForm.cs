using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Diagnostics;

namespace DatabaseProject
{
    public partial class GuideRatingsReportForm : Form
    {
        private string _serviceProviderId;
        private DataGridView dgvGuides;
        private DataGridView dgvRatingsReport;
        private Label lblSelectGuide;
        private Label lblNoGuides;
        private Label lblReportTitle;
        private Label lblSelectedGuide;
        private Panel pnlAverageRatings;
        private Button btnBack;
        private Button btnGenerateReport;
        private Button btnExportCSV; // New button for CSV export
        private DataTable _currentRatingsData; // To store current ratings data for export

        public GuideRatingsReportForm(string serviceProviderId)
        {
            _serviceProviderId = serviceProviderId;
            InitializeComponents();
            LoadGuides();
        }

        private void InitializeComponents()
        {
            this.Text = "Guide Ratings Report";
            this.ClientSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            btnGenerateReport = new Button
            {
                Text = "Generate Report",
                Size = new Size(120, 30),
                Location = new Point(650, 600),
                Font = new Font("Segoe UI", 10)
            };
            btnGenerateReport.Click += BtnGenerateReport_Click;

            // Title label
            Label lblTitle = new Label
            {
                Text = "Guide Ratings Report",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Instructions label
            lblSelectGuide = new Label
            {
                Text = "Select a guide to view ratings:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 60)
            };

            // No guides message (initially hidden)
            lblNoGuides = new Label
            {
                Text = "You don't have any guides registered in the system.",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(250, 200),
                Visible = false
            };

            // Guides DataGridView
            dgvGuides = new DataGridView
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
            dgvGuides.CellClick += DgvGuides_CellClick;

            // Report title
            lblReportTitle = new Label
            {
                Text = "Ratings Report",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 260),
                Visible = false
            };

            // Selected guide
            lblSelectedGuide = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(20, 290),
                Visible = false
            };

            // Ratings report DataGridView
            dgvRatingsReport = new DataGridView
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

            // Average Ratings Panel
            pnlAverageRatings = new Panel
            {
                Location = new Point(20, 550),
                Size = new Size(860, 40),
                Visible = false
            };

            // Back button
            btnBack = new Button
            {
                Text = "Back",
                Size = new Size(100, 30),
                Location = new Point(780, 600),
                Font = new Font("Segoe UI", 10)
            };
            btnBack.Click += (s, e) => this.Close();

            // Export to CSV button
            btnExportCSV = new Button
            {
                Text = "Export to CSV",
                Size = new Size(120, 30),
                Location = new Point(520, 600),
                Font = new Font("Segoe UI", 10),
                Visible = false // Initially hidden
            };
            btnExportCSV.Click += BtnExportCSV_Click;

            // Add all controls
            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblSelectGuide,
                lblNoGuides,
                dgvGuides,
                lblReportTitle,
                lblSelectedGuide,
                dgvRatingsReport,
                pnlAverageRatings,
                btnBack,
                btnGenerateReport,
                btnExportCSV
            });
        }

        private void LoadGuides()
        {
            try
            {
                DataTable dtGuides = ServiceProviderRepository.GetGuidesByServiceProvider(_serviceProviderId);

                // Debug info
                Debug.WriteLine($"Loaded {dtGuides.Rows.Count} guides");
                foreach (DataColumn col in dtGuides.Columns)
                {
                    Debug.WriteLine($"Column found: {col.ColumnName}");
                }

                if (dtGuides.Rows.Count > 0)
                {
                    dgvGuides.DataSource = dtGuides;

                    // Configure columns - only hide GuideID and check if other columns exist
                    dgvGuides.Columns["GuideID"].Visible = false;

                    // Only set properties for columns that exist
                    if (dgvGuides.Columns.Contains("Rating"))
                    {
                        dgvGuides.Columns["Rating"].HeaderText = "Rating";
                        dgvGuides.Columns["Rating"].DefaultCellStyle.Format = "0.00";
                    }

                    if (dgvGuides.Columns.Contains("PhoneNumber"))
                    {
                        dgvGuides.Columns["PhoneNumber"].HeaderText = "Phone";
                    }

                    if (dgvGuides.Columns.Contains("Email"))
                    {
                        dgvGuides.Columns["Email"].HeaderText = "Email";
                    }

                    // Make sure the guide's name is displayed
                    if (dgvGuides.Columns.Contains("Name"))
                    {
                        dgvGuides.Columns["Name"].HeaderText = "Guide Name";
                        dgvGuides.Columns["Name"].DisplayIndex = 0; // Display first
                    }
                    else if (dgvGuides.Columns.Contains("GuideName"))
                    {
                        dgvGuides.Columns["GuideName"].HeaderText = "Guide Name";
                        dgvGuides.Columns["GuideName"].DisplayIndex = 0; // Display first
                    }

                    // Make sure all columns are visible for debugging
                    foreach (DataGridViewColumn col in dgvGuides.Columns)
                    {
                        if (col.Name != "GuideID")
                            col.Visible = true;
                    }

                    lblNoGuides.Visible = false;
                    dgvGuides.Visible = true;
                }
                else
                {
                    lblNoGuides.Visible = true;
                    dgvGuides.Visible = false;

                    // Message for debugging
                    Debug.WriteLine("No guides found for this service provider");
                    MessageBox.Show($"No guides found for Service Provider ID: {_serviceProviderId}", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading guides: {ex.Message}");
                MessageBox.Show($"Error loading guides: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvGuides_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    int guideId = Convert.ToInt32(dgvGuides.Rows[e.RowIndex].Cells["GuideID"].Value);

                    // Get guide name to display in the selected guide label
                    string guideName = "Selected Guide";
                    if (dgvGuides.Columns.Contains("Name"))
                    {
                        guideName = dgvGuides.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    }
                    else if (dgvGuides.Columns.Contains("GuideName"))
                    {
                        guideName = dgvGuides.Rows[e.RowIndex].Cells["GuideName"].Value.ToString();
                    }

                    lblSelectedGuide.Text = $"Guide: {guideName}";
                    lblReportTitle.Visible = true;
                    lblSelectedGuide.Visible = true;

                    LoadRatingsReport(_serviceProviderId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error selecting guide: {ex.Message}");
                    MessageBox.Show($"Error selecting guide: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadRatingsReport(string providerId)
        {
            try
            {
                DataTable dtGuideInfo = ServiceProviderRepository.GetGuidesByServiceProvider(providerId);
                _currentRatingsData = dtGuideInfo;

                Debug.WriteLine($"Loaded info for provider {providerId}");

                if (dtGuideInfo.Rows.Count > 0)
                {
                    dgvRatingsReport.DataSource = dtGuideInfo;

                    if (dgvRatingsReport.Columns.Contains("GuideID"))
                        dgvRatingsReport.Columns["GuideID"].HeaderText = "Guide ID";

                    if (dgvRatingsReport.Columns.Contains("Name"))
                        dgvRatingsReport.Columns["Name"].HeaderText = "Name";

                    if (dgvRatingsReport.Columns.Contains("Rating"))
                    {
                        dgvRatingsReport.Columns["Rating"].HeaderText = "Rating";
                        dgvRatingsReport.Columns["Rating"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvRatingsReport.CellFormatting += (s, e) =>
                        {
                            if (e.ColumnIndex == dgvRatingsReport.Columns["Rating"].Index && e.Value != null)
                            {
                                if (decimal.TryParse(e.Value.ToString(), out decimal rating))
                                {
                                    if (rating <= 2)
                                        e.CellStyle.BackColor = Color.LightCoral;
                                    else if (rating <= 3.5m)
                                        e.CellStyle.BackColor = Color.LightYellow;
                                    else
                                        e.CellStyle.BackColor = Color.LightGreen;
                                }
                            }
                        };
                    }

                    if (dgvRatingsReport.Columns.Contains("PhoneNumber"))
                        dgvRatingsReport.Columns["PhoneNumber"].HeaderText = "Phone";

                    if (dgvRatingsReport.Columns.Contains("Email"))
                        dgvRatingsReport.Columns["Email"].HeaderText = "Email";

                    dgvRatingsReport.Visible = true;
                    btnExportCSV.Visible = true;
                }
                else
                {
                    dgvRatingsReport.DataSource = null;
                    dgvRatingsReport.Visible = false;
                    btnExportCSV.Visible = false;
                    MessageBox.Show("No data found for this provider.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading provider info: {ex.Message}");
                MessageBox.Show($"Error loading provider info: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            // This method can be used to refresh the report or perform other actions
            if (dgvGuides.SelectedRows.Count > 0)
            {
                int guideId = Convert.ToInt32(dgvGuides.SelectedRows[0].Cells["GuideID"].Value);
                LoadRatingsReport(_serviceProviderId);
            }
        }

        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            if (_currentRatingsData != null && _currentRatingsData.Rows.Count > 0)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv";
                    sfd.FileName = $"GuideRatings_{DateTime.Now:yyyyMMdd}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();

                            // Write headers
                            string[] columnNames = _currentRatingsData.Columns.Cast<DataColumn>()
                                .Select(column => column.ColumnName)
                                .ToArray();
                            sb.AppendLine(string.Join(",", columnNames));

                            // Write data rows
                            foreach (DataRow row in _currentRatingsData.Rows)
                            {
                                string[] fields = row.ItemArray.Select(field =>
                                {
                                    string str = field.ToString();
                                    // Escape quotes and wrap fields containing commas in quotes
                                    if (str.Contains(",") || str.Contains("\""))
                                    {
                                        str = "\"" + str.Replace("\"", "\"\"") + "\"";
                                    }
                                    return str;
                                }).ToArray();
                                sb.AppendLine(string.Join(",", fields));
                            }

                            File.WriteAllText(sfd.FileName, sb.ToString());
                            MessageBox.Show("CSV file exported successfully!", "Export Complete",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error exporting to CSV: {ex.Message}", "Export Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No data available to export.", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}