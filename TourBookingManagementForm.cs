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
    public partial class TourBookingManagementForm : Form
    {
        private DataGridView dgvBookings;
        private ComboBox cmbStatusFilter;
        private Label lblStatus, lblFrom, lblTo, lblTripFilter;
        private DateTimePicker dtpFrom, dtpTo;
        private ComboBox cmbTripFilter;
        private Button btnFilter, btnDetails;
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";


        private readonly string _operatorId;
        public TourBookingManagementForm(string operatorId)
        {
            _operatorId = operatorId;
            InitializeComponents();
            LoadOperatorTrips();
            LoadBookings();
        }
        private void LoadOperatorTrips()
        {
            try
            {
                con.Open();
                // First, clear and add an “All” option:
                var trips = new List<(int? Id, string Title)>();
                trips.Add((null, "All Trips"));

                using (var cmd = new SqlCommand(
                    "SELECT TripID, Title FROM TRIP WHERE OperatorID = @OpID ORDER BY Title", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                            trips.Add((rd.GetInt32(0), rd.GetString(1)));
                    }
                }

                cmbTripFilter.DataSource = trips;
                cmbTripFilter.DisplayMember = nameof(ValueTuple<int?, string>.Item2);
                cmbTripFilter.ValueMember = nameof(ValueTuple<int?, string>.Item1);
                cmbTripFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void InitializeComponents()
        {
            this.Text = "Booking Management";
            this.ClientSize = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            lblStatus = new Label { Text = "Status:", Location = new Point(20, 20), AutoSize = true };
            cmbStatusFilter = new ComboBox { Location = new Point(80, 16), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Pending", "Confirmed", "Cancelled", "Completed" });
            cmbStatusFilter.SelectedIndex = 0;

            lblFrom = new Label { Text = "From:", Location = new Point(220, 20), AutoSize = true };
            dtpFrom = new DateTimePicker { Location = new Point(260, 16), Format = DateTimePickerFormat.Short, Width = 100 };

            lblTo = new Label { Text = "To:", Location = new Point(380, 20), AutoSize = true };
            dtpTo = new DateTimePicker { Location = new Point(410, 16), Format = DateTimePickerFormat.Short, Width = 100 };

            lblTripFilter = new Label { Text = "Trip:", Location = new Point(530, 20), AutoSize = true };
            cmbTripFilter = new ComboBox { Location = new Point(570, 16), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            // TODO: Load operator's trips into cmbTripFilter

            btnFilter = new Button { Text = "Filter", Location = new Point(790, 15), Size = new Size(80, 25) };
            btnDetails = new Button { Text = "Details...", Location = new Point(880, 15), Size = new Size(80, 25) };

            dgvBookings = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(940, 480),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            // Example columns:
            dgvBookings.Columns.Add("BookingID", "Booking ID");
            dgvBookings.Columns.Add("TripTitle", "Trip");
            dgvBookings.Columns.Add("TravelerName", "Traveler");
            dgvBookings.Columns.Add("Status", "Status");
            dgvBookings.Columns.Add("BookingDate", "Booking Date");
            dgvBookings.Columns.Add("TotalAmount", "Amount");

            this.Controls.AddRange(new Control[]
            {
                lblStatus, cmbStatusFilter,
                lblFrom, dtpFrom,
                lblTo, dtpTo,
                lblTripFilter, cmbTripFilter,
                btnFilter, btnDetails,
                dgvBookings
            });

            this.Load += (s, e) => LoadBookings();
            btnFilter.Click += (s, e) => LoadBookings();
            btnDetails.Click += BtnDetails_Click;
        }

        private void LoadBookings()
        {
            // Gather filters
            string status = cmbStatusFilter.SelectedItem.ToString();
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
            int? tripId = (cmbTripFilter.SelectedValue as int?) ?? null;

            var sql = new StringBuilder(@"
        SELECT 
          b.BookingID,
          t.Title          AS TripTitle,
          tr.FirstName + ' ' + tr.LastName AS TravelerName,
          b.Status,
          b.Date           AS BookingDate,
          b.TotalAmount
        FROM BOOKING b
        JOIN TRIP    t  ON b.TripID      = t.TripID
        JOIN TRAVELER tr ON b.TravelerID = tr.TravelerID
        WHERE t.OperatorID = @OpID
          AND b.Date BETWEEN @From AND @To
    ");

            if (status != "All")
                sql.Append(" AND b.Status = @Status");
            if (tripId.HasValue)
                sql.Append(" AND b.TripID = @TripID");

            sql.Append(" ORDER BY b.Date DESC");

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    cmd.Parameters.AddWithValue("@From", from);
                    cmd.Parameters.AddWithValue("@To", to);
                    if (status != "All") cmd.Parameters.AddWithValue("@Status", status);
                    if (tripId.HasValue) cmd.Parameters.AddWithValue("@TripID", tripId.Value);

                    var dt = new DataTable();
                    using (var da = new SqlDataAdapter(cmd))
                        da.Fill(dt);

                    dgvBookings.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Please select a booking.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingId = Convert.ToInt32(dgvBookings.CurrentRow.Cells["BookingID"].Value);
            //using (var detailsForm = new BookingDetailsForm(bookingId))
            //{
            //    detailsForm.ShowDialog(this);
            //}
            LoadBookings();
        }
    }
}
