using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace DatabaseProject
{
    public partial class HotelOccupancyReportForm : Form
    {
        private string _serviceProviderId;
        private DataGridView dgvHotels;
        private DataGridView dgvOccupancyReport;
        private Label lblSelectHotel;
        private Label lblNoHotels;
        private Label lblReportTitle;
        private Label lblSelectedHotel;
        private Button btnBack;
        private Button btnExportCSV;


        public HotelOccupancyReportForm(string serviceProviderId)
        {
            _serviceProviderId = serviceProviderId;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Hotel Occupancy Report";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title label
            Label lblTitle = new Label
            {
                Text = "Hotel Occupancy Report",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Instructions label
            lblSelectHotel = new Label
            {
                Text = "Select a hotel to view its occupancy rate:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 60)
            };

            // No hotels message (initially hidden)
            lblNoHotels = new Label
            {
                Text = "You don't have any hotels registered in the system.",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(250, 200),
                Visible = false
            };

            // Hotels DataGridView
            dgvHotels = new DataGridView
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
            dgvHotels.CellClick += DgvHotels_CellClick;

            // Report section
            lblReportTitle = new Label
            {
                Text = "Occupancy Rate Report",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 260),
                Visible = false
            };

            lblSelectedHotel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(20, 290),
                Visible = false
            };

            // Occupancy report DataGridView
            dgvOccupancyReport = new DataGridView
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

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblSelectHotel,
                lblNoHotels,
                dgvHotels,
                lblReportTitle,
                lblSelectedHotel,
                dgvOccupancyReport,
                btnBack
            });

            // Load hotels when form loads
            this.Load += HotelOccupancyReportForm_Load;

            btnExportCSV = new Button
            {
                Text = "Export to CSV",
                Size = new Size(120, 30),
                Location = new Point(640, 550),
                Font = new Font("Segoe UI", 10),
                Visible = false // initially hidden
            };
            btnExportCSV.Click += BtnExportCSV_Click;
            this.Controls.Add(btnExportCSV);

        }

        private void HotelOccupancyReportForm_Load(object sender, EventArgs e)
        {
            LoadHotels();
        }

        private void LoadHotels()
        {
            DataTable dtHotels = ServiceProviderRepository.GetHotelsByServiceProvider(_serviceProviderId);

            if (dtHotels.Rows.Count > 0)
            {
                dgvHotels.DataSource = dtHotels;
                dgvHotels.Columns["HotelID"].Visible = false;
                dgvHotels.Columns["Name"].HeaderText = "Hotel Name";
                dgvHotels.Columns["Capacity"].HeaderText = "Capacity";
                dgvHotels.Columns["Amenities"].HeaderText = "Amenities";
                dgvHotels.Columns["Description"].HeaderText = "Description";

                lblNoHotels.Visible = false;
                dgvHotels.Visible = true;
            }
            else
            {
                lblNoHotels.Visible = true;
                dgvHotels.Visible = false;
            }
        }

        private void DgvHotels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string hotelId = dgvHotels.Rows[e.RowIndex].Cells["HotelID"].Value.ToString();
                string hotelName = dgvHotels.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                // Show report title and selected hotel
                lblReportTitle.Visible = true;
                lblSelectedHotel.Text = $"For: {hotelName}";
                lblSelectedHotel.Visible = true;

                // Load and display occupancy report
                LoadOccupancyReport(hotelId);
            }
        }

        private void LoadOccupancyReport(string hotelId)
        {
            DataTable dtOccupancy = ServiceProviderRepository.GetHotelOccupancyRate(hotelId);


            if (dtOccupancy.Rows.Count > 0)
            {
                btnExportCSV.Visible = true;
                btnExportCSV.Tag = dtOccupancy; // store the datatable in the button's tag for exporting
                dgvOccupancyReport.DataSource = dtOccupancy;
                dgvOccupancyReport.Columns["TripID"].Visible = false;
                dgvOccupancyReport.Columns["TripName"].HeaderText = "Trip Name";
                dgvOccupancyReport.Columns["StartDate"].HeaderText = "Start Date";
                dgvOccupancyReport.Columns["EndDate"].HeaderText = "End Date";
                dgvOccupancyReport.Columns["BookingsCount"].HeaderText = "Number of Bookings";
                dgvOccupancyReport.Columns["TotalGuests"].HeaderText = "Total Guests";
                dgvOccupancyReport.Columns["Capacity"].HeaderText = "Hotel Capacity";
                dgvOccupancyReport.Columns["OccupancyRate"].HeaderText = "Occupancy Rate (%)";

                // Format the date columns
                dgvOccupancyReport.Columns["StartDate"].DefaultCellStyle.Format = "MMM dd, yyyy";
                dgvOccupancyReport.Columns["EndDate"].DefaultCellStyle.Format = "MMM dd, yyyy";

                // Format the occupancy rate column
                dgvOccupancyReport.Columns["OccupancyRate"].DefaultCellStyle.Format = "0.00 %";

                // Add color coding for occupancy rate
                dgvOccupancyReport.CellFormatting += (s, e) => {
                    if (e.ColumnIndex == dgvOccupancyReport.Columns["OccupancyRate"].Index
                        && e.Value != null
                        && e.Value != DBNull.Value)
                    {
                        decimal rate = Convert.ToDecimal(e.Value);
                        if (rate < 30)
                            e.CellStyle.BackColor = Color.LightCoral;
                        else if (rate >= 30 && rate < 70)
                            e.CellStyle.BackColor = Color.LightYellow;
                        else
                            e.CellStyle.BackColor = Color.LightGreen;
                    }



                };

                dgvOccupancyReport.Visible = true;
            }
            else
            {
                dgvOccupancyReport.DataSource = null;
                MessageBox.Show("No occupancy data available for this hotel.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            if (btnExportCSV.Tag is DataTable dt && dt.Rows.Count > 0)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv";
                    sfd.FileName = "HotelOccupancyReport.csv";

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