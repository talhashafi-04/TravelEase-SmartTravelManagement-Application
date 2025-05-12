using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DatabaseProject;

namespace DatabaseProject
{
    public partial class Form2 : Form  
    {
        private DataGridView dgvTrips;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnCreateTrip;
        private Button btnEditTrip;
        private Button btnResourceCoordination;  // NEW
        private readonly string _operatorId;

        private readonly SqlConnection con = new SqlConnection("Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;");

        public Form2(string operatorId)
        {
            InitializeComponents();
            _operatorId = operatorId;
            LoadTrips();   // initial load
        }

        private void InitializeComponents()
        {
            this.Text = "Trip Management";
            this.ClientSize = new Size(920, 650);
            this.StartPosition = FormStartPosition.CenterParent;

            // Search controls
            txtSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Font = new Font("Segoe UI", 9)
            };
            btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(230, 18),
                Size = new Size(80, 25)
            };

            // Trips grid
            dgvTrips = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(880, 440),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
            };
            dgvTrips.Columns.Add("TripID", "Trip ID");
            dgvTrips.Columns.Add("Title", "Title");
            dgvTrips.Columns.Add("Status", "Status");
            dgvTrips.Columns.Add("Price", "Price");
            dgvTrips.Columns.Add("Duration", "Duration");
            dgvTrips.Columns.Add("StartDate", "Start Date");
            dgvTrips.Columns.Add("EndDate", "End Date");

            // Action buttons
            btnCreateTrip = new Button
            {
                Text = "Create Trip",
                Location = new Point(20, 520),
                Size = new Size(100, 30)
            };
            btnEditTrip = new Button
            {
                Text = "Edit Trip",
                Location = new Point(130, 520),
                Size = new Size(100, 30)
            };
            btnResourceCoordination = new Button
            {
                Text = "Resource Coord.",
                Location = new Point(240, 520),
                Size = new Size(120, 30)
            };

            // Add to form
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(dgvTrips);
            this.Controls.Add(btnCreateTrip);
            this.Controls.Add(btnEditTrip);
            this.Controls.Add(btnResourceCoordination);

            // Event wiring
            this.Load += (s, e) => LoadTrips();
            btnSearch.Click += (s, e) => LoadTrips(txtSearch.Text);
            btnCreateTrip.Click += BtnCreateTrip_Click;
            btnEditTrip.Click += BtnEditTrip_Click;
            btnResourceCoordination.Click += BtnResourceCoordination_Click;
        }
        private void LoadTrips()
        {
            const string sql = @"
        SELECT TripID, Title, Status, Price,
               Duration_Days AS Duration,
               StartDate, EndDate
          FROM TRIP
         WHERE OperatorID = @OpID
      ORDER BY StartDate DESC";

            var dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@OpID", _operatorId);

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dgvTrips.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                da?.Dispose();
                cmd?.Dispose();
                con.Close();
            }
        }

        private void BtnCreateTrip_Click(object sender, EventArgs e)
        {
            using (var createForm = new CreateTripForm())
            {
                createForm.ShowDialog(this);
            }
            LoadTrips(txtSearch.Text);
        }

        private void BtnEditTrip_Click(object sender, EventArgs e)
        {
            if (dgvTrips.CurrentRow == null) return;
            int tripId = Convert.ToInt32(dgvTrips.CurrentRow.Cells["TripID"].Value);
            using (var editForm = new EditTripForm(tripId))
            {
                editForm.ShowDialog(this);
            }
            LoadTrips(txtSearch.Text);
        }

        private void BtnResourceCoordination_Click(object sender, EventArgs e)
        {
            if (dgvTrips.CurrentRow == null) return;
            int tripId = Convert.ToInt32(dgvTrips.CurrentRow.Cells["TripID"].Value);
            using (var rcForm = new ResourceCoordinationForm(tripId))
            {
                rcForm.ShowDialog(this);
            }
            LoadTrips(txtSearch.Text);
        }

        private void BtnBookingManagementClick_Click(object sender, EventArgs e)
        {
            if (dgvTrips.CurrentRow == null) return;
            int tripId = Convert.ToInt32(dgvTrips.CurrentRow.Cells["TripID"].Value);
            using (var rcForm = new TourBookingManagementForm(_operatorId))
            {
                rcForm.ShowDialog(this);
            }
            LoadTrips(txtSearch.Text);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTrips(txtSearch.Text);
        }

        private void LoadTrips(string filter = "")
        {
            // TODO: Query your DB for trips belonging to this operator, with optional filter
            // Example:
            // DataTable dt = TripRepository.GetTrips(operatorId, filter);
            // dgvTrips.DataSource = dt;
        }
    }
}
