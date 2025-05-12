using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DatabaseProject
{
    public partial class OperatorManagementForm : Form
    {
        private DataGridView dgvOperators;
        private TextBox txtSearch;
        private ComboBox cmbStatusFilter;
        private Button btnFilter;
        private Button btnApprove;
        private Button btnReject;
        private Button btnDetails;
        SqlConnection con = new SqlConnection(
    @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");

        public OperatorManagementForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Operator Management";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Search box
            txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Text = "Search by agency name"
            };

            // Status filter
            cmbStatusFilter = new ComboBox
            {
                Location = new Point(240, 20),
                Width = 140,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatusFilter.Items.AddRange(new object[] { "All Statuses", "Pending", "Approved", "Rejected" });
            cmbStatusFilter.SelectedIndex = 0;

            // Action buttons
            btnFilter = new Button { Text = "Filter", Location = new Point(400, 18), Size = new Size(80, 25) };
            btnApprove = new Button { Text = "Approve", Location = new Point(500, 18), Size = new Size(80, 25) };
            btnReject = new Button { Text = "Reject", Location = new Point(600, 18), Size = new Size(80, 25) };
            btnDetails = new Button { Text = "Details...", Location = new Point(700, 18), Size = new Size(80, 25) };

            // Operators grid
            dgvOperators = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(860, 500),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9)
            };
            dgvOperators.Columns.Add("OperatorID", "Operator ID");
            dgvOperators.Columns.Add("AgencyName", "Agency");
            dgvOperators.Columns.Add("Status", "Status");
            dgvOperators.Columns.Add("Rating", "Rating");
            dgvOperators.Columns.Add("EstablishedDate", "Established");

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                txtSearch, cmbStatusFilter,
                btnFilter, btnApprove, btnReject, btnDetails,
                dgvOperators
            });

            // Event wiring
            this.Load += (s, e) => LoadOperators();
            btnFilter.Click += (s, e) => LoadOperators();
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            btnDetails.Click += BtnDetails_Click;
        }
        private void LoadOperators()
        {
            string search = txtSearch.Text.Trim();
            string status = cmbStatusFilter.SelectedIndex > 0
                ? cmbStatusFilter.SelectedItem.ToString()
                : null;

            var dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                con.Open();

                // Parameterized query:
                string sql = @"
            SELECT 
              OperatorID,
              AgencyName,
              Status,
              Rating,
              EstablishedDate
            FROM TOUR_OPERATOR
            WHERE (@search IS NULL OR AgencyName LIKE '%' + @search + '%')
              AND (@status IS NULL OR Status = @status)
            ORDER BY AgencyName";

                cmd = new SqlCommand(sql, con);
                // If empty, pass DBNull so the filter is ignored
                cmd.Parameters.AddWithValue("@search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);
                cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(status) ? (object)DBNull.Value : status);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dgvOperators.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading operators: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                da?.Dispose();
                cmd?.Dispose();
                con.Close();
            }
        }
        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvOperators.CurrentRow == null)
            {
                MessageBox.Show("Select an operator first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string opId = dgvOperators.CurrentRow.Cells["OperatorID"].Value.ToString();

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE TOUR_OPERATOR SET Status = 'Approved' WHERE OperatorID = @OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", opId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show($"Operator {opId} approved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOperators();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving operator: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (dgvOperators.CurrentRow == null)
            {
                MessageBox.Show("Select an operator first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string opId = dgvOperators.CurrentRow.Cells["OperatorID"].Value.ToString();

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(
                    "UPDATE TOUR_OPERATOR SET Status = 'Rejected' WHERE OperatorID = @OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", opId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show($"Operator {opId} rejected.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOperators();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting operator: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (dgvOperators.CurrentRow == null) return;
            string opId = dgvOperators.CurrentRow.Cells["OperatorID"].Value.ToString();
            using (var detailsForm = new OperatorDetailsForm(opId))
                detailsForm.ShowDialog(this);

            LoadOperators();
        }

    }
}
