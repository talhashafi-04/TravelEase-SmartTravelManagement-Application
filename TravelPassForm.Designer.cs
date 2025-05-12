namespace TravelEase
{
    partial class TravelPassForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelPass = new System.Windows.Forms.Panel();
            this.panelPassHeader = new System.Windows.Forms.Panel();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblExpiryTitle = new System.Windows.Forms.Label();
            this.lblPassType = new System.Windows.Forms.Label();
            this.lblPassId = new System.Windows.Forms.Label();
            this.lblPassTitle = new System.Windows.Forms.Label();
            this.panelPassBody = new System.Windows.Forms.Panel();
            this.panelQRSection = new System.Windows.Forms.Panel();
            this.lblQRInstructions = new System.Windows.Forms.Label();
            this.lblQRTitle = new System.Windows.Forms.Label();
            this.panelTravelerInfo = new System.Windows.Forms.Panel();
            this.lblNationality = new System.Windows.Forms.Label();
            this.lblNationalityTitle = new System.Windows.Forms.Label();
            this.lblCNIC = new System.Windows.Forms.Label();
            this.lblCNICTitle = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPhoneTitle = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmailTitle = new System.Windows.Forms.Label();
            this.lblTravelerName = new System.Windows.Forms.Label();
            this.lblTravelerNameTitle = new System.Windows.Forms.Label();
            this.lblTravelerInfoTitle = new System.Windows.Forms.Label();
            this.panelTripInfo = new System.Windows.Forms.Panel();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDurationTitle = new System.Windows.Forms.Label();
            this.lblDates = new System.Windows.Forms.Label();
            this.lblDatesTitle = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblDestinationTitle = new System.Windows.Forms.Label();
            this.lblTripTitle = new System.Windows.Forms.Label();
            this.lblTripTitleTitle = new System.Windows.Forms.Label();
            this.lblTripInfoTitle = new System.Windows.Forms.Label();
            this.panelBookingInfo = new System.Windows.Forms.Panel();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAmountTitle = new System.Windows.Forms.Label();
            this.lblBookingStatus = new System.Windows.Forms.Label();
            this.lblBookingStatusTitle = new System.Windows.Forms.Label();
            this.lblBookingDate = new System.Windows.Forms.Label();
            this.lblBookingDateTitle = new System.Windows.Forms.Label();
            this.lblBookingId = new System.Windows.Forms.Label();
            this.lblBookingIdTitle = new System.Windows.Forms.Label();
            this.lblBookingInfoTitle = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelHeader.SuspendLayout();
            this.panelPass.SuspendLayout();
            this.panelPassHeader.SuspendLayout();
            this.panelPassBody.SuspendLayout();
            this.panelQRSection.SuspendLayout();
            this.panelTravelerInfo.SuspendLayout();
            this.panelTripInfo.SuspendLayout();
            this.panelBookingInfo.SuspendLayout();
            this.panelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(900, 60);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Travel Pass";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPass
            // 
            this.panelPass.BackColor = System.Drawing.Color.White;
            this.panelPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPass.Controls.Add(this.panelPassHeader);
            this.panelPass.Controls.Add(this.panelPassBody);
            this.panelPass.Location = new System.Drawing.Point(20, 80);
            this.panelPass.Name = "panelPass";
            this.panelPass.Size = new System.Drawing.Size(860, 600);
            this.panelPass.TabIndex = 1;
            // 
            // panelPassHeader
            // 
            this.panelPassHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(165)))));
            this.panelPassHeader.Controls.Add(this.lblExpiry);
            this.panelPassHeader.Controls.Add(this.lblExpiryTitle);
            this.panelPassHeader.Controls.Add(this.lblPassType);
            this.panelPassHeader.Controls.Add(this.lblPassId);
            this.panelPassHeader.Controls.Add(this.lblPassTitle);
            this.panelPassHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPassHeader.Location = new System.Drawing.Point(0, 0);
            this.panelPassHeader.Name = "panelPassHeader";
            this.panelPassHeader.Size = new System.Drawing.Size(858, 100);
            this.panelPassHeader.TabIndex = 0;
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiry.ForeColor = System.Drawing.Color.White;
            this.lblExpiry.Location = new System.Drawing.Point(726, 65);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(84, 21);
            this.lblExpiry.TabIndex = 4;
            this.lblExpiry.Text = "No Expiry";
            // 
            // lblExpiryTitle
            // 
            this.lblExpiryTitle.AutoSize = true;
            this.lblExpiryTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryTitle.ForeColor = System.Drawing.Color.White;
            this.lblExpiryTitle.Location = new System.Drawing.Point(643, 65);
            this.lblExpiryTitle.Name = "lblExpiryTitle";
            this.lblExpiryTitle.Size = new System.Drawing.Size(72, 19);
            this.lblExpiryTitle.TabIndex = 3;
            this.lblExpiryTitle.Text = "Expires:";
            // 
            // lblPassType
            // 
            this.lblPassType.AutoSize = true;
            this.lblPassType.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassType.ForeColor = System.Drawing.Color.White;
            this.lblPassType.Location = new System.Drawing.Point(20, 63);
            this.lblPassType.Name = "lblPassType";
            this.lblPassType.Size = new System.Drawing.Size(96, 23);
            this.lblPassType.TabIndex = 2;
            this.lblPassType.Text = "Standard";
            // 
            // lblPassId
            // 
            this.lblPassId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassId.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassId.ForeColor = System.Drawing.Color.White;
            this.lblPassId.Location = new System.Drawing.Point(643, 20);
            this.lblPassId.Name = "lblPassId";
            this.lblPassId.Size = new System.Drawing.Size(195, 21);
            this.lblPassId.TabIndex = 1;
            this.lblPassId.Text = "PASS-000000";
            this.lblPassId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPassTitle
            // 
            this.lblPassTitle.AutoSize = true;
            this.lblPassTitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassTitle.ForeColor = System.Drawing.Color.White;
            this.lblPassTitle.Location = new System.Drawing.Point(18, 14);
            this.lblPassTitle.Name = "lblPassTitle";
            this.lblPassTitle.Size = new System.Drawing.Size(242, 37);
            this.lblPassTitle.TabIndex = 0;
            this.lblPassTitle.Text = "TravelEase Pass";
            // 
            // panelPassBody
            // 
            this.panelPassBody.Controls.Add(this.panelQRSection);
            this.panelPassBody.Controls.Add(this.panelTravelerInfo);
            this.panelPassBody.Controls.Add(this.panelTripInfo);
            this.panelPassBody.Controls.Add(this.panelBookingInfo);
            this.panelPassBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPassBody.Location = new System.Drawing.Point(0, 0);
            this.panelPassBody.Name = "panelPassBody";
            this.panelPassBody.Padding = new System.Windows.Forms.Padding(20);
            this.panelPassBody.Size = new System.Drawing.Size(858, 598);
            this.panelPassBody.TabIndex = 1;
            // 
            // panelQRSection
            // 
            this.panelQRSection.Controls.Add(this.lblQRInstructions);
            this.panelQRSection.Controls.Add(this.pbQRCode);
            this.panelQRSection.Controls.Add(this.lblQRTitle);
            this.panelQRSection.Location = new System.Drawing.Point(523, 109);
            this.panelQRSection.Name = "panelQRSection";
            this.panelQRSection.Size = new System.Drawing.Size(315, 475);
            this.panelQRSection.TabIndex = 3;
            // 
            // lblQRInstructions
            // 
            this.lblQRInstructions.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQRInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblQRInstructions.Location = new System.Drawing.Point(3, 320);
            this.lblQRInstructions.Name = "lblQRInstructions";
            this.lblQRInstructions.Size = new System.Drawing.Size(309, 64);
            this.lblQRInstructions.TabIndex = 2;
            this.lblQRInstructions.Text = "Show this QR code at check-in points. It contains your booking information and ca" +
    "n be scanned for quick verification.";
            this.lblQRInstructions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblQRTitle
            // 
            this.lblQRTitle.AutoSize = true;
            this.lblQRTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQRTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblQRTitle.Location = new System.Drawing.Point(99, 15);
            this.lblQRTitle.Name = "lblQRTitle";
            this.lblQRTitle.Size = new System.Drawing.Size(117, 23);
            this.lblQRTitle.TabIndex = 0;
            this.lblQRTitle.Text = "Quick Pass";
            // 
            // panelTravelerInfo
            // 
            this.panelTravelerInfo.Controls.Add(this.lblNationality);
            this.panelTravelerInfo.Controls.Add(this.lblNationalityTitle);
            this.panelTravelerInfo.Controls.Add(this.lblCNIC);
            this.panelTravelerInfo.Controls.Add(this.lblCNICTitle);
            this.panelTravelerInfo.Controls.Add(this.lblPhone);
            this.panelTravelerInfo.Controls.Add(this.lblPhoneTitle);
            this.panelTravelerInfo.Controls.Add(this.lblEmail);
            this.panelTravelerInfo.Controls.Add(this.lblEmailTitle);
            this.panelTravelerInfo.Controls.Add(this.lblTravelerName);
            this.panelTravelerInfo.Controls.Add(this.lblTravelerNameTitle);
            this.panelTravelerInfo.Controls.Add(this.lblTravelerInfoTitle);
            this.panelTravelerInfo.Location = new System.Drawing.Point(20, 440);
            this.panelTravelerInfo.Name = "panelTravelerInfo";
            this.panelTravelerInfo.Size = new System.Drawing.Size(480, 158);
            this.panelTravelerInfo.TabIndex = 2;
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNationality.Location = new System.Drawing.Point(375, 120);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(80, 21);
            this.lblNationality.TabIndex = 10;
            this.lblNationality.Text = "Pakistan";
            // 
            // lblNationalityTitle
            // 
            this.lblNationalityTitle.AutoSize = true;
            this.lblNationalityTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNationalityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNationalityTitle.Location = new System.Drawing.Point(273, 120);
            this.lblNationalityTitle.Name = "lblNationalityTitle";
            this.lblNationalityTitle.Size = new System.Drawing.Size(100, 19);
            this.lblNationalityTitle.TabIndex = 9;
            this.lblNationalityTitle.Text = "Nationality:";
            // 
            // lblCNIC
            // 
            this.lblCNIC.AutoSize = true;
            this.lblCNIC.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCNIC.Location = new System.Drawing.Point(83, 120);
            this.lblCNIC.Name = "lblCNIC";
            this.lblCNIC.Size = new System.Drawing.Size(127, 21);
            this.lblCNIC.TabIndex = 8;
            this.lblCNIC.Text = "1234567890123";
            // 
            // lblCNICTitle
            // 
            this.lblCNICTitle.AutoSize = true;
            this.lblCNICTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCNICTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCNICTitle.Location = new System.Drawing.Point(20, 120);
            this.lblCNICTitle.Name = "lblCNICTitle";
            this.lblCNICTitle.Size = new System.Drawing.Size(58, 19);
            this.lblCNICTitle.TabIndex = 7;
            this.lblCNICTitle.Text = "CNIC:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(345, 90);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(120, 21);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "+92300123456";
            // 
            // lblPhoneTitle
            // 
            this.lblPhoneTitle.AutoSize = true;
            this.lblPhoneTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPhoneTitle.Location = new System.Drawing.Point(273, 90);
            this.lblPhoneTitle.Name = "lblPhoneTitle";
            this.lblPhoneTitle.Size = new System.Drawing.Size(66, 19);
            this.lblPhoneTitle.TabIndex = 5;
            this.lblPhoneTitle.Text = "Phone:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(83, 90);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(144, 21);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "user@email.com";
            // 
            // lblEmailTitle
            // 
            this.lblEmailTitle.AutoSize = true;
            this.lblEmailTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmailTitle.Location = new System.Drawing.Point(20, 90);
            this.lblEmailTitle.Name = "lblEmailTitle";
            this.lblEmailTitle.Size = new System.Drawing.Size(58, 19);
            this.lblEmailTitle.TabIndex = 3;
            this.lblEmailTitle.Text = "Email:";
            // 
            // lblTravelerName
            // 
            this.lblTravelerName.AutoSize = true;
            this.lblTravelerName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTravelerName.Location = new System.Drawing.Point(118, 60);
            this.lblTravelerName.Name = "lblTravelerName";
            this.lblTravelerName.Size = new System.Drawing.Size(132, 21);
            this.lblTravelerName.TabIndex = 2;
            this.lblTravelerName.Text = "Traveler Name";
            // 
            // lblTravelerNameTitle
            // 
            this.lblTravelerNameTitle.AutoSize = true;
            this.lblTravelerNameTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTravelerNameTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTravelerNameTitle.Location = new System.Drawing.Point(20, 60);
            this.lblTravelerNameTitle.Name = "lblTravelerNameTitle";
            this.lblTravelerNameTitle.Size = new System.Drawing.Size(78, 19);
            this.lblTravelerNameTitle.TabIndex = 1;
            this.lblTravelerNameTitle.Text = "Traveler:";
            // 
            // lblTravelerInfoTitle
            // 
            this.lblTravelerInfoTitle.AutoSize = true;
            this.lblTravelerInfoTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTravelerInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTravelerInfoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTravelerInfoTitle.Name = "lblTravelerInfoTitle";
            this.lblTravelerInfoTitle.Size = new System.Drawing.Size(202, 23);
            this.lblTravelerInfoTitle.TabIndex = 0;
            this.lblTravelerInfoTitle.Text = "Traveler Information";
            // 
            // panelTripInfo
            // 
            this.panelTripInfo.Controls.Add(this.lblDuration);
            this.panelTripInfo.Controls.Add(this.lblDurationTitle);
            this.panelTripInfo.Controls.Add(this.lblDates);
            this.panelTripInfo.Controls.Add(this.lblDatesTitle);
            this.panelTripInfo.Controls.Add(this.lblDestination);
            this.panelTripInfo.Controls.Add(this.lblDestinationTitle);
            this.panelTripInfo.Controls.Add(this.lblTripTitle);
            this.panelTripInfo.Controls.Add(this.lblTripTitleTitle);
            this.panelTripInfo.Controls.Add(this.lblTripInfoTitle);
            this.panelTripInfo.Location = new System.Drawing.Point(20, 264);
            this.panelTripInfo.Name = "panelTripInfo";
            this.panelTripInfo.Size = new System.Drawing.Size(480, 170);
            this.panelTripInfo.TabIndex = 1;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(107, 135);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(64, 21);
            this.lblDuration.TabIndex = 8;
            this.lblDuration.Text = "7 Days";
            // 
            // lblDurationTitle
            // 
            this.lblDurationTitle.AutoSize = true;
            this.lblDurationTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDurationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDurationTitle.Location = new System.Drawing.Point(20, 135);
            this.lblDurationTitle.Name = "lblDurationTitle";
            this.lblDurationTitle.Size = new System.Drawing.Size(81, 19);
            this.lblDurationTitle.TabIndex = 7;
            this.lblDurationTitle.Text = "Duration:";
            // 
            // lblDates
            // 
            this.lblDates.AutoSize = true;
            this.lblDates.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDates.Location = new System.Drawing.Point(83, 105);
            this.lblDates.Name = "lblDates";
            this.lblDates.Size = new System.Drawing.Size(178, 21);
            this.lblDates.TabIndex = 6;
            this.lblDates.Text = "Jan 01 - Jan 07, 2024";
            // 
            // lblDatesTitle
            // 
            this.lblDatesTitle.AutoSize = true;
            this.lblDatesTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDatesTitle.Location = new System.Drawing.Point(20, 105);
            this.lblDatesTitle.Name = "lblDatesTitle";
            this.lblDatesTitle.Size = new System.Drawing.Size(60, 19);
            this.lblDatesTitle.TabIndex = 5;
            this.lblDatesTitle.Text = "Dates:";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.Location = new System.Drawing.Point(127, 75);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(104, 21);
            this.lblDestination.TabIndex = 4;
            this.lblDestination.Text = "Destination";
            // 
            // lblDestinationTitle
            // 
            this.lblDestinationTitle.AutoSize = true;
            this.lblDestinationTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDestinationTitle.Location = new System.Drawing.Point(20, 75);
            this.lblDestinationTitle.Name = "lblDestinationTitle";
            this.lblDestinationTitle.Size = new System.Drawing.Size(103, 19);
            this.lblDestinationTitle.TabIndex = 3;
            this.lblDestinationTitle.Text = "Destination:";
            // 
            // lblTripTitle
            // 
            this.lblTripTitle.AutoSize = true;
            this.lblTripTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTripTitle.Location = new System.Drawing.Point(65, 45);
            this.lblTripTitle.Name = "lblTripTitle";
            this.lblTripTitle.Size = new System.Drawing.Size(75, 21);
            this.lblTripTitle.TabIndex = 2;
            this.lblTripTitle.Text = "Trip Title";
            // 
            // lblTripTitleTitle
            // 
            this.lblTripTitleTitle.AutoSize = true;
            this.lblTripTitleTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTripTitleTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTripTitleTitle.Location = new System.Drawing.Point(20, 45);
            this.lblTripTitleTitle.Name = "lblTripTitleTitle";
            this.lblTripTitleTitle.Size = new System.Drawing.Size(41, 19);
            this.lblTripTitleTitle.TabIndex = 1;
            this.lblTripTitleTitle.Text = "Trip:";
            // 
            // lblTripInfoTitle
            // 
            this.lblTripInfoTitle.AutoSize = true;
            this.lblTripInfoTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTripInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTripInfoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTripInfoTitle.Name = "lblTripInfoTitle";
            this.lblTripInfoTitle.Size = new System.Drawing.Size(159, 23);
            this.lblTripInfoTitle.TabIndex = 0;
            this.lblTripInfoTitle.Text = "Trip Information";
            // 
            // panelBookingInfo
            // 
            this.panelBookingInfo.Controls.Add(this.lblAmount);
            this.panelBookingInfo.Controls.Add(this.lblAmountTitle);
            this.panelBookingInfo.Controls.Add(this.lblBookingStatus);
            this.panelBookingInfo.Controls.Add(this.lblBookingStatusTitle);
            this.panelBookingInfo.Controls.Add(this.lblBookingDate);
            this.panelBookingInfo.Controls.Add(this.lblBookingDateTitle);
            this.panelBookingInfo.Controls.Add(this.lblBookingId);
            this.panelBookingInfo.Controls.Add(this.lblBookingIdTitle);
            this.panelBookingInfo.Controls.Add(this.lblBookingInfoTitle);
            this.panelBookingInfo.Location = new System.Drawing.Point(20, 106);
            this.panelBookingInfo.Name = "panelBookingInfo";
            this.panelBookingInfo.Size = new System.Drawing.Size(480, 152);
            this.panelBookingInfo.TabIndex = 0;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblAmount.Location = new System.Drawing.Point(350, 105);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(54, 19);
            this.lblAmount.TabIndex = 8;
            this.lblAmount.Text = "$0.00";
            // 
            // lblAmountTitle
            // 
            this.lblAmountTitle.AutoSize = true;
            this.lblAmountTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAmountTitle.Location = new System.Drawing.Point(265, 105);
            this.lblAmountTitle.Name = "lblAmountTitle";
            this.lblAmountTitle.Size = new System.Drawing.Size(78, 19);
            this.lblAmountTitle.TabIndex = 7;
            this.lblAmountTitle.Text = "Amount:";
            // 
            // lblBookingStatus
            // 
            this.lblBookingStatus.AutoSize = true;
            this.lblBookingStatus.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblBookingStatus.Location = new System.Drawing.Point(90, 105);
            this.lblBookingStatus.Name = "lblBookingStatus";
            this.lblBookingStatus.Size = new System.Drawing.Size(95, 19);
            this.lblBookingStatus.TabIndex = 6;
            this.lblBookingStatus.Text = "Confirmed";
            // 
            // lblBookingStatusTitle
            // 
            this.lblBookingStatusTitle.AutoSize = true;
            this.lblBookingStatusTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingStatusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookingStatusTitle.Location = new System.Drawing.Point(20, 105);
            this.lblBookingStatusTitle.Name = "lblBookingStatusTitle";
            this.lblBookingStatusTitle.Size = new System.Drawing.Size(60, 19);
            this.lblBookingStatusTitle.TabIndex = 5;
            this.lblBookingStatusTitle.Text = "Status:";
            // 
            // lblBookingDate
            // 
            this.lblBookingDate.AutoSize = true;
            this.lblBookingDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingDate.Location = new System.Drawing.Point(350, 75);
            this.lblBookingDate.Name = "lblBookingDate";
            this.lblBookingDate.Size = new System.Drawing.Size(104, 21);
            this.lblBookingDate.TabIndex = 4;
            this.lblBookingDate.Text = "01 Jan 2024";
            // 
            // lblBookingDateTitle
            // 
            this.lblBookingDateTitle.AutoSize = true;
            this.lblBookingDateTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookingDateTitle.Location = new System.Drawing.Point(215, 75);
            this.lblBookingDateTitle.Name = "lblBookingDateTitle";
            this.lblBookingDateTitle.Size = new System.Drawing.Size(124, 19);
            this.lblBookingDateTitle.TabIndex = 3;
            this.lblBookingDateTitle.Text = "Booking Date:";
            // 
            // lblBookingId
            // 
            this.lblBookingId.AutoSize = true;
            this.lblBookingId.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingId.Location = new System.Drawing.Point(130, 75);
            this.lblBookingId.Name = "lblBookingId";
            this.lblBookingId.Size = new System.Drawing.Size(55, 21);
            this.lblBookingId.TabIndex = 2;
            this.lblBookingId.Text = "12345";
            // 
            // lblBookingIdTitle
            // 
            this.lblBookingIdTitle.AutoSize = true;
            this.lblBookingIdTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingIdTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBookingIdTitle.Location = new System.Drawing.Point(20, 75);
            this.lblBookingIdTitle.Name = "lblBookingIdTitle";
            this.lblBookingIdTitle.Size = new System.Drawing.Size(103, 19);
            this.lblBookingIdTitle.TabIndex = 1;
            this.lblBookingIdTitle.Text = "Booking ID:";
            // 
            // lblBookingInfoTitle
            // 
            this.lblBookingInfoTitle.AutoSize = true;
            this.lblBookingInfoTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblBookingInfoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblBookingInfoTitle.Name = "lblBookingInfoTitle";
            this.lblBookingInfoTitle.Size = new System.Drawing.Size(207, 23);
            this.lblBookingInfoTitle.TabIndex = 0;
            this.lblBookingInfoTitle.Text = "Booking Information";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelActions.Controls.Add(this.btnClose);
            this.panelActions.Controls.Add(this.btnPrint);
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 730);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(900, 60);
            this.panelActions.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(750, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(385, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(130, 40);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print Pass";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Pass";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // pbQRCode
            // 
            this.pbQRCode.Image = global::TravelApplication.Properties.Resources.TravelEaselogo;
            this.pbQRCode.Location = new System.Drawing.Point(40, 60);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(240, 240);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbQRCode.TabIndex = 1;
            this.pbQRCode.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TravelApplication.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // TravelPassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(900, 790);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelPass);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TravelPassForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelEase - Travel Pass";
            this.panelHeader.ResumeLayout(false);
            this.panelPass.ResumeLayout(false);
            this.panelPassHeader.ResumeLayout(false);
            this.panelPassHeader.PerformLayout();
            this.panelPassBody.ResumeLayout(false);
            this.panelQRSection.ResumeLayout(false);
            this.panelQRSection.PerformLayout();
            this.panelTravelerInfo.ResumeLayout(false);
            this.panelTravelerInfo.PerformLayout();
            this.panelTripInfo.ResumeLayout(false);
            this.panelTripInfo.PerformLayout();
            this.panelBookingInfo.ResumeLayout(false);
            this.panelBookingInfo.PerformLayout();
            this.panelActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelPass;
        private System.Windows.Forms.Panel panelPassHeader;
        private System.Windows.Forms.Label lblPassTitle;
        private System.Windows.Forms.Label lblPassId;
        private System.Windows.Forms.Label lblPassType;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblExpiryTitle;
        private System.Windows.Forms.Panel panelPassBody;
        private System.Windows.Forms.Panel panelBookingInfo;
        private System.Windows.Forms.Label lblBookingInfoTitle;
        private System.Windows.Forms.Label lblBookingIdTitle;
        private System.Windows.Forms.Label lblBookingId;
        private System.Windows.Forms.Label lblBookingDateTitle;
        private System.Windows.Forms.Label lblBookingDate;
        private System.Windows.Forms.Label lblBookingStatusTitle;
        private System.Windows.Forms.Label lblBookingStatus;
        private System.Windows.Forms.Label lblAmountTitle;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Panel panelTripInfo;
        private System.Windows.Forms.Label lblTripInfoTitle;
        private System.Windows.Forms.Label lblTripTitleTitle;
        private System.Windows.Forms.Label lblTripTitle;
        private System.Windows.Forms.Label lblDestinationTitle;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblDatesTitle;
        private System.Windows.Forms.Label lblDates;
        private System.Windows.Forms.Label lblDurationTitle;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Panel panelTravelerInfo;
        private System.Windows.Forms.Label lblTravelerInfoTitle;
        private System.Windows.Forms.Label lblTravelerNameTitle;
        private System.Windows.Forms.Label lblTravelerName;
        private System.Windows.Forms.Label lblEmailTitle;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhoneTitle;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCNICTitle;
        private System.Windows.Forms.Label lblCNIC;
        private System.Windows.Forms.Label lblNationalityTitle;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.Panel panelQRSection;
        private System.Windows.Forms.Label lblQRTitle;
        private System.Windows.Forms.PictureBox pbQRCode;
        private System.Windows.Forms.Label lblQRInstructions;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}