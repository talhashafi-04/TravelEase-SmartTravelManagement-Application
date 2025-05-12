using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Make sure to use Microsoft.Data.SqlClient consistently

namespace DatabaseProject
{
    // Simple data-holder for trip dropdown; ToString() returns Title
    public class TripItem
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public override string ToString() => Title;
    }

    public partial class BookingManagementForm : Form
    {
        // UI Controls
        private DataGridView dgvBookings;
        private ComboBox cmbStatusFilter;
        private Label lblStatus, lblFrom, lblTo, lblTripFilter;
        private DateTimePicker dtpFrom, dtpTo;
        private ComboBox cmbTripFilter;
        private Button btnFilter, btnDetails;

        // Database connection
        private readonly string _operatorId;
        private readonly SqlConnection con;

        public BookingManagementForm(string operatorId)
        {
            _operatorId = operatorId;
            // Using same connection string format as Form1
            con = new SqlConnection(
                @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;");

            InitializeComponents();
            LoadOperatorTrips();
            LoadBookings();
        }

        private void InitializeComponents()
        {
            // Form setup
            Text = "Booking Management";
            ClientSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterParent;

            // Status Filter
            lblStatus = new Label { Text = "Status:", Location = new Point(20, 20), AutoSize = true };
            cmbStatusFilter = new ComboBox { Location = new Point(80, 16), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Pending", "Confirmed", "Cancelled", "Completed" });
            cmbStatusFilter.SelectedIndex = 0;

            // Date range
            lblFrom = new Label { Text = "From:", Location = new Point(220, 20), AutoSize = true };
            dtpFrom = new DateTimePicker { Location = new Point(260, 16), Format = DateTimePickerFormat.Short, Width = 100 };
            dtpFrom.Value = DateTime.Now.AddYears(-1);

            lblTo = new Label { Text = "To:", Location = new Point(380, 20), AutoSize = true };
            dtpTo = new DateTimePicker { Location = new Point(410, 16), Format = DateTimePickerFormat.Short, Width = 100 };
            dtpTo.Value = DateTime.Now;

            // Trip dropdown
            lblTripFilter = new Label { Text = "Trip:", Location = new Point(530, 20), AutoSize = true };
            cmbTripFilter = new ComboBox { Location = new Point(570, 16), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            // Buttons
            btnFilter = new Button { Text = "Filter", Location = new Point(790, 15), Size = new Size(80, 25) };
            btnDetails = new Button { Text = "Details...", Location = new Point(880, 15), Size = new Size(80, 25) };

            // Data grid
            dgvBookings = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(940, 520),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Add controls
            Controls.AddRange(new Control[]
            {
                lblStatus, cmbStatusFilter,
                lblFrom, dtpFrom,
                lblTo, dtpTo,
                lblTripFilter, cmbTripFilter,
                btnFilter, btnDetails,
                dgvBookings
            });

            // Event handlers
            Load += (s, e) => LoadBookings();
            btnFilter.Click += (s, e) => LoadBookings();
            btnDetails.Click += BtnDetails_Click;
        }

        private void LoadOperatorTrips()
        {
            // Populate combo exclusively with TripItem instances
            cmbTripFilter.Items.Clear();
            cmbTripFilter.Items.Add(new TripItem { Id = null, Title = "All Trips" });

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT TripID, Title FROM TRIP WHERE OperatorID = @OpID ORDER BY Title", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            cmbTripFilter.Items.Add(new TripItem
                            {
                                Id = rd.GetInt32(0),
                                Title = rd.GetString(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading trips: {ex.Message}", "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            if (cmbTripFilter.Items.Count > 0)
                cmbTripFilter.SelectedIndex = 0;
        }

        private void LoadBookings()
        {
            // Gather filters
            string status = cmbStatusFilter.SelectedItem.ToString();
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
            TripItem sel = cmbTripFilter.SelectedItem as TripItem;
            int? tripId = sel?.Id;

            // Build query with modified JOIN approach
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT b.BookingID, t.Title AS TripTitle, u.FirstName + ' ' + u.LastName AS TravelerName,");
            sb.AppendLine("       b.Status, b.Date AS BookingDate, b.TotalAmount");
            sb.AppendLine("FROM BOOKING b");
            sb.AppendLine("JOIN TRIP t ON b.TripID = t.TripID");
            sb.AppendLine("JOIN [USER] u ON b.TravelerID = u.UserID"); 
            // Direct join to USER table - this assumes TravelerID in BOOKING matches UserID in USER table
            sb.AppendLine("WHERE t.OperatorID = @OpID");
            sb.AppendLine("  AND b.Date BETWEEN @From AND @To");
            if (status != "All") sb.AppendLine("  AND b.Status = @Status");
            if (tripId.HasValue) sb.AppendLine("  AND b.TripID = @TripID");
            sb.AppendLine("ORDER BY b.Date DESC");

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    cmd.Parameters.AddWithValue("@From", from);
                    cmd.Parameters.AddWithValue("@To", to);
                    if (status != "All") cmd.Parameters.AddWithValue("@Status", status);
                    if (tripId.HasValue) cmd.Parameters.AddWithValue("@TripID", tripId.Value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                dgvBookings.DataSource = dt;
                if (dt.Rows.Count == 0)
                    MessageBox.Show("No bookings found for the selected filters.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Please select a booking.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingId = Convert.ToInt32(dgvBookings.CurrentRow.Cells["BookingID"].Value);
            
            // Here you would typically open a detailed booking form passing the bookingId
            MessageBox.Show($"Booking details for ID: {bookingId} would be shown here.",
                "Booking Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}