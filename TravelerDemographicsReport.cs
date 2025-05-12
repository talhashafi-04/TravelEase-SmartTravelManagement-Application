using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TravelEase
{
    public partial class TravelerDemographicsForm : Form
    {
        private string connectionString = @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;";

        public TravelerDemographicsForm()
        {
            InitializeComponent();
            LoadDemographics();
        }

        private void LoadDemographics()
        {
            LoadAgeNationalityDistribution();
            LoadTripPreferences();
            LoadSpendingHabits();
        }

        private void LoadAgeNationalityDistribution()
        {
            string query = @"SELECT Nationality, COUNT(*) AS Count FROM TRAVELER GROUP BY Nationality";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                chartNationality.Series.Clear();
                Series series = new Series("Nationality");
                series.ChartType = SeriesChartType.Pie;

                foreach (DataRow row in table.Rows)
                {
                    series.Points.AddXY(row["Nationality"], row["Count"]);
                }

                chartNationality.Series.Add(series);
            }

            string ageQuery = @"SELECT Age, COUNT(*) AS Count FROM TRAVELER GROUP BY Age ORDER BY Age";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(ageQuery, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                chartAge.Series.Clear();
                Series series = new Series("Age");
                series.ChartType = SeriesChartType.Column;

                foreach (DataRow row in table.Rows)
                {
                    series.Points.AddXY(row["Age"], row["Count"]);
                }

                chartAge.Series.Add(series);
            }
        }

        private void LoadTripPreferences()
        {
            string query = @"
                SELECT C.Name AS Category, COUNT(*) AS Count
                FROM BOOKING B
                JOIN TRIP T ON B.TripID = T.TripID
                JOIN TRIP_CATEGORY C ON T.CategoryID = C.CategoryID
                GROUP BY C.Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                chartTripPreferences.Series.Clear();
                Series series = new Series("Trip Preferences");
                series.ChartType = SeriesChartType.Bar;

                foreach (DataRow row in table.Rows)
                {
                    series.Points.AddXY(row["Category"], row["Count"]);
                }

                chartTripPreferences.Series.Add(series);
            }
        }

        private void LoadSpendingHabits()
        {
            string query = @"
                SELECT T.Nationality, AVG(B.TotalAmount) AS AvgSpent
                FROM BOOKING B
                JOIN TRAVELER T ON B.TravelerID = T.TravelerID
                GROUP BY T.Nationality";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvSpending.DataSource = table;
            }
        }
    }
}
