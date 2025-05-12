namespace TravelEase
{
    partial class TravelerAccountForm
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
            this.gbSecurity = new System.Windows.Forms.GroupBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.gbPreferences = new System.Windows.Forms.GroupBox();
            this.lblLoyaltyPointsValue = new System.Windows.Forms.Label();
            this.lblLoyaltyPoints = new System.Windows.Forms.Label();
            this.cmbPreferredLanguage = new System.Windows.Forms.ComboBox();
            this.lblPreferredLanguage = new System.Windows.Forms.Label();
            this.gbContactInfo = new System.Windows.Forms.GroupBox();
            this.txtEmergencyContact = new System.Windows.Forms.TextBox();
            this.lblEmergencyContact = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.gbPersonalInfo = new System.Windows.Forms.GroupBox();
            this.txtNationality = new System.Windows.Forms.TextBox();
            this.lblNationality = new System.Windows.Forms.Label();
            this.lblAgeValue = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.txtCNIC = new System.Windows.Forms.TextBox();
            this.lblCNIC = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.gbSecurity.SuspendLayout();
            this.gbPreferences.SuspendLayout();
            this.gbContactInfo.SuspendLayout();
            this.gbPersonalInfo.SuspendLayout();
            this.panelButtons.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(1089, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(173, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "My Account";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1026, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelMain.Controls.Add(this.gbSecurity);
            this.panelMain.Controls.Add(this.gbPreferences);
            this.panelMain.Controls.Add(this.gbContactInfo);
            this.panelMain.Controls.Add(this.gbPersonalInfo);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1089, 660);
            this.panelMain.TabIndex = 1;
            // 
            // gbSecurity
            // 
            this.gbSecurity.Controls.Add(this.btnChangePassword);
            this.gbSecurity.Controls.Add(this.txtConfirmPassword);
            this.gbSecurity.Controls.Add(this.lblConfirmPassword);
            this.gbSecurity.Controls.Add(this.txtNewPassword);
            this.gbSecurity.Controls.Add(this.lblNewPassword);
            this.gbSecurity.Controls.Add(this.txtCurrentPassword);
            this.gbSecurity.Controls.Add(this.lblCurrentPassword);
            this.gbSecurity.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSecurity.Location = new System.Drawing.Point(491, 390);
            this.gbSecurity.Name = "gbSecurity";
            this.gbSecurity.Size = new System.Drawing.Size(575, 233);
            this.gbSecurity.TabIndex = 3;
            this.gbSecurity.TabStop = false;
            this.gbSecurity.Text = "Security";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(200, 183);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(264, 37);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(234, 126);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(318, 28);
            this.txtConfirmPassword.TabIndex = 5;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassword.Location = new System.Drawing.Point(10, 133);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(161, 21);
            this.lblConfirmPassword.TabIndex = 4;
            this.lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword.Location = new System.Drawing.Point(234, 76);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(318, 28);
            this.txtNewPassword.TabIndex = 3;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPassword.Location = new System.Drawing.Point(10, 83);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(136, 21);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "New Password:";
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPassword.Location = new System.Drawing.Point(234, 26);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Size = new System.Drawing.Size(318, 28);
            this.txtCurrentPassword.TabIndex = 1;
            this.txtCurrentPassword.UseSystemPasswordChar = true;
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPassword.Location = new System.Drawing.Point(10, 33);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(160, 21);
            this.lblCurrentPassword.TabIndex = 0;
            this.lblCurrentPassword.Text = "Current Password:";
            // 
            // gbPreferences
            // 
            this.gbPreferences.Controls.Add(this.lblLoyaltyPointsValue);
            this.gbPreferences.Controls.Add(this.lblLoyaltyPoints);
            this.gbPreferences.Controls.Add(this.cmbPreferredLanguage);
            this.gbPreferences.Controls.Add(this.lblPreferredLanguage);
            this.gbPreferences.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPreferences.Location = new System.Drawing.Point(20, 390);
            this.gbPreferences.Name = "gbPreferences";
            this.gbPreferences.Size = new System.Drawing.Size(448, 220);
            this.gbPreferences.TabIndex = 2;
            this.gbPreferences.TabStop = false;
            this.gbPreferences.Text = "Preferences";
            // 
            // lblLoyaltyPointsValue
            // 
            this.lblLoyaltyPointsValue.AutoSize = true;
            this.lblLoyaltyPointsValue.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoyaltyPointsValue.Location = new System.Drawing.Point(140, 85);
            this.lblLoyaltyPointsValue.Name = "lblLoyaltyPointsValue";
            this.lblLoyaltyPointsValue.Size = new System.Drawing.Size(19, 21);
            this.lblLoyaltyPointsValue.TabIndex = 3;
            this.lblLoyaltyPointsValue.Text = "0";
            // 
            // lblLoyaltyPoints
            // 
            this.lblLoyaltyPoints.AutoSize = true;
            this.lblLoyaltyPoints.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoyaltyPoints.Location = new System.Drawing.Point(15, 85);
            this.lblLoyaltyPoints.Name = "lblLoyaltyPoints";
            this.lblLoyaltyPoints.Size = new System.Drawing.Size(125, 21);
            this.lblLoyaltyPoints.TabIndex = 2;
            this.lblLoyaltyPoints.Text = "Loyalty Points:";
            // 
            // cmbPreferredLanguage
            // 
            this.cmbPreferredLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreferredLanguage.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPreferredLanguage.FormattingEnabled = true;
            this.cmbPreferredLanguage.Items.AddRange(new object[] {
            "English",
            "Urdu",
            "Arabic",
            "French",
            "Spanish",
            "Chinese",
            "German"});
            this.cmbPreferredLanguage.Location = new System.Drawing.Point(224, 30);
            this.cmbPreferredLanguage.Name = "cmbPreferredLanguage";
            this.cmbPreferredLanguage.Size = new System.Drawing.Size(209, 29);
            this.cmbPreferredLanguage.TabIndex = 1;
            // 
            // lblPreferredLanguage
            // 
            this.lblPreferredLanguage.AutoSize = true;
            this.lblPreferredLanguage.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreferredLanguage.Location = new System.Drawing.Point(15, 33);
            this.lblPreferredLanguage.Name = "lblPreferredLanguage";
            this.lblPreferredLanguage.Size = new System.Drawing.Size(180, 21);
            this.lblPreferredLanguage.TabIndex = 0;
            this.lblPreferredLanguage.Text = "Preferred Language:";
            // 
            // gbContactInfo
            // 
            this.gbContactInfo.Controls.Add(this.txtEmergencyContact);
            this.gbContactInfo.Controls.Add(this.lblEmergencyContact);
            this.gbContactInfo.Controls.Add(this.txtAddress);
            this.gbContactInfo.Controls.Add(this.lblAddress);
            this.gbContactInfo.Controls.Add(this.txtPhoneNumber);
            this.gbContactInfo.Controls.Add(this.lblPhoneNumber);
            this.gbContactInfo.Controls.Add(this.txtEmail);
            this.gbContactInfo.Controls.Add(this.lblEmail);
            this.gbContactInfo.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbContactInfo.Location = new System.Drawing.Point(491, 20);
            this.gbContactInfo.Name = "gbContactInfo";
            this.gbContactInfo.Size = new System.Drawing.Size(575, 350);
            this.gbContactInfo.TabIndex = 1;
            this.gbContactInfo.TabStop = false;
            this.gbContactInfo.Text = "Contact Information";
            // 
            // txtEmergencyContact
            // 
            this.txtEmergencyContact.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmergencyContact.Location = new System.Drawing.Point(234, 290);
            this.txtEmergencyContact.Name = "txtEmergencyContact";
            this.txtEmergencyContact.Size = new System.Drawing.Size(318, 28);
            this.txtEmergencyContact.TabIndex = 7;
            // 
            // lblEmergencyContact
            // 
            this.lblEmergencyContact.AutoSize = true;
            this.lblEmergencyContact.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmergencyContact.Location = new System.Drawing.Point(10, 293);
            this.lblEmergencyContact.Name = "lblEmergencyContact";
            this.lblEmergencyContact.Size = new System.Drawing.Size(183, 21);
            this.lblEmergencyContact.TabIndex = 6;
            this.lblEmergencyContact.Text = "Emergency Contact:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(234, 140);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(318, 120);
            this.txtAddress.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(10, 143);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(82, 21);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Address:";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(234, 85);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(318, 28);
            this.txtPhoneNumber.TabIndex = 3;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNumber.Location = new System.Drawing.Point(10, 88);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(138, 21);
            this.lblPhoneNumber.TabIndex = 2;
            this.lblPhoneNumber.Text = "Phone Number:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(234, 30);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(318, 28);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(10, 33);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(57, 21);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            // 
            // gbPersonalInfo
            // 
            this.gbPersonalInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.gbPersonalInfo.Controls.Add(this.txtNationality);
            this.gbPersonalInfo.Controls.Add(this.lblNationality);
            this.gbPersonalInfo.Controls.Add(this.lblAgeValue);
            this.gbPersonalInfo.Controls.Add(this.lblAge);
            this.gbPersonalInfo.Controls.Add(this.dtpDateOfBirth);
            this.gbPersonalInfo.Controls.Add(this.lblDateOfBirth);
            this.gbPersonalInfo.Controls.Add(this.txtCNIC);
            this.gbPersonalInfo.Controls.Add(this.lblCNIC);
            this.gbPersonalInfo.Controls.Add(this.txtLastName);
            this.gbPersonalInfo.Controls.Add(this.lblLastName);
            this.gbPersonalInfo.Controls.Add(this.txtFirstName);
            this.gbPersonalInfo.Controls.Add(this.lblFirstName);
            this.gbPersonalInfo.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPersonalInfo.Location = new System.Drawing.Point(20, 20);
            this.gbPersonalInfo.Name = "gbPersonalInfo";
            this.gbPersonalInfo.Size = new System.Drawing.Size(448, 350);
            this.gbPersonalInfo.TabIndex = 0;
            this.gbPersonalInfo.TabStop = false;
            this.gbPersonalInfo.Text = "Personal Information";
            // 
            // txtNationality
            // 
            this.txtNationality.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNationality.Location = new System.Drawing.Point(171, 293);
            this.txtNationality.Name = "txtNationality";
            this.txtNationality.Size = new System.Drawing.Size(220, 28);
            this.txtNationality.TabIndex = 11;
            // 
            // lblNationality
            // 
            this.lblNationality.AutoSize = true;
            this.lblNationality.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNationality.Location = new System.Drawing.Point(15, 293);
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.Size = new System.Drawing.Size(104, 21);
            this.lblNationality.TabIndex = 10;
            this.lblNationality.Text = "Nationality:";
            // 
            // lblAgeValue
            // 
            this.lblAgeValue.AutoSize = true;
            this.lblAgeValue.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgeValue.Location = new System.Drawing.Point(167, 239);
            this.lblAgeValue.Name = "lblAgeValue";
            this.lblAgeValue.Size = new System.Drawing.Size(19, 21);
            this.lblAgeValue.TabIndex = 9;
            this.lblAgeValue.Text = "0";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAge.Location = new System.Drawing.Point(15, 235);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(50, 21);
            this.lblAge.TabIndex = 8;
            this.lblAge.Text = "Age:";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(171, 183);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(220, 28);
            this.dtpDateOfBirth.TabIndex = 7;
            this.dtpDateOfBirth.ValueChanged += new System.EventHandler(this.dtpDateOfBirth_ValueChanged);
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfBirth.Location = new System.Drawing.Point(15, 185);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(119, 21);
            this.lblDateOfBirth.TabIndex = 6;
            this.lblDateOfBirth.Text = "Date of Birth:";
            // 
            // txtCNIC
            // 
            this.txtCNIC.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCNIC.Location = new System.Drawing.Point(171, 133);
            this.txtCNIC.Name = "txtCNIC";
            this.txtCNIC.Size = new System.Drawing.Size(220, 28);
            this.txtCNIC.TabIndex = 5;
            // 
            // lblCNIC
            // 
            this.lblCNIC.AutoSize = true;
            this.lblCNIC.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCNIC.Location = new System.Drawing.Point(15, 133);
            this.lblCNIC.Name = "lblCNIC";
            this.lblCNIC.Size = new System.Drawing.Size(61, 21);
            this.lblCNIC.TabIndex = 4;
            this.lblCNIC.Text = "CNIC:";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(171, 83);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(220, 28);
            this.txtLastName.TabIndex = 3;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(15, 83);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(105, 21);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(171, 33);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(220, 28);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(15, 33);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(101, 21);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "First Name:";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnEdit);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 720);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1089, 80);
            this.panelButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(630, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(318, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(474, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(150, 40);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "Edit Profile";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // TravelerAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 800);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TravelerAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TravelerAccountForm";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.gbSecurity.ResumeLayout(false);
            this.gbSecurity.PerformLayout();
            this.gbPreferences.ResumeLayout(false);
            this.gbPreferences.PerformLayout();
            this.gbContactInfo.ResumeLayout(false);
            this.gbContactInfo.PerformLayout();
            this.gbPersonalInfo.ResumeLayout(false);
            this.gbPersonalInfo.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox gbPersonalInfo;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblCNIC;
        private System.Windows.Forms.TextBox txtCNIC;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblAgeValue;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.TextBox txtNationality;
        private System.Windows.Forms.GroupBox gbContactInfo;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblEmergencyContact;
        private System.Windows.Forms.TextBox txtEmergencyContact;
        private System.Windows.Forms.GroupBox gbPreferences;
        private System.Windows.Forms.Label lblPreferredLanguage;
        private System.Windows.Forms.ComboBox cmbPreferredLanguage;
        private System.Windows.Forms.Label lblLoyaltyPoints;
        private System.Windows.Forms.Label lblLoyaltyPointsValue;
        private System.Windows.Forms.GroupBox gbSecurity;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}