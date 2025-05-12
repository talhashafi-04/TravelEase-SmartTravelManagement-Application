using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class ReviewDetailsForm : Form
    {
        private readonly int _reviewId;
        private TextBox txtRating, txtComment, txtResponse, txtStatus, txtTraveler;
        private Button btnSaveResponse;

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
            // TODO: Fetch full review by _reviewId from DB
            // Example stub:
            txtTraveler.Text = "TR1022";
            txtRating.Text = "2.5";
            txtStatus.Text = "Pending";
            txtComment.Text = "Not a great experience.";
            txtResponse.Text = ""; // could be pre-filled if response exists
        }

        private void BtnSaveResponse_Click(object sender, EventArgs e)
        {
            string response = txtResponse.Text.Trim();
            if (response.Length == 0)
            {
                MessageBox.Show("Response cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Update review record with response in DB
            MessageBox.Show("Response saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
