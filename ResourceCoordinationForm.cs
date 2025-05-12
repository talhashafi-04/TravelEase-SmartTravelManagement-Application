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
    public partial class ResourceCoordinationForm : Form
    {
        private readonly int _tripId;

        // Controls
        private Label lblAvailable, lblAssigned, lblSchedule;
        private ComboBox cmbServiceType;
        private DataGridView dgvAvailableServices;
        private DataGridView dgvAssignedServices;
        private DateTimePicker dtpScheduleDate;
        private Button btnAssign, btnUnassign, btnSave, btnCancel;
        SqlConnection con = new SqlConnection(
    "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");


        public ResourceCoordinationForm(int tripId)
        {
            _tripId = tripId;
            InitializeComponents();
            LoadAvailableServices();
            LoadAssignedServices();
        }

        private void InitializeComponents()
        {
            this.Text = "Resource Coordination";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Labels
            lblAvailable = new Label { Text = "Available Services:", Location = new Point(20, 20), AutoSize = true };
            lblAssigned = new Label { Text = "Assigned Services:", Location = new Point(460, 20), AutoSize = true };
            lblSchedule = new Label { Text = "Schedule Date:", Location = new Point(20, 520), AutoSize = true };

            // Service Type filter
            cmbServiceType = new ComboBox
            {
                Location = new Point(140, 16),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbServiceType.Items.AddRange(new object[] { "Hotel", "Guide", "Transport" });
            cmbServiceType.SelectedIndexChanged += (s, e) => LoadAvailableServices();
            cmbServiceType.SelectedIndex = 0;

            // Available services grid
            dgvAvailableServices = new DataGridView
            {
                Location = new Point(20, 50),
                Size = new Size(400, 450),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvAvailableServices.Columns.Add("ServiceID", "ID");
            dgvAvailableServices.Columns.Add("Name", "Name");
            dgvAvailableServices.Columns.Add("Details", "Details");

            // Assigned services grid
            dgvAssignedServices = new DataGridView
            {
                Location = new Point(460, 50),
                Size = new Size(400, 450),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvAssignedServices.Columns.Add("ServiceID", "ID");
            dgvAssignedServices.Columns.Add("Name", "Name");
            dgvAssignedServices.Columns.Add("ScheduledDate", "Scheduled Date");

            // Date picker for schedule
            dtpScheduleDate = new DateTimePicker
            {
                Location = new Point(140, 516),
                Format = DateTimePickerFormat.Short,
                Width = 120
            };

            // Buttons
            btnAssign = new Button { Text = "Assign →", Location = new Point(340, 520), Size = new Size(80, 30) };
            btnUnassign = new Button { Text = "← Unassign", Location = new Point(440, 520), Size = new Size(80, 30) };
            btnSave = new Button { Text = "Save", Location = new Point(700, 520), Size = new Size(80, 30) };
            btnCancel = new Button { Text = "Cancel", Location = new Point(800, 520), Size = new Size(80, 30) };

            btnAssign.Click += BtnAssign_Click;
            btnUnassign.Click += BtnUnassign_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.AddRange(new Control[] {
                lblAvailable, cmbServiceType, dgvAvailableServices,
                lblAssigned, dgvAssignedServices,
                lblSchedule, dtpScheduleDate,
                btnAssign, btnUnassign, btnSave, btnCancel
            });
        }

        private void LoadAvailableServices()
        {
            dgvAvailableServices.Rows.Clear();
            string type = cmbServiceType.SelectedItem.ToString();

            const string sql = @"
        SELECT 
            s.ServiceID,
            CASE s.ServiceType
              WHEN 'Hotel'     THEN h.Name
              WHEN 'Guide'     THEN g.Specializations
              WHEN 'Transport' THEN t.Specializations
            END           AS Name,
            CASE s.ServiceType
              WHEN 'Hotel'     THEN h.Description
              WHEN 'Guide'     THEN g.Certifications
              WHEN 'Transport' THEN t.LicenseDetails
            END           AS Details
          FROM SERVICES s
          LEFT JOIN HOTEL              h ON s.ServiceID = h.HotelID
          LEFT JOIN GUIDE              g ON s.ServiceID = g.GuideID
          LEFT JOIN TRANSPORT_PROVIDER t ON s.ServiceID = t.TransportID
         WHERE s.ServiceType = @type
           AND NOT EXISTS(
               SELECT 1 
                 FROM TRIP_SERVICES_Renrollment tr
                WHERE tr.TripID    = @TripID
                  AND tr.ServiceID = s.ServiceID
           )
         ORDER BY Name";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@TripID", _tripId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            dgvAvailableServices.Rows.Add(
                                rd.GetInt32(0),
                                rd.GetString(1),
                                rd.GetString(2)
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available services: " + ex.Message,
                                "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadAssignedServices()
        {
            dgvAssignedServices.Rows.Clear();

            const string sql = @"
        SELECT 
          tr.ServiceID,
          CASE s.ServiceType
            WHEN 'Hotel'     THEN h.Name
            WHEN 'Guide'     THEN g.Specializations
            WHEN 'Transport' THEN t.Specializations
          END          AS Name,
          tr.ScheduleDate
        FROM TRIP_SERVICES_Renrollment tr
        JOIN SERVICES               s ON tr.ServiceID = s.ServiceID
        LEFT JOIN HOTEL              h ON s.ServiceID = h.HotelID
        LEFT JOIN GUIDE              g ON s.ServiceID = g.GuideID
        LEFT JOIN TRANSPORT_PROVIDER t ON s.ServiceID = t.TransportID
       WHERE tr.TripID = @TripID
       ORDER BY tr.ScheduleDate";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@TripID", _tripId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            dgvAssignedServices.Rows.Add(
                                rd.GetInt32(0),
                                rd.GetString(1),
                                rd.GetDateTime(2).ToShortDateString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assigned services: " + ex.Message,
                                "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnAssign_Click(object sender, EventArgs e)
        {
            if (dgvAvailableServices.CurrentRow == null) return;

            int svcId = Convert.ToInt32(dgvAvailableServices.CurrentRow.Cells["ServiceID"].Value);
            DateTime sd = dtpScheduleDate.Value.Date;

            const string sql = @"
        INSERT INTO TRIP_SERVICES_Renrollment (TripID, ServiceID, ScheduleDate)
        VALUES (@TripID, @ServiceID, @SchDate)";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@TripID", _tripId);
                    cmd.Parameters.AddWithValue("@ServiceID", svcId);
                    cmd.Parameters.AddWithValue("@SchDate", sd);
                    cmd.ExecuteNonQuery();
                }
                // Refresh both lists
                LoadAvailableServices();
                LoadAssignedServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning service: " + ex.Message,
                                "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnUnassign_Click(object sender, EventArgs e)
        {
            if (dgvAssignedServices.CurrentRow == null) return;

            int svcId = Convert.ToInt32(dgvAssignedServices.CurrentRow.Cells["ServiceID"].Value);

            const string sql = @"
        DELETE FROM TRIP_SERVICES_Renrollment
         WHERE TripID = @TripID
           AND ServiceID = @ServiceID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@TripID", _tripId);
                    cmd.Parameters.AddWithValue("@ServiceID", svcId);
                    cmd.ExecuteNonQuery();
                }
                // Refresh both lists
                LoadAvailableServices();
                LoadAssignedServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error unassigning service: " + ex.Message,
                                "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All assignments saved.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
