using System;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data;


namespace Service_Provider_Section
{
    public partial class DeleteServices : Form
    {
        private Label lblHeading;
        private Label lblServiceID;
        private TextBox txtServiceID;
        private Button btnDelete;
        private Button btnBack;

        // 🔧 Replace with your actual connection string
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        private string providerId;

        public DeleteServices(string providerId)
        {
            this.providerId = providerId;
            InitializeComponent();
            LoadProviderServices();
        }

        private DataGridView dgvServices;


        private void InitializeComponent()
        {

            // DataGridView - Services
            this.dgvServices = new DataGridView();
            this.dgvServices.Location = new Point(500, 10);
            this.dgvServices.Size = new Size(380, 150);
            this.dgvServices.ReadOnly = true;
            this.dgvServices.AllowUserToAddRows = false;
            this.dgvServices.AllowUserToDeleteRows = false;
            this.dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Controls.Add(this.dgvServices);



            this.lblHeading = new Label();
            this.lblServiceID = new Label();
            this.txtServiceID = new TextBox();
            this.btnDelete = new Button();
            this.btnBack = new Button();

            // Form Settings
            this.ClientSize = new Size(400, 250);
            this.Text = "Delete Service";

            // Heading
            this.lblHeading.Text = "Delete Service";
            this.lblHeading.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.lblHeading.Location = new Point(110, 20);
            this.lblHeading.AutoSize = true;

            // Label - Service ID
            this.lblServiceID.Text = "Enter Service ID:";
            this.lblServiceID.Location = new Point(50, 80);
            this.lblServiceID.Size = new Size(120, 25);

            // TextBox - Service ID
            this.txtServiceID.Location = new Point(180, 80);
            this.txtServiceID.Size = new Size(150, 25);

            // Delete Button
            this.btnDelete.Text = "Delete Service";
            this.btnDelete.Location = new Point(130, 150);
            this.btnDelete.Size = new Size(120, 35);
            this.btnDelete.Click += new EventHandler(this.BtnDelete_Click);

            // Back Button
            this.btnBack.Text = "Back";
            this.btnBack.Location = new Point(10, 190);
            this.btnBack.Size = new Size(75, 30);
            this.btnBack.Click += new EventHandler(this.BtnBack_Click);

            // Add controls
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblServiceID);
            this.Controls.Add(this.txtServiceID);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnBack);
        }

        private void LoadProviderServices()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT ServiceID, ServiceType FROM SERVICES 
                             WHERE ServiceID IN (
                                SELECT HotelID FROM HOTEL WHERE ProviderID = @ProviderID
                                UNION
                                SELECT GuideID FROM GUIDE WHERE ProviderID = @ProviderID
                                UNION
                                SELECT TransportID FROM TRANSPORT_PROVIDER WHERE ProviderID = @ProviderID
                             )";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProviderID", providerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvServices.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading services: " + ex.Message);
                }
            }
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string serviceId = txtServiceID.Text.Trim();

            if (string.IsNullOrEmpty(serviceId))
            {
                MessageBox.Show("Please enter a Service ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd;
                    int rowsAffected = 0;

                    // Try deleting from HOTEL
                    cmd = new SqlCommand("DELETE FROM HOTEL WHERE HotelID = @ID AND ProviderID = @ProviderID", conn);
                    cmd.Parameters.AddWithValue("@ID", serviceId);
                    cmd.Parameters.AddWithValue("@ProviderID", providerId);
                    rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Try deleting from GUIDE
                        cmd = new SqlCommand("DELETE FROM GUIDE WHERE GuideID = @ID AND ProviderID = @ProviderID", conn);
                        cmd.Parameters.AddWithValue("@ID", serviceId);
                        cmd.Parameters.AddWithValue("@ProviderID", providerId);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }

                    if (rowsAffected == 0)
                    {
                        // Try deleting from TRANSPORT_PROVIDER
                        cmd = new SqlCommand("DELETE FROM TRANSPORT_PROVIDER WHERE TransportID = @ID AND ProviderID = @ProviderID", conn);
                        cmd.Parameters.AddWithValue("@ID", serviceId);
                        cmd.Parameters.AddWithValue("@ProviderID", providerId);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }

                    if (rowsAffected > 0)
                    {
                        // Delete from SERVICES table now
                        cmd = new SqlCommand("DELETE FROM SERVICES WHERE ServiceID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", serviceId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Service deleted successfully.");
                        txtServiceID.Text = "";
                        LoadProviderServices(); // Refresh the DataGrid
                    }
                    else
                    {
                        MessageBox.Show("No service found with the given ID for this provider.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void BtnBack_Click(object sender, EventArgs e)
        {
            var dashboard = new ServiceProviderDashboard(providerId);
            dashboard.Show();
            this.Close();
        }

    }
}
