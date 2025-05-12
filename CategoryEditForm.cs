using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class CategoryEditForm : Form
    {
        private readonly int? _categoryId;
        private TextBox txtName, txtDescription;
        private Button btnSave, btnCancel;

        // Database connection
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

        public CategoryEditForm(int? categoryId = null)
        {
            _categoryId = categoryId;
            InitializeComponents();
            if (_categoryId.HasValue)
                LoadCategory();
        }

        private void InitializeComponents()
        {
            this.Text = _categoryId.HasValue ? "Edit Category" : "Add Category";
            this.ClientSize = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblName = new Label { Text = "Name:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(120, 20), Width = 240 };

            Label lblDesc = new Label { Text = "Description:", Location = new Point(20, 60), AutoSize = true };
            txtDescription = new TextBox
            {
                Location = new Point(120, 60),
                Width = 240,
                Height = 80,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            btnSave = new Button { Text = "Save", Location = new Point(120, 160), Size = new Size(80, 30) };
            btnCancel = new Button { Text = "Cancel", Location = new Point(220, 160), Size = new Size(80, 30) };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblName, txtName, lblDesc, txtDescription, btnSave, btnCancel });
        }

        private void LoadCategory()
        {
            try
            {
                con.Open();
                using (var cmd = new SqlCommand(
                    "SELECT Name, Description FROM TRIP_CATEGORY WHERE CategoryID = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", _categoryId.Value);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            txtName.Text = rd.GetString(0);
                            txtDescription.Text = rd.IsDBNull(1) ? "" : rd.GetString(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading category: " + ex.Message,
                                "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string desc = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                string sql;
                if (_categoryId.HasValue)
                {
                    sql = @"
                        UPDATE TRIP_CATEGORY
                           SET Name        = @Name,
                               Description = @Desc
                         WHERE CategoryID = @ID";
                }
                else
                {
                    sql = @"
                        INSERT INTO TRIP_CATEGORY (Name, Description)
                        VALUES (@Name, @Desc)";
                }

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Desc", string.IsNullOrEmpty(desc) ? (object)DBNull.Value : desc);
                    if (_categoryId.HasValue)
                        cmd.Parameters.AddWithValue("@ID", _categoryId.Value);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    _categoryId.HasValue ? "Category updated." : "Category added.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving category: " + ex.Message,
                                "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
