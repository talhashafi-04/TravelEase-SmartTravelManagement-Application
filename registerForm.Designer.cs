namespace TravelEase
{
    partial class RegisterForm
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();

            this.panelRight = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();

            // Common fields for all users
            this.lblUserRole = new System.Windows.Forms.Label();
            this.cmbUserRole = new System.Windows.Forms.ComboBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();

            // Traveler-specific fields
            this.lblCNIC = new System.Windows.Forms.Label();
            this.txtCNIC = new System.Windows.Forms.TextBox();
            this.lblDOB = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblNationality = new System.Windows.Forms.Label();
            this.cmbNationality = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.txtEmergencyContact = new System.Windows.Forms.TextBox();

            // Service Provider-specific fields
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.cmbServiceType = new System.Windows.Forms.ComboBox();

            // Tour Operator-specific fields
            this.lblBusinessNumber = new System.Windows.Forms.Label();
            this.txtBusinessNumber = new System.Windows.Forms.TextBox();
            this.lblEstablishedDate = new System.Windows.Forms.Label();
            this.dtpEstablishedDate = new System.Windows.Forms.DateTimePicker();
            this.lblCompanyDesc = new System.Windows.Forms.Label();
            this.txtCompanyDesc = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();

            // Form controls
            this.btnRegister = new System.Windows.Forms.Button();
            this.lnkLogin = new System.Windows.Forms.LinkLabel();

            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelLeft.Controls.Add(this.lblAppName);
            this.panelLeft.Controls.Add(this.pbLogo);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(300, 700);
            this.panelLeft.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(70, 320);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(161, 37);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "TravelEase";
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(75, 150);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(150, 150);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.AutoScroll = true;
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.lnkLogin);
            this.panelRight.Controls.Add(this.btnRegister);
            this.panelRight.Controls.Add(this.lblTitle);
            this.panelRight.Controls.Add(this.btnClose);

            // Add common user fields
            this.panelRight.Controls.Add(this.lblUserRole);
            this.panelRight.Controls.Add(this.cmbUserRole);
            this.panelRight.Controls.Add(this.lblFirstName);
            this.panelRight.Controls.Add(this.txtFirstName);
            this.panelRight.Controls.Add(this.lblLastName);
            this.panelRight.Controls.Add(this.txtLastName);
            this.panelRight.Controls.Add(this.lblEmail);
            this.panelRight.Controls.Add(this.txtEmail);
            this.panelRight.Controls.Add(this.lblPassword);
            this.panelRight.Controls.Add(this.txtPassword);
            this.panelRight.Controls.Add(this.lblConfirmPassword);
            this.panelRight.Controls.Add(this.txtConfirmPassword);
            this.panelRight.Controls.Add(this.lblPhone);
            this.panelRight.Controls.Add(this.txtPhone);
            this.panelRight.Controls.Add(this.lblAddress);
            this.panelRight.Controls.Add(this.txtAddress);

            // Add traveler-specific fields
            this.panelRight.Controls.Add(this.lblCNIC);
            this.panelRight.Controls.Add(this.txtCNIC);
            this.panelRight.Controls.Add(this.lblDOB);
            this.panelRight.Controls.Add(this.dtpDateOfBirth);
            this.panelRight.Controls.Add(this.lblNationality);
            this.panelRight.Controls.Add(this.cmbNationality);
            this.panelRight.Controls.Add(this.lblLanguage);
            this.panelRight.Controls.Add(this.cmbLanguage);
            this.panelRight.Controls.Add(this.lblEmergency);
            this.panelRight.Controls.Add(this.txtEmergencyContact);

            // Add service provider-specific fields
            this.panelRight.Controls.Add(this.lblCompanyName);
            this.panelRight.Controls.Add(this.txtCompanyName);
            this.panelRight.Controls.Add(this.lblServiceType);
            this.panelRight.Controls.Add(this.cmbServiceType);

            // Add tour operator-specific fields
            this.panelRight.Controls.Add(this.lblBusinessNumber);
            this.panelRight.Controls.Add(this.txtBusinessNumber);
            this.panelRight.Controls.Add(this.lblEstablishedDate);
            this.panelRight.Controls.Add(this.dtpEstablishedDate);
            this.panelRight.Controls.Add(this.lblCompanyDesc);
            this.panelRight.Controls.Add(this.txtCompanyDesc);
            this.panelRight.Controls.Add(this.lblWebsite);
            this.panelRight.Controls.Add(this.txtWebsite);

            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(300, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(550, 700);
            this.panelRight.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(288, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create New Account";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnClose.Location = new System.Drawing.Point(510, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserRole.Location = new System.Drawing.Point(32, 100);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(95, 21);
            this.lblUserRole.TabIndex = 1;
            this.lblUserRole.Text = "User Role:";
            // 
            // cmbUserRole
            // 
            this.cmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserRole.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUserRole.FormattingEnabled = true;
            this.cmbUserRole.Location = new System.Drawing.Point(200, 97);
            this.cmbUserRole.Name = "cmbUserRole";
            this.cmbUserRole.Size = new System.Drawing.Size(250, 29);
            this.cmbUserRole.TabIndex = 1;
            this.cmbUserRole.SelectedIndexChanged += new System.EventHandler(this.cmbUserRole_SelectedIndexChanged);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(32, 140);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(103, 21);
            this.lblFirstName.TabIndex = 2;
            this.lblFirstName.Text = "First Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(200, 137);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(250, 28);
            this.txtFirstName.TabIndex = 2;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(32, 180);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(103, 21);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(200, 177);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(250, 28);
            this.txtLastName.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(32, 220);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(59, 21);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(200, 217);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 28);
            this.txtEmail.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(32, 260);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(95, 21);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(200, 257);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(250, 28);
            this.txtPassword.TabIndex = 5;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassword.Location = new System.Drawing.Point(32, 300);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(162, 21);
            this.lblConfirmPassword.TabIndex = 6;
            this.lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(200, 297);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '•';
            this.txtConfirmPassword.Size = new System.Drawing.Size(250, 28);
            this.txtConfirmPassword.TabIndex = 6;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(32, 340);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(67, 21);
            this.lblPhone.TabIndex = 7;
            this.lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(200, 337);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 28);
            this.txtPhone.TabIndex = 7;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(32, 380);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(82, 21);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(200, 377);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(250, 28);
            this.txtAddress.TabIndex = 8;
            // 
            // lblCNIC
            // 
            this.lblCNIC.AutoSize = true;
            this.lblCNIC.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCNIC.Location = new System.Drawing.Point(32, 420);
            this.lblCNIC.Name = "lblCNIC";
            this.lblCNIC.Size = new System.Drawing.Size(59, 21);
            this.lblCNIC.TabIndex = 9;
            this.lblCNIC.Text = "CNIC:";
            // 
            // txtCNIC
            // 
            this.txtCNIC.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCNIC.Location = new System.Drawing.Point(200, 417);
            this.txtCNIC.Name = "txtCNIC";
            this.txtCNIC.Size = new System.Drawing.Size(250, 28);
            this.txtCNIC.TabIndex = 9;
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOB.Location = new System.Drawing.Point(32, 460);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(123, 21);
            this.lblDOB.TabIndex = 10;
            this.lblDOB.Text = "Date of Birth:";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(200, 457);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(250, 28);
            this.dtpDateOfBirth.TabIndex = 10;
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNationality.Location = new System.Drawing.Point(32, 500);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(107, 21);
            this.lblNationality.TabIndex = 11;
            this.lblNationality.Text = "Nationality:";
            // 
            // cmbNationality
            // 
            this.cmbNationality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNationality.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNationality.FormattingEnabled = true;
            this.cmbNationality.Location = new System.Drawing.Point(200, 497);
            this.cmbNationality.Name = "cmbNationality";
            this.cmbNationality.Size = new System.Drawing.Size(250, 29);
            this.cmbNationality.TabIndex = 11;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.Location = new System.Drawing.Point(32, 540);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(101, 21);
            this.lblLanguage.TabIndex = 12;
            this.lblLanguage.Text = "Language:";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(200, 537);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(250, 29);
            this.cmbLanguage.TabIndex = 12;
            // 
            // lblEmergency
            // 
            this.lblEmergency.AutoSize = true;
            this.lblEmergency.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmergency.Location = new System.Drawing.Point(32, 580);
            this.lblEmergency.Name = "lblEmergency";
            this.lblEmergency.Size = new System.Drawing.Size(147, 21);
            this.lblEmergency.TabIndex = 13;
            this.lblEmergency.Text = "Emergency No.:";
            // 
            // txtEmergencyContact
            // 
            this.txtEmergencyContact.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmergencyContact.Location = new System.Drawing.Point(200, 577);
            this.txtEmergencyContact.Name = "txtEmergencyContact";
            this.txtEmergencyContact.Size = new System.Drawing.Size(250, 28);
            this.txtEmergencyContact.TabIndex = 13;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(32, 420);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(153, 21);
            this.lblCompanyName.TabIndex = 14;
            this.lblCompanyName.Text = "Company Name:";
            this.lblCompanyName.Visible = false;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.Location = new System.Drawing.Point(200, 417);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(250, 28);
            this.txtCompanyName.TabIndex = 14;
            this.txtCompanyName.Visible = false;
            // 
            // lblServiceType
            // 
            this.lblServiceType.AutoSize = true;
            this.lblServiceType.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceType.Location = new System.Drawing.Point(32, 460);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(123, 21);
            this.lblServiceType.TabIndex = 15;
            this.lblServiceType.Text = "Service Type:";
            this.lblServiceType.Visible = false;
            // 
            // cmbServiceType
            // 
            this.cmbServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceType.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceType.FormattingEnabled = true;
            this.cmbServiceType.Location = new System.Drawing.Point(200, 457);
            this.cmbServiceType.Name = "cmbServiceType";
            this.cmbServiceType.Size = new System.Drawing.Size(250, 29);
            this.cmbServiceType.TabIndex = 15;
            this.cmbServiceType.Visible = false;
            // 
            // lblBusinessNumber
            // 
            this.lblBusinessNumber.AutoSize = true;
            this.lblBusinessNumber.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessNumber.Location = new System.Drawing.Point(32, 460);
            this.lblBusinessNumber.Name = "lblBusinessNumber";
            this.lblBusinessNumber.Size = new System.Drawing.Size(162, 21);
            this.lblBusinessNumber.TabIndex = 16;
            this.lblBusinessNumber.Text = "Business Number:";
            this.lblBusinessNumber.Visible = false;
            // 
            // txtBusinessNumber
            // 
            this.txtBusinessNumber.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusinessNumber.Location = new System.Drawing.Point(200, 457);
            this.txtBusinessNumber.Name = "txtBusinessNumber";
            this.txtBusinessNumber.Size = new System.Drawing.Size(250, 28);
            this.txtBusinessNumber.TabIndex = 16;
            this.txtBusinessNumber.Visible = false;
            // 
            // lblEstablishedDate
            // 
            this.lblEstablishedDate.AutoSize = true;
            this.lblEstablishedDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstablishedDate.Location = new System.Drawing.Point(32, 500);
            this.lblEstablishedDate.Name = "lblEstablishedDate";
            this.lblEstablishedDate.Size = new System.Drawing.Size(159, 21);
            this.lblEstablishedDate.TabIndex = 17;
            this.lblEstablishedDate.Text = "Established Date:";
            this.lblEstablishedDate.Visible = false;
            // 
            // dtpEstablishedDate
            // 
            this.dtpEstablishedDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEstablishedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEstablishedDate.Location = new System.Drawing.Point(200, 497);
            this.dtpEstablishedDate.Name = "dtpEstablishedDate";
            this.dtpEstablishedDate.Size = new System.Drawing.Size(250, 28);
            this.dtpEstablishedDate.TabIndex = 17;
            this.dtpEstablishedDate.Visible = false;
            // 
            // lblCompanyDesc
            // 
            this.lblCompanyDesc.AutoSize = true;
            this.lblCompanyDesc.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyDesc.Location = new System.Drawing.Point(32, 540);
            this.lblCompanyDesc.Name = "lblCompanyDesc";
            this.lblCompanyDesc.Size = new System.Drawing.Size(164, 21);
            this.lblCompanyDesc.TabIndex = 18;
            this.lblCompanyDesc.Text = "Company Details:";
            this.lblCompanyDesc.Visible = false;
            // 
            // txtCompanyDesc
            // 
            this.txtCompanyDesc.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyDesc.Location = new System.Drawing.Point(200, 537);
            this.txtCompanyDesc.Multiline = true;
            this.txtCompanyDesc.Name = "txtCompanyDesc";
            this.txtCompanyDesc.Size = new System.Drawing.Size(250, 60);
            this.txtCompanyDesc.TabIndex = 18;
            this.txtCompanyDesc.Visible = false;
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsite.Location = new System.Drawing.Point(32, 610);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(111, 21);
            this.lblWebsite.TabIndex = 19;
            this.lblWebsite.Text = "Website URL:";
            this.lblWebsite.Visible = false;
            // 
            // txtWebsite
            // 
            this.txtWebsite.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWebsite.Location = new System.Drawing.Point(200, 607);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(250, 28);
            this.txtWebsite.TabIndex = 19;
            this.txtWebsite.Visible = false;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(200, 620);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(180, 40);
            this.btnRegister.TabIndex = 20;
            this.btnRegister.Text = "REGISTER";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lnkLogin
            // 
            this.lnkLogin.AutoSize = true;
            this.lnkLogin.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLogin.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lnkLogin.Location = new System.Drawing.Point(154, 670);
            this.lnkLogin.Name = "lnkLogin";
            this.lnkLogin.Size = new System.Drawing.Size(201, 20);
            this.lnkLogin.TabIndex = 21;
            this.lnkLogin.TabStop = true;
            this.lnkLogin.Text = "Already have an account?";
            this.lnkLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLogin_LinkClicked);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 700);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register - TravelEase";
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;

        // Common fields for all users
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.ComboBox cmbUserRole;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;

        // Traveler-specific fields
        private System.Windows.Forms.Label lblCNIC;
        private System.Windows.Forms.TextBox txtCNIC;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.ComboBox cmbNationality;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lblEmergency;
        private System.Windows.Forms.TextBox txtEmergencyContact;

        // Service Provider-specific fields
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.ComboBox cmbServiceType;

        // Tour Operator-specific fields
        private System.Windows.Forms.Label lblBusinessNumber;
        private System.Windows.Forms.TextBox txtBusinessNumber;
        private System.Windows.Forms.Label lblEstablishedDate;
        private System.Windows.Forms.DateTimePicker dtpEstablishedDate;
        private System.Windows.Forms.Label lblCompanyDesc;
        private System.Windows.Forms.TextBox txtCompanyDesc;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.TextBox txtWebsite;

        // Form controls
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel lnkLogin;
    }
}