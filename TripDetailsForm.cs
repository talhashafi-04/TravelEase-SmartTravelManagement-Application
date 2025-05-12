using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TravelEase
{
    public partial class TripDetailsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";
        private int tripId;
        private int bookingId;
        private string travelerId;
        private bool isBookedTrip = false;
        private bool isInWishlist = false;

        // Image gallery
        private List<Image> galleryImages = new List<Image>();
        private int currentImageIndex = 0;
        private Timer slideTimer;
        private Button btnPrevImage;
        private Button btnNextImage;
        private Panel panelImageIndicators;

        // Image service
        private readonly ImageService imageService = new ImageService();

        public TripDetailsForm(int tripId, int bookingId, string travelerId)
        {
            InitializeComponent();
            this.tripId = tripId;
            this.bookingId = bookingId;
            this.travelerId = travelerId;
            this.isBookedTrip = bookingId > 0;

            // Set initial panel sizes
            AdjustPanelSizes();

            SetupAwesomeUI();
            ConfigureUIElements();
            LoadData();
        }

        private void AdjustPanelSizes()
        {
            // Set the header panel height
            panelHeader.Height = 600; // Adjust as needed

            // Calculate and set the main panel height
            int mainPanelHeight = 400; // Adjust as needed
            panelMain.Height = mainPanelHeight;

            // Ensure button panel is at the bottom and visible
            panelButtons.Height = 80;
            panelButtons.Dock = DockStyle.Bottom;

            // Set the total form height
            this.Height = panelHeader.Height + panelMain.Height + panelButtons.Height + 150; // Extra padding
        }

        private void SetupAwesomeUI()
        {
            // Set the form to be scrollable with proper dimensions
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1200, 1100);

            // Set the form size but allow it to be resizable
            this.Size = new Size(1200, 800); // Adjust height to screen size
            this.MaximumSize = new Size(0, 0);
           // this.WindowState = FormWindowState.Maximized; // Or use Normal with proper size

            // Setup image gallery controls
            SetupImageGallery();

            // Make the panel over image transparent
            MakeHeaderTransparent();

            // Add smooth scrolling
            EnableSmoothScrolling();
        }

        private void SetupImageGallery()
        {
            // Previous button
            btnPrevImage = new Button();
            btnPrevImage.Size = new Size(50, 50);
            btnPrevImage.Location = new Point(20, 300);
            btnPrevImage.Text = "❮";
            btnPrevImage.Font = new Font("Arial", 20, FontStyle.Bold);
            btnPrevImage.ForeColor = Color.White;
            btnPrevImage.BackColor = Color.FromArgb(100, 0, 0, 0);
            btnPrevImage.FlatStyle = FlatStyle.Flat;
            btnPrevImage.FlatAppearance.BorderSize = 0;
            btnPrevImage.Cursor = Cursors.Hand;
            btnPrevImage.Click += BtnPrevImage_Click;
            panelHeader.Controls.Add(btnPrevImage);
            btnPrevImage.BringToFront();

            // Next button
            btnNextImage = new Button();
            btnNextImage.Size = new Size(50, 50);
            btnNextImage.Location = new Point(1130, 300);
            btnNextImage.Text = "❯";
            btnNextImage.Font = new Font("Arial", 20, FontStyle.Bold);
            btnNextImage.ForeColor = Color.White;
            btnNextImage.BackColor = Color.FromArgb(100, 0, 0, 0);
            btnNextImage.FlatStyle = FlatStyle.Flat;
            btnNextImage.FlatAppearance.BorderSize = 0;
            btnNextImage.Cursor = Cursors.Hand;
            btnNextImage.Click += BtnNextImage_Click;
            panelHeader.Controls.Add(btnNextImage);
            btnNextImage.BringToFront();

            // Image indicators panel
            panelImageIndicators = new Panel();
            panelImageIndicators.Size = new Size(200, 30);
            panelImageIndicators.Location = new Point(-200, 260);
            panelImageIndicators.BackColor = Color.Transparent;
            panelHeader.Controls.Add(panelImageIndicators);
            panelImageIndicators.BringToFront();

            // Auto-slide timer
            slideTimer = new Timer();
            slideTimer.Interval = 5000; // Change image every 5 seconds
            slideTimer.Tick += SlideTimer_Tick;
            slideTimer.Start();
        }

        private void MakeHeaderTransparent()
        {
            // Make the content panel truly transparent
            panelHeaderContent.BackColor = Color.Transparent;

            // Remove any background color
            panelHeaderContent.BackColor = Color.FromArgb(0, 0, 0, 0);

            // Set the panel to support transparency
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Custom paint for semi-transparent overlay
            panelHeaderContent.Paint += (sender, e) =>
            {
                Rectangle rect = panelHeaderContent.ClientRectangle;
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(100, 0, 0, 0), // More transparent at top
                    Color.FromArgb(180, 0, 0, 0), // Less transparent at bottom
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            };

            // Make price panel semi-transparent
            panelPrice.BackColor = Color.FromArgb(240, 255, 255, 255);
            panelPrice.Paint += (sender, e) =>
            {
                Rectangle rect = panelPrice.ClientRectangle;
                using (GraphicsPath path = GetRoundedRectangle(rect, 15))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 255, 255, 255)))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void EnableSmoothScrolling()
        {
            // Enable double buffering for smooth scrolling
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.DoubleBuffer, true);
        }

        private async void LoadData()
        {
            await LoadTripDetails();
            LoadReviews();
            LoadServices();
        }

        private void ConfigureUIElements()
        {
            // Set colors with transparency
            panelHeader.BackColor = Color.Transparent;
            btnBook.BackColor = Color.FromArgb(46, 204, 113);
            btnAddToWishlist.BackColor = Color.FromArgb(52, 152, 219);
            btnWriteReview.BackColor = Color.FromArgb(255, 152, 0);

            // Apply rounded corners
            btnBook.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnBook.Width, btnBook.Height, 15, 15));
            btnAddToWishlist.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAddToWishlist.Width, btnAddToWishlist.Height, 15, 15));
            btnWriteReview.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnWriteReview.Width, btnWriteReview.Height, 15, 15));

            // Configure review DataGridView
            ConfigureReviewGrid();

            // Configure service DataGridView
            ConfigureServiceGrid();

            // Check if trip is in wishlist
            CheckWishlistStatus();

            // Show appropriate buttons
            if (isBookedTrip)
            {
                btnBook.Visible = false;
                btnAddToWishlist.Visible = false;
                btnWriteReview.Visible = true;
            }
            else
            {
                btnBook.Visible = true;
                btnAddToWishlist.Visible = true;
                btnWriteReview.Visible = false;
            }

            // Add hover effects
            AddHoverEffects();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private async Task LoadTripDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            T.Title,
                            T.Price,
                            T.Duration_Days,
                            T.Description,
                            T.StartDate,
                            T.EndDate,
                            T.Status,
                            T.Difficulty,
                            T.Capacity,
                            D.DestinationID,
                            D.Name AS DestinationName,
                            D.Country,
                            D.Region,
                            D.Climate,
                            D.Currency,
                            D.Language,
                            D.Description AS DestinationDescription,
                            D.BestVisitTime,
                            D.ImageURL,
                            TC.Name AS CategoryName,
                            TC.Description AS CategoryDescription,
                            (SELECT AVG(R.Rating) FROM REVIEW R 
                             INNER JOIN Trip_REVIEW TR ON R.ReviewID = TR.ReviewID 
                             WHERE TR.TripID = T.TripID) as AverageRating,
                            (SELECT COUNT(*) FROM REVIEW R 
                             INNER JOIN Trip_REVIEW TR ON R.ReviewID = TR.ReviewID 
                             WHERE TR.TripID = T.TripID) as TotalReviews
                        FROM TRIP T
                        INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                        INNER JOIN TRIP_CATEGORY TC ON T.CategoryID = TC.CategoryID
                        WHERE T.TripID = @TripID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TripID", tripId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Header Information
                            lblTripTitle.Text = reader["Title"].ToString();
                            lblDestination.Text = $"{reader["DestinationName"]}, {reader["Country"]}";
                            lblCategory.Text = reader["CategoryName"].ToString();

                            // Pricing
                            lblPrice.Text = $"${Convert.ToDecimal(reader["Price"]):N2}";
                            lblPriceSubtext.Text = "per person";

                            // Duration and Dates
                            lblDuration.Text = $"{reader["Duration_Days"]} Days";
                            DateTime startDate = Convert.ToDateTime(reader["StartDate"]);
                            DateTime endDate = Convert.ToDateTime(reader["EndDate"]);
                            lblDates.Text = $"{startDate:dd MMM yyyy} - {endDate:dd MMM yyyy}";

                            // Trip Info
                            lblDifficulty.Text = reader["Difficulty"].ToString();
                            lblCapacity.Text = $"{reader["Capacity"]} people";
                            lblStatus.Text = reader["Status"].ToString();

                            // Color code status
                            if (reader["Status"].ToString() == "Active")
                            {
                                lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
                            }
                            else
                            {
                                lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                            }

                            // Description
                            txtDescription.Text = reader["Description"].ToString();

                            // Destination Info
                            lblRegion.Text = reader["Region"].ToString();
                            lblClimate.Text = reader["Climate"].ToString();
                            lblCurrency.Text = reader["Currency"].ToString();
                            lblLanguage.Text = reader["Language"].ToString();
                            lblBestTime.Text = reader["BestVisitTime"].ToString();
                            txtDestinationDescription.Text = reader["DestinationDescription"].ToString();

                            // Rating
                            object avgRating = reader["AverageRating"];
                            if (avgRating != DBNull.Value)
                            {
                                decimal rating = Convert.ToDecimal(avgRating);
                                lblRating.Text = rating.ToString("F1");
                                UpdateStarRating(rating);
                            }
                            else
                            {
                                lblRating.Text = "N/A";
                                UpdateStarRating(0);
                            }

                            lblTotalReviews.Text = $"({reader["TotalReviews"]} reviews)";

                            // Load trip images gallery
                            string destinationName = reader["DestinationName"]?.ToString();
                            string destinationRegiom = reader["Region"]?.ToString();
                            string destinationCountry = reader["Country"]?.ToString();
                            string destinationClimate = reader["Climate"]?.ToString();

                            string destinationdescription = reader["DestinationDescription"]?.ToString();

                            string searchQuery = $"{destinationName} {destinationRegiom} {destinationCountry} {destinationClimate}";
                            string alternateSearchQuery = $"{destinationdescription}";
                            // Load multiple images for gallery
                            await LoadTripImageGallery(reader["DestinationID"]?.ToString(), searchQuery, alternateSearchQuery);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTripImageGallery(string did, string searchQuery, string alternateSQ)
        {
            try
            {
                // Clear existing images
                galleryImages.Clear();

                // Show loading indicator
                pbTripImage.Image = CreateLoadingImage();

                if (string.IsNullOrEmpty(searchQuery))
                    return;

                //// Extract place name from filename
                //string placeName = Path.GetFileNameWithoutExtension(imageFilename);

                // Fetch multiple images from API
                List<string> imagePaths = await imageService.GetMultipleImagesFromApi(searchQuery, 5);

                if (imagePaths.Count == 0 && !string.IsNullOrEmpty(alternateSQ))
                {
                    // If no images found, try alternate search query
                    imagePaths = await imageService.GetMultipleImagesFromApi(alternateSQ, 5);
                }


                foreach (string imagePath in imagePaths)
                {
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        galleryImages.Add(Image.FromFile(imagePath));
                    }
                }

                // If we got images, show the first one
                if (galleryImages.Count > 0)
                {
                    pbTripImage.Image = galleryImages[0];
                    UpdateImageIndicators();
                }
                else
                {
                    // Fallback to placeholder
                    pbTripImage.Image = TravelApplication.Properties.Resources.defaultImage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading trip images: {ex.Message}");
                pbTripImage.Image = CreatePlaceholderImage();
            }
        }

        private void UpdateImageIndicators()
        {
            panelImageIndicators.Controls.Clear();

            for (int i = 0; i < galleryImages.Count; i++)
            {
                Label indicator = new Label();
                indicator.Size = new Size(12, 12);
                indicator.Location = new Point(i * 20 + 5, 9);
                indicator.BackColor = i == currentImageIndex ? Color.White : Color.FromArgb(150, 255, 255, 255);
                indicator.Cursor = Cursors.Hand;

                // Make it circular
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, 12, 12);
                    indicator.Region = new Region(path);
                }

                int index = i;
                indicator.Click += (sender, e) =>
                {
                    currentImageIndex = index;
                    ShowCurrentImage();
                };

                panelImageIndicators.Controls.Add(indicator);
            }
        }

        private void ShowCurrentImage()
        {
            if (galleryImages.Count > 0 && currentImageIndex < galleryImages.Count)
            {
                // Fade transition effect
                FadeTransition(pbTripImage, galleryImages[currentImageIndex]);
                UpdateImageIndicators();
            }
        }

        private void FadeTransition(PictureBox pictureBox, Image newImage)
        {
            // Simple fade effect
            Timer fadeTimer = new Timer();
            fadeTimer.Interval = 50;
            int alpha = 0;

            Bitmap fadeBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);

            fadeTimer.Tick += (sender, e) =>
            {
                alpha += 15;
                if (alpha >= 255)
                {
                    alpha = 255;
                    fadeTimer.Stop();
                    pictureBox.Image = newImage;
                    fadeTimer.Dispose();
                    return;
                }

                using (Graphics g = Graphics.FromImage(fadeBitmap))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(newImage, 0, 0, pictureBox.Width, pictureBox.Height);
                }

                pictureBox.Image = fadeBitmap;
            };

            fadeTimer.Start();
        }

        private void BtnPrevImage_Click(object sender, EventArgs e)
        {
            if (galleryImages.Count > 0)
            {
                currentImageIndex = (currentImageIndex - 1 + galleryImages.Count) % galleryImages.Count;
                ShowCurrentImage();
            }
        }

        private void BtnNextImage_Click(object sender, EventArgs e)
        {
            if (galleryImages.Count > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % galleryImages.Count;
                ShowCurrentImage();
            }
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            BtnNextImage_Click(null, null);
        }

        private Image CreateLoadingImage()
        {
            Bitmap loading = new Bitmap(400, 300);
            using (Graphics g = Graphics.FromImage(loading))
            {
                g.Clear(Color.FromArgb(245, 245, 245));
                using (Font font = new Font("Century Gothic", 16))
                {
                    string text = "Loading...";
                    SizeF textSize = g.MeasureString(text, font);
                    float x = (loading.Width - textSize.Width) / 2;
                    float y = (loading.Height - textSize.Height) / 2;
                    g.DrawString(text, font, Brushes.DarkGray, x, y);
                }
            }
            return loading;
        }

        private Image CreatePlaceholderImage()
        {
            Bitmap placeholder = new Bitmap(400, 300);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.LightGray);
                using (Font font = new Font("Century Gothic", 16))
                {
                    string text = "No Image Available";
                    SizeF textSize = g.MeasureString(text, font);
                    float x = (placeholder.Width - textSize.Width) / 2;
                    float y = (placeholder.Height - textSize.Height) / 2;
                    g.DrawString(text, font, Brushes.DarkGray, x, y);
                }
            }
            return placeholder;
        }

        // Rest of your existing methods (LoadReviews, LoadServices, etc.)
        private void LoadReviews()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            R.Rating,
                            R.Comment,
                            R.Date,
                            U.FirstName + ' ' + U.LastName AS ReviewerName,
                            TR.AccommodationRating,
                            TR.ValueForMoneyRating
                        FROM REVIEW R
                        INNER JOIN Trip_REVIEW TR ON R.ReviewID = TR.ReviewID
                        INNER JOIN TRAVELER T ON R.TravelerID = T.TravelerID
                        INNER JOIN [USER] U ON T.TravelerID = U.UserID
                        WHERE TR.TripID = @TripID 
                        AND R.Visibility = 'Public' 
                        AND R.ApprovalStatus = 'Approved'
                        ORDER BY R.Date DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TripID", tripId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable reviewsTable = new DataTable();
                    adapter.Fill(reviewsTable);

                    dgvReviews.DataSource = reviewsTable;
                    FormatReviewGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            S.ServiceType,
                            CASE 
                                WHEN S.ServiceType = 'Hotel' THEN H.Name
                                WHEN S.ServiceType = 'Guide' THEN U.FirstName + ' ' + U.LastName
                                WHEN S.ServiceType = 'Transport' THEN SP.CompanyName
                            END AS ServiceName,
                            CASE 
                                WHEN S.ServiceType = 'Hotel' THEN H.Description
                                WHEN S.ServiceType = 'Guide' THEN G.Specializations
                                WHEN S.ServiceType = 'Transport' THEN TP.ServiceAreas
                            END AS ServiceDescription,
                            SP.Rating
                        FROM TRIP_SERVICES_Renrollment TSR
                        INNER JOIN SERVICES S ON TSR.ServiceID = S.ServiceID
                        LEFT JOIN HOTEL H ON S.ServiceID = H.HotelID
                        LEFT JOIN GUIDE G ON S.ServiceID = G.GuideID
                        LEFT JOIN TRANSPORT_PROVIDER TP ON S.ServiceID = TP.TransportID
                        LEFT JOIN SERVICE_PROVIDER SP ON 
                            (H.ProviderID = SP.ProviderID OR 
                             G.ProviderID = SP.ProviderID OR 
                             TP.ProviderID = SP.ProviderID)
                        LEFT JOIN [USER] U ON G.ProviderID = U.UserID
                        WHERE TSR.TripID = @TripID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TripID", tripId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable servicesTable = new DataTable();
                    adapter.Fill(servicesTable);

                    dgvServices.DataSource = servicesTable;
                    FormatServiceGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Your existing grid formatting and other methods...
        private void ConfigureReviewGrid()
        {
            dgvReviews.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvReviews.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvReviews.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReviews.EnableHeadersVisualStyles = false;
            dgvReviews.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReviews.ReadOnly = true;
            dgvReviews.RowHeadersVisible = false;
            dgvReviews.BorderStyle = BorderStyle.None;
            dgvReviews.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReviews.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void ConfigureServiceGrid()
        {
            dgvServices.DefaultCellStyle.Font = new Font("Century Gothic", 9);
            dgvServices.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgvServices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServices.EnableHeadersVisualStyles = false;
            dgvServices.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServices.ReadOnly = true;
            dgvServices.RowHeadersVisible = false;
            dgvServices.BorderStyle = BorderStyle.None;
            dgvServices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvServices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void FormatReviewGrid()
        {
            if (dgvReviews.Columns.Count > 0)
            {
                dgvReviews.Columns["ReviewerName"].HeaderText = "Reviewer";
                dgvReviews.Columns["ReviewerName"].Width = 120;

                dgvReviews.Columns["Rating"].HeaderText = "Rating";
                dgvReviews.Columns["Rating"].Width = 80;

                dgvReviews.Columns["Comment"].HeaderText = "Comment";
                dgvReviews.Columns["Comment"].Width = 300;

                dgvReviews.Columns["Date"].HeaderText = "Date";
                dgvReviews.Columns["Date"].Width = 100;
                dgvReviews.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                dgvReviews.Columns["AccommodationRating"].HeaderText = "Accommodation";
                dgvReviews.Columns["AccommodationRating"].Width = 120;

                dgvReviews.Columns["ValueForMoneyRating"].HeaderText = "Value for Money";
                dgvReviews.Columns["ValueForMoneyRating"].Width = 120;
            }
        }

        private void FormatServiceGrid()
        {
            if (dgvServices.Columns.Count > 0)
            {
                dgvServices.Columns["ServiceType"].HeaderText = "Type";
                dgvServices.Columns["ServiceType"].Width = 100;

                dgvServices.Columns["ServiceName"].HeaderText = "Service Provider";
                dgvServices.Columns["ServiceName"].Width = 200;

                dgvServices.Columns["ServiceDescription"].HeaderText = "Description";
                dgvServices.Columns["ServiceDescription"].Width = 300;

                dgvServices.Columns["Rating"].HeaderText = "Rating";
                dgvServices.Columns["Rating"].Width = 80;
                dgvServices.Columns["Rating"].DefaultCellStyle.Format = "F1";
            }
        }

        private void UpdateStarRating(decimal rating)
        {
            Label[] stars = { lblStar1, lblStar2, lblStar3, lblStar4, lblStar5 };

            for (int i = 0; i < stars.Length; i++)
            {
                if (i < Math.Floor(rating))
                {
                    stars[i].Text = "★";
                    stars[i].ForeColor = Color.FromArgb(255, 199, 0);
                }
                else if (i < rating)
                {
                    stars[i].Text = "★";
                    stars[i].ForeColor = Color.FromArgb(255, 199, 0);
                    stars[i].Font = new Font(stars[i].Font.FontFamily, stars[i].Font.Size * 0.5f);
                }
                else
                {
                    stars[i].Text = "☆";
                    stars[i].ForeColor = Color.Gray;
                }
            }
        }

        private void CheckWishlistStatus()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT COUNT(*) FROM WISHLIST 
                                   WHERE TravelerID = @TravelerID AND TripID = @TripID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TravelerID", travelerId);
                    command.Parameters.AddWithValue("@TripID", tripId);

                    int count = (int)command.ExecuteScalar();
                    isInWishlist = count > 0;

                    if (isInWishlist)
                    {
                        btnAddToWishlist.Text = "Remove from Wishlist";
                        btnAddToWishlist.BackColor = Color.FromArgb(231, 76, 60);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking wishlist status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddHoverEffects()
        {
            btnBook.MouseEnter += (s, e) => { btnBook.BackColor = Color.FromArgb(39, 174, 96); };
            btnBook.MouseLeave += (s, e) => { btnBook.BackColor = Color.FromArgb(46, 204, 113); };

            btnAddToWishlist.MouseEnter += (s, e) =>
            {
                btnAddToWishlist.BackColor = isInWishlist ? Color.FromArgb(192, 57, 43) : Color.FromArgb(41, 128, 185);
            };
            btnAddToWishlist.MouseLeave += (s, e) =>
            {
                btnAddToWishlist.BackColor = isInWishlist ? Color.FromArgb(231, 76, 60) : Color.FromArgb(52, 152, 219);
            };

            btnWriteReview.MouseEnter += (s, e) => { btnWriteReview.BackColor = Color.FromArgb(230, 126, 0); };
            btnWriteReview.MouseLeave += (s, e) => { btnWriteReview.BackColor = Color.FromArgb(255, 152, 0); };
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            BookingsForm bookingForm = new BookingsForm(travelerId, tripId);
            if (bookingForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Trip booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnAddToWishlist_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (isInWishlist)
                    {
                        // Remove from wishlist
                        string query = @"DELETE FROM WISHLIST 
                                       WHERE TravelerID = @TravelerID AND TripID = @TripID";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TravelerID", travelerId);
                        command.Parameters.AddWithValue("@TripID", tripId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Trip removed from wishlist!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnAddToWishlist.Text = "Add to Wishlist";
                        btnAddToWishlist.BackColor = Color.FromArgb(52, 152, 219);
                        isInWishlist = false;
                    }
                    else
                    {
                        // Add to wishlist
                        string query = @"INSERT INTO WISHLIST (TravelerID, TripID, DateAdded, PriceAtAdding, PriceAlert) 
                                       VALUES (@TravelerID, @TripID, GETDATE(), @Price, 0)";

                        // Get current price
                        decimal currentPrice = 0;
                        string priceQuery = "SELECT Price FROM TRIP WHERE TripID = @TripID";
                        SqlCommand priceCommand = new SqlCommand(priceQuery, connection);
                        priceCommand.Parameters.AddWithValue("@TripID", tripId);
                        currentPrice = Convert.ToDecimal(priceCommand.ExecuteScalar());

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TravelerID", travelerId);
                        command.Parameters.AddWithValue("@TripID", tripId);
                        command.Parameters.AddWithValue("@Price", currentPrice);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Trip added to wishlist!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnAddToWishlist.Text = "Remove from Wishlist";
                        btnAddToWishlist.BackColor = Color.FromArgb(231, 76, 60);
                        isInWishlist = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating wishlist: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWriteReview_Click(object sender, EventArgs e)
        {
            WriteReviewForm reviewForm = new WriteReviewForm(travelerId, 0, "Trip", tripId);
            if (reviewForm.ShowDialog() == DialogResult.OK)
            {
                LoadReviews();
                LoadData(); // Reload to update rating
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Enhanced Image Service Class with multiple image support
    public class ImageService
    {
        private readonly string apiKey = "50219836-d8f5926214f28ae15be327d2a"; // Your  Access Key
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string cacheFolder;

        public ImageService()
        {
            // Create cache folder if it doesn't exist
            cacheFolder = Path.Combine(Application.StartupPath, "ImageCache");
            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }
        }

        public async Task<string> GetCachedImage(string placeName)
        {
            string fileName = $"{placeName.ToLower()}.jpg";
            string filePath = Path.Combine(cacheFolder, fileName);

            if (File.Exists(filePath))
            {
                // Check if file is not too old (cache for 7 days)
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.LastWriteTime > DateTime.Now.AddDays(-7))
                {
                    return filePath;
                }
            }

            return null;
        }

        public async Task<string> GetImageFromApi(string placeName)
        {
            try
            {
                // Format search query - append "pakistan" to place name
                string searchQuery = $"{placeName}%20pakistan%20tourism";

                // Call Unsplash API
                string apiUrl = $"https://api.unsplash.com/search/photos?query={Uri.EscapeDataString(searchQuery)}&client_id={apiKey}&per_page=1";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonResponse);

                    if (data.results != null && data.results.Count > 0)
                    {
                        string imageUrl = data.results[0].urls.regular.ToString();

                        // Download and cache the image
                        return await DownloadAndCacheImage(imageUrl, placeName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching image from API: {ex.Message}");
            }

            return null;
        }
        public async Task<List<string>> GetMultipleImagesFromApi(string sq, int count)
        {
            List<string> imagePaths = new List<string>();

            try
            {
                // Format search query - append "Pakistan" to place name
                string searchQuery = sq;

                // Construct Pixabay API URL
                string apiUrl = $"https://pixabay.com/api/?key={apiKey}&q={Uri.EscapeDataString(searchQuery)}&image_type=photo&orientation=horizontal&per_page={count}&safesearch=true";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonResponse);

                    if (data.hits != null && data.hits.Count > 0)
                    {
                        for (int i = 0; i < data.hits.Count; i++)
                        {
                            string imageUrl = data.hits[i].webformatURL.ToString();
                            string fileName = $"{sq.ToLower()}_{i}.jpg";

                            // Check if already cached
                            string cachedPath = Path.Combine(cacheFolder, fileName);
                            if (File.Exists(cachedPath))
                            {
                                FileInfo fileInfo = new FileInfo(cachedPath);
                                if (fileInfo.LastWriteTime > DateTime.Now.AddDays(-7))
                                {
                                    imagePaths.Add(cachedPath);
                                    continue;
                                }
                            }

                            // Download and cache the image
                            string downloadedPath = await DownloadAndCacheImage(imageUrl, $"{sq}_{i}");
                            if (!string.IsNullOrEmpty(downloadedPath))
                            {
                                imagePaths.Add(downloadedPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching multiple images from Pixabay API: {ex.Message}");
            }

            return imagePaths;
        }



        private async Task<string> DownloadAndCacheImage(string imageUrl, string placeName)
        {
            try
            {
                byte[] imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                string fileName = $"{placeName.ToLower()}.jpg";
                string filePath = Path.Combine(cacheFolder, fileName);

                File.WriteAllBytes(filePath, imageBytes);

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading image: {ex.Message}");
                return null;
            }
        }
    }
}