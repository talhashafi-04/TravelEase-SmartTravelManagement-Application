namespace TravelEase
{
    partial class WishlistForm
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
            this.dgvWishlist = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.btnSetPriceAlert = new System.Windows.Forms.Button();
            this.btnBook = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.chkShowPriceAlerts = new System.Windows.Forms.CheckBox();
            this.btnSortByDate = new System.Windows.Forms.Button();
            this.btnSortByPrice = new System.Windows.Forms.Button();
            this.lblSort = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelPriceAlerts = new System.Windows.Forms.Panel();
            this.lblPriceAlerts = new System.Windows.Forms.Label();
            this.lblPriceAlertsTitle = new System.Windows.Forms.Label();
            this.panelPriceDropped = new System.Windows.Forms.Panel();
            this.lblPriceDropped = new System.Windows.Forms.Label();
            this.lblPriceDroppedTitle = new System.Windows.Forms.Label();
            this.panelTotalItems = new System.Windows.Forms.Panel();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.lblTotalItemsTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWishlist)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelPriceAlerts.SuspendLayout();
            this.panelPriceDropped.SuspendLayout();
            this.panelTotalItems.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(1200, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(152, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "My Wishlist";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1160, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dgvWishlist);
            this.panelMain.Controls.Add(this.panelButtons);
            this.panelMain.Controls.Add(this.panelFilter);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 180);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1200, 570);
            this.panelMain.TabIndex = 2;
            // 
            // dgvWishlist
            // 
            this.dgvWishlist.AllowUserToAddRows = false;
            this.dgvWishlist.AllowUserToDeleteRows = false;
            this.dgvWishlist.BackgroundColor = System.Drawing.Color.White;
            this.dgvWishlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWishlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWishlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWishlist.Location = new System.Drawing.Point(20, 70);
            this.dgvWishlist.Name = "dgvWishlist";
            this.dgvWishlist.ReadOnly = true;
            this.dgvWishlist.RowHeadersWidth = 51;
            this.dgvWishlist.RowTemplate.Height = 40;
            this.dgvWishlist.Size = new System.Drawing.Size(1160, 400);
            this.dgvWishlist.TabIndex = 2;
            this.dgvWishlist.SelectionChanged += new System.EventHandler(this.dgvWishlist_SelectionChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnAddNote);
            this.panelButtons.Controls.Add(this.btnSetPriceAlert);
            this.panelButtons.Controls.Add(this.btnBook);
            this.panelButtons.Controls.Add(this.btnViewDetails);
            this.panelButtons.Controls.Add(this.btnRemove);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(20, 470);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1160, 80);
            this.panelButtons.TabIndex = 1;
            // 
            // btnAddNote
            // 
            this.btnAddNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAddNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNote.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNote.ForeColor = System.Drawing.Color.White;
            this.btnAddNote.Location = new System.Drawing.Point(860, 20);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(160, 40);
            this.btnAddNote.TabIndex = 4;
            this.btnAddNote.Text = "Add Note";
            this.btnAddNote.UseVisualStyleBackColor = false;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // btnSetPriceAlert
            // 
            this.btnSetPriceAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnSetPriceAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPriceAlert.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetPriceAlert.ForeColor = System.Drawing.Color.White;
            this.btnSetPriceAlert.Location = new System.Drawing.Point(680, 20);
            this.btnSetPriceAlert.Name = "btnSetPriceAlert";
            this.btnSetPriceAlert.Size = new System.Drawing.Size(160, 40);
            this.btnSetPriceAlert.TabIndex = 3;
            this.btnSetPriceAlert.Text = "Set Price Alert";
            this.btnSetPriceAlert.UseVisualStyleBackColor = false;
            this.btnSetPriceAlert.Click += new System.EventHandler(this.btnSetPriceAlert_Click);
            // 
            // btnBook
            // 
            this.btnBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBook.ForeColor = System.Drawing.Color.White;
            this.btnBook.Location = new System.Drawing.Point(500, 20);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(160, 40);
            this.btnBook.TabIndex = 2;
            this.btnBook.Text = "Book Now";
            this.btnBook.UseVisualStyleBackColor = false;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDetails.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDetails.ForeColor = System.Drawing.Color.White;
            this.btnViewDetails.Location = new System.Drawing.Point(320, 20);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(160, 40);
            this.btnViewDetails.TabIndex = 1;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(140, 20);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(160, 40);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelFilter.Controls.Add(this.chkShowPriceAlerts);
            this.panelFilter.Controls.Add(this.btnSortByDate);
            this.panelFilter.Controls.Add(this.btnSortByPrice);
            this.panelFilter.Controls.Add(this.lblSort);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(20, 20);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1160, 50);
            this.panelFilter.TabIndex = 0;
            // 
            // chkShowPriceAlerts
            // 
            this.chkShowPriceAlerts.AutoSize = true;
            this.chkShowPriceAlerts.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPriceAlerts.Location = new System.Drawing.Point(520, 13);
            this.chkShowPriceAlerts.Name = "chkShowPriceAlerts";
            this.chkShowPriceAlerts.Size = new System.Drawing.Size(212, 25);
            this.chkShowPriceAlerts.TabIndex = 3;
            this.chkShowPriceAlerts.Text = "Show Price Alerts Only";
            this.chkShowPriceAlerts.UseVisualStyleBackColor = true;
            this.chkShowPriceAlerts.CheckedChanged += new System.EventHandler(this.chkShowPriceAlerts_CheckedChanged);
            // 
            // btnSortByDate
            // 
            this.btnSortByDate.BackColor = System.Drawing.Color.White;
            this.btnSortByDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortByDate.Location = new System.Drawing.Point(250, 10);
            this.btnSortByDate.Name = "btnSortByDate";
            this.btnSortByDate.Size = new System.Drawing.Size(120, 30);
            this.btnSortByDate.TabIndex = 2;
            this.btnSortByDate.Text = "By Date";
            this.btnSortByDate.UseVisualStyleBackColor = false;
            // 
            // btnSortByPrice
            // 
            this.btnSortByPrice.BackColor = System.Drawing.Color.White;
            this.btnSortByPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSortByPrice.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSortByPrice.Location = new System.Drawing.Point(120, 10);
            this.btnSortByPrice.Name = "btnSortByPrice";
            this.btnSortByPrice.Size = new System.Drawing.Size(120, 30);
            this.btnSortByPrice.TabIndex = 1;
            this.btnSortByPrice.Text = "By Price";
            this.btnSortByPrice.UseVisualStyleBackColor = false;
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSort.Location = new System.Drawing.Point(20, 15);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(69, 19);
            this.lblSort.TabIndex = 0;
            this.lblSort.Text = "Sort by:";
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelStats.Controls.Add(this.panelPriceAlerts);
            this.panelStats.Controls.Add(this.panelPriceDropped);
            this.panelStats.Controls.Add(this.panelTotalItems);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 60);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(20);
            this.panelStats.Size = new System.Drawing.Size(1200, 120);
            this.panelStats.TabIndex = 1;
            // 
            // panelPriceAlerts
            // 
            this.panelPriceAlerts.BackColor = System.Drawing.Color.White;
            this.panelPriceAlerts.Controls.Add(this.lblPriceAlerts);
            this.panelPriceAlerts.Controls.Add(this.lblPriceAlertsTitle);
            this.panelPriceAlerts.Location = new System.Drawing.Point(820, 20);
            this.panelPriceAlerts.Name = "panelPriceAlerts";
            this.panelPriceAlerts.Size = new System.Drawing.Size(360, 80);
            this.panelPriceAlerts.TabIndex = 2;
            // 
            // lblPriceAlerts
            // 
            this.lblPriceAlerts.AutoSize = true;
            this.lblPriceAlerts.Font = new System.Drawing.Font("Century Gothic", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblPriceAlerts.Location = new System.Drawing.Point(160, 26);
            this.lblPriceAlerts.Name = "lblPriceAlerts";
            this.lblPriceAlerts.Size = new System.Drawing.Size(40, 44);
            this.lblPriceAlerts.TabIndex = 1;
            this.lblPriceAlerts.Text = "0";
            // 
            // lblPriceAlertsTitle
            // 
            this.lblPriceAlertsTitle.AutoSize = true;
            this.lblPriceAlertsTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceAlertsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPriceAlertsTitle.Location = new System.Drawing.Point(130, 5);
            this.lblPriceAlertsTitle.Name = "lblPriceAlertsTitle";
            this.lblPriceAlertsTitle.Size = new System.Drawing.Size(101, 21);
            this.lblPriceAlertsTitle.TabIndex = 0;
            this.lblPriceAlertsTitle.Text = "Price Alerts";
            // 
            // panelPriceDropped
            // 
            this.panelPriceDropped.BackColor = System.Drawing.Color.White;
            this.panelPriceDropped.Controls.Add(this.lblPriceDropped);
            this.panelPriceDropped.Controls.Add(this.lblPriceDroppedTitle);
            this.panelPriceDropped.Location = new System.Drawing.Point(420, 20);
            this.panelPriceDropped.Name = "panelPriceDropped";
            this.panelPriceDropped.Size = new System.Drawing.Size(360, 80);
            this.panelPriceDropped.TabIndex = 1;
            // 
            // lblPriceDropped
            // 
            this.lblPriceDropped.AutoSize = true;
            this.lblPriceDropped.Font = new System.Drawing.Font("Century Gothic", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceDropped.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblPriceDropped.Location = new System.Drawing.Point(158, 26);
            this.lblPriceDropped.Name = "lblPriceDropped";
            this.lblPriceDropped.Size = new System.Drawing.Size(40, 44);
            this.lblPriceDropped.TabIndex = 1;
            this.lblPriceDropped.Text = "0";
            // 
            // lblPriceDroppedTitle
            // 
            this.lblPriceDroppedTitle.AutoSize = true;
            this.lblPriceDroppedTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceDroppedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPriceDroppedTitle.Location = new System.Drawing.Point(120, 5);
            this.lblPriceDroppedTitle.Name = "lblPriceDroppedTitle";
            this.lblPriceDroppedTitle.Size = new System.Drawing.Size(131, 21);
            this.lblPriceDroppedTitle.TabIndex = 0;
            this.lblPriceDroppedTitle.Text = "Price Dropped";
            // 
            // panelTotalItems
            // 
            this.panelTotalItems.BackColor = System.Drawing.Color.White;
            this.panelTotalItems.Controls.Add(this.lblTotalItems);
            this.panelTotalItems.Controls.Add(this.lblTotalItemsTitle);
            this.panelTotalItems.Location = new System.Drawing.Point(20, 20);
            this.panelTotalItems.Name = "panelTotalItems";
            this.panelTotalItems.Size = new System.Drawing.Size(360, 80);
            this.panelTotalItems.TabIndex = 0;
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Century Gothic", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTotalItems.Location = new System.Drawing.Point(160, 26);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(40, 44);
            this.lblTotalItems.TabIndex = 1;
            this.lblTotalItems.Text = "0";
            // 
            // lblTotalItemsTitle
            // 
            this.lblTotalItemsTitle.AutoSize = true;
            this.lblTotalItemsTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItemsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotalItemsTitle.Location = new System.Drawing.Point(135, 5);
            this.lblTotalItemsTitle.Name = "lblTotalItemsTitle";
            this.lblTotalItemsTitle.Size = new System.Drawing.Size(101, 21);
            this.lblTotalItemsTitle.TabIndex = 0;
            this.lblTotalItemsTitle.Text = "Total Items";
            // 
            // WishlistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WishlistForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Wishlist";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWishlist)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelPriceAlerts.ResumeLayout(false);
            this.panelPriceAlerts.PerformLayout();
            this.panelPriceDropped.ResumeLayout(false);
            this.panelPriceDropped.PerformLayout();
            this.panelTotalItems.ResumeLayout(false);
            this.panelTotalItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Panel panelTotalItems;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label lblTotalItemsTitle;
        private System.Windows.Forms.Panel panelPriceDropped;
        private System.Windows.Forms.Label lblPriceDropped;
        private System.Windows.Forms.Label lblPriceDroppedTitle;
        private System.Windows.Forms.Panel panelPriceAlerts;
        private System.Windows.Forms.Label lblPriceAlerts;
        private System.Windows.Forms.Label lblPriceAlertsTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dgvWishlist;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnSetPriceAlert;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.Button btnSortByPrice;
        private System.Windows.Forms.Button btnSortByDate;
        private System.Windows.Forms.CheckBox chkShowPriceAlerts;
    }
}