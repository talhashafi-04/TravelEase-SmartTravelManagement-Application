using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class CreateTripForm : Form
    {
        // Controls
        private Label lblTitle, lblPrice, lblDuration, lblDescription, lblStartDate, lblEndDate,
                      lblDifficulty, lblCapacity, lblDestination, lblCategory, lblServices, lblImage;
        private TextBox txtTitle, txtPrice;
        private NumericUpDown nuDuration, nuCapacity;
        private TextBox txtDescription;
        private DateTimePicker dtpStartDate, dtpEndDate;
        private ComboBox cmbDifficulty, cmbDestination, cmbCategory;
        private CheckedListBox clbServices;
        private PictureBox pbImage;
        private Button btnBrowseImage, btnSave, btnCancel;
        SqlConnection con = new SqlConnection("Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Trust Server Certificate=True");
        public CreateTripForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Create Trip";
            this.ClientSize = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            int leftX = 20, labelWidth = 100, ctrlLeft = 130, ctrlWidth = 200, vSpacing = 30;
            int y = 20;

            // Title
            lblTitle = new Label { Text = "Title:", Location = new Point(leftX, y), Width = labelWidth };
            txtTitle = new TextBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth };
            y += vSpacing;

            // Price
            lblPrice = new Label { Text = "Price:", Location = new Point(leftX, y), Width = labelWidth };
            txtPrice = new TextBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth };
            y += vSpacing;

            // Duration
            lblDuration = new Label { Text = "Duration (days):", Location = new Point(leftX, y), Width = labelWidth };
            nuDuration = new NumericUpDown { Location = new Point(ctrlLeft, y), Width = 80, Minimum = 1, Maximum = 365 };
            y += vSpacing;

            // Description
            lblDescription = new Label { Text = "Description:", Location = new Point(leftX, y), Width = labelWidth };
            txtDescription = new TextBox { Location = new Point(ctrlLeft, y), Width = 400, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical };
            y += 90;

            // Start Date
            lblStartDate = new Label { Text = "Start Date:", Location = new Point(leftX, y), Width = labelWidth };
            dtpStartDate = new DateTimePicker { Location = new Point(ctrlLeft, y), Format = DateTimePickerFormat.Short };
            y += vSpacing;

            // End Date
            lblEndDate = new Label { Text = "End Date:", Location = new Point(leftX, y), Width = labelWidth };
            dtpEndDate = new DateTimePicker { Location = new Point(ctrlLeft, y), Format = DateTimePickerFormat.Short };
            y += vSpacing;

            // Difficulty
            lblDifficulty = new Label { Text = "Difficulty:", Location = new Point(leftX, y), Width = labelWidth };
            cmbDifficulty = new ComboBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDifficulty.Items.AddRange(new object[] { "Easy", "Medium", "Hard" });
            y += vSpacing;

            // Capacity
            lblCapacity = new Label { Text = "Capacity:", Location = new Point(leftX, y), Width = labelWidth };
            nuCapacity = new NumericUpDown { Location = new Point(ctrlLeft, y), Width = 80, Minimum = 1, Maximum = 1000 };
            y += vSpacing;

            // Destination
            lblDestination = new Label { Text = "Destination:", Location = new Point(leftX, y), Width = labelWidth };
            cmbDestination = new ComboBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            // TODO: Load destinations from DB: cmbDestination.DataSource = ...
            y += vSpacing;

            // Category
            lblCategory = new Label { Text = "Category:", Location = new Point(leftX, y), Width = labelWidth };
            cmbCategory = new ComboBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            // TODO: Load categories from DB: cmbCategory.DataSource = ...
            y += vSpacing;

            // Services
            lblServices = new Label { Text = "Services:", Location = new Point(leftX, y), Width = labelWidth };
            clbServices = new CheckedListBox
            {
                Location = new Point(ctrlLeft, y),
                Width = ctrlWidth,
                Height = 100,
                CheckOnClick = true
            };
            // TODO: Load service list from DB: clbServices.Items.AddRange(...)
            y += 110;

            // Image upload
            lblImage = new Label { Text = "Image:", Location = new Point(leftX, y), Width = labelWidth };
            pbImage = new PictureBox { Location = new Point(ctrlLeft, y), Size = new Size(150, 100), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom };
            btnBrowseImage = new Button { Text = "Browse...", Location = new Point(ctrlLeft + 160, y + 40), Size = new Size(80, 30) };
            btnBrowseImage.Click += BtnBrowseImage_Click;
            y += 120;

            // Save & Cancel
            btnSave = new Button { Text = "Save Trip", Location = new Point(ctrlLeft, y), Size = new Size(100, 35) };
            btnCancel = new Button { Text = "Cancel", Location = new Point(ctrlLeft + 120, y), Size = new Size(100, 35) };
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.AddRange(new Control[] {
                lblTitle, txtTitle, lblPrice, txtPrice, lblDuration, nuDuration,
                lblDescription, txtDescription, lblStartDate, dtpStartDate, lblEndDate, dtpEndDate,
                lblDifficulty, cmbDifficulty, lblCapacity, nuCapacity,
                lblDestination, cmbDestination, lblCategory, cmbCategory,
                lblServices, clbServices, lblImage, pbImage, btnBrowseImage,
                btnSave, btnCancel
            });
        }

        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pbImage.Image = Image.FromFile(dlg.FileName);
                    pbImage.Tag = dlg.FileName; // store the path for later save
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 1) Basic validation
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out var price))
            {
                MessageBox.Show("Invalid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Collect values
            string title = txtTitle.Text.Trim();
            int duration = (int)nuDuration.Value;
            string description = txtDescription.Text.Trim();
            DateTime start = dtpStartDate.Value.Date;
            DateTime end = dtpEndDate.Value.Date;
            string difficulty = cmbDifficulty.SelectedItem?.ToString() ?? "";
            int capacity = (int)nuCapacity.Value;
            int destId = (int)cmbDestination.SelectedValue;
            int catId = (int)cmbCategory.SelectedValue;
            var services = clbServices.CheckedItems
                                    .Cast<ServiceItem>()
                                    .Select(s => s.ServiceID)
                                    .ToList();

            // 3) Insert into DB
            var connStr = "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Trust Server Certificate=True";
            using (var con = new SqlConnection(connStr))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        // a) Insert Trip and get new TripID
                        const string tripSql = @"
                    INSERT INTO TRIP
                      (Title, Price, Duration_Days, Description, StartDate, EndDate,
                       Status, Difficulty, Capacity, DestinationID, CategoryID, CreationDate)
                    VALUES
                      (@Title, @Price, @Duration, @Description, @StartDate, @EndDate,
                       'Active', @Difficulty, @Capacity, @DestinationID, @CategoryID, GETDATE());
                    SELECT SCOPE_IDENTITY();";

                        using (var cmd = new SqlCommand(tripSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@Title", title);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@Duration", duration);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@StartDate", start);
                            cmd.Parameters.AddWithValue("@EndDate", end);
                            cmd.Parameters.AddWithValue("@Difficulty", difficulty);
                            cmd.Parameters.AddWithValue("@Capacity", capacity);
                            cmd.Parameters.AddWithValue("@DestinationID", destId);
                            cmd.Parameters.AddWithValue("@CategoryID", catId);

                            int tripId = Convert.ToInt32(cmd.ExecuteScalar());

                            // b) Insert each selected service
                            const string svcSql = @"
                        INSERT INTO TRIP_SERVICES_Renrollment (TripID, ServiceID)
                        VALUES (@TripID, @ServiceID);";

                            foreach (var svcId in services)
                            {
                                using (var svcCmd = new SqlCommand(svcSql, con, tx))
                                {
                                    svcCmd.Parameters.AddWithValue("@TripID", tripId);
                                    svcCmd.Parameters.AddWithValue("@ServiceID", svcId);
                                    svcCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                        MessageBox.Show("Trip created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Error saving trip: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // For services, you might use a helper class:
        private class ServiceItem
        {
            public int ServiceID { get; set; }
            public string ServiceName { get; set; }
            public override string ToString() => ServiceName;
        }
    }
}
