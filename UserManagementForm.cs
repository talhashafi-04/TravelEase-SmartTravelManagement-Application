using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class UserManagementForm : Form
    {
        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private ComboBox cmbRoleFilter;
        private ComboBox cmbStatusFilter;
        private Button btnFilter;
        private Button btnDetails;
        private SqlConnection con = new SqlConnection(
            @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");

        public UserManagementForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "User Management";
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterParent;

            // Search box: start blank
            txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Text = string.Empty
            };

            // Role filter
            cmbRoleFilter = new ComboBox
            {
                Location = new Point(240, 20),
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRoleFilter.Items.AddRange(new object[] { "All Roles", "Traveler", "ServiceProvider", "TourOperator", "Admin" });
            cmbRoleFilter.SelectedIndex = 0;

            // Status filter
            cmbStatusFilter = new ComboBox
            {
                Location = new Point(380, 20),
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatusFilter.Items.AddRange(new object[] { "All Statuses", "Active", "Inactive", "Banned" });
            cmbStatusFilter.SelectedIndex = 0;

            // Filter and Details buttons
            btnFilter = new Button
            {
                Text = "Filter",
                Location = new Point(520, 18),
                Size = new Size(80, 25)
            };
            btnDetails = new Button
            {
                Text = "Details...",
                Location = new Point(620, 18),
                Size = new Size(80, 25)
            };

            // Users grid
            dgvUsers = new DataGridView
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
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UserID",
                HeaderText = "User ID",
                DataPropertyName = "UserID"
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Name",
                DataPropertyName = "Name"
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email"
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Role",
                HeaderText = "Role",
                DataPropertyName = "Role"
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status"
            });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RegistrationDate",
                HeaderText = "Registered On",
                DataPropertyName = "RegistrationDate"
            });

            // Add controls
            Controls.AddRange(new Control[]
            {
                txtSearch, cmbRoleFilter, cmbStatusFilter,
                btnFilter, btnDetails, dgvUsers
            });

            // Wire events
            Load += (s, e) => LoadUsers();
            btnFilter.Click += (s, e) => LoadUsers();
            btnDetails.Click += BtnDetails_Click;
        }

        private void LoadUsers()
        {
            string search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
            string role = cmbRoleFilter.SelectedIndex > 0 ? cmbRoleFilter.SelectedItem.ToString() : null;
            string status = cmbStatusFilter.SelectedIndex > 0 ? cmbStatusFilter.SelectedItem.ToString() : null;

            const string sql = @"
SELECT 
  UserID,
  FirstName + ' ' + LastName AS Name,
  Email,
  UserRole  AS Role,
  Status,
  CAST(RegistrationDate AS DATE) AS RegistrationDate
FROM [USER]
WHERE (@search IS NULL OR FirstName + ' ' + LastName LIKE '%' + @search + '%' OR Email LIKE '%' + @search + '%')
  AND (@role   IS NULL OR UserRole = @role)
  AND (@status IS NULL OR Status = @status)
ORDER BY RegistrationDate DESC";

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@search", (object)search ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@role", (object)role ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", (object)status ?? DBNull.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Select a user first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userId = dgvUsers.CurrentRow.Cells["UserID"].Value.ToString();
            using (var detailsForm = new UserDetailsForm(userId))
            {
                detailsForm.ShowDialog(this);
            }
            LoadUsers();
        }
    }
}
