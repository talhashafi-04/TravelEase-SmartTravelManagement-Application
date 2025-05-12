using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelEase
{
    public partial class TravelerDemographicsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;";

        public TravelerDemographicsForm()
        {
            try
            {
                InitializeComponent();
                
                // Add debug labels to verify form initialization
                Label debugLabel = new Label
                {
                    Text = "Form Initialized",
                    Location = new System.Drawing.Point(10, 10),
                    Size = new System.Drawing.Size(200, 20),
                    ForeColor = System.Drawing.Color.Red
                };
                this.Controls.Add(debugLabel);
                
                LoadDemographics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing form: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDemographics()
        {
            try
            {
                LoadAgeNationalityDistribution();
                LoadTripPreferences();
                LoadSpendingHabits();
                
                // Add success label if all data loaded
                Label successLabel = new Label
                {
                    Text = "All data loaded successfully",
                    Location = new System.Drawing.Point(10, 40),
                    Size = new System.Drawing.Size(200, 20),
                    ForeColor = System.Drawing.Color.Green
                };
                this.Controls.Add(successLabel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading demographics: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAgeNationalityDistribution()
        {
            try
            {
                string query = @"SELECT Nationality, COUNT(*) AS Count FROM TRAVELER GROUP BY Nationality";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("No nationality data found in the database", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    chartNationality.Series.Clear();
                    Series series = new Series("Nationality");
                    series.ChartType = SeriesChartType.Pie;

                    foreach (DataRow row in table.Rows)
                    {
                        series.Points.AddXY(row["Nationality"].ToString(), Convert.ToInt32(row["Count"]));
                    }

                    chartNationality.Series.Add(series);
                    chartNationality.Titles.Add("Traveler Nationalities");
                }

                string ageQuery = @"SELECT Age, COUNT(*) AS Count FROM TRAVELER GROUP BY Age ORDER BY Age";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(ageQuery, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    chartAge.Series.Clear();
                    Series series = new Series("Age");
                    series.ChartType = SeriesChartType.Column;

                    foreach (DataRow row in table.Rows)
                    {
                        series.Points.AddXY(row["Age"].ToString(), Convert.ToInt32(row["Count"]));
                    }

                    chartAge.Series.Add(series);
                    chartAge.Titles.Add("Age Distribution");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading age and nationality data: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTripPreferences()
        {
            try
            {
                string query = @"
                    SELECT C.Name AS Category, COUNT(*) AS Count
                    FROM BOOKING B
                    JOIN TRIP T ON B.TripID = T.TripID
                    JOIN TRIP_CATEGORY C ON T.CategoryID = C.CategoryID
                    GROUP BY C.Name";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    chartTripPreferences.Series.Clear();
                    Series series = new Series("Trip Preferences");
                    series.ChartType = SeriesChartType.Bar;

                    foreach (DataRow row in table.Rows)
                    {
                        series.Points.AddXY(row["Category"].ToString(), Convert.ToInt32(row["Count"]));
                    }

                    chartTripPreferences.Series.Add(series);
                    chartTripPreferences.Titles.Add("Trip Category Preferences");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip preferences: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSpendingHabits()
        {
            try
            {
                string query = @"
                    SELECT T.Nationality, AVG(B.TotalAmount) AS AvgSpent
                    FROM BOOKING B
                    JOIN TRAVELER T ON B.TravelerID = T.TravelerID
                    GROUP BY T.Nationality";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    
                    if (table.Rows.Count > 0)
                    {
                        dgvSpending.DataSource = table;
                        dgvSpending.Columns["Nationality"].HeaderText = "Traveler Nationality";
                        dgvSpending.Columns["AvgSpent"].HeaderText = "Average Spending";
                        dgvSpending.Columns["AvgSpent"].DefaultCellStyle.Format = "C2";
                    }
                    else
                    {
                        MessageBox.Show("No spending data found", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading spending habits: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
