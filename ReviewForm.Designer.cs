namespace TravelEase
{
    partial class ReviewsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnDeleteReview = new System.Windows.Forms.Button();
            this.btnEditReview = new System.Windows.Forms.Button();
            this.btnWriteReview = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.cmbReviewType = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelPendingReviews = new System.Windows.Forms.Panel();
            this.lblPendingReviews = new System.Windows.Forms.Label();
            this.lblPendingReviewsTitle = new System.Windows.Forms.Label();
            this.panelAverageRating = new System.Windows.Forms.Panel();
            this.lblAverageRating = new System.Windows.Forms.Label();
            this.lblAverageRatingTitle = new System.Windows.Forms.Label();
            this.panelTotalReviews = new System.Windows.Forms.Panel();
            this.lblTotalReviews = new System.Windows.Forms.Label();
            this.lblTotalReviewsTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelPendingReviews.SuspendLayout();
            this.panelAverageRating.SuspendLayout();
            this.panelTotalReviews.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(167, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "My Reviews";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(960, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dgvReviews);
            this.panelMain.Controls.Add(this.panelButtons);
            this.panelMain.Controls.Add(this.panelFilter);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 180);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1000, 520);
            this.panelMain.TabIndex = 2;
            // 
            // dgvReviews
            // 
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AllowUserToDeleteRows = false;
            this.dgvReviews.BackgroundColor = System.Drawing.Color.White;
            this.dgvReviews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReviews.Location = new System.Drawing.Point(20, 70);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersWidth = 51;
            this.dgvReviews.RowTemplate.Height = 40;
            this.dgvReviews.Size = new System.Drawing.Size(960, 350);
            this.dgvReviews.TabIndex = 2;
            this.dgvReviews.SelectionChanged += new System.EventHandler(this.dgvReviews_SelectionChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnDeleteReview);
            this.panelButtons.Controls.Add(this.btnEditReview);
            this.panelButtons.Controls.Add(this.btnWriteReview);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(20, 420);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(960, 80);
            this.panelButtons.TabIndex = 1;
            // 
            // btnDeleteReview
            // 
            this.btnDeleteReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDeleteReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteReview.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteReview.ForeColor = System.Drawing.Color.White;
            this.btnDeleteReview.Location = new System.Drawing.Point(600, 20);
            this.btnDeleteReview.Name = "btnDeleteReview";
            this.btnDeleteReview.Size = new System.Drawing.Size(160, 40);
            this.btnDeleteReview.TabIndex = 2;
            this.btnDeleteReview.Text = "Delete Review";
            this.btnDeleteReview.UseVisualStyleBackColor = false;
            this.btnDeleteReview.Click += new System.EventHandler(this.btnDeleteReview_Click);
            // 
            // btnEditReview
            // 
            this.btnEditReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEditReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditReview.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditReview.ForeColor = System.Drawing.Color.White;
            this.btnEditReview.Location = new System.Drawing.Point(390, 20);
            this.btnEditReview.Name = "btnEditReview";
            this.btnEditReview.Size = new System.Drawing.Size(160, 40);
            this.btnEditReview.TabIndex = 1;
            this.btnEditReview.Text = "Edit Review";
            this.btnEditReview.UseVisualStyleBackColor = false;
            this.btnEditReview.Click += new System.EventHandler(this.btnEditReview_Click);
            // 
            // btnWriteReview
            // 
            this.btnWriteReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnWriteReview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteReview.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWriteReview.ForeColor = System.Drawing.Color.White;
            this.btnWriteReview.Location = new System.Drawing.Point(180, 20);
            this.btnWriteReview.Name = "btnWriteReview";
            this.btnWriteReview.Size = new System.Drawing.Size(160, 40);
            this.btnWriteReview.TabIndex = 0;
            this.btnWriteReview.Text = "Write Review";
            this.btnWriteReview.UseVisualStyleBackColor = false;
            this.btnWriteReview.Click += new System.EventHandler(this.btnWriteReview_Click);
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelFilter.Controls.Add(this.cmbReviewType);
            this.panelFilter.Controls.Add(this.lblFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(20, 20);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(960, 50);
            this.panelFilter.TabIndex = 0;
            // 
            // cmbReviewType
            // 
            this.cmbReviewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReviewType.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReviewType.FormattingEnabled = true;
            this.cmbReviewType.Items.AddRange(new object[] {
            "All",
            "Trip",
            "Hotel",
            "Guide",
            "Transport"});
            this.cmbReviewType.Location = new System.Drawing.Point(120, 11);
            this.cmbReviewType.Name = "cmbReviewType";
            this.cmbReviewType.Size = new System.Drawing.Size(200, 29);
            this.cmbReviewType.TabIndex = 1;
            this.cmbReviewType.SelectedIndexChanged += new System.EventHandler(this.cmbReviewType_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(20, 15);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(76, 19);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter by:";
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelStats.Controls.Add(this.panelPendingReviews);
            this.panelStats.Controls.Add(this.panelAverageRating);
            this.panelStats.Controls.Add(this.panelTotalReviews);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 60);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(20);
            this.panelStats.Size = new System.Drawing.Size(1000, 120);
            this.panelStats.TabIndex = 1;
            // 
            // panelPendingReviews
            // 
            this.panelPendingReviews.BackColor = System.Drawing.Color.White;
            this.panelPendingReviews.Controls.Add(this.lblPendingReviews);
            this.panelPendingReviews.Controls.Add(this.lblPendingReviewsTitle);
            this.panelPendingReviews.Location = new System.Drawing.Point(680, 20);
            this.panelPendingReviews.Name = "panelPendingReviews";
            this.panelPendingReviews.Size = new System.Drawing.Size(300, 80);
            this.panelPendingReviews.TabIndex = 2;
            // 
            // lblPendingReviews
            // 
            this.lblPendingReviews.AutoSize = true;
            this.lblPendingReviews.Font = new System.Drawing.Font("Century Gothic", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingReviews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(185)))), ((int)(((byte)(0)))));
            this.lblPendingReviews.Location = new System.Drawing.Point(143, 26);
            this.lblPendingReviews.Name = "lblPendingReviews";
            this.lblPendingReviews.Size = new System.Drawing.Size(40, 44);
            this.lblPendingReviews.TabIndex = 1;
            this.lblPendingReviews.Text = "0";
            // 
            // lblPendingReviewsTitle
            // 
            this.lblPendingReviewsTitle.AutoSize = true;
            this.lblPendingReviewsTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingReviewsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPendingReviewsTitle.Location = new System.Drawing.Point(100, 5);
            this.lblPendingReviewsTitle.Name = "lblPendingReviewsTitle";
            this.lblPendingReviewsTitle.Size = new System.Drawing.Size(149, 21);
            this.lblPendingReviewsTitle.TabIndex = 0;
            this.lblPendingReviewsTitle.Text = "Pending Reviews";
            this.lblPendingReviewsTitle.Click += new System.EventHandler(this.lblPendingReviewsTitle_Click);
            // 
            // panelAverageRating
            // 
            this.panelAverageRating.BackColor = System.Drawing.Color.White;
            this.panelAverageRating.Controls.Add(this.lblAverageRating);
            this.panelAverageRating.Controls.Add(this.lblAverageRatingTitle);
            this.panelAverageRating.Location = new System.Drawing.Point(350, 20);
            this.panelAverageRating.Name = "panelAverageRating";
            this.panelAverageRating.Size = new System.Drawing.Size(300, 80);
            this.panelAverageRating.TabIndex = 1;
            // 
            // lblAverageRating
            // 
            this.lblAverageRating.AutoSize = true;
            this.lblAverageRating.Font = new System.Drawing.Font("Century Gothic", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageRating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblAverageRating.Location = new System.Drawing.Point(120, 26);
            this.lblAverageRating.Name = "lblAverageRating";
            this.lblAverageRating.Size = new System.Drawing.Size(71, 44);
            this.lblAverageRating.TabIndex = 1;
            this.lblAverageRating.Text = "0.0";
            // 
            // lblAverageRatingTitle
            // 
            this.lblAverageRatingTitle.AutoSize = true;
            this.lblAverageRatingTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAverageRatingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAverageRatingTitle.Location = new System.Drawing.Point(100, 5);
            this.lblAverageRatingTitle.Name = "lblAverageRatingTitle";
            this.lblAverageRatingTitle.Size = new System.Drawing.Size(142, 21);
            this.lblAverageRatingTitle.TabIndex = 0;
            this.lblAverageRatingTitle.Text = "Average Rating";
            // 
            // panelTotalReviews
            // 
            this.panelTotalReviews.BackColor = System.Drawing.Color.White;
            this.panelTotalReviews.Controls.Add(this.lblTotalReviews);
            this.panelTotalReviews.Controls.Add(this.lblTotalReviewsTitle);
            this.panelTotalReviews.Location = new System.Drawing.Point(20, 20);
            this.panelTotalReviews.Name = "panelTotalReviews";
            this.panelTotalReviews.Size = new System.Drawing.Size(300, 80);
            this.panelTotalReviews.TabIndex = 0;
            // 
            // lblTotalReviews
            // 
            this.lblTotalReviews.AutoSize = true;
            this.lblTotalReviews.Font = new System.Drawing.Font("Century Gothic", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReviews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblTotalReviews.Location = new System.Drawing.Point(136, 26);
            this.lblTotalReviews.Name = "lblTotalReviews";
            this.lblTotalReviews.Size = new System.Drawing.Size(40, 44);
            this.lblTotalReviews.TabIndex = 1;
            this.lblTotalReviews.Text = "0";
            // 
            // lblTotalReviewsTitle
            // 
            this.lblTotalReviewsTitle.AutoSize = true;
            this.lblTotalReviewsTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReviewsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalReviewsTitle.Location = new System.Drawing.Point(100, 5);
            this.lblTotalReviewsTitle.Name = "lblTotalReviewsTitle";
            this.lblTotalReviewsTitle.Size = new System.Drawing.Size(123, 21);
            this.lblTotalReviewsTitle.TabIndex = 0;
            this.lblTotalReviewsTitle.Text = "Total Reviews";
            // 
            // ReviewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReviewsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Reviews";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelPendingReviews.ResumeLayout(false);
            this.panelPendingReviews.PerformLayout();
            this.panelAverageRating.ResumeLayout(false);
            this.panelAverageRating.PerformLayout();
            this.panelTotalReviews.ResumeLayout(false);
            this.panelTotalReviews.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Panel panelTotalReviews;
        private System.Windows.Forms.Label lblTotalReviews;
        private System.Windows.Forms.Label lblTotalReviewsTitle;
        private System.Windows.Forms.Panel panelAverageRating;
        private System.Windows.Forms.Label lblAverageRating;
        private System.Windows.Forms.Label lblAverageRatingTitle;
        private System.Windows.Forms.Panel panelPendingReviews;
        private System.Windows.Forms.Label lblPendingReviews;
        private System.Windows.Forms.Label lblPendingReviewsTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dgvReviews;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnDeleteReview;
        private System.Windows.Forms.Button btnEditReview;
        private System.Windows.Forms.Button btnWriteReview;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.ComboBox cmbReviewType;
        private System.Windows.Forms.Label lblFilter;
    }
}