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
    public partial class UserManagementForm : Form
    {
        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private ComboBox cmbRoleFilter;
        private ComboBox cmbStatusFilter;
        private Button btnFilter;
        private Button btnDetails;
        SqlConnection con = new SqlConnection(
    "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

        public UserManagementForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "User Management";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Search box
            txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Text = "Search by name or email"
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

            // Filter button
            btnFilter = new Button
            {
                Text = "Filter",
                Location = new Point(520, 18),
                Size = new Size(80, 25)
            };

            // Details button
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
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvUsers.Columns.Add("UserID", "User ID");
            dgvUsers.Columns.Add("Name", "Name");
            dgvUsers.Columns.Add("Email", "Email");
            dgvUsers.Columns.Add("Role", "Role");
            dgvUsers.Columns.Add("Status", "Status");
            dgvUsers.Columns.Add("RegistrationDate", "Registered On");

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                txtSearch, cmbRoleFilter, cmbStatusFilter, btnFilter, btnDetails, dgvUsers
            });

            // Event wiring
            this.Load += (s, e) => LoadUsers();
            btnFilter.Click += (s, e) => LoadUsers();
            btnDetails.Click += BtnDetails_Click;
        }

        private void LoadUsers()
        {
            string search = txtSearch.Text.Trim();
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
        WHERE (@search IS NULL OR FirstName + ' ' + LastName LIKE '%' + @search + '%' 
             OR Email LIKE '%' + @search + '%')
          AND (@role   IS NULL OR UserRole = @role)
          AND (@status IS NULL OR Status = @status)
        ORDER BY RegistrationDate DESC";

            var dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search);
                cmd.Parameters.AddWithValue("@role", string.IsNullOrEmpty(role) ? (object)DBNull.Value : role);
                cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(status) ? (object)DBNull.Value : status);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                da?.Dispose();
                cmd?.Dispose();
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
