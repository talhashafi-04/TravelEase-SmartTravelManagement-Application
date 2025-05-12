using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace DatabaseProject
{
    public partial class ServiceUtilizationReportForm : Form
    {
        private string _serviceProviderId;
        private DataGridView dgvServices;
        private Label lblTitle;
        private Label lblNoServices;
        private Button btnBack;


        public ServiceUtilizationReportForm(string serviceProviderId)
        {
            _serviceProviderId = serviceProviderId;
            InitializeComponents();
            LoadServiceUtilization();
        }

        private void InitializeComponents()
        {
            this.Text = "Service Utilization Report";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Button btnExportCSV = new Button
            {
                Text = "Export to CSV",
                Size = new Size(120, 30),
                Location = new Point(480, 550),
                Font = new Font("Segoe UI", 10)
            };
            btnExportCSV.Click += BtnExportCSV_Click;
            this.Controls.Add(btnExportCSV);

            // Title label
            lblTitle = new Label
            {
                Text = "Service Utilization Report",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // No services message (initially hidden)
            lblNoServices = new Label
            {
                Text = "You don't have any services registered in the system.",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(250, 200),
                Visible = false
            };

            // Services DataGridView
            dgvServices = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(860, 480),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
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
                lblNoServices,
                dgvServices,
                btnBack
            });
        }

        private void LoadServiceUtilization()
        {
            DataTable dtUtilization = ServiceProviderRepository.GetServiceUtilization(_serviceProviderId);

            if (dtUtilization.Rows.Count > 0)
            {
                dgvServices.DataSource = dtUtilization;

                // Configure columns
                dgvServices.Columns["ServiceID"].Visible = false;
                dgvServices.Columns["ServiceType"].HeaderText = "Service Type";
                dgvServices.Columns["ServiceName"].HeaderText = "Service Name";
                dgvServices.Columns["TotalTripsAssigned"].HeaderText = "Trips Assigned";
                dgvServices.Columns["TotalBookings"].HeaderText = "Total Bookings";
                dgvServices.Columns["TotalTravelers"].HeaderText = "Total Travelers";

                // Add calculated column for utilization rate
                DataColumn utilizationRate = new DataColumn("UtilizationRate", typeof(decimal));
                utilizationRate.Expression = "IIF(TotalBookings = 0, 0, (TotalTravelers / (TotalTripsAssigned * 10)))";
                dtUtilization.Columns.Add(utilizationRate);
                dgvServices.Columns["UtilizationRate"].HeaderText = "Utilization Rate (%)";
                dgvServices.Columns["UtilizationRate"].DefaultCellStyle.Format = "0.00 %";

                // Format the utilization rate column
                dgvServices.Columns["UtilizationRate"].DefaultCellStyle.Format = "0.00 %";

                // Add color coding for utilization rate
                dgvServices.CellFormatting += (s, e) => {
                    if (e.ColumnIndex == dgvServices.Columns["UtilizationRate"].Index && e.Value != null)
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

                lblNoServices.Visible = false;
                dgvServices.Visible = true;
            }
            else
            {
                lblNoServices.Visible = true;
                dgvServices.Visible = false;
            }
        }


        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            if (dgvServices.DataSource == null || dgvServices.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Files|*.csv",
                Title = "Save Report",
                FileName = $"ServiceUtilizationReport_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToCSV((DataTable)dgvServices.DataSource, saveFileDialog.FileName);
                    MessageBox.Show("Report exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optionally open the file with default application
                    // System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCSV(DataTable dataTable, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Write headers
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sw.Write(dataTable.Columns[i].ColumnName);
                    if (i < dataTable.Columns.Count - 1)
                        sw.Write(",");
                }
                sw.WriteLine();

                // Write data
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        string value = row[i].ToString();

                        // Handle values that contain commas
                        if (value.Contains(",") || value.Contains("\""))
                        {
                            value = $"\"{value.Replace("\"", "\"\"")}\"";
                        }

                        sw.Write(value);
                        if (i < dataTable.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();
                }
            }
        }
    }

}