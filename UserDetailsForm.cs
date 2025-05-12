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
    public partial class UserDetailsForm : Form
    {
        private readonly string _userId;
        private Label lblUserID, lblName, lblEmail, lblRole, lblStatus, lblRegistered;
        private TextBox txtName, txtEmail;
        private ComboBox cmbRole, cmbStatus;
        private DataGridView dgvActions;
        private Button btnSave, btnClose;
        SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

        public UserDetailsForm(string userId)
        {
            _userId = userId;
            InitializeComponents();
            LoadUserDetails();
            LoadActionLog();
        }

        private void InitializeComponents()
        {
            this.Text = "User Details";
            this.ClientSize = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            Font labelFont = new Font("Segoe UI", 9, FontStyle.Regular);

            int leftX = 20, labelWidth = 100, ctrlLeft = 130, ctrlWidth = 200, vSpacing = 30;
            int y = 20;

            // Static ID label
            lblUserID = new Label { Text = $"User ID: {_userId}", Location = new Point(leftX, y), AutoSize = true, Font = labelFont };
            y += vSpacing;

            // Name
            lblName = new Label { Text = "Name:", Location = new Point(leftX, y), Width = labelWidth };
            txtName = new TextBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth };
            y += vSpacing;

            // Email
            lblEmail = new Label { Text = "Email:", Location = new Point(leftX, y), Width = labelWidth };
            txtEmail = new TextBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth };
            y += vSpacing;

            // Role
            lblRole = new Label { Text = "Role:", Location = new Point(leftX, y), Width = labelWidth };
            cmbRole = new ComboBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new object[] { "Traveler", "ServiceProvider", "TourOperator", "Admin" });
            y += vSpacing;

            // Status
            lblStatus = new Label { Text = "Status:", Location = new Point(leftX, y), Width = labelWidth };
            cmbStatus = new ComboBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive", "Banned" });
            y += vSpacing;

            // Registered date
            lblRegistered = new Label { Text = "Registered On:", Location = new Point(leftX, y), Width = labelWidth };
            var lblRegDate = new Label { Name = "lblRegDate", Location = new Point(ctrlLeft, y), AutoSize = true };
            y += vSpacing + 10;

            // Actions log grid
            dgvActions = new DataGridView
            {
                Location = new Point(leftX, y),
                Size = new Size(650, 350),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
            };
            dgvActions.Columns.Add("ActionType", "Action");
            dgvActions.Columns.Add("ActionDate", "Date");
            dgvActions.Columns.Add("Description", "Description");
            y += 360;

            // Save & Close
            btnSave = new Button { Text = "Save", Location = new Point(leftX, y), Size = new Size(100, 30) };
            btnClose = new Button { Text = "Close", Location = new Point(leftX + 120, y), Size = new Size(100, 30) };

            btnSave.Click += BtnSave_Click;
            btnClose.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.AddRange(new Control[]
            {
                lblUserID, lblName, txtName, lblEmail, txtEmail,
                lblRole, cmbRole, lblStatus, cmbStatus,
                lblRegistered, lblRegDate,
                dgvActions, btnSave, btnClose
            });
        }

        private void LoadUserDetails()
        {
            const string sql = @"
        SELECT FirstName, LastName, Email, UserRole, Status, RegistrationDate
          FROM [USER]
         WHERE UserID = @UserID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", _userId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            // Combine first + last into one field
                            txtName.Text = rd.GetString(0) + " " + rd.GetString(1);
                            txtEmail.Text = rd.GetString(2);
                            cmbRole.SelectedItem = rd.GetString(3);
                            cmbStatus.SelectedItem = rd.GetString(4);
                            var lblReg = Controls.Find("lblRegDate", true).FirstOrDefault() as Label;
                            if (lblReg != null)
                                lblReg.Text = rd.GetDateTime(5).ToShortDateString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user details: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadActionLog()
        {
            dgvActions.Rows.Clear();

            const string sql = @"
        SELECT ActionType, TimeStamp, 'Performed by Admin' AS Description
          FROM ADMIN_ACTIONS
         WHERE UserID = @UserID
         ORDER BY TimeStamp DESC";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", _userId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            dgvActions.Rows.Add(
                                rd.GetString(0),
                                rd.GetDateTime(1).ToShortDateString(),
                                rd.GetString(2)
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading action log: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Gather & split name
            string fullName = txtName.Text.Trim();
            var parts = fullName.Split(new[] { ' ' }, 2);
            string first = parts[0];
            string last = parts.Length > 1 ? parts[1] : "";

            string email = txtEmail.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString() ?? "";
            string status = cmbStatus.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Name and Email are required.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string sql = @"
        UPDATE [USER]
           SET FirstName = @First,
               LastName  = @Last,
               Email     = @Email,
               UserRole  = @Role,
               Status    = @Status
         WHERE UserID = @UserID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@First", first);
                    cmd.Parameters.AddWithValue("@Last", last);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@UserID", _userId);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("User details updated.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user details: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

    }
}
