using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DatabaseProject
{
    public partial class EditTripForm : Form
    {
        private readonly int _tripId;

        // Controls (same as CreateTripForm)
        private Label lblTitle, lblPrice, lblDuration, lblDescription, lblStartDate, lblEndDate,
                      lblDifficulty, lblCapacity, lblDestination, lblCategory, lblServices, lblImage;
        private TextBox txtTitle, txtPrice, txtDescription;
        private NumericUpDown nuDuration, nuCapacity;
        private DateTimePicker dtpStartDate, dtpEndDate;
        private ComboBox cmbDifficulty, cmbDestination, cmbCategory;
        private CheckedListBox clbServices;
        private PictureBox pbImage;
        private Button btnBrowseImage, btnSave, btnCancel;
        SqlConnection con = new SqlConnection("Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Trust Server Certificate=True");

        public EditTripForm(int tripId)
        {
            _tripId = tripId;
            InitializeComponents();
            LoadAllServices();
            LoadTripData();
        }

        private void InitializeComponents()
        {
            this.Text = "Edit Trip";
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
            txtDescription = new TextBox
            {
                Location = new Point(ctrlLeft, y),
                Width = 400,
                Height = 80,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
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
            // TODO: Load destinations from DB here
            y += vSpacing;

            // Category
            lblCategory = new Label { Text = "Category:", Location = new Point(leftX, y), Width = labelWidth };
            cmbCategory = new ComboBox { Location = new Point(ctrlLeft, y), Width = ctrlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            // TODO: Load categories from DB here
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
            // TODO: Load services from DB here
            y += 110;

            // Image upload
            lblImage = new Label { Text = "Image:", Location = new Point(leftX, y), Width = labelWidth };
            pbImage = new PictureBox
            {
                Location = new Point(ctrlLeft, y),
                Size = new Size(150, 100),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            btnBrowseImage = new Button
            {
                Text = "Browse...",
                Location = new Point(ctrlLeft + 160, y + 40),
                Size = new Size(80, 30)
            };
            btnBrowseImage.Click += BtnBrowseImage_Click;
            y += 120;

            // Save & Cancel
            btnSave = new Button { Text = "Save Changes", Location = new Point(ctrlLeft, y), Size = new Size(120, 35) };
            btnCancel = new Button { Text = "Cancel", Location = new Point(ctrlLeft + 140, y), Size = new Size(100, 35) };
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Add all controls
            this.Controls.AddRange(new Control[] {
                lblTitle, txtTitle, lblPrice, txtPrice, lblDuration, nuDuration,
                lblDescription, txtDescription, lblStartDate, dtpStartDate, lblEndDate, dtpEndDate,
                lblDifficulty, cmbDifficulty, lblCapacity, nuCapacity,
                lblDestination, cmbDestination, lblCategory, cmbCategory,
                lblServices, clbServices, lblImage, pbImage, btnBrowseImage,
                btnSave, btnCancel
            });
        }
        private void LoadAllServices()
        {
            clbServices.Items.Clear();

            try
            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT ServiceID, ServiceType FROM SERVICES", con))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        clbServices.Items.Add(new ServiceItem
                        {
                            ServiceID = rd.GetInt32(0),
                            ServiceName = rd.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message,
                                "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
        private void LoadTripData()
        {
            // Ensure connection is closed before we start
            if (con.State != ConnectionState.Closed)
                con.Close();

            try
            {
                con.Open();

                // 1) Load the main Trip record
                const string tripSql = @"
            SELECT Title, Price, Duration_Days, Description,
                   StartDate, EndDate, Difficulty, Capacity,
                   DestinationID, CategoryID
              FROM TRIP
             WHERE TripID = @TripID";
                using (var cmd = new SqlCommand(tripSql, con))
                {
                    cmd.Parameters.AddWithValue("@TripID", _tripId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            txtTitle.Text = rd.GetString(0);
                            txtPrice.Text = rd.GetDecimal(1).ToString();
                            nuDuration.Value = rd.GetInt32(2);
                            txtDescription.Text = rd.GetString(3);
                            dtpStartDate.Value = rd.GetDateTime(4);
                            dtpEndDate.Value = rd.GetDateTime(5);
                            cmbDifficulty.SelectedItem = rd.GetString(6);
                            nuCapacity.Value = rd.GetInt32(7);
                            cmbDestination.SelectedValue = rd.GetInt32(8);
                            cmbCategory.SelectedValue = rd.GetInt32(9);
                        }
                    }
                }

                // 2) Load its service enrollments
                const string svcSql = @"
            SELECT ServiceID
              FROM TRIP_SERVICES_Renrollment
             WHERE TripID = @TripID";
                using (var cmd = new SqlCommand(svcSql, con))
                {
                    cmd.Parameters.AddWithValue("@TripID", _tripId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        var selected = new HashSet<int>();
                        while (rd.Read())
                            selected.Add(rd.GetInt32(0));

                        for (int i = 0; i < clbServices.Items.Count; i++)
                        {
                            var item = (ServiceItem)clbServices.Items[i];
                            clbServices.SetItemChecked(i, selected.Contains(item.ServiceID));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip data: " + ex.Message,
                                "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pbImage.Image = Image.FromFile(dlg.FileName);
                    pbImage.Tag = dlg.FileName;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 1) Validate
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

            // 2) Collect
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

            // 3) Save
            if (con.State != ConnectionState.Closed)
                con.Close();

            con.Open();
            using (var tx = con.BeginTransaction())
            {
                try
                {
                    // a) Update TRIP row
                    const string updTrip = @"
                UPDATE TRIP
                   SET Title         = @Title,
                       Price         = @Price,
                       Duration_Days = @Duration,
                       Description   = @Description,
                       StartDate     = @StartDate,
                       EndDate       = @EndDate,
                       Difficulty    = @Difficulty,
                       Capacity      = @Capacity,
                       DestinationID = @DestinationID,
                       CategoryID    = @CategoryID
                 WHERE TripID = @TripID";
                    using (var cmd = new SqlCommand(updTrip, con, tx))
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
                        cmd.Parameters.AddWithValue("@TripID", _tripId);
                        cmd.ExecuteNonQuery();
                    }

                    // b) Clear old service links
                    using (var del = new SqlCommand(
                        "DELETE FROM TRIP_SERVICES_Renrollment WHERE TripID = @TripID",
                        con, tx))
                    {
                        del.Parameters.AddWithValue("@TripID", _tripId);
                        del.ExecuteNonQuery();
                    }

                    // c) Re-insert selected services
                    const string insSvc = @"
                INSERT INTO TRIP_SERVICES_Renrollment (TripID, ServiceID)
                VALUES (@TripID, @ServiceID)";
                    foreach (int svcId in services)
                    {
                        using (var cmd = new SqlCommand(insSvc, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@TripID", _tripId);
                            cmd.Parameters.AddWithValue("@ServiceID", svcId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    MessageBox.Show("Trip updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("Error updating trip: " + ex.Message,
                                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private class ServiceItem
        {
            public int ServiceID { get; set; }
            public string ServiceName { get; set; }
            public override string ToString() => ServiceName;
        }
    }
}
