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
    public partial class ReviewDetailsForm : Form
    {
        private readonly int _reviewId;
        private TextBox txtRating, txtComment, txtResponse, txtStatus, txtTraveler;
        private Button btnSaveResponse;
        SqlConnection con = new SqlConnection(
           @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;Trusted_Connection=True");
        public ReviewDetailsForm(int reviewId)
        {
            _reviewId = reviewId;
            InitializeComponents();
            LoadReviewDetails();
        }

        private void InitializeComponents()
        {
            this.Text = $"Review Details - ID: {_reviewId}";
            this.ClientSize = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTraveler = new Label { Text = "Traveler ID:", Location = new Point(20, 20), AutoSize = true };
            txtTraveler = new TextBox { Location = new Point(130, 20), Width = 400, ReadOnly = true };

            Label lblRating = new Label { Text = "Rating:", Location = new Point(20, 60), AutoSize = true };
            txtRating = new TextBox { Location = new Point(130, 60), Width = 100, ReadOnly = true };

            Label lblStatus = new Label { Text = "Status:", Location = new Point(260, 60), AutoSize = true };
            txtStatus = new TextBox { Location = new Point(320, 60), Width = 210, ReadOnly = true };

            Label lblComment = new Label { Text = "Comment:", Location = new Point(20, 100), AutoSize = true };
            txtComment = new TextBox
            {
                Location = new Point(130, 100),
                Width = 400,
                Height = 100,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            Label lblResponse = new Label { Text = "Admin Response:", Location = new Point(20, 220), AutoSize = true };
            txtResponse = new TextBox
            {
                Location = new Point(130, 220),
                Width = 400,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            btnSaveResponse = new Button { Text = "Save Response", Location = new Point(130, 340), Size = new Size(120, 30) };
            btnSaveResponse.Click += BtnSaveResponse_Click;

            this.Controls.AddRange(new Control[]
            {
                lblTraveler, txtTraveler,
                lblRating, txtRating,
                lblStatus, txtStatus,
                lblComment, txtComment,
                lblResponse, txtResponse,
                btnSaveResponse
            });
        }

        private void LoadReviewDetails()
        {
            const string sql = @"
        SELECT 
            TravelerID,
            Rating,
            Comment,
            ApprovalStatus,
            ResponseText
        FROM REVIEW
        WHERE ReviewID = @ReviewID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", _reviewId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            txtTraveler.Text = rd["TravelerID"].ToString();
                            txtRating.Text = rd["Rating"].ToString();
                            txtComment.Text = rd["Comment"].ToString();
                            txtStatus.Text = rd["ApprovalStatus"].ToString();
                            txtResponse.Text = rd["ResponseText"] is DBNull ? "" : rd["ResponseText"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading review details: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnSaveResponse_Click(object sender, EventArgs e)
        {
            string response = txtResponse.Text.Trim();
            if (response.Length == 0)
            {
                MessageBox.Show("Response cannot be empty.", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string sql = @"
        UPDATE REVIEW
           SET ResponseText   = @Resp,
               ResponseDate   = GETDATE(),
               ApprovalStatus = 'Approved'
         WHERE ReviewID = @ReviewID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Resp", response);
                    cmd.Parameters.AddWithValue("@ReviewID", _reviewId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Response saved successfully.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving response: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
