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
    public partial class TourCategoriesManagementForm : Form
    {
        private DataGridView dgvCategories;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh;
        SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");
        public TourCategoriesManagementForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Tour Categories Management";
            this.ClientSize = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            dgvCategories = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(560, 380),
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvCategories.Columns.Add("CategoryID", "ID");
            dgvCategories.Columns.Add("Name", "Name");
            dgvCategories.Columns.Add("Description", "Description");

            btnAdd = new Button { Text = "Add", Location = new Point(20, 420), Size = new Size(80, 30) };
            btnEdit = new Button { Text = "Edit", Location = new Point(120, 420), Size = new Size(80, 30) };
            btnDelete = new Button { Text = "Delete", Location = new Point(220, 420), Size = new Size(80, 30) };
            btnRefresh = new Button { Text = "Refresh", Location = new Point(320, 420), Size = new Size(80, 30) };

            this.Controls.AddRange(new Control[] { dgvCategories, btnAdd, btnEdit, btnDelete, btnRefresh });

            this.Load += (s, e) => LoadCategories();
            btnRefresh.Click += (s, e) => LoadCategories();
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void LoadCategories()
        {
            DataTable dt = new DataTable();
            const string sql = @"
        SELECT CategoryID, Name, Description
          FROM TRIP_CATEGORY
      ORDER BY Name";

            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);

                dgvCategories.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var edit = new CategoryEditForm())
            {
                edit.ShowDialog(this);
            }
            LoadCategories();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null)
            {
                MessageBox.Show("Select a category.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = Convert.ToInt32(dgvCategories.CurrentRow.Cells["CategoryID"].Value);
            using (var edit = new CategoryEditForm(id))
            {
                edit.ShowDialog(this);
            }
            LoadCategories();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvCategories.CurrentRow.Cells["CategoryID"].Value);
            if (MessageBox.Show("Delete this category?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            const string sql = "DELETE FROM TRIP_CATEGORY WHERE CategoryID = @ID";
            try
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
                LoadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
