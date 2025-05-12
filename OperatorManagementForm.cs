using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class OperatorManagementForm : Form
    {
        private DataGridView dgvOperators;
        private TextBox txtSearch;
        private ComboBox cmbStatusFilter;
        private Button btnFilter, btnApprove, btnReject, btnDetails;
        private SqlConnection con = new SqlConnection(
            @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");

        public OperatorManagementForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Operator Management";
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterParent;

            // Search box
            txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Text = string.Empty
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
                Font = new Font("Segoe UI", 9),
                AutoGenerateColumns = false
            };

            // Define bound columns
            dgvOperators.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OperatorID",
                HeaderText = "Operator ID",
                DataPropertyName = "OperatorID"
            });
            dgvOperators.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AgencyName",
                HeaderText = "Agency",
                DataPropertyName = "AgencyName"
            });
            dgvOperators.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status"
            });
            dgvOperators.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Rating",
                HeaderText = "Rating",
                DataPropertyName = "Rating"
            });
            dgvOperators.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstablishedDate",
                HeaderText = "Established",
                DataPropertyName = "EstablishedDate"
            });

            Controls.AddRange(new Control[]
            {
                txtSearch, cmbStatusFilter,
                btnFilter, btnApprove, btnReject, btnDetails,
                dgvOperators
            });

            // Events
            Load += (s, e) => LoadOperators();
            btnFilter.Click += (s, e) => LoadOperators();
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            btnDetails.Click += BtnDetails_Click;
        }

        private void LoadOperators()
        {
            string search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
            string status = cmbStatusFilter.SelectedIndex > 0
                ? cmbStatusFilter.SelectedItem.ToString()
                : null;

            var dt = new DataTable();
            const string sql = @"
SELECT 
  o.OperatorID,
  o.AgencyName,
  u.Status,
  o.Rating,
  o.EstablishedDate
FROM TOUR_OPERATOR o
JOIN [USER] u ON o.OperatorID = u.UserID
WHERE (@search IS NULL OR o.AgencyName LIKE '%' + @search + '%')
  AND (@status IS NULL OR u.Status = @status)
ORDER BY o.AgencyName";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@search", (object)search ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", (object)status ?? DBNull.Value);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                dgvOperators.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading operators: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
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
                    "UPDATE [USER] SET Status = 'Approved' WHERE UserID = @OpID", con))
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
                    "UPDATE [USER] SET Status = 'Rejected' WHERE UserID = @OpID", con))
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
            {
                detailsForm.ShowDialog(this);
            }
            LoadOperators();
        }
    }
}
