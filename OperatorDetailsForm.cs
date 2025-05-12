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
    public partial class OperatorDetailsForm : Form
    {
        private readonly string _operatorId;

        // Controls
        private Label lblAgencyName, lblBusinessNum, lblEstablished, lblURL, lblDesc, lblRating, lblStatus;
        private TextBox txtAgencyName, txtBusinessNum, txtURL, txtDesc;
        private DateTimePicker dtpEstablished;
        private NumericUpDown nuRating;
        private ComboBox cmbStatus;
        private DataGridView dgvMetrics;
        private Button btnSave, btnClose;
        SqlConnection con = new SqlConnection(
    @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");

        public OperatorDetailsForm(string operatorId)
        {
            _operatorId = operatorId;
            InitializeComponents();
            LoadOperatorInfo();
            LoadPerformanceMetrics();
        }

        private void InitializeComponents()
        {
            this.Text = "Operator Details";
            this.ClientSize = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterParent;

            int leftX = 20, labelW = 120, ctrlX = 150, ctrlW = 300, v = 30;
            int y = 20;
            Font lblFont = new Font("Segoe UI", 9, FontStyle.Regular);

            // Agency Name
            lblAgencyName = new Label { Text = "Agency Name:", Location = new Point(leftX, y), Width = labelW, Font = lblFont };
            txtAgencyName = new TextBox { Location = new Point(ctrlX, y), Width = ctrlW };
            y += v;

            // Business Number
            lblBusinessNum = new Label { Text = "Business No.:", Location = new Point(leftX, y), Width = labelW };
            txtBusinessNum = new TextBox { Location = new Point(ctrlX, y), Width = ctrlW };
            y += v;

            // Established Date
            lblEstablished = new Label { Text = "Established:", Location = new Point(leftX, y), Width = labelW };
            dtpEstablished = new DateTimePicker { Location = new Point(ctrlX, y), Format = DateTimePickerFormat.Short };
            y += v;

            // URL
            lblURL = new Label { Text = "Website URL:", Location = new Point(leftX, y), Width = labelW };
            txtURL = new TextBox { Location = new Point(ctrlX, y), Width = ctrlW };
            y += v;

            // Description
            lblDesc = new Label { Text = "Description:", Location = new Point(leftX, y), Width = labelW };
            txtDesc = new TextBox { Location = new Point(ctrlX, y), Width = ctrlW, Height = 60, Multiline = true, ScrollBars = ScrollBars.Vertical };
            y += 70;

            // Rating
            lblRating = new Label { Text = "Rating:", Location = new Point(leftX, y), Width = labelW };
            nuRating = new NumericUpDown { Location = new Point(ctrlX, y), Width = 80, DecimalPlaces = 2, Minimum = 0, Maximum = 5, Increment = 0.1M };
            y += v;

            // Status
            lblStatus = new Label { Text = "Status:", Location = new Point(leftX, y), Width = labelW };
            cmbStatus = new ComboBox { Location = new Point(ctrlX, y), Width = ctrlW, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Pending", "Approved", "Rejected" });
            y += v + 10;

            // Performance Metrics Grid
            dgvMetrics = new DataGridView
            {
                Location = new Point(leftX, y),
                Size = new Size(620, 200),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
            };
            dgvMetrics.Columns.Add("Metric", "Metric");
            dgvMetrics.Columns.Add("Value", "Value");
            y += 220;

            // Save & Close
            btnSave = new Button { Text = "Save", Location = new Point(leftX, y), Size = new Size(100, 30) };
            btnClose = new Button { Text = "Close", Location = new Point(leftX + 120, y), Size = new Size(100, 30) };

            btnSave.Click += BtnSave_Click;
            btnClose.Click += (s, e) => this.Close();

            // Add all controls
            this.Controls.AddRange(new Control[] {
                lblAgencyName, txtAgencyName,
                lblBusinessNum, txtBusinessNum,
                lblEstablished, dtpEstablished,
                lblURL, txtURL,
                lblDesc, txtDesc,
                lblRating, nuRating,
                lblStatus, cmbStatus,
                dgvMetrics,
                btnSave, btnClose
            });
        }

        private void LoadOperatorInfo()
        {
            try
            {
                con.Open();
                const string sql = @"
            SELECT 
              AgencyName,
              BusinessInsertionNumber,
              EstablishedDate,
              URL,
              CompanyDesc,
              Rating,
              Status
            FROM TOUR_OPERATOR
            WHERE OperatorID = @OpID";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            txtAgencyName.Text = rd.GetString(0);
                            txtBusinessNum.Text = rd.GetString(1);
                            dtpEstablished.Value = rd.GetDateTime(2);
                            txtURL.Text = rd.GetString(3);
                            txtDesc.Text = rd.GetString(4);
                            nuRating.Value = rd.GetDecimal(5);
                            cmbStatus.SelectedItem = rd.GetString(6);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading operator info: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadPerformanceMetrics()
        {
            dgvMetrics.Rows.Clear();

            try
            {
                con.Open();

                // 1) Total Trips
                int tripCount;
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM TRIP WHERE OperatorID = @OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    tripCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                dgvMetrics.Rows.Add("Total Trips", tripCount);

                // 2) Total Bookings
                int bookingCount;
                using (var cmd = new SqlCommand(
                    @"SELECT COUNT(*) 
                FROM BOOKING b
                JOIN TRIP t ON b.TripID = t.TripID
               WHERE t.OperatorID = @OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    bookingCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                dgvMetrics.Rows.Add("Total Bookings", bookingCount);

                // 3) Total Revenue
                decimal revenue;
                using (var cmd = new SqlCommand(
                    @"SELECT ISNULL(SUM(p.Amount),0)
                FROM PAYMENT p
                JOIN BOOKING b ON p.BookingID = b.BookingID
                JOIN TRIP t    ON b.TripID    = t.TripID
               WHERE t.OperatorID = @OpID
                 AND p.Status = 'Completed'", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    revenue = Convert.ToDecimal(cmd.ExecuteScalar());
                }
                dgvMetrics.Rows.Add("Total Revenue", revenue.ToString("C2"));

                // 4) Average Operator Rating
                decimal avgRating;
                using (var cmd = new SqlCommand(
                    @"SELECT ISNULL(AVG(r.Rating),0)
                FROM REVIEW r
                JOIN Trip_REVIEW tr ON r.ReviewID = tr.ReviewID
                JOIN TRIP t         ON tr.TripID  = t.TripID
               WHERE t.OperatorID = @OpID", con))
                {
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);
                    avgRating = Convert.ToDecimal(cmd.ExecuteScalar());
                }
                dgvMetrics.Rows.Add("Avg. Rating", avgRating.ToString("N2"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading performance metrics: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 1) Collect & validate
            string agency = txtAgencyName.Text.Trim();
            string business = txtBusinessNum.Text.Trim();
            DateTime est = dtpEstablished.Value.Date;
            string url = txtURL.Text.Trim();
            string desc = txtDesc.Text.Trim();
            decimal rating = nuRating.Value;
            string status = cmbStatus.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrEmpty(agency) || string.IsNullOrEmpty(business))
            {
                MessageBox.Show("Agency name and business number are required.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                const string sql = @"
            UPDATE TOUR_OPERATOR
               SET AgencyName               = @Agency,
                   BusinessInsertionNumber = @Business,
                   EstablishedDate         = @Est,
                   URL                     = @URL,
                   CompanyDesc             = @Desc,
                   Rating                  = @Rating,
                   Status                  = @Status
             WHERE OperatorID = @OpID";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Agency", agency);
                    cmd.Parameters.AddWithValue("@Business", business);
                    cmd.Parameters.AddWithValue("@Est", est);
                    cmd.Parameters.AddWithValue("@URL", url);
                    cmd.Parameters.AddWithValue("@Desc", string.IsNullOrEmpty(desc) ? (object)DBNull.Value : desc);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@OpID", _operatorId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Operator details updated successfully.",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving operator details: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
