using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class BookingsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private string travelerId;

        public BookingsForm(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            LoadBookings();
            SetupForm();
        }
        public BookingsForm(string travelerId, int tripIdToSelect)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            LoadBookings();
            SetupForm();
            SelectRowByTripId(tripIdToSelect);
        }
        private void SelectRowByTripId(int tripId)
        {
            foreach (DataGridViewRow row in dgvBookings.Rows)
            {
                if (Convert.ToInt32(row.Cells["TripID"].Value) == tripId)
                {
                    row.Selected = true;
                    dgvBookings.FirstDisplayedScrollingRowIndex = row.Index; // scroll into view
                    break;
                }
            }
        }

        private void SetupForm()
        {
            // Initialize date pickers
            dtpStartDate.Value = DateTime.Now.AddMonths(-3);
            dtpEndDate.Value = DateTime.Now.AddMonths(6);

            // Setup combobox for status filter
            cboStatus.Items.Add("All Statuses");
            cboStatus.Items.Add("Pending");
            cboStatus.Items.Add("Confirmed");
            cboStatus.Items.Add("Completed");
            cboStatus.Items.Add("Cancelled");
            cboStatus.SelectedIndex = 0;

            // Add event handlers
            btnFilter.Click += BtnFilter_Click;
            btnReset.Click += BtnReset_Click;
            btnViewDetails.Click += BtnViewDetails_Click;
            btnCancelBooking.Click += BtnCancelBooking_Click;
            btnViewPass.Click += BtnViewPass_Click;
            btnBack.Click += BtnBack_Click;
        }

        private void LoadBookings(string statusFilter = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            B.BookingID, 
                            T.TripID,
                            T.Title AS TripTitle, 
                            D.Name AS Destination,
                            B.Date AS BookingDate, 
                            T.StartDate, 
                            T.EndDate,
                            B.NoOfTravelers, 
                            B.TotalAmount, 
                            B.Status,
                            CASE WHEN TP.Pass_ID IS NOT NULL THEN 'Available' ELSE 'Not Available' END AS TravelPass
                        FROM BOOKING B
                        INNER JOIN TRIP T ON B.TripID = T.TripID
                        INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                        LEFT JOIN TRAVEL_PASS TP ON B.BookingID = TP.BookingID
                        WHERE B.TravelerID = @TravelerID";

                    // Add filters if specified
                    if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All Statuses")
                    {
                        query += " AND B.Status = @Status";
                    }

                    if (startDate.HasValue)
                    {
                        query += " AND T.StartDate >= @StartDate";
                    }

                    if (endDate.HasValue)
                    {
                        query += " AND T.StartDate <= @EndDate";
                    }

                    query += " ORDER BY B.Date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All Statuses")
                    {
                        command.Parameters.AddWithValue("@Status", statusFilter);
                    }

                    if (startDate.HasValue)
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Value);
                    }

                    if (endDate.HasValue)
                    {
                        command.Parameters.AddWithValue("@EndDate", endDate.Value);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable bookingsTable = new DataTable();
                    adapter.Fill(bookingsTable);

                    dgvBookings.DataSource = bookingsTable;
                    FormatBookingsGrid();

                    // Update booking statistics
                    UpdateBookingStats();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatBookingsGrid()
        {
            dgvBookings.Columns["BookingID"].Visible = false;
            dgvBookings.Columns["TripID"].Visible = false;

            dgvBookings.Columns["TripTitle"].HeaderText = "Trip";
            dgvBookings.Columns["TripTitle"].Width = 200;

            dgvBookings.Columns["Destination"].HeaderText = "Destination";
            dgvBookings.Columns["Destination"].Width = 150;

            dgvBookings.Columns["BookingDate"].HeaderText = "Booking Date";
            dgvBookings.Columns["BookingDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvBookings.Columns["BookingDate"].Width = 100;

            dgvBookings.Columns["StartDate"].HeaderText = "Trip Start";
            dgvBookings.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvBookings.Columns["StartDate"].Width = 100;

            dgvBookings.Columns["EndDate"].HeaderText = "Trip End";
            dgvBookings.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvBookings.Columns["EndDate"].Width = 100;

            dgvBookings.Columns["NoOfTravelers"].HeaderText = "Travelers";
            dgvBookings.Columns["NoOfTravelers"].Width = 80;

            dgvBookings.Columns["TotalAmount"].HeaderText = "Amount";
            dgvBookings.Columns["TotalAmount"].DefaultCellStyle.Format = "c2";
            dgvBookings.Columns["TotalAmount"].Width = 100;

            dgvBookings.Columns["Status"].HeaderText = "Status";
            dgvBookings.Columns["Status"].Width = 100;

            dgvBookings.Columns["TravelPass"].HeaderText = "Travel Pass";
            dgvBookings.Columns["TravelPass"].Width = 100;

            // Status column conditional formatting
            dgvBookings.CellFormatting += (sender, e) => {
                if (e.ColumnIndex == dgvBookings.Columns["Status"].Index && e.RowIndex >= 0)
                {
                    string status = dgvBookings.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    switch (status)
                    {
                        case "Pending":
                            e.CellStyle.ForeColor = Color.DarkOrange;
                            e.CellStyle.Font = new Font(dgvBookings.Font, FontStyle.Bold);
                            break;
                        case "Confirmed":
                            e.CellStyle.ForeColor = Color.Green;
                            e.CellStyle.Font = new Font(dgvBookings.Font, FontStyle.Bold);
                            break;
                        case "Completed":
                            e.CellStyle.ForeColor = Color.Blue;
                            e.CellStyle.Font = new Font(dgvBookings.Font, FontStyle.Bold);
                            break;
                        case "Cancelled":
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.Font = new Font(dgvBookings.Font, FontStyle.Bold);
                            break;
                    }
                }
            };

            // Set row formatting
            dgvBookings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvBookings.EnableHeadersVisualStyles = false;
            dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBookings.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvBookings.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvBookings.ReadOnly = true;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Handle selection changed
            dgvBookings.SelectionChanged += DgvBookings_SelectionChanged;
        }

        private void UpdateBookingStats()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) AS PendingCount,
                            SUM(CASE WHEN Status = 'Confirmed' THEN 1 ELSE 0 END) AS ConfirmedCount,
                            SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedCount,
                            SUM(CASE WHEN Status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledCount,
                            COUNT(*) AS TotalCount,
                            SUM(TotalAmount) AS TotalSpent
                        FROM BOOKING
                        WHERE TravelerID = @TravelerID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblPendingCount.Text = reader["PendingCount"].ToString();
                            lblConfirmedCount.Text = reader["ConfirmedCount"].ToString();
                            lblCompletedCount.Text = reader["CompletedCount"].ToString();
                            lblCancelledCount.Text = reader["CancelledCount"].ToString();
                            lblTotalCount.Text = reader["TotalCount"].ToString();

                            decimal totalSpent = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("TotalSpent")))
                            {
                                totalSpent = reader.GetDecimal(reader.GetOrdinal("TotalSpent"));
                            }
                            lblTotalSpent.Text = totalSpent.ToString("C2");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating statistics: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvBookings_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvBookings.SelectedRows.Count > 0;

            btnViewDetails.Enabled = hasSelection;
            btnViewPass.Enabled = false;
            btnCancelBooking.Enabled = false;

            if (hasSelection)
            {
                DataGridViewRow row = dgvBookings.SelectedRows[0];
                string status = row.Cells["Status"].Value.ToString();
                string travelPass = row.Cells["TravelPass"].Value.ToString();

                // Only allow cancellation for Pending or Confirmed bookings
                btnCancelBooking.Enabled = (status == "Pending" || status == "Confirmed");

                // Only enable travel pass button if one is available
                btnViewPass.Enabled = (travelPass == "Available");

                // Update selected booking info panel
                string tripTitle = row.Cells["TripTitle"].Value.ToString();
                string destination = row.Cells["Destination"].Value.ToString();
                DateTime startDate = Convert.ToDateTime(row.Cells["StartDate"].Value);
                DateTime endDate = Convert.ToDateTime(row.Cells["EndDate"].Value);

                lblSelectedTrip.Text = tripTitle;
                lblSelectedDestination.Text = destination;
                lblSelectedDates.Text = $"{startDate:dd MMM yyyy} - {endDate:dd MMM yyyy}";
                lblSelectedStatus.Text = status;

                // Change status label color based on status
                switch (status)
                {
                    case "Pending":
                        lblSelectedStatus.ForeColor = Color.DarkOrange;
                        break;
                    case "Confirmed":
                        lblSelectedStatus.ForeColor = Color.Green;
                        break;
                    case "Completed":
                        lblSelectedStatus.ForeColor = Color.Blue;
                        break;
                    case "Cancelled":
                        lblSelectedStatus.ForeColor = Color.Red;
                        break;
                    default:
                        lblSelectedStatus.ForeColor = Color.Black;
                        break;
                }
            }
            else
            {
                // Clear selected booking info
                lblSelectedTrip.Text = "-";
                lblSelectedDestination.Text = "-";
                lblSelectedDates.Text = "-";
                lblSelectedStatus.Text = "-";
                lblSelectedStatus.ForeColor = Color.Black;
            }
        }

        #region Button Click Handlers
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            string statusFilter = cboStatus.SelectedItem.ToString();
            if (statusFilter == "All Statuses") statusFilter = null;

            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            LoadBookings(statusFilter, startDate, endDate);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Now.AddMonths(-3);
            dtpEndDate.Value = DateTime.Now.AddMonths(6);
            LoadBookings();
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["BookingID"].Value);
                int tripId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["TripID"].Value);

                // Open the BookingDetailsForm
                BookingDetailsForm detailsForm = new BookingDetailsForm(bookingId, tripId, travelerId);
                detailsForm.ShowDialog();

                // Refresh data after returning
                LoadBookings(
                    cboStatus.SelectedItem.ToString() == "All Statuses" ? null : cboStatus.SelectedItem.ToString(),
                    dtpStartDate.Value,
                    dtpEndDate.Value
                );
            }
        }

        private void BtnCancelBooking_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBookings.SelectedRows[0];
                string status = row.Cells["Status"].Value.ToString();

                if (status == "Pending" || status == "Confirmed")
                {
                    int bookingId = Convert.ToInt32(row.Cells["BookingID"].Value);
                    string tripTitle = row.Cells["TripTitle"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        $"Are you sure you want to cancel your booking for '{tripTitle}'?\nThis action cannot be undone.",
                        "Confirm Cancellation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Show cancellation reason form
                        CancellationReasonForm reasonForm = new CancellationReasonForm();
                        if (reasonForm.ShowDialog() == DialogResult.OK)
                        {
                            string cancellationReason = reasonForm.CancellationReason;
                            CancelBooking(bookingId, cancellationReason);
                        }
                    }
                }
            }
        }

        private void CancelBooking(int bookingId, string cancellationReason)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Start a transaction
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Update booking status
                        string updateBookingQuery = @"
                            UPDATE BOOKING
                            SET Status = 'Cancelled', CancellationReason = @Reason
                            WHERE BookingID = @BookingID";

                        SqlCommand updateBookingCmd = new SqlCommand(updateBookingQuery, connection, transaction);
                        updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingId);
                        updateBookingCmd.Parameters.AddWithValue("@Reason", cancellationReason);
                        updateBookingCmd.ExecuteNonQuery();

                        // Update payment record if it exists
                        string updatePaymentQuery = @"
                            UPDATE PAYMENT
                            SET Status = 'Refunded', RefundAmount = Amount
                            WHERE BookingID = @BookingID AND Status = 'Completed'";

                        SqlCommand updatePaymentCmd = new SqlCommand(updatePaymentQuery, connection, transaction);
                        updatePaymentCmd.Parameters.AddWithValue("@BookingID", bookingId);
                        updatePaymentCmd.ExecuteNonQuery();

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Your booking has been successfully cancelled.",
                            "Cancellation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the bookings grid
                        LoadBookings(
                            cboStatus.SelectedItem.ToString() == "All Statuses" ? null : cboStatus.SelectedItem.ToString(),
                            dtpStartDate.Value,
                            dtpEndDate.Value
                        );
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction if an error occurred
                        transaction.Rollback();
                        throw new Exception("Failed to cancel booking: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewPass_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["BookingID"].Value);
                string travelPass = dgvBookings.SelectedRows[0].Cells["TravelPass"].Value.ToString();

                if (travelPass == "Available")
                {
                    //// Open the TravelPassForm
                    TravelPassForm passForm = new TravelPassForm(bookingId);
                    passForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No travel pass is available for this booking.",
                        "No Travel Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }

    // Helper form for collecting cancellation reason
    public class CancellationReasonForm : Form
    {
        private TextBox txtReason;
        private Button btnSubmit;
        private Button btnCancel;
        private Label lblPrompt;

        public string CancellationReason { get; private set; }

        public CancellationReasonForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrompt.Location = new System.Drawing.Point(12, 20);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(393, 21);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Text = "Please provide a reason for the cancellation:";
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(16, 53);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(456, 100);
            this.txtReason.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(235, 168);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 35);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(361, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CancellationReasonForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 221);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblPrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CancellationReasonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cancellation Reason";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason for cancellation.",
                    "Reason Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CancellationReason = txtReason.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}