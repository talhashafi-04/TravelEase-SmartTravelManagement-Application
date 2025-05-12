namespace TravelEase
{
    partial class TravelerBookingsForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblFilterTitle = new System.Windows.Forms.Label();
            this.panelBookings = new System.Windows.Forms.Panel();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.panelSelectedBooking = new System.Windows.Forms.Panel();
            this.lblSelectedStatusTitle = new System.Windows.Forms.Label();
            this.lblSelectedStatus = new System.Windows.Forms.Label();
            this.lblSelectedDatesTitle = new System.Windows.Forms.Label();
            this.lblSelectedDates = new System.Windows.Forms.Label();
            this.lblSelectedDestinationTitle = new System.Windows.Forms.Label();
            this.lblSelectedDestination = new System.Windows.Forms.Label();
            this.lblSelectedTripTitle = new System.Windows.Forms.Label();
            this.lblSelectedTrip = new System.Windows.Forms.Label();
            this.lblSelectedBookingTitle = new System.Windows.Forms.Label();
            this.btnViewPass = new System.Windows.Forms.Button();
            this.btnCancelBooking = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.panelStatistics = new System.Windows.Forms.Panel();
            this.lblTotalSpentTitle = new System.Windows.Forms.Label();
            this.lblTotalSpent = new System.Windows.Forms.Label();
            this.lblTotalCountTitle = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblCancelledCountTitle = new System.Windows.Forms.Label();
            this.lblCancelledCount = new System.Windows.Forms.Label();
            this.lblCompletedCountTitle = new System.Windows.Forms.Label();
            this.lblCompletedCount = new System.Windows.Forms.Label();
            this.lblConfirmedCountTitle = new System.Windows.Forms.Label();
            this.lblConfirmedCount = new System.Windows.Forms.Label();
            this.lblPendingCountTitle = new System.Windows.Forms.Label();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.lblStatisticsTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.panelBookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.panelActions.SuspendLayout();
            this.panelSelectedBooking.SuspendLayout();
            this.panelStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.btnBack);
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(90, 35);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "← Back";
            this.btnBack.UseVisualStyleBackColor = false;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1200, 60);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "My Bookings";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.White;
            this.panelFilters.Controls.Add(this.btnReset);
            this.panelFilters.Controls.Add(this.btnFilter);
            this.panelFilters.Controls.Add(this.lblEndDate);
            this.panelFilters.Controls.Add(this.dtpEndDate);
            this.panelFilters.Controls.Add(this.lblStartDate);
            this.panelFilters.Controls.Add(this.dtpStartDate);
            this.panelFilters.Controls.Add(this.lblStatus);
            this.panelFilters.Controls.Add(this.cboStatus);
            this.panelFilters.Controls.Add(this.lblFilterTitle);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 60);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1200, 100);
            this.panelFilters.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(1008, 48);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(95, 35);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(907, 48);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(95, 35);
            this.btnFilter.TabIndex = 7;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(621, 55);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(77, 20);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(704, 52);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(169, 26);
            this.dtpEndDate.TabIndex = 5;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(360, 55);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(85, 20);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(451, 52);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(164, 26);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(97, 55);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(159, 52);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(181, 28);
            this.cboStatus.TabIndex = 1;
            // 
            // lblFilterTitle
            // 
            this.lblFilterTitle.AutoSize = true;
            this.lblFilterTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblFilterTitle.Location = new System.Drawing.Point(12, 14);
            this.lblFilterTitle.Name = "lblFilterTitle";
            this.lblFilterTitle.Size = new System.Drawing.Size(132, 23);
            this.lblFilterTitle.TabIndex = 0;
            this.lblFilterTitle.Text = "Filter Options";
            // 
            // panelBookings
            // 
            this.panelBookings.Controls.Add(this.dgvBookings);
            this.panelBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBookings.Location = new System.Drawing.Point(0, 160);
            this.panelBookings.Name = "panelBookings";
            this.panelBookings.Padding = new System.Windows.Forms.Padding(20);
            this.panelBookings.Size = new System.Drawing.Size(1200, 440);
            this.panelBookings.TabIndex = 2;
            // 
            // dgvBookings
            // 
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookings.Location = new System.Drawing.Point(20, 20);
            this.dgvBookings.MultiSelect = false;
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.ReadOnly = true;
            this.dgvBookings.RowHeadersWidth = 51;
            this.dgvBookings.RowTemplate.Height = 24;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.Size = new System.Drawing.Size(1160, 400);
            this.dgvBookings.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelActions.Controls.Add(this.panelSelectedBooking);
            this.panelActions.Controls.Add(this.btnViewPass);
            this.panelActions.Controls.Add(this.btnCancelBooking);
            this.panelActions.Controls.Add(this.btnViewDetails);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 600);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1200, 80);
            this.panelActions.TabIndex = 3;
            // 
            // panelSelectedBooking
            // 
            this.panelSelectedBooking.BackColor = System.Drawing.Color.White;
            this.panelSelectedBooking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSelectedBooking.Controls.Add(this.lblSelectedStatusTitle);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedStatus);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedDatesTitle);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedDates);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedDestinationTitle);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedDestination);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedTripTitle);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedTrip);
            this.panelSelectedBooking.Controls.Add(this.lblSelectedBookingTitle);
            this.panelSelectedBooking.Location = new System.Drawing.Point(20, 10);
            this.panelSelectedBooking.Name = "panelSelectedBooking";
            this.panelSelectedBooking.Size = new System.Drawing.Size(667, 60);
            this.panelSelectedBooking.TabIndex = 4;
            // 
            // lblSelectedStatusTitle
            // 
            this.lblSelectedStatusTitle.AutoSize = true;
            this.lblSelectedStatusTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedStatusTitle.Location = new System.Drawing.Point(547, 32);
            this.lblSelectedStatusTitle.Name = "lblSelectedStatusTitle";
            this.lblSelectedStatusTitle.Size = new System.Drawing.Size(53, 18);
            this.lblSelectedStatusTitle.TabIndex = 8;
            this.lblSelectedStatusTitle.Text = "Status:";
            // 
            // lblSelectedStatus
            // 
            this.lblSelectedStatus.AutoSize = true;
            this.lblSelectedStatus.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedStatus.Location = new System.Drawing.Point(606, 32);
            this.lblSelectedStatus.Name = "lblSelectedStatus";
            this.lblSelectedStatus.Size = new System.Drawing.Size(13, 20);
            this.lblSelectedStatus.TabIndex = 7;
            this.lblSelectedStatus.Text = "-";
            // 
            // lblSelectedDatesTitle
            // 
            this.lblSelectedDatesTitle.AutoSize = true;
            this.lblSelectedDatesTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDatesTitle.Location = new System.Drawing.Point(342, 32);
            this.lblSelectedDatesTitle.Name = "lblSelectedDatesTitle";
            this.lblSelectedDatesTitle.Size = new System.Drawing.Size(53, 18);
            this.lblSelectedDatesTitle.TabIndex = 6;
            this.lblSelectedDatesTitle.Text = "Dates:";
            // 
            // lblSelectedDates
            // 
            this.lblSelectedDates.AutoSize = true;
            this.lblSelectedDates.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDates.Location = new System.Drawing.Point(401, 32);
            this.lblSelectedDates.Name = "lblSelectedDates";
            this.lblSelectedDates.Size = new System.Drawing.Size(13, 20);
            this.lblSelectedDates.TabIndex = 5;
            this.lblSelectedDates.Text = "-";
            // 
            // lblSelectedDestinationTitle
            // 
            this.lblSelectedDestinationTitle.AutoSize = true;
            this.lblSelectedDestinationTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDestinationTitle.Location = new System.Drawing.Point(88, 32);
            this.lblSelectedDestinationTitle.Name = "lblSelectedDestinationTitle";
            this.lblSelectedDestinationTitle.Size = new System.Drawing.Size(94, 18);
            this.lblSelectedDestinationTitle.TabIndex = 4;
            this.lblSelectedDestinationTitle.Text = "Destination:";
            // 
            // lblSelectedDestination
            // 
            this.lblSelectedDestination.AutoSize = true;
            this.lblSelectedDestination.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDestination.Location = new System.Drawing.Point(188, 32);
            this.lblSelectedDestination.Name = "lblSelectedDestination";
            this.lblSelectedDestination.Size = new System.Drawing.Size(13, 20);
            this.lblSelectedDestination.TabIndex = 3;
            this.lblSelectedDestination.Text = "-";
            // 
            // lblSelectedTripTitle
            // 
            this.lblSelectedTripTitle.AutoSize = true;
            this.lblSelectedTripTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedTripTitle.Location = new System.Drawing.Point(88, 10);
            this.lblSelectedTripTitle.Name = "lblSelectedTripTitle";
            this.lblSelectedTripTitle.Size = new System.Drawing.Size(37, 18);
            this.lblSelectedTripTitle.TabIndex = 2;
            this.lblSelectedTripTitle.Text = "Trip:";
            // 
            // lblSelectedTrip
            // 
            this.lblSelectedTrip.AutoSize = true;
            this.lblSelectedTrip.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedTrip.Location = new System.Drawing.Point(131, 10);
            this.lblSelectedTrip.Name = "lblSelectedTrip";
            this.lblSelectedTrip.Size = new System.Drawing.Size(13, 20);
            this.lblSelectedTrip.TabIndex = 1;
            this.lblSelectedTrip.Text = "-";
            // 
            // lblSelectedBookingTitle
            // 
            this.lblSelectedBookingTitle.AutoSize = true;
            this.lblSelectedBookingTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedBookingTitle.Location = new System.Drawing.Point(8, 10);
            this.lblSelectedBookingTitle.Name = "lblSelectedBookingTitle";
            this.lblSelectedBookingTitle.Size = new System.Drawing.Size(79, 18);
            this.lblSelectedBookingTitle.TabIndex = 0;
            this.lblSelectedBookingTitle.Text = "Selected:";
            // 
            // btnViewPass
            // 
            this.btnViewPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnViewPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewPass.Enabled = false;
            this.btnViewPass.FlatAppearance.BorderSize = 0;
            this.btnViewPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewPass.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPass.ForeColor = System.Drawing.Color.White;
            this.btnViewPass.Location = new System.Drawing.Point(1053, 22);
            this.btnViewPass.Name = "btnViewPass";
            this.btnViewPass.Size = new System.Drawing.Size(127, 40);
            this.btnViewPass.TabIndex = 2;
            this.btnViewPass.Text = "View Pass";
            this.btnViewPass.UseVisualStyleBackColor = false;
            // 
            // btnCancelBooking
            // 
            this.btnCancelBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelBooking.Enabled = false;
            this.btnCancelBooking.FlatAppearance.BorderSize = 0;
            this.btnCancelBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelBooking.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelBooking.ForeColor = System.Drawing.Color.White;
            this.btnCancelBooking.Location = new System.Drawing.Point(894, 22);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(153, 40);
            this.btnCancelBooking.TabIndex = 1;
            this.btnCancelBooking.Text = "Cancel Booking";
            this.btnCancelBooking.UseVisualStyleBackColor = false;
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnViewDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewDetails.Enabled = false;
            this.btnViewDetails.FlatAppearance.BorderSize = 0;
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDetails.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDetails.ForeColor = System.Drawing.Color.White;
            this.btnViewDetails.Location = new System.Drawing.Point(702, 22);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(186, 40);
            this.btnViewDetails.TabIndex = 0;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            // 
            // panelStatistics
            // 
            this.panelStatistics.BackColor = System.Drawing.Color.White;
            this.panelStatistics.Controls.Add(this.lblTotalSpentTitle);
            this.panelStatistics.Controls.Add(this.lblTotalSpent);
            this.panelStatistics.Controls.Add(this.lblTotalCountTitle);
            this.panelStatistics.Controls.Add(this.lblTotalCount);
            this.panelStatistics.Controls.Add(this.lblCancelledCountTitle);
            this.panelStatistics.Controls.Add(this.lblCancelledCount);
            this.panelStatistics.Controls.Add(this.lblCompletedCountTitle);
            this.panelStatistics.Controls.Add(this.lblCompletedCount);
            this.panelStatistics.Controls.Add(this.lblConfirmedCountTitle);
            this.panelStatistics.Controls.Add(this.lblConfirmedCount);
            this.panelStatistics.Controls.Add(this.lblPendingCountTitle);
            this.panelStatistics.Controls.Add(this.lblPendingCount);
            this.panelStatistics.Controls.Add(this.lblStatisticsTitle);
            this.panelStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatistics.Location = new System.Drawing.Point(0, 680);
            this.panelStatistics.Name = "panelStatistics";
            this.panelStatistics.Size = new System.Drawing.Size(1200, 120);
            this.panelStatistics.TabIndex = 4;
            // 
            // lblTotalSpentTitle
            // 
            this.lblTotalSpentTitle.AutoSize = true;
            this.lblTotalSpentTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSpentTitle.Location = new System.Drawing.Point(928, 58);
            this.lblTotalSpentTitle.Name = "lblTotalSpentTitle";
            this.lblTotalSpentTitle.Size = new System.Drawing.Size(90, 18);
            this.lblTotalSpentTitle.TabIndex = 12;
            this.lblTotalSpentTitle.Text = "Total Spent:";
            // 
            // lblTotalSpent
            // 
            this.lblTotalSpent.AutoSize = true;
            this.lblTotalSpent.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSpent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTotalSpent.Location = new System.Drawing.Point(1028, 57);
            this.lblTotalSpent.Name = "lblTotalSpent";
            this.lblTotalSpent.Size = new System.Drawing.Size(63, 19);
            this.lblTotalSpent.TabIndex = 11;
            this.lblTotalSpent.Text = "$0.00";
            // 
            // lblTotalCountTitle
            // 
            this.lblTotalCountTitle.AutoSize = true;
            this.lblTotalCountTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCountTitle.Location = new System.Drawing.Point(928, 33);
            this.lblTotalCountTitle.Name = "lblTotalCountTitle";
            this.lblTotalCountTitle.Size = new System.Drawing.Size(120, 18);
            this.lblTotalCountTitle.TabIndex = 10;
            this.lblTotalCountTitle.Text = "Total Bookings:";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTotalCount.Location = new System.Drawing.Point(1054, 32);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(19, 19);
            this.lblTotalCount.TabIndex = 9;
            this.lblTotalCount.Text = "0";
            // 
            // lblCancelledCountTitle
            // 
            this.lblCancelledCountTitle.AutoSize = true;
            this.lblCancelledCountTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelledCountTitle.Location = new System.Drawing.Point(702, 58);
            this.lblCancelledCountTitle.Name = "lblCancelledCountTitle";
            this.lblCancelledCountTitle.Size = new System.Drawing.Size(89, 18);
            this.lblCancelledCountTitle.TabIndex = 8;
            this.lblCancelledCountTitle.Text = "Cancelled:";
            // 
            // lblCancelledCount
            // 
            this.lblCancelledCount.AutoSize = true;
            this.lblCancelledCount.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelledCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCancelledCount.Location = new System.Drawing.Point(797, 57);
            this.lblCancelledCount.Name = "lblCancelledCount";
            this.lblCancelledCount.Size = new System.Drawing.Size(19, 19);
            this.lblCancelledCount.TabIndex = 7;
            this.lblCancelledCount.Text = "0";
            // 
            // lblCompletedCountTitle
            // 
            this.lblCompletedCountTitle.AutoSize = true;
            this.lblCompletedCountTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletedCountTitle.Location = new System.Drawing.Point(702, 33);
            this.lblCompletedCountTitle.Name = "lblCompletedCountTitle";
            this.lblCompletedCountTitle.Size = new System.Drawing.Size(95, 18);
            this.lblCompletedCountTitle.TabIndex = 6;
            this.lblCompletedCountTitle.Text = "Completed:";
            // 
            // lblCompletedCount
            // 
            this.lblCompletedCount.AutoSize = true;
            this.lblCompletedCount.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletedCount.ForeColor = System.Drawing.Color.Blue;
            this.lblCompletedCount.Location = new System.Drawing.Point(803, 32);
            this.lblCompletedCount.Name = "lblCompletedCount";
            this.lblCompletedCount.Size = new System.Drawing.Size(19, 19);
            this.lblCompletedCount.TabIndex = 5;
            this.lblCompletedCount.Text = "0";
            // 
            // lblConfirmedCountTitle
            // 
            this.lblConfirmedCountTitle.AutoSize = true;
            this.lblConfirmedCountTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmedCountTitle.Location = new System.Drawing.Point(465, 58);
            this.lblConfirmedCountTitle.Name = "lblConfirmedCountTitle";
            this.lblConfirmedCountTitle.Size = new System.Drawing.Size(89, 18);
            this.lblConfirmedCountTitle.TabIndex = 4;
            this.lblConfirmedCountTitle.Text = "Confirmed:";
            // 
            // lblConfirmedCount
            // 
            this.lblConfirmedCount.AutoSize = true;
            this.lblConfirmedCount.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmedCount.ForeColor = System.Drawing.Color.Green;
            this.lblConfirmedCount.Location = new System.Drawing.Point(560, 57);
            this.lblConfirmedCount.Name = "lblConfirmedCount";
            this.lblConfirmedCount.Size = new System.Drawing.Size(19, 19);
            this.lblConfirmedCount.TabIndex = 3;
            this.lblConfirmedCount.Text = "0";
            // 
            // lblPendingCountTitle
            // 
            this.lblPendingCountTitle.AutoSize = true;
            this.lblPendingCountTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingCountTitle.Location = new System.Drawing.Point(465, 33);
            this.lblPendingCountTitle.Name = "lblPendingCountTitle";
            this.lblPendingCountTitle.Size = new System.Drawing.Size(72, 18);
            this.lblPendingCountTitle.TabIndex = 2;
            this.lblPendingCountTitle.Text = "Pending:";
            // 
            // lblPendingCount
            // 
            this.lblPendingCount.AutoSize = true;
            this.lblPendingCount.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingCount.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblPendingCount.Location = new System.Drawing.Point(543, 32);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Size = new System.Drawing.Size(19, 19);
            this.lblPendingCount.TabIndex = 1;
            this.lblPendingCount.Text = "0";
            // 
            // lblStatisticsTitle
            // 
            this.lblStatisticsTitle.AutoSize = true;
            this.lblStatisticsTitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatisticsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblStatisticsTitle.Location = new System.Drawing.Point(15, 16);
            this.lblStatisticsTitle.Name = "lblStatisticsTitle";
            this.lblStatisticsTitle.Size = new System.Drawing.Size(172, 23);
            this.lblStatisticsTitle.TabIndex = 0;
            this.lblStatisticsTitle.Text = "Booking Statistics";
            // 
            // BookingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelBookings);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelStatistics);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.Name = "BookingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelEase - My Bookings";
            this.panelHeader.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelBookings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelSelectedBooking.ResumeLayout(false);
            this.panelSelectedBooking.PerformLayout();
            this.panelStatistics.ResumeLayout(false);
            this.panelStatistics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblFilterTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panelBookings;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnCancelBooking;
        private System.Windows.Forms.Button btnViewPass;
        private System.Windows.Forms.Panel panelSelectedBooking;
        private System.Windows.Forms.Label lblSelectedBookingTitle;
        private System.Windows.Forms.Label lblSelectedTripTitle;
        private System.Windows.Forms.Label lblSelectedTrip;
        private System.Windows.Forms.Label lblSelectedDestinationTitle;
        private System.Windows.Forms.Label lblSelectedDestination;
        private System.Windows.Forms.Label lblSelectedDatesTitle;
        private System.Windows.Forms.Label lblSelectedDates;
        private System.Windows.Forms.Label lblSelectedStatusTitle;
        private System.Windows.Forms.Label lblSelectedStatus;
        private System.Windows.Forms.Panel panelStatistics;
        private System.Windows.Forms.Label lblStatisticsTitle;
        private System.Windows.Forms.Label lblPendingCountTitle;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Label lblConfirmedCountTitle;
        private System.Windows.Forms.Label lblConfirmedCount;
        private System.Windows.Forms.Label lblCompletedCountTitle;
        private System.Windows.Forms.Label lblCompletedCount;
        private System.Windows.Forms.Label lblCancelledCountTitle;
        private System.Windows.Forms.Label lblCancelledCount;
        private System.Windows.Forms.Label lblTotalCountTitle;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblTotalSpentTitle;
        private System.Windows.Forms.Label lblTotalSpent;
    }
}