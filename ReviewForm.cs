using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class ReviewsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";
        private string travelerId;
        private string selectedReviewType = "All";
        private int selectedReviewId = 0;

        public ReviewsForm(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            ConfigureUIElements();
            LoadReviews();
        }

        private void ConfigureUIElements()
        {
            // Set colors
            panelHeader.BackColor = Color.FromArgb(41, 128, 185);
            panelFilter.BackColor = Color.FromArgb(245, 245, 245);
            btnWriteReview.BackColor = Color.FromArgb(46, 204, 113);
            btnEditReview.BackColor = Color.FromArgb(52, 152, 219);
            btnDeleteReview.BackColor = Color.FromArgb(231, 76, 60);

            // Apply rounded corners
            btnWriteReview.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnWriteReview.Width, btnWriteReview.Height, 10, 10));
            btnEditReview.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnEditReview.Width, btnEditReview.Height, 10, 10));
            btnDeleteReview.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDeleteReview.Width, btnDeleteReview.Height, 10, 10));

            // Configure DataGridView
            dgvReviews.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvReviews.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvReviews.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReviews.EnableHeadersVisualStyles = false;
            dgvReviews.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReviews.ReadOnly = true;

            // Set up buttons
            btnEditReview.Enabled = false;
            btnDeleteReview.Enabled = false;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void LoadReviews()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = BuildReviewQuery();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable reviewsTable = new DataTable();
                    adapter.Fill(reviewsTable);

                    dgvReviews.DataSource = reviewsTable;
                    FormatReviewGrid();
                    UpdateStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildReviewQuery()
        {
            string baseQuery = @"
                SELECT 
                    R.ReviewID,
                    R.Rating,
                    R.Comment,
                    R.Date,
                    R.Visibility,
                    R.ApprovalStatus,
                    R.ResponseText,
                    R.ResponseDate,
                    CASE 
                        WHEN TR.ReviewID IS NOT NULL THEN 'Trip'
                        WHEN HR.ReviewID IS NOT NULL THEN 'Hotel'
                        WHEN GR.ReviewID IS NOT NULL THEN 'Guide'
                        WHEN TrR.ReviewID IS NOT NULL THEN 'Transport'
                    END AS ReviewType,
                    COALESCE(T.Title, H.Name, 
                            CONCAT(GU.FirstName, ' ', GU.LastName),
                            SP.CompanyName) AS ServiceName
                FROM REVIEW R
                LEFT JOIN Trip_REVIEW TR ON R.ReviewID = TR.ReviewID
                LEFT JOIN TRIP T ON TR.TripID = T.TripID
                LEFT JOIN Hotel_REVIEW HR ON R.ReviewID = HR.ReviewID
                LEFT JOIN HOTEL H ON HR.HotelID = H.HotelID
                LEFT JOIN Guide_REVIEW GR ON R.ReviewID = GR.ReviewID
                LEFT JOIN GUIDE G ON GR.GuideID = G.GuideID
                LEFT JOIN SERVICE_PROVIDER GP ON G.ProviderID = GP.ProviderID
                LEFT JOIN [USER] GU ON GP.ProviderID = GU.UserID
                LEFT JOIN TRANSPORT_REVIEW TrR ON R.ReviewID = TrR.ReviewID
                LEFT JOIN TRANSPORT_PROVIDER TP ON TrR.TransportID = TP.TransportID
                LEFT JOIN SERVICE_PROVIDER SP ON TP.ProviderID = SP.ProviderID
                WHERE R.TravelerID = @TravelerID";

            if (selectedReviewType != "All")
            {
                switch (selectedReviewType)
                {
                    case "Trip":
                        baseQuery += " AND TR.ReviewID IS NOT NULL";
                        break;
                    case "Hotel":
                        baseQuery += " AND HR.ReviewID IS NOT NULL";
                        break;
                    case "Guide":
                        baseQuery += " AND GR.ReviewID IS NOT NULL";
                        break;
                    case "Transport":
                        baseQuery += " AND TrR.ReviewID IS NOT NULL";
                        break;
                }
            }

            baseQuery += " ORDER BY R.Date DESC";
            return baseQuery;
        }

        private void FormatReviewGrid()
        {
            if (dgvReviews.Columns.Count > 0)
            {
                dgvReviews.Columns["ReviewID"].Visible = false;

                dgvReviews.Columns["ServiceName"].HeaderText = "Service";
                dgvReviews.Columns["ServiceName"].Width = 200;

                dgvReviews.Columns["ReviewType"].HeaderText = "Type";
                dgvReviews.Columns["ReviewType"].Width = 80;

                dgvReviews.Columns["Rating"].HeaderText = "Rating";
                dgvReviews.Columns["Rating"].Width = 80;

                dgvReviews.Columns["Comment"].HeaderText = "Your Review";
                dgvReviews.Columns["Comment"].Width = 300;

                dgvReviews.Columns["Date"].HeaderText = "Review Date";
                dgvReviews.Columns["Date"].Width = 100;
                dgvReviews.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                dgvReviews.Columns["Visibility"].HeaderText = "Visibility";
                dgvReviews.Columns["Visibility"].Width = 80;

                dgvReviews.Columns["ApprovalStatus"].HeaderText = "Status";
                dgvReviews.Columns["ApprovalStatus"].Width = 80;

                dgvReviews.Columns["ResponseText"].HeaderText = "Response";
                dgvReviews.Columns["ResponseText"].Width = 200;

                dgvReviews.Columns["ResponseDate"].HeaderText = "Response Date";
                dgvReviews.Columns["ResponseDate"].Width = 100;
                dgvReviews.Columns["ResponseDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get total reviews count
                    string countQuery = "SELECT COUNT(*) FROM REVIEW WHERE TravelerID = @TravelerID";
                    SqlCommand countCommand = new SqlCommand(countQuery, connection);
                    countCommand.Parameters.AddWithValue("@TravelerID", travelerId);
                    int totalReviews = (int)countCommand.ExecuteScalar();
                    lblTotalReviews.Text = totalReviews.ToString();

                    // Get average rating
                    string avgQuery = "SELECT AVG(Rating) FROM REVIEW WHERE TravelerID = @TravelerID";
                    SqlCommand avgCommand = new SqlCommand(avgQuery, connection);
                    avgCommand.Parameters.AddWithValue("@TravelerID", travelerId);
                    object avgResult = avgCommand.ExecuteScalar();
                    decimal avgRating = avgResult != DBNull.Value ? Convert.ToDecimal(avgResult) : 0;
                    lblAverageRating.Text = avgRating.ToString("F1");

                    // Get pending reviews count
                    string pendingQuery = @"SELECT COUNT(*) FROM REVIEW 
                                          WHERE TravelerID = @TravelerID 
                                          AND ApprovalStatus = 'Pending'";
                    SqlCommand pendingCommand = new SqlCommand(pendingQuery, connection);
                    pendingCommand.Parameters.AddWithValue("@TravelerID", travelerId);
                    int pendingReviews = (int)pendingCommand.ExecuteScalar();
                    lblPendingReviews.Text = pendingReviews.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating statistics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbReviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedReviewType = cmbReviewType.Text;
            LoadReviews();
        }

        private void dgvReviews_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReviews.SelectedRows.Count > 0)
            {
                selectedReviewId = Convert.ToInt32(dgvReviews.SelectedRows[0].Cells["ReviewID"].Value);
                btnEditReview.Enabled = true;
                btnDeleteReview.Enabled = true;
            }
            else
            {
                selectedReviewId = 0;
                btnEditReview.Enabled = false;
                btnDeleteReview.Enabled = false;
            }
        }

        private void btnWriteReview_Click(object sender, EventArgs e)
        {
            WriteReviewForm writeForm = new WriteReviewForm(travelerId);
            if (writeForm.ShowDialog() == DialogResult.OK)
            {
                LoadReviews();
            }
        }

        private void btnEditReview_Click(object sender, EventArgs e)
        {
            if (selectedReviewId > 0)
            {
                string reviewType = dgvReviews.SelectedRows[0].Cells["ReviewType"].Value.ToString();
                int serviceId = GetServiceIdForReview(selectedReviewId, reviewType);

                WriteReviewForm editForm = new WriteReviewForm(travelerId, selectedReviewId, reviewType, serviceId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadReviews();
                }
            }
        }

        private int GetServiceIdForReview(int reviewId, string reviewType)
        {
            int serviceId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "";

                    switch (reviewType)
                    {
                        case "Trip":
                            query = "SELECT TripID FROM Trip_REVIEW WHERE ReviewID = @ReviewID";
                            break;
                        case "Hotel":
                            query = "SELECT HotelID FROM Hotel_REVIEW WHERE ReviewID = @ReviewID";
                            break;
                        case "Guide":
                            query = "SELECT GuideID FROM Guide_REVIEW WHERE ReviewID = @ReviewID";
                            break;
                        case "Transport":
                            query = "SELECT TransportID FROM TRANSPORT_REVIEW WHERE ReviewID = @ReviewID";
                            break;
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ReviewID", reviewId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            serviceId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting service ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return serviceId;
        }

        private void btnDeleteReview_Click(object sender, EventArgs e)
        {
            if (selectedReviewId > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this review?",
                                                    "Confirm Delete",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM REVIEW WHERE ReviewID = @ReviewID";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@ReviewID", selectedReviewId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Review deleted successfully!", "Success",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadReviews();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting review: " + ex.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblPendingReviewsTitle_Click(object sender, EventArgs e)
        {

        }
    }
}