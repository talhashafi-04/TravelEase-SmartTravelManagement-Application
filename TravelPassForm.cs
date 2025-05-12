using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing; 
using QRCoder;
using System.Windows.Forms;

namespace TravelEase
{
    public partial class TravelPassForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";
        private int bookingId;
        private int passId;

        public TravelPassForm(int bookingId)
        {
            InitializeComponent();
            this.bookingId = bookingId;
            LoadTravelPassData();
            SetupForm();
        }

        private void SetupForm()
        {
            // Set form properties
            this.Text = "TravelEase - Travel Pass";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Add event handlers
            btnSave.Click += BtnSave_Click;
            btnPrint.Click += BtnPrint_Click;
            btnClose.Click += BtnClose_Click;
        }

        private void LoadTravelPassData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to get travel pass and booking details
                    string query = @"
                        SELECT 
                            TP.Pass_ID,
                            TP.PassType,
                            TP.ExpiryDate,
                            B.BookingID,
                            B.Date AS BookingDate,
                            B.Status AS BookingStatus,
                            B.TotalAmount,
                            T.Title AS TripTitle,
                            T.StartDate,
                            T.EndDate,
                            T.Duration_Days,
                            D.Name AS Destination,
                            D.Country,
                            U.FirstName,
                            U.LastName,
                            U.Email,
                            U.PhoneNumber,
                            TR.CNIC,
                            TR.Nationality
                        FROM TRAVEL_PASS TP
                        INNER JOIN BOOKING B ON TP.BookingID = B.BookingID
                        INNER JOIN TRIP T ON B.TripID = T.TripID
                        INNER JOIN DESTINATION D ON T.DestinationID = D.DestinationID
                        INNER JOIN TRAVELER TR ON B.TravelerID = TR.TravelerID
                        INNER JOIN [USER] U ON TR.TravelerID = U.UserID
                        WHERE B.BookingID = @BookingID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set pass information
                            passId = Convert.ToInt32(reader["Pass_ID"]);
                            lblPassId.Text = $"PASS-{passId:D6}";
                            lblPassType.Text = reader["PassType"].ToString();

                            DateTime? expiryDate = reader["ExpiryDate"] as DateTime?;
                            lblExpiry.Text = expiryDate?.ToString("dd MMM yyyy") ?? "No Expiry";

                            // Set trip information
                            lblTripTitle.Text = reader["TripTitle"].ToString();
                            lblDestination.Text = $"{reader["Destination"]}, {reader["Country"]}";

                            DateTime startDate = Convert.ToDateTime(reader["StartDate"]);
                            DateTime endDate = Convert.ToDateTime(reader["EndDate"]);
                            lblDates.Text = $"{startDate:dd MMM yyyy} - {endDate:dd MMM yyyy}";
                            lblDuration.Text = $"{reader["Duration_Days"]} Days";

                            // Set traveler information
                            lblTravelerName.Text = $"{reader["FirstName"]} {reader["LastName"]}";
                            lblEmail.Text = reader["Email"].ToString();
                            lblPhone.Text = reader["PhoneNumber"].ToString();
                            lblCNIC.Text = reader["CNIC"].ToString();
                            lblNationality.Text = reader["Nationality"].ToString();

                            // Set booking information
                            lblBookingId.Text = reader["BookingID"].ToString();
                            DateTime bookingDate = Convert.ToDateTime(reader["BookingDate"]);
                            lblBookingDate.Text = bookingDate.ToString("dd MMM yyyy");
                            lblBookingStatus.Text = reader["BookingStatus"].ToString();
                            decimal amount = Convert.ToDecimal(reader["TotalAmount"]);
                            lblAmount.Text = amount.ToString("C2");

                            // Set status color
                            SetStatusColor(reader["BookingStatus"].ToString());

                            // Generate QR Code
                            GenerateQRCode();

                            // Set pass type styling
                            SetPassTypeStyling(reader["PassType"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("No travel pass found for this booking.",
                                "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading travel pass: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Bitmap AddLogoToQR(Bitmap qrCode, Bitmap logo)
        {
            int qrWidth = qrCode.Width;
            int qrHeight = qrCode.Height;

            // Logo should be about 15% of QR code size
            int logoSize = (int)(qrWidth * 0.15);

            Bitmap result = new Bitmap(qrCode);
            using (Graphics g = Graphics.FromImage(result))
            {
                // Calculate position to center the logo
                int x = (qrWidth - logoSize) / 2;
                int y = (qrHeight - logoSize) / 2;

                // Draw white background for logo
                g.FillRectangle(Brushes.White, x - 5, y - 5, logoSize + 10, logoSize + 10);

                // Draw the logo
                g.DrawImage(logo, x, y, logoSize, logoSize);
            }

            return result;
        }


        private void GenerateQRCode()
        {
            try
            {
                // Generate QR code content with pass information
                string qrContent = $"TRAVELEASE_PASS|{passId}|{bookingId}|{lblTravelerName.Text}|{lblTripTitle.Text}|{lblDates.Text}";

                // Create QR code generator
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Create QR code image
                Bitmap qrCodeImage = qrCode.GetGraphic(8, Color.Black, Color.White, true);

                // Optionally, add a logo to the center of the QR code
                Bitmap qrCodeWithLogo = AddLogoToQR(qrCodeImage, TravelApplication.Properties.Resources.TravelEaselogo);

                pbQRCode.Image = qrCodeWithLogo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fall back to placeholder if QRCoder is not available
                GenerateQRPlaceholder();
            }
        }

        // Alternative GenerateQRCode method if QRCoder library is not available
        private void GenerateQRPlaceholder()
        {
            try
            {
                // Generate a placeholder QR code (since actual QR generation requires external library)
                string qrContent = $"TRAVELEASE_PASS\n|PASSID : {passId}\n|BOOKING ID : {bookingId}\n|NAME: {lblTravelerName.Text}\n|TRIP : {lblTripTitle.Text}\n|DATE: {lblDates.Text}";

                // Create a placeholder image with text
                Bitmap qrPlaceholder = new Bitmap(240, 240);
                using (Graphics g = Graphics.FromImage(qrPlaceholder))
                {
                    g.Clear(Color.White);

                    // Draw a border
                    using (Pen borderPen = new Pen(Color.Black, 2))
                    {
                        g.DrawRectangle(borderPen, 1, 1, 238, 238);
                    }

                    // Draw some pattern to look like QR code
                    Random rand = new Random(passId);
                    using (Brush blackBrush = new SolidBrush(Color.Black))
                    {
                        for (int x = 10; x < 230; x += 5)
                        {
                            for (int y = 10; y < 230; y += 5)
                            {
                                if (rand.Next(2) == 1)
                                {
                                    g.FillRectangle(blackBrush, x, y, 4, 4);
                                }
                            }
                        }
                    }

                    // Draw corner squares (QR code pattern)
                    using (Brush blackBrush = new SolidBrush(Color.Black))
                    {
                        // Top-left corner
                        g.FillRectangle(blackBrush, 5, 5, 35, 35);
                        g.FillRectangle(Brushes.White, 10, 10, 25, 25);
                        g.FillRectangle(blackBrush, 15, 15, 15, 15);

                        // Top-right corner
                        g.FillRectangle(blackBrush, 200, 5, 35, 35);
                        g.FillRectangle(Brushes.White, 205, 10, 25, 25);
                        g.FillRectangle(blackBrush, 210, 15, 15, 15);

                        // Bottom-left corner
                        g.FillRectangle(blackBrush, 5, 200, 35, 35);
                        g.FillRectangle(Brushes.White, 10, 205, 25, 25);
                        g.FillRectangle(blackBrush, 15, 210, 15, 15);
                    }

                    // Add text overlay
                    using (Font font = new Font("Arial", 8, FontStyle.Bold))
                    using (Brush textBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        g.DrawString($"PASS #{passId}", font, textBrush, new RectangleF(0, 110, 240, 20), sf);
                    }
                }

                pbQRCode.Image = qrPlaceholder;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetPassTypeStyling(string passType)
        {
            switch (passType.ToUpper())
            {
                case "VIP":
                    panelPassHeader.BackColor = Color.FromArgb(155, 89, 182); // Purple
                    lblPassType.ForeColor = Color.Gold;
                    break;
                case "PREMIUM":
                    panelPassHeader.BackColor = Color.FromArgb(41, 128, 185); // Primary Blue
                    lblPassType.ForeColor = Color.Silver;
                    break;
                default: // Standard
                    panelPassHeader.BackColor = Color.FromArgb(0, 191, 165); // Teal
                    lblPassType.ForeColor = Color.White;
                    break;
            }
        }

        private void SetStatusColor(string status)
        {
            switch (status)
            {
                case "Confirmed":
                    lblBookingStatus.ForeColor = Color.FromArgb(46, 204, 113); // Success Green
                    break;
                case "Pending":
                    lblBookingStatus.ForeColor = Color.FromArgb(255, 193, 7); // Warning Yellow
                    break;
                case "Cancelled":
                    lblBookingStatus.ForeColor = Color.FromArgb(244, 67, 54); // Error Red
                    break;
                case "Completed":
                    lblBookingStatus.ForeColor = Color.FromArgb(41, 128, 185); // Primary Blue
                    break;
                default:
                    lblBookingStatus.ForeColor = Color.FromArgb(66, 66, 66); // Dark Gray
                    break;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
            saveDialog.Title = "Save Travel Pass";
            saveDialog.FileName = $"TravelPass_{passId}_{DateTime.Now:yyyyMMdd}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create bitmap of the pass panel
                    Bitmap passImage = new Bitmap(panelPass.Width, panelPass.Height);
                    panelPass.DrawToBitmap(passImage, new Rectangle(0, 0, panelPass.Width, panelPass.Height));

                    // Save based on selected format
                    if (saveDialog.FileName.EndsWith(".png"))
                    {
                        passImage.Save(saveDialog.FileName, ImageFormat.Png);
                    }
                    else if (saveDialog.FileName.EndsWith(".jpg"))
                    {
                        passImage.Save(saveDialog.FileName, ImageFormat.Jpeg);
                    }

                    MessageBox.Show("Travel pass saved successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving travel pass: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (s, ev) =>
            {
                // Create bitmap of the pass panel
                Bitmap passImage = new Bitmap(panelPass.Width, panelPass.Height);
                panelPass.DrawToBitmap(passImage, new Rectangle(0, 0, panelPass.Width, panelPass.Height));

                // Calculate scaling to fit on page
                float scaleX = ev.PageBounds.Width / (float)passImage.Width;
                float scaleY = ev.PageBounds.Height / (float)passImage.Height;
                float scale = Math.Min(scaleX, scaleY) * 0.9f; // 90% of page size

                // Calculate centered position
                float x = (ev.PageBounds.Width - passImage.Width * scale) / 2;
                float y = (ev.PageBounds.Height - passImage.Height * scale) / 2;

                // Draw the image
                ev.Graphics.DrawImage(passImage, x, y, passImage.Width * scale, passImage.Height * scale);
            };

            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error printing travel pass: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}