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
    public partial class ReviewModerationForm : Form
    {
        private DataGridView dgvReviews;
        private Button btnApprove, btnReject, btnViewDetails;
        SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

        public ReviewModerationForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Review Moderation";
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            dgvReviews = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(840, 460),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvReviews.Columns.Add("ReviewID", "Review ID");
            dgvReviews.Columns.Add("Rating", "Rating");
            dgvReviews.Columns.Add("Comment", "Comment");
            dgvReviews.Columns.Add("Visibility", "Visibility");
            dgvReviews.Columns.Add("ReportedFlag", "Reported");
            dgvReviews.Columns.Add("ApprovalStatus", "Status");
            dgvReviews.Columns.Add("Date", "Date");

            btnApprove = new Button { Text = "Approve", Location = new Point(20, 500), Size = new Size(100, 30) };
            btnReject = new Button { Text = "Reject", Location = new Point(130, 500), Size = new Size(100, 30) };
            btnViewDetails = new Button { Text = "View Details", Location = new Point(240, 500), Size = new Size(120, 30) };

            this.Controls.AddRange(new Control[] { dgvReviews, btnApprove, btnReject, btnViewDetails });

            this.Load += (s, e) => LoadFlaggedReviews();
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            btnViewDetails.Click += BtnViewDetails_Click;
        }

        private void LoadFlaggedReviews()
        {
            dgvReviews.Rows.Clear();

            const string sql = @"
        SELECT 
          ReviewID,
          Rating,
          Comment,
          Visibility,
          ReportedFlag,
          ApprovalStatus,
          CAST(Date AS DATE) AS ReviewDate
        FROM REVIEW
        WHERE ReportedFlag = 1 
           OR ApprovalStatus = 'Pending'
        ORDER BY Date DESC";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        dgvReviews.Rows.Add(
                            rd.GetInt32(0),
                            rd.GetDecimal(1),
                            rd.GetString(2),
                            rd.GetString(3),
                            rd.GetBoolean(4),
                            rd.GetString(5),
                            rd.GetDateTime(6).ToShortDateString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvReviews.CurrentRow == null) return;

            int reviewId = Convert.ToInt32(dgvReviews.CurrentRow.Cells["ReviewID"].Value);
            const string sql = @"
        UPDATE REVIEW
           SET ApprovalStatus = 'Approved'
         WHERE ReviewID = @ReviewID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show($"Review {reviewId} approved.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFlaggedReviews();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving review: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (dgvReviews.CurrentRow == null) return;

            int reviewId = Convert.ToInt32(dgvReviews.CurrentRow.Cells["ReviewID"].Value);
            const string sql = @"
        UPDATE REVIEW
           SET ApprovalStatus = 'Rejected'
         WHERE ReviewID = @ReviewID";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show($"Review {reviewId} rejected.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFlaggedReviews();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rejecting review: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvReviews.CurrentRow != null)
            {
                int reviewId = Convert.ToInt32(dgvReviews.CurrentRow.Cells["ReviewID"].Value);
                using (var detailsForm = new ReviewDetailsForm(reviewId))
                {
                    detailsForm.ShowDialog(this);
                }
            }
        }
    }
}
