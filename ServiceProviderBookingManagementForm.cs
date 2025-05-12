using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;


namespace DatabaseProject
{
    public partial class BookingManagementFormService : Form
    {
        private DataGridView dgvBookings;
        private ComboBox cmbStatusFilter;
        private Label lblStatus, lblFrom, lblTo, lblTripFilter;
        private DateTimePicker dtpFrom, dtpTo;
        private ComboBox cmbTripFilter;
        private Button btnFilter, btnDetails;

        public BookingManagementFormService()
        {
            InitializeComponents();
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
            string status = cmbStatusFilter.SelectedItem.ToString();
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;
            int? tripId = cmbTripFilter.SelectedIndex > 0 ? (int?)cmbTripFilter.SelectedValue : null;

            // TODO: Replace with actual ADO.NET call:
            // DataTable dt = BookingRepository.GetBookingsForOperator(currentOperatorId, status, from, to, tripId);
            // dgvBookings.DataSource = dt;
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
            // Optionally refresh after details
            LoadBookings();
        }
    }
}
