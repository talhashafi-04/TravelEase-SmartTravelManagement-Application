using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Service_Provider_Section
{
    public partial class Service_Integration : Form
    {
        private Label lblHeading;
        private Button btnAddService;
        private Button btnEditService;
        private Button btnDeleteService;
        private Button btnBack;
        private string serviceProviderId;

        // ✅ Constructor accepting service provider ID
        public Service_Integration(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Invalid Service Provider ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            serviceProviderId = id;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblHeading = new Label();
            this.btnAddService = new Button();
            this.btnEditService = new Button();
            this.btnDeleteService = new Button();
            this.btnBack = new Button();

            // Form
            this.ClientSize = new Size(400, 300);
            this.Text = "Service Integration";

            // Label - Heading
            this.lblHeading.Text = "Service Integration";
            this.lblHeading.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblHeading.AutoSize = true;
            this.lblHeading.Location = new Point(90, 20);

            // Add Service Button
            this.btnAddService.Text = "Add Service";
            this.btnAddService.Location = new Point(100, 80);
            this.btnAddService.Size = new Size(200, 40);
            this.btnAddService.Click += new EventHandler(this.BtnAddService_Click);

            // Edit Service Button
            this.btnEditService.Text = "Edit Service";
            this.btnEditService.Location = new Point(100, 130);
            this.btnEditService.Size = new Size(200, 40);
            this.btnEditService.Click += new EventHandler(this.BtnEditService_Click);

            // Delete Service Button
            this.btnDeleteService.Text = "Delete Service";
            this.btnDeleteService.Location = new Point(100, 180);
            this.btnDeleteService.Size = new Size(200, 40);
            this.btnDeleteService.Click += new EventHandler(this.BtnDeleteService_Click);

            // Back Button
            this.btnBack.Text = "Back";
            this.btnBack.Location = new Point(10, 240);
            this.btnBack.Size = new Size(75, 30);
            this.btnBack.Click += new EventHandler(this.BtnBack_Click);

            // Add Controls
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btnAddService);
            this.Controls.Add(this.btnEditService);
            this.Controls.Add(this.btnDeleteService);
            this.Controls.Add(this.btnBack);
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            var form = new AddServices(serviceProviderId); // ✅ pass ID
            form.Show();
            this.Hide();
        }

        private void BtnEditService_Click(object sender, EventArgs e)
        {
            var form = new EditServices(serviceProviderId); // ✅ pass ID
            form.Show();
            this.Hide();
        }

        private void BtnDeleteService_Click(object sender, EventArgs e)
        {
            var form = new DeleteServices(serviceProviderId); // ✅ pass ID
            form.Show();
            this.Hide();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            var form = new ServiceProviderDashboard(serviceProviderId); // ✅ pass ID back
            form.Show();
            this.Close();
        }
    }
}
