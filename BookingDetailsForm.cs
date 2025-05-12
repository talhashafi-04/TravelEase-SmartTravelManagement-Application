using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class BookingDetailsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        private int bookingId;
        private int tripId;
        private string travelerId;
        private bool canReview = false;

        public BookingDetailsForm(int bookingId, int tripId, string travelerId)
        {
            InitializeComponent();
            this.bookingId = bookingId;
            this.tripId = tripId;
            this.travelerId = travelerId;
            LoadBookingDetails();
            LoadBookingServices();
            
            LoadPaymentDetails();
            LoadTravelPass();
        }

        private void LoadBookingDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            B.BookingID, 
                            B.Date AS BookingDate,
                            B.Status,
                            B.NoOfTravelers,
                            B.TotalAmount,
                            B.Tax,
                            B.Discount,
                            B.BookingNotes,
                            B.CancellationReason,
                            T.TripID,
                            T.Title AS TripTitle,
                            T.Price AS TripPrice, 
                            T.Description AS TripDescription,
                            T.StartDate, 
                            T.EndDate,
                            T.Difficulty,
                            D.Name AS Destination,
                            D.Country,
                            D.Description AS DestinationDescription,
                            D.Climate,
                            D.Currency,
                            TC.Name AS CategoryName
                        FROM BOOKING B
                        INNER JOIN TRIP T ON B.TripID = T.TripID
                        INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                        INNER JOIN TRIP_CATEGORY TC ON T.CategoryID = TC.CategoryID
                        WHERE B.BookingID = @BookingID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Booking details
                            lblBookingID.Text = reader["BookingID"].ToString();
                            lblBookingDate.Text = Convert.ToDateTime(reader["BookingDate"]).ToString("dd MMMM yyyy, HH:mm");
                            lblBookingStatus.Text = reader["Status"].ToString();
                            lblTravelersCount.Text = reader["NoOfTravelers"].ToString();

                            // Calculate and show cost breakdown
                            decimal tripPrice = Convert.ToDecimal(reader["TripPrice"]);
                            int noOfTravelers = Convert.ToInt32(reader["NoOfTravelers"]);
                            decimal subtotal = tripPrice * noOfTravelers;
                            lblSubtotal.Text = subtotal.ToString("C2");

                            decimal tax = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("Tax")))
                                tax = Convert.ToDecimal(reader["Tax"]);
                            lblTax.Text = tax.ToString("C2");

                            decimal discount = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("Discount")))
                                discount = Convert.ToDecimal(reader["Discount"]);
                            lblDiscount.Text = discount.ToString("C2");

                            decimal totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                            lblTotalAmount.Text = totalAmount.ToString("C2");

                            string bookingNotes = reader.IsDBNull(reader.GetOrdinal("BookingNotes")) ?
                                "No additional notes" : reader["BookingNotes"].ToString();
                            txtBookingNotes.Text = bookingNotes;

                            // Trip details
                            lblTripTitle.Text = reader["TripTitle"].ToString();
                            lblTripDates.Text = $"{Convert.ToDateTime(reader["StartDate"]).ToString("dd MMM yyyy")} - {Convert.ToDateTime(reader["EndDate"]).ToString("dd MMM yyyy")}";
                            lblTripDuration.Text = (Convert.ToDateTime(reader["EndDate"]) - Convert.ToDateTime(reader["StartDate"])).Days.ToString() + " days";
                            lblTripPrice.Text = tripPrice.ToString("C2");
                            lblTripCategory.Text = reader["CategoryName"].ToString();
                            lblTripDifficulty.Text = reader["Difficulty"].ToString();
                            txtTripDescription.Text = reader["TripDescription"].ToString();

                            // Destination details
                            lblDestination.Text = $"{reader["Destination"].ToString()}, {reader["Country"].ToString()}";
                            lblClimate.Text = reader["Climate"].ToString();
                            lblCurrency.Text = reader["Currency"].ToString();
                            txtDestinationDescription.Text = reader["DestinationDescription"].ToString();

                            // Show cancellation reason if available
                            if (!reader.IsDBNull(reader.GetOrdinal("CancellationReason")))
                            {
                                lblCancellationReasonTitle.Visible = true;
                                txtCancellationReason.Visible = true;
                                txtCancellationReason.Text = reader["CancellationReason"].ToString();
                            }
                            else
                            {
                                lblCancellationReasonTitle.Visible = false;
                                txtCancellationReason.Visible = false;
                            }

                            // Style the status label
                            switch (lblBookingStatus.Text)
                            {
                                case "Pending":
                                    lblBookingStatus.ForeColor = Color.DarkOrange;
                                    break;
                                case "Confirmed":
                                    lblBookingStatus.ForeColor = Color.Green;
                                    break;
                                case "Completed":
                                    lblBookingStatus.ForeColor = Color.Blue;
                                    canReview = true;
                                    break;
                                case "Cancelled":
                                    lblBookingStatus.ForeColor = Color.Red;
                                    break;
                            }

                            // Set cancel button visibility based on status
                            btnCancelBooking.Visible = (lblBookingStatus.Text == "Pending" || lblBookingStatus.Text == "Confirmed");

                            // Enable review button if the trip is completed
                            btnWriteReview.Enabled = canReview;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booking details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBookingServices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            S.ServiceID,
                            S.ServiceType,
                            CASE 
                                WHEN S.ServiceType = 'Hotel' THEN H.Name
                                WHEN S.ServiceType = 'Guide' THEN CONCAT('Guide: ', U.FirstName, ' ', U.LastName)
                                WHEN S.ServiceType = 'Transport' THEN CONCAT('Transport: ', TP.ServiceAreas)
                                ELSE 'Unknown'
                            END AS ServiceName,
                            CASE 
                                WHEN S.ServiceType = 'Hotel' THEN H.Description
                                WHEN S.ServiceType = 'Guide' THEN G.Specializations
                                WHEN S.ServiceType = 'Transport' THEN TP.Specializations
                                ELSE ''
                            END AS Description,
                            SP.Rating
                        FROM TRIP_SERVICES_Renrollment TSR
                        JOIN SERVICES S ON TSR.ServiceID = S.ServiceID
                        LEFT JOIN HOTEL H ON S.ServiceID = H.HotelID
                        LEFT JOIN GUIDE G ON S.ServiceID = G.GuideID
                        LEFT JOIN TRANSPORT_PROVIDER TP ON S.ServiceID = TP.TransportID
                        LEFT JOIN SERVICE_PROVIDER SP ON (H.ProviderID = SP.ProviderID OR G.ProviderID = SP.ProviderID OR TP.ProviderID = SP.ProviderID)
                        LEFT JOIN [USER] U ON SP.ProviderID = U.UserID
                        WHERE TSR.TripID = @TripID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TripID", tripId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable servicesTable = new DataTable();
                    adapter.Fill(servicesTable);

                    dgvServices.DataSource = servicesTable;
                    FormatServicesGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip services: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void BtnViewPass_Click(object sender, EventArgs e)
        {
            try
            {
                TravelPassForm passForm = new TravelPassForm(bookingId);
                passForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening travel pass: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgvServices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get service information
                int serviceId = Convert.ToInt32(dgvServices.Rows[e.RowIndex].Cells["ServiceID"].Value);
                string serviceType = dgvServices.Rows[e.RowIndex].Cells["ServiceType"].Value.ToString();
                string serviceName = dgvServices.Rows[e.RowIndex].Cells["ServiceName"].Value.ToString();

                // Check if the booking is completed to allow reviews
                if (canReview)
                {
                    // Confirm if user wants to write a review
                    DialogResult result = MessageBox.Show(
                        $"Would you like to write a review for this {serviceType}?",
                        "Write Review",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        WriteReviewForm reviewForm = new WriteReviewForm(travelerId, serviceId, serviceType);
                        reviewForm.ShowDialog();
                    }
                }
                else
                {
                    // Show service details in a messagebox
                    MessageBox.Show(
                        $"Service: {serviceName}\n\nType: {serviceType}\n\nRating: {dgvServices.Rows[e.RowIndex].Cells["Rating"].Value}\n\nDescription: {dgvServices.Rows[e.RowIndex].Cells["Description"].Value}",
                        "Service Details",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void BtnWriteReview_Click(object sender, EventArgs e)
        {
            if (canReview)
            {
                try
                {
                    // Open review form for the trip
                   // WriteReviewForm reviewForm = new WriteReviewForm(travelerId, tripId, "Trip");
                   // reviewForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening review form: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void FormatServicesGrid()
        {
            dgvServices.Columns["ServiceID"].Visible = false;

            dgvServices.Columns["ServiceType"].HeaderText = "Type";
            dgvServices.Columns["ServiceType"].Width = 100;

            dgvServices.Columns["ServiceName"].HeaderText = "Name";
            dgvServices.Columns["ServiceName"].Width = 200;

            dgvServices.Columns["Description"].HeaderText = "Description";
            dgvServices.Columns["Description"].Width = 300;

            dgvServices.Columns["Rating"].HeaderText = "Rating";
            dgvServices.Columns["Rating"].Width = 80;
            dgvServices.Columns["Rating"].DefaultCellStyle.Format = "0.00";

            // Apply formatting
            dgvServices.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvServices.EnableHeadersVisualStyles = false;
            dgvServices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServices.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvServices.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvServices.ReadOnly = true;
            dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadPaymentDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            PaymentID,
                            Amount,
                            Date AS PaymentDate,
                            Status,
                            TransactionID,
                            Method,
                            BillingAddress,
                            FailureReason,
                            RefundAmount
                        FROM PAYMENT
                        WHERE BookingID = @BookingID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblPaymentStatus.Text = reader["Status"].ToString();
                            lblPaymentAmount.Text = Convert.ToDecimal(reader["Amount"]).ToString("C2");
                            lblPaymentDate.Text = Convert.ToDateTime(reader["PaymentDate"]).ToString("dd MMM yyyy, HH:mm");
                            lblPaymentMethod.Text = reader["Method"].ToString();

                            string transactionId = reader.IsDBNull(reader.GetOrdinal("TransactionID")) ?
                                "N/A" : reader["TransactionID"].ToString();
                            lblTransactionID.Text = transactionId;

                            // Style the payment status label
                            switch (lblPaymentStatus.Text)
                            {
                                case "Pending":
                                    lblPaymentStatus.ForeColor = Color.DarkOrange;
                                    break;
                                case "Completed":
                                    lblPaymentStatus.ForeColor = Color.Green;
                                    break;
                                case "Failed":
                                    lblPaymentStatus.ForeColor = Color.Red;
                                    break;
                                case "Refunded":
                                    lblPaymentStatus.ForeColor = Color.Blue;

                                    // Show refund information
                                    lblRefundAmountTitle.Visible = true;
                                    lblRefundAmount.Visible = true;
                                    lblRefundAmount.Text = reader.IsDBNull(reader.GetOrdinal("RefundAmount")) ?
                                        "$0.00" : Convert.ToDecimal(reader["RefundAmount"]).ToString("C2");
                                    break;
                            }

                            // Show failure reason if payment failed
                            if (lblPaymentStatus.Text == "Failed" && !reader.IsDBNull(reader.GetOrdinal("FailureReason")))
                            {
                                lblFailureReasonTitle.Visible = true;
                                lblFailureReason.Visible = true;
                                lblFailureReason.Text = reader["FailureReason"].ToString();
                            }
                            else
                            {
                                lblFailureReasonTitle.Visible = false;
                                lblFailureReason.Visible = false;
                            }
                        }
                        else
                        {
                            // No payment record found
                            panelPayment.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payment details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTravelPass()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            Pass_ID,
                            PassType,
                            ExpiryDate
                        FROM TRAVEL_PASS
                        WHERE BookingID = @BookingID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblPassID.Text = reader["Pass_ID"].ToString();
                            lblPassType.Text = reader["PassType"].ToString();

                            DateTime? expiryDate = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("ExpiryDate")))
                            {
                                expiryDate = Convert.ToDateTime(reader["ExpiryDate"]);
                                lblExpiryDate.Text = expiryDate.Value.ToString("dd MMM yyyy");
                            }
                            else
                            {
                                lblExpiryDate.Text = "No expiry";
                            }

                            btnViewPass.Enabled = true;
                        }
                        else
                        {
                            // No travel pass found
                            panelTravelPass.Visible = false;
                            btnViewPass.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading travel pass details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCancelBooking_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to cancel this booking?\nThis action cannot be undone.",
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
                    CancelBooking(cancellationReason);
                }
            }
        }

        private void CancelBooking(string cancellationReason)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            B.BookingID, 
                            B.Date AS BookingDate,
                            B.Status,
                            B.NoOfTravelers,
                            B.TotalAmount,
                            B.Tax,
                            B.Discount,
                            B.BookingNotes,
                            B.CancellationReason,
                            T.TripID,
                            T.Title AS TripTitle,
                            T.Price AS TripPrice, 
                            T.Description AS TripDescription,
                            T.StartDate, 
                            T.EndDate,
                            T.Difficulty,
                            D.Name AS Destination,
                            D.Country,
                            D.Description AS DestinationDescription,
                            D.Climate,
                            D.Currency,
                            TC.Name AS CategoryName
                        FROM BOOKING B
                        INNER JOIN TRIP T ON B.TripID = T.TripID
                        INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                        INNER JOIN TRIP_CATEGORY TC ON T.CategoryID = TC.CategoryID
                        WHERE B.BookingID = @BookingID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Booking details
                            lblBookingID.Text = reader["BookingID"].ToString();
                            lblBookingDate.Text = Convert.ToDateTime(reader["BookingDate"]).ToString("dd MMMM yyyy, HH:mm");
                            lblBookingStatus.Text = reader["Status"].ToString();
                            lblTravelersCount.Text = reader["NoOfTravelers"].ToString();

                            // Calculate and show cost breakdown
                            decimal tripPrice = Convert.ToDecimal(reader["TripPrice"]);
                            int noOfTravelers = Convert.ToInt32(reader["NoOfTravelers"]);
                            decimal subtotal = tripPrice * noOfTravelers;
                            lblSubtotal.Text = subtotal.ToString("C2");

                            decimal tax = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("Tax")))
                                tax = Convert.ToDecimal(reader["Tax"]);
                            lblTax.Text = tax.ToString("C2");

                            decimal discount = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("Discount")))
                                discount = Convert.ToDecimal(reader["Discount"]);
                            lblDiscount.Text = discount.ToString("C2");

                            decimal totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                            lblTotalAmount.Text = totalAmount.ToString("C2");

                            string bookingNotes = reader.IsDBNull(reader.GetOrdinal("BookingNotes")) ?
                                "No additional notes" : reader["BookingNotes"].ToString();
                            txtBookingNotes.Text = bookingNotes;

                            // Trip details
                            lblTripTitle.Text = reader["TripTitle"].ToString();
                            lblTripDates.Text = $"{Convert.ToDateTime(reader["StartDate"]).ToString("dd MMM yyyy")} - {Convert.ToDateTime(reader["EndDate"]).ToString("dd MMM yyyy")}";
                            lblTripDuration.Text = (Convert.ToDateTime(reader["EndDate"]) - Convert.ToDateTime(reader["StartDate"])).Days.ToString() + " days";
                            lblTripPrice.Text = tripPrice.ToString("C2");
                            lblTripCategory.Text = reader["CategoryName"].ToString();
                            lblTripDifficulty.Text = reader["Difficulty"].ToString();
                            txtTripDescription.Text = reader["TripDescription"].ToString();

                            // Destination details
                            lblDestination.Text = $"{reader["Destination"].ToString()}, {reader["Country"].ToString()}";
                            lblClimate.Text = reader["Climate"].ToString();
                            lblCurrency.Text = reader["Currency"].ToString();
                            txtDestinationDescription.Text = reader["DestinationDescription"].ToString();

                            // Show cancellation reason if available
                            if (!reader.IsDBNull(reader.GetOrdinal("CancellationReason")))
                            {
                                lblCancellationReasonTitle.Visible = true;
                                txtCancellationReason.Visible = true;
                                txtCancellationReason.Text = reader["CancellationReason"].ToString();
                            }
                            else
                            {
                                lblCancellationReasonTitle.Visible = false;
                                txtCancellationReason.Visible = false;
                            }

                            // Style the status label
                            switch (lblBookingStatus.Text)
                            {
                                case "Pending":
                                    lblBookingStatus.ForeColor = Color.DarkOrange;
                                    break;
                                case "Confirmed":
                                    lblBookingStatus.ForeColor = Color.Green;
                                    break;
                                case "Completed":
                                    lblBookingStatus.ForeColor = Color.Blue;
                                    canReview = true;
                                    break;
                                case "Cancelled":
                                    lblBookingStatus.ForeColor = Color.Red;
                                    break;
                            }

                            // Set cancel button visibility based on status
                            btnCancelBooking.Visible = (lblBookingStatus.Text == "Pending" || lblBookingStatus.Text == "Confirmed");

                            // Enable review button if the trip is completed
                            btnWriteReview.Enabled = canReview;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booking details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}











