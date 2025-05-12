namespace TravelEase
{
    partial class TripSearchForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblOperatorSearch = new System.Windows.Forms.Label();
            this.lblDestSearch = new System.Windows.Forms.Label();
            this.txtOperatorSearch = new System.Windows.Forms.TextBox();
            this.txtDestinationSearch = new System.Windows.Forms.TextBox();
            this.lstOperatorSuggestions = new System.Windows.Forms.ListBox();
            this.lstDestinationSuggestions = new System.Windows.Forms.ListBox();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.cmbDuration = new System.Windows.Forms.ComboBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.numMaxPrice = new System.Windows.Forms.NumericUpDown();
            this.lblTo = new System.Windows.Forms.Label();
            this.numMinPrice = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbDestination = new System.Windows.Forms.ComboBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.panelResults = new System.Windows.Forms.Panel();
            this.flpCardView = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvSearchResults = new System.Windows.Forms.DataGridView();
            this.panelResultsHeader = new System.Windows.Forms.Panel();
            this.btnToggleView = new System.Windows.Forms.Button();
            this.lblResultsCount = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinPrice)).BeginInit();
            this.panelResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).BeginInit();
            this.panelResultsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1600, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1560, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(169, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Search Trips";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelFilters.Controls.Add(this.lblOperatorSearch);
            this.panelFilters.Controls.Add(this.lblDestSearch);
            this.panelFilters.Controls.Add(this.txtOperatorSearch);
            this.panelFilters.Controls.Add(this.txtDestinationSearch);
            this.panelFilters.Controls.Add(this.lstOperatorSuggestions);
            this.panelFilters.Controls.Add(this.lstDestinationSuggestions);
            this.panelFilters.Controls.Add(this.cmbRating);
            this.panelFilters.Controls.Add(this.lblRating);
            this.panelFilters.Controls.Add(this.cmbDuration);
            this.panelFilters.Controls.Add(this.lblDuration);
            this.panelFilters.Controls.Add(this.lblCategory);
            this.panelFilters.Controls.Add(this.btnReset);
            this.panelFilters.Controls.Add(this.btnSearch);
            this.panelFilters.Controls.Add(this.cmbDifficulty);
            this.panelFilters.Controls.Add(this.lblDifficulty);
            this.panelFilters.Controls.Add(this.numMaxPrice);
            this.panelFilters.Controls.Add(this.lblTo);
            this.panelFilters.Controls.Add(this.numMinPrice);
            this.panelFilters.Controls.Add(this.lblPrice);
            this.panelFilters.Controls.Add(this.dtpEndDate);
            this.panelFilters.Controls.Add(this.lblEndDate);
            this.panelFilters.Controls.Add(this.dtpStartDate);
            this.panelFilters.Controls.Add(this.lblStartDate);
            this.panelFilters.Controls.Add(this.cmbCategory);
            this.panelFilters.Controls.Add(this.cmbDestination);
            this.panelFilters.Controls.Add(this.lblDestination);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 60);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1600, 306);
            this.panelFilters.TabIndex = 1;
            // 
            // lblOperatorSearch
            // 
            this.lblOperatorSearch.AutoSize = true;
            this.lblOperatorSearch.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperatorSearch.Location = new System.Drawing.Point(286, 13);
            this.lblOperatorSearch.Name = "lblOperatorSearch";
            this.lblOperatorSearch.Size = new System.Drawing.Size(189, 21);
            this.lblOperatorSearch.TabIndex = 19;
            this.lblOperatorSearch.Text = "Search Tour Operator";
            // 
            // lblDestSearch
            // 
            this.lblDestSearch.AutoSize = true;
            this.lblDestSearch.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestSearch.Location = new System.Drawing.Point(46, 13);
            this.lblDestSearch.Name = "lblDestSearch";
            this.lblDestSearch.Size = new System.Drawing.Size(166, 21);
            this.lblDestSearch.TabIndex = 18;
            this.lblDestSearch.Text = "Search Destination";
            // 
            // txtOperatorSearch
            // 
            this.txtOperatorSearch.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.txtOperatorSearch.Location = new System.Drawing.Point(286, 37);
            this.txtOperatorSearch.Name = "txtOperatorSearch";
            this.txtOperatorSearch.Size = new System.Drawing.Size(220, 28);
            this.txtOperatorSearch.TabIndex = 17;
            this.txtOperatorSearch.TextChanged += new System.EventHandler(this.txtOperatorSearch_TextChanged);
            // 
            // txtDestinationSearch
            // 
            this.txtDestinationSearch.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.txtDestinationSearch.Location = new System.Drawing.Point(46, 37);
            this.txtDestinationSearch.Name = "txtDestinationSearch";
            this.txtDestinationSearch.Size = new System.Drawing.Size(220, 28);
            this.txtDestinationSearch.TabIndex = 16;
            this.txtDestinationSearch.TextChanged += new System.EventHandler(this.txtDestinationSearch_TextChanged);
            // 
            // lstOperatorSuggestions
            // 
            this.lstOperatorSuggestions.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lstOperatorSuggestions.FormattingEnabled = true;
            this.lstOperatorSuggestions.ItemHeight = 20;
            this.lstOperatorSuggestions.Location = new System.Drawing.Point(286, 61);
            this.lstOperatorSuggestions.Name = "lstOperatorSuggestions";
            this.lstOperatorSuggestions.Size = new System.Drawing.Size(220, 4);
            this.lstOperatorSuggestions.TabIndex = 20;
            this.lstOperatorSuggestions.Visible = false;
            this.lstOperatorSuggestions.Click += new System.EventHandler(this.lstOperatorSuggestions_Click);
            // 
            // lstDestinationSuggestions
            // 
            this.lstDestinationSuggestions.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.lstDestinationSuggestions.FormattingEnabled = true;
            this.lstDestinationSuggestions.ItemHeight = 20;
            this.lstDestinationSuggestions.Location = new System.Drawing.Point(46, 61);
            this.lstDestinationSuggestions.Name = "lstDestinationSuggestions";
            this.lstDestinationSuggestions.Size = new System.Drawing.Size(220, 4);
            this.lstDestinationSuggestions.TabIndex = 21;
            this.lstDestinationSuggestions.Visible = false;
            this.lstDestinationSuggestions.Click += new System.EventHandler(this.lstDestinationSuggestions_Click);
            // 
            // cmbRating
            // 
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRating.FormattingEnabled = true;
            this.cmbRating.Location = new System.Drawing.Point(830, 182);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(120, 29);
            this.cmbRating.TabIndex = 16;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRating.Location = new System.Drawing.Point(830, 157);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(63, 21);
            this.lblRating.TabIndex = 15;
            this.lblRating.Text = "Rating";
            // 
            // cmbDuration
            // 
            this.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDuration.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDuration.FormattingEnabled = true;
            this.cmbDuration.Location = new System.Drawing.Point(830, 79);
            this.cmbDuration.Name = "cmbDuration";
            this.cmbDuration.Size = new System.Drawing.Size(120, 29);
            this.cmbDuration.TabIndex = 14;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(830, 53);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(81, 21);
            this.lblDuration.TabIndex = 13;
            this.lblDuration.Text = "Duration";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(981, 157);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(89, 21);
            this.lblCategory.TabIndex = 12;
            this.lblCategory.Text = "Category";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Gray;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(1145, 233);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(195, 54);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1355, 233);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(195, 54);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDifficulty.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "-- Any Difficulty --",
            "Easy",
            "Moderate",
            "Hard",
            "Expert"});
            this.cmbDifficulty.Location = new System.Drawing.Point(577, 182);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(220, 29);
            this.cmbDifficulty.TabIndex = 9;
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDifficulty.Location = new System.Drawing.Point(577, 157);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(78, 21);
            this.lblDifficulty.TabIndex = 8;
            this.lblDifficulty.Text = "Difficulty";
            // 
            // numMaxPrice
            // 
            this.numMaxPrice.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.numMaxPrice.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxPrice.Location = new System.Drawing.Point(1432, 78);
            this.numMaxPrice.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numMaxPrice.Name = "numMaxPrice";
            this.numMaxPrice.Size = new System.Drawing.Size(100, 28);
            this.numMaxPrice.TabIndex = 7;
            this.numMaxPrice.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(1402, 80);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(28, 21);
            this.lblTo.TabIndex = 6;
            this.lblTo.Text = "to";
            // 
            // numMinPrice
            // 
            this.numMinPrice.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.numMinPrice.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMinPrice.Location = new System.Drawing.Point(1297, 78);
            this.numMinPrice.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numMinPrice.Name = "numMinPrice";
            this.numMinPrice.Size = new System.Drawing.Size(100, 28);
            this.numMinPrice.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(1297, 53);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(109, 21);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Price Range";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(1141, 79);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(150, 28);
            this.dtpEndDate.TabIndex = 3;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(1141, 54);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(89, 21);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(981, 79);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(150, 28);
            this.dtpStartDate.TabIndex = 2;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(981, 54);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(97, 21);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(981, 182);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(120, 29);
            this.cmbCategory.TabIndex = 4;
            // 
            // cmbDestination
            // 
            this.cmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestination.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDestination.FormattingEnabled = true;
            this.cmbDestination.Location = new System.Drawing.Point(573, 78);
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(224, 29);
            this.cmbDestination.TabIndex = 1;
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.Location = new System.Drawing.Point(573, 53);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(104, 21);
            this.lblDestination.TabIndex = 0;
            this.lblDestination.Text = "Destination";
            // 
            // panelResults
            // 
            this.panelResults.Controls.Add(this.flpCardView);
            this.panelResults.Controls.Add(this.dgvSearchResults);
            this.panelResults.Controls.Add(this.panelResultsHeader);
            this.panelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResults.Location = new System.Drawing.Point(0, 366);
            this.panelResults.Name = "panelResults";
            this.panelResults.Padding = new System.Windows.Forms.Padding(20);
            this.panelResults.Size = new System.Drawing.Size(1600, 736);
            this.panelResults.TabIndex = 2;
            // 
            // flpCardView
            // 
            this.flpCardView.AutoScroll = true;
            this.flpCardView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCardView.Location = new System.Drawing.Point(20, 70);
            this.flpCardView.Name = "flpCardView";
            this.flpCardView.Size = new System.Drawing.Size(1560, 646);
            this.flpCardView.TabIndex = 2;
            this.flpCardView.Visible = false;
            // 
            // dgvSearchResults
            // 
            this.dgvSearchResults.AllowUserToAddRows = false;
            this.dgvSearchResults.AllowUserToDeleteRows = false;
            this.dgvSearchResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchResults.Location = new System.Drawing.Point(20, 70);
            this.dgvSearchResults.Name = "dgvSearchResults";
            this.dgvSearchResults.ReadOnly = true;
            this.dgvSearchResults.RowHeadersWidth = 51;
            this.dgvSearchResults.RowTemplate.Height = 24;
            this.dgvSearchResults.Size = new System.Drawing.Size(1560, 646);
            this.dgvSearchResults.TabIndex = 1;
            // 
            // panelResultsHeader
            // 
            this.panelResultsHeader.Controls.Add(this.btnToggleView);
            this.panelResultsHeader.Controls.Add(this.lblResultsCount);
            this.panelResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResultsHeader.Location = new System.Drawing.Point(20, 20);
            this.panelResultsHeader.Name = "panelResultsHeader";
            this.panelResultsHeader.Size = new System.Drawing.Size(1560, 50);
            this.panelResultsHeader.TabIndex = 0;
            // 
            // btnToggleView
            // 
            this.btnToggleView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnToggleView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleView.FlatAppearance.BorderSize = 0;
            this.btnToggleView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleView.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleView.ForeColor = System.Drawing.Color.White;
            this.btnToggleView.Location = new System.Drawing.Point(1370, 8);
            this.btnToggleView.Name = "btnToggleView";
            this.btnToggleView.Size = new System.Drawing.Size(180, 35);
            this.btnToggleView.TabIndex = 1;
            this.btnToggleView.Text = "Switch to Card View";
            this.btnToggleView.UseVisualStyleBackColor = false;
            this.btnToggleView.Click += new System.EventHandler(this.btnToggleView_Click);
            // 
            // lblResultsCount
            // 
            this.lblResultsCount.AutoSize = true;
            this.lblResultsCount.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblResultsCount.Location = new System.Drawing.Point(3, 15);
            this.lblResultsCount.Name = "lblResultsCount";
            this.lblResultsCount.Size = new System.Drawing.Size(363, 19);
            this.lblResultsCount.TabIndex = 0;
            this.lblResultsCount.Text = "Use the search filters to find your perfect trip";
            // 
            // TripSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 1102);
            this.Controls.Add(this.panelResults);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TripSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trip Search";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinPrice)).EndInit();
            this.panelResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).EndInit();
            this.panelResultsHeader.ResumeLayout(false);
            this.panelResultsHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.ComboBox cmbDestination;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numMinPrice;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.NumericUpDown numMaxPrice;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.Panel panelResultsHeader;
        private System.Windows.Forms.Label lblResultsCount;
        private System.Windows.Forms.Button btnToggleView;
        private System.Windows.Forms.DataGridView dgvSearchResults;
        private System.Windows.Forms.FlowLayoutPanel flpCardView;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbDuration;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.TextBox txtDestinationSearch;
        private System.Windows.Forms.TextBox txtOperatorSearch;
        private System.Windows.Forms.Label lblDestSearch;
        private System.Windows.Forms.Label lblOperatorSearch;
        private System.Windows.Forms.ListBox lstDestinationSuggestions;
        private System.Windows.Forms.ListBox lstOperatorSuggestions;
    }
}