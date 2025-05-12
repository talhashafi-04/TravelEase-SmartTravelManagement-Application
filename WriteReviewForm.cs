using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Linq;

namespace TravelEase
{
    public partial class WriteReviewForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private string travelerId;
        private int? reviewId; // null for new review, value for editing
        private string selectedReviewType = "";
        private int selectedServiceId = 0;
        private bool isEditMode = false;

        // Constructor for new review
        public WriteReviewForm(string travelerId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            ConfigureUIElements();
            SetupNewReview();
        }

        public WriteReviewForm(string travelerId, int s_id , string s_t)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            selectedServiceId = s_id;

            ConfigureUIElements();
            SetupNewReview();
        }


        // Constructor for editing existing review
        public WriteReviewForm(string travelerId, int reviewId, string reviewType, int serviceId)
        {
            InitializeComponent();
            this.travelerId = travelerId;
            this.reviewId = reviewId;
            this.selectedReviewType = reviewType;
            this.selectedServiceId = serviceId;
            this.isEditMode = true;
            ConfigureUIElements();
            SetupEditReview();
        }

        private void ConfigureUIElements()
        {
            // Set colors
            panelHeader.BackColor = Color.FromArgb(41, 128, 185);
            btnSubmit.BackColor = Color.FromArgb(46, 204, 113);
            btnCancel.BackColor = Color.FromArgb(231, 76, 60);

            // Apply rounded corners
            btnSubmit.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSubmit.Width, btnSubmit.Height, 10, 10));
            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 10, 10));

            // Configure star rating
            ConfigureStarRating();

            // Set visibility options
            cmbVisibility.Items.AddRange(new string[] { "Public", "Private" });
            cmbVisibility.SelectedIndex = 0;

            // Hide additional rating panels by default
            panelTripRating.Visible = false;
            panelHotelRating.Visible = false;
            panelGuideRating.Visible = false;
            panelTransportRating.Visible = false;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void ConfigureStarRating()
        {
            // Set star labels for main rating
            Label[] stars = { lblStar1, lblStar2, lblStar3, lblStar4, lblStar5 };
            foreach (Label star in stars)
            {
                star.Text = "☆";
                star.Font = new Font("Century Gothic", 20, FontStyle.Bold);
                star.ForeColor = Color.FromArgb(255, 199, 0);
                star.Cursor = Cursors.Hand;
                star.Click += Star_Click;
                star.MouseEnter += Star_MouseEnter;
                star.MouseLeave += Star_MouseLeave;
            }
        }

        private void SetupNewReview()
        {
            lblTitle.Text = "Write a Review";
            btnSubmit.Text = "Submit Review";

            // Load service types
            cmbReviewType.Items.AddRange(new string[] { "Trip", "Hotel", "Guide", "Transport" });
            cmbReviewType.SelectedIndex = -1;
            cmbReviewType.SelectedIndexChanged += CmbReviewType_SelectedIndexChanged;
        }

        private void SetupEditReview()
        {
            lblTitle.Text = "Edit Review";
            btnSubmit.Text = "Update Review";

            // Disable type selection for editing
            cmbReviewType.Items.Add(selectedReviewType);
            cmbReviewType.SelectedIndex = 0;
            cmbReviewType.Enabled = false;

            // Load service
            LoadService(selectedReviewType, selectedServiceId);

            // Load existing review data
            LoadReviewData();
        }

        private void CmbReviewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedReviewType = cmbReviewType.Text;
            LoadServices();
            ShowAdditionalRatingPanel();
        }

        private void LoadServices()
        {
            cmbService.Items.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = GetServiceQuery();

                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int serviceId = Convert.ToInt32(reader["ServiceID"]);
                            string serviceName = reader["ServiceName"].ToString();
                            cmbService.Items.Add(new ComboboxItem { Text = serviceName, Value = serviceId });
                        }
                    }
                }

                if (cmbService.Items.Count > 0)
                {
                    cmbService.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetServiceQuery()
        {
            switch (selectedReviewType)
            {
                case "Trip":
                    return @"SELECT TripID as ServiceID, Title as ServiceName 
                            FROM TRIP 
                            WHERE Status = 'Active'
                            ORDER BY Title";

                case "Hotel":
                    return @"SELECT HotelID as ServiceID, Name as ServiceName 
                            FROM HOTEL 
                            ORDER BY Name";

                case "Guide":
                    return @"SELECT G.GuideID as ServiceID, 
                                   CONCAT(U.FirstName, ' ', U.LastName) as ServiceName
                            FROM GUIDE G
                            INNER JOIN SERVICE_PROVIDER SP ON G.ProviderID = SP.ProviderID
                            INNER JOIN [USER] U ON SP.ProviderID = U.UserID
                            ORDER BY ServiceName";

                case "Transport":
                    return @"SELECT TP.TransportID as ServiceID, 
                                   SP.CompanyName as ServiceName
                            FROM TRANSPORT_PROVIDER TP
                            INNER JOIN SERVICE_PROVIDER SP ON TP.ProviderID = SP.ProviderID
                            ORDER BY ServiceName";

                default:
                    return "";
            }
        }

        private void LoadService(string reviewType, int serviceId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = GetServiceQuery();

                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ServiceID"]);
                            if (id == serviceId)
                            {
                                string serviceName = reader["ServiceName"].ToString();
                                cmbService.Items.Add(new ComboboxItem { Text = serviceName, Value = id });
                                cmbService.SelectedIndex = 0;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReviewData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load basic review data
                    string query = @"SELECT Rating, Comment, Visibility 
                                   FROM REVIEW 
                                   WHERE ReviewID = @ReviewID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UpdateStarRating(Convert.ToInt32(reader["Rating"]));
                            txtComment.Text = reader["Comment"].ToString();
                            cmbVisibility.Text = reader["Visibility"].ToString();
                        }
                    }

                    // Load additional ratings based on review type
                    LoadAdditionalRatings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading review data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAdditionalRatings()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "";

                    switch (selectedReviewType)
                    {
                        case "Trip":
                            query = @"SELECT AccommodationRating, ValueForMoneyRating 
                                    FROM Trip_REVIEW WHERE ReviewID = @ReviewID";
                            break;

                        case "Hotel":
                            query = @"SELECT CleanlinessRating, ServiceRating, ComfortRating 
                                    FROM Hotel_REVIEW WHERE ReviewID = @ReviewID";
                            break;

                        case "Guide":
                            query = @"SELECT KnowledgeRating, ProfessionalRating, LanguageProficiency 
                                    FROM Guide_REVIEW WHERE ReviewID = @ReviewID";
                            break;

                        case "Transport":
                            query = @"SELECT DriverRating, SafetyRating, PunctualityRating 
                                    FROM TRANSPORT_REVIEW WHERE ReviewID = @ReviewID";
                            break;
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ReviewID", reviewId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update UI controls with additional ratings
                                ShowAdditionalRatingPanel();
                                UpdateAdditionalRatings(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading additional ratings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateAdditionalRatings(SqlDataReader reader)
        {
            // Update the numeric up/down controls based on review type
            switch (selectedReviewType)
            {
                case "Trip":
                    nudAccommodation.Value = Convert.ToDecimal(reader["AccommodationRating"]);
                    nudValueForMoney.Value = Convert.ToDecimal(reader["ValueForMoneyRating"]);
                    break;

                case "Hotel":
                    nudCleanliness.Value = Convert.ToDecimal(reader["CleanlinessRating"]);
                    nudService.Value = Convert.ToDecimal(reader["ServiceRating"]);
                    nudComfort.Value = Convert.ToDecimal(reader["ComfortRating"]);
                    break;

                case "Guide":
                    nudKnowledge.Value = Convert.ToDecimal(reader["KnowledgeRating"]);
                    nudProfessional.Value = Convert.ToDecimal(reader["ProfessionalRating"]);
                    nudLanguage.Value = Convert.ToDecimal(reader["LanguageProficiency"]);
                    break;

                case "Transport":
                    nudDriver.Value = Convert.ToDecimal(reader["DriverRating"]);
                    nudSafety.Value = Convert.ToDecimal(reader["SafetyRating"]);
                    nudPunctuality.Value = Convert.ToDecimal(reader["PunctualityRating"]);
                    break;
            }
        }

        private void ShowAdditionalRatingPanel()
        {
            // Hide all panels first
            panelTripRating.Visible = false;
            panelHotelRating.Visible = false;
            panelGuideRating.Visible = false;
            panelTransportRating.Visible = false;

            // Show relevant panel
            switch (selectedReviewType)
            {
                case "Trip":
                    panelTripRating.Visible = true;
                    break;
                case "Hotel":
                    panelHotelRating.Visible = true;
                    break;
                case "Guide":
                    panelGuideRating.Visible = true;
                    break;
                case "Transport":
                    panelTransportRating.Visible = true;
                    break;
            }
        }

        private void Star_Click(object sender, EventArgs e)
        {
            Label clickedStar = sender as Label;
            int rating = Convert.ToInt32(clickedStar.Tag);
            UpdateStarRating(rating);
        }

        private void Star_MouseEnter(object sender, EventArgs e)
        {
            Label hoveredStar = sender as Label;
            int rating = Convert.ToInt32(hoveredStar.Tag);
            ShowHoverRating(rating);
        }

        private void Star_MouseLeave(object sender, EventArgs e)
        {
            ShowHoverRating(GetCurrentRating());
        }

        private void UpdateStarRating(int rating)
        {
            Label[] stars = { lblStar1, lblStar2, lblStar3, lblStar4, lblStar5 };

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Text = i < rating ? "★" : "☆";
                stars[i].ForeColor = i < rating ? Color.FromArgb(255, 199, 0) : Color.Gray;
            }
        }

        private void ShowHoverRating(int rating)
        {
            Label[] stars = { lblStar1, lblStar2, lblStar3, lblStar4, lblStar5 };

            for (int i = 0; i < stars.Length; i++)
            {
                if (i < rating)
                {
                    stars[i].Text = "★";
                    stars[i].ForeColor = Color.FromArgb(255, 199, 0);
                }
            }
        }

        private int GetCurrentRating()
        {
            Label[] stars = { lblStar1, lblStar2, lblStar3, lblStar4, lblStar5 };
            int rating = 0;

            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i].Text == "★")
                    rating++;
            }

            return rating;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            try
            {
                if (isEditMode)
                {
                    UpdateReview();
                }
                else
                {
                    CreateNewReview();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (cmbReviewType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a review type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbService.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a service to review.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (GetCurrentRating() == 0)
            {
                MessageBox.Show("Please select a rating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Please write a comment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void CreateNewReview()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    ComboboxItem selectedService = cmbService.SelectedItem as ComboboxItem;
                    selectedServiceId = selectedService.Value;

                    // Insert base review
                    string reviewQuery = @"INSERT INTO REVIEW 
                                         (Rating, Comment, Date, Visibility, ReportedFlag, ApprovalStatus, TravelerID)
                                         VALUES (@Rating, @Comment, GETDATE(), @Visibility, 0, 'Pending', @TravelerID);
                                         SELECT SCOPE_IDENTITY();";

                    SqlCommand reviewCommand = new SqlCommand(reviewQuery, connection, transaction);
                    reviewCommand.Parameters.AddWithValue("@Rating", GetCurrentRating());
                    reviewCommand.Parameters.AddWithValue("@Comment", txtComment.Text.Trim());
                    reviewCommand.Parameters.AddWithValue("@Visibility", cmbVisibility.Text);
                    reviewCommand.Parameters.AddWithValue("@TravelerID", travelerId);

                    int newReviewId = Convert.ToInt32(reviewCommand.ExecuteScalar());

                    // Insert type-specific review
                    InsertTypeSpecificReview(newReviewId, connection, transaction);

                    transaction.Commit();
                    MessageBox.Show("Review submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private void UpdateReview()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Update base review
                    string reviewQuery = @"UPDATE REVIEW 
                                         SET Rating = @Rating, 
                                             Comment = @Comment, 
                                             Visibility = @Visibility,
                                             ApprovalStatus = 'Pending'
                                         WHERE ReviewID = @ReviewID";

                    SqlCommand reviewCommand = new SqlCommand(reviewQuery, connection, transaction);
                    reviewCommand.Parameters.AddWithValue("@Rating", GetCurrentRating());
                    reviewCommand.Parameters.AddWithValue("@Comment", txtComment.Text.Trim());
                    reviewCommand.Parameters.AddWithValue("@Visibility", cmbVisibility.Text);
                    reviewCommand.Parameters.AddWithValue("@ReviewID", reviewId);

                    reviewCommand.ExecuteNonQuery();

                    // Update type-specific review
                    UpdateTypeSpecificReview(connection, transaction);

                    transaction.Commit();
                    MessageBox.Show("Review updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        private void InsertTypeSpecificReview(int reviewId, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "";
            SqlCommand command = null;

            switch (selectedReviewType)
            {
                case "Trip":
                    query = @"INSERT INTO Trip_REVIEW (ReviewID, TripID, AccommodationRating, ValueForMoneyRating)
                             VALUES (@ReviewID, @ServiceID, @AccommodationRating, @ValueForMoneyRating)";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@ServiceID", selectedServiceId);
                    command.Parameters.AddWithValue("@AccommodationRating", nudAccommodation.Value);
                    command.Parameters.AddWithValue("@ValueForMoneyRating", nudValueForMoney.Value);
                    break;

                case "Hotel":
                    query = @"INSERT INTO Hotel_REVIEW (ReviewID, HotelID, CleanlinessRating, ServiceRating, ComfortRating)
                             VALUES (@ReviewID, @ServiceID, @CleanlinessRating, @ServiceRating, @ComfortRating)";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@ServiceID", selectedServiceId);
                    command.Parameters.AddWithValue("@CleanlinessRating", nudCleanliness.Value);
                    command.Parameters.AddWithValue("@ServiceRating", nudService.Value);
                    command.Parameters.AddWithValue("@ComfortRating", nudComfort.Value);
                    break;

                case "Guide":
                    query = @"INSERT INTO Guide_REVIEW (ReviewID, GuideID, KnowledgeRating, ProfessionalRating, LanguageProficiency)
                             VALUES (@ReviewID, @ServiceID, @KnowledgeRating, @ProfessionalRating, @LanguageProficiency)";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@ServiceID", selectedServiceId);
                    command.Parameters.AddWithValue("@KnowledgeRating", nudKnowledge.Value);
                    command.Parameters.AddWithValue("@ProfessionalRating", nudProfessional.Value);
                    command.Parameters.AddWithValue("@LanguageProficiency", nudLanguage.Value);
                    break;

                case "Transport":
                    query = @"INSERT INTO TRANSPORT_REVIEW (ReviewID, TransportID, DriverRating, SafetyRating, PunctualityRating)
                             VALUES (@ReviewID, @ServiceID, @DriverRating, @SafetyRating, @PunctualityRating)";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@ServiceID", selectedServiceId);
                    command.Parameters.AddWithValue("@DriverRating", nudDriver.Value);
                    command.Parameters.AddWithValue("@SafetyRating", nudSafety.Value);
                    command.Parameters.AddWithValue("@PunctualityRating", nudPunctuality.Value);
                    break;
            }

            if (command != null)
            {
                command.ExecuteNonQuery();
            }
        }

        private void UpdateTypeSpecificReview(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "";
            SqlCommand command = null;

            switch (selectedReviewType)
            {
                case "Trip":
                    query = @"UPDATE Trip_REVIEW 
                             SET AccommodationRating = @AccommodationRating, 
                                 ValueForMoneyRating = @ValueForMoneyRating
                             WHERE ReviewID = @ReviewID";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@AccommodationRating", nudAccommodation.Value);
                    command.Parameters.AddWithValue("@ValueForMoneyRating", nudValueForMoney.Value);
                    break;

                case "Hotel":
                    query = @"UPDATE Hotel_REVIEW 
                             SET CleanlinessRating = @CleanlinessRating, 
                                 ServiceRating = @ServiceRating, 
                                 ComfortRating = @ComfortRating
                             WHERE ReviewID = @ReviewID";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@CleanlinessRating", nudCleanliness.Value);
                    command.Parameters.AddWithValue("@ServiceRating", nudService.Value);
                    command.Parameters.AddWithValue("@ComfortRating", nudComfort.Value);
                    break;

                case "Guide":
                    query = @"UPDATE Guide_REVIEW 
                             SET KnowledgeRating = @KnowledgeRating, 
                                 ProfessionalRating = @ProfessionalRating, 
                                 LanguageProficiency = @LanguageProficiency
                             WHERE ReviewID = @ReviewID";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@KnowledgeRating", nudKnowledge.Value);
                    command.Parameters.AddWithValue("@ProfessionalRating", nudProfessional.Value);
                    command.Parameters.AddWithValue("@LanguageProficiency", nudLanguage.Value);
                    break;

                case "Transport":
                    query = @"UPDATE TRANSPORT_REVIEW 
                             SET DriverRating = @DriverRating, 
                                 SafetyRating = @SafetyRating, 
                                 PunctualityRating = @PunctualityRating
                             WHERE ReviewID = @ReviewID";
                    command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@ReviewID", reviewId);
                    command.Parameters.AddWithValue("@DriverRating", nudDriver.Value);
                    command.Parameters.AddWithValue("@SafetyRating", nudSafety.Value);
                    command.Parameters.AddWithValue("@PunctualityRating", nudPunctuality.Value);
                    break;
            }

            if (command != null)
            {
                command.ExecuteNonQuery();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Helper class for combo box items
    public class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}