using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace DatabaseProject
{
    public partial class PlatformGrowthReportForm : Form
    {
        private Chart chartNewUsers, chartActiveUsers, chartPartners, chartDestinations;
        private Button btnGenerate, btnExportCsv;
        private readonly SqlConnection con = new SqlConnection(
            @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;");

        // DataTables for CSV export
        private DataTable dtNewUsers, dtActiveUsers, dtPartners, dtDestinations;

        public PlatformGrowthReportForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Platform Growth Report";
            this.ClientSize = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnGenerate = new Button
            {
                Text = "Generate Report",
                Location = new Point(20, 20),
                Size = new Size(150, 30)
            };
            btnGenerate.Click += (s, e) => LoadReportData();

            btnExportCsv = new Button
            {
                Text = "Export to CSV",
                Location = new Point(200, 20),
                Size = new Size(150, 30),
                Enabled = false
            };
            btnExportCsv.Click += (s, e) => ExportCsvFiles();

            chartNewUsers = CreateChart("New Registrations by User Type", new Point(20, 70), true);
            chartActiveUsers = CreateChart("Monthly Active Users", new Point(520, 70), true);
            chartPartners = CreateChart("Partnership Growth (Hotels & Operators)", new Point(20, 400), true);
            chartDestinations = CreateChart("Regional Expansion", new Point(520, 400), false);

            this.Controls.AddRange(new Control[]
            {
                btnGenerate, btnExportCsv,
                chartNewUsers, chartActiveUsers,
                chartPartners, chartDestinations
            });
        }

        private Chart CreateChart(string title, Point location, bool showLegend)
        {
            var chart = new Chart { Location = location, Size = new Size(450, 300) };
            var area = new ChartArea();
            area.AxisX.Title = "Month";
            area.AxisY.Title = "Count";
            chart.ChartAreas.Add(area);
            chart.Titles.Add(title);
            if (showLegend)
                chart.Legends.Add(new Legend { Docking = Docking.Bottom });
            return chart;
        }

        private void LoadReportData()
        {
            LoadNewUsers();
            LoadActiveUsers();
            LoadPartnershipGrowth();
            LoadRegionalExpansion();
            btnExportCsv.Enabled = true;
        }

        private void LoadNewUsers()
        {
            chartNewUsers.Series.Clear();

            const string sql = @"
SELECT
    MONTH(RegistrationDate) AS Mon,
    SUM(CASE WHEN UserRole = 'Traveler' THEN 1 ELSE 0 END) AS TravelerCount,
    SUM(CASE WHEN UserRole = 'TourOperator' THEN 1 ELSE 0 END) AS OperatorCount,
    SUM(CASE WHEN UserRole = 'ServiceProvider' THEN 1 ELSE 0 END) AS ProviderCount
FROM [USER]
GROUP BY MONTH(RegistrationDate)
ORDER BY Mon";

            dtNewUsers = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtNewUsers);

            var seriesTraveler = new Series("Travelers") { ChartType = SeriesChartType.Column };
            var seriesOperator = new Series("Operators") { ChartType = SeriesChartType.Column };
            var seriesProvider = new Series("Providers") { ChartType = SeriesChartType.Column };

            chartNewUsers.Series.Add(seriesTraveler);
            chartNewUsers.Series.Add(seriesOperator);
            chartNewUsers.Series.Add(seriesProvider);

            foreach (DataRow row in dtNewUsers.Rows)
            {
                int mon = row.IsNull("Mon") ? 0 : Convert.ToInt32(row["Mon"]);
                string monthName = mon >= 1 && mon <= 12
                    ? CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(mon)
                    : mon.ToString();

                double tCount = row.IsNull("TravelerCount") ? 0 : Convert.ToDouble(row["TravelerCount"]);
                double oCount = row.IsNull("OperatorCount") ? 0 : Convert.ToDouble(row["OperatorCount"]);
                double pCount = row.IsNull("ProviderCount") ? 0 : Convert.ToDouble(row["ProviderCount"]);

                seriesTraveler.Points.AddXY(monthName, tCount);
                seriesOperator.Points.AddXY(monthName, oCount);
                seriesProvider.Points.AddXY(monthName, pCount);
            }
        }

        private void LoadActiveUsers()
        {
            chartActiveUsers.Series.Clear();

            const string sql = @"
SELECT
    MONTH(LastLogIn) AS Mon,
    SUM(CASE WHEN UserRole = 'Traveler' THEN 1 ELSE 0 END)     AS ActiveTravelers,
    SUM(CASE WHEN UserRole = 'TourOperator' THEN 1 ELSE 0 END) AS ActiveOperators
FROM [USER]
WHERE LastLogIn IS NOT NULL
GROUP BY MONTH(LastLogIn)
ORDER BY Mon";

            dtActiveUsers = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtActiveUsers);

            var seriesTraveler = new Series("Travelers") { ChartType = SeriesChartType.Line, BorderWidth = 2 };
            var seriesOperator = new Series("Operators") { ChartType = SeriesChartType.Line, BorderWidth = 2 };

            chartActiveUsers.Series.Add(seriesTraveler);
            chartActiveUsers.Series.Add(seriesOperator);

            foreach (DataRow row in dtActiveUsers.Rows)
            {
                int mon = row.IsNull("Mon") ? 0 : Convert.ToInt32(row["Mon"]);
                string monthName = mon >= 1 && mon <= 12
                    ? CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(mon)
                    : mon.ToString();

                double tCount = row.IsNull("ActiveTravelers") ? 0 : Convert.ToDouble(row["ActiveTravelers"]);
                double oCount = row.IsNull("ActiveOperators") ? 0 : Convert.ToDouble(row["ActiveOperators"]);

                seriesTraveler.Points.AddXY(monthName, tCount);
                seriesOperator.Points.AddXY(monthName, oCount);
            }
        }

        private void LoadPartnershipGrowth()
        {
            chartPartners.Series.Clear();

            const string sql = @"
SELECT
    COALESCE(h.Mon, o.Mon)          AS Mon,
    ISNULL(h.HotelCount, 0)         AS HotelCount,
    ISNULL(o.OperatorCount, 0)      AS OperatorCount
FROM
    (SELECT MONTH(StartDate) AS Mon, COUNT(*) AS HotelCount
     FROM HOTEL
     GROUP BY MONTH(StartDate)) h
    FULL OUTER JOIN
    (SELECT MONTH(EstablishedDate) AS Mon, COUNT(*) AS OperatorCount
     FROM TOUR_OPERATOR
     GROUP BY MONTH(EstablishedDate)) o
      ON h.Mon = o.Mon
ORDER BY Mon";

            dtPartners = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtPartners);

            var seriesHotel = new Series("Hotels") { ChartType = SeriesChartType.Column };
            var seriesOperator = new Series("Operators") { ChartType = SeriesChartType.Column };

            chartPartners.Series.Add(seriesHotel);
            chartPartners.Series.Add(seriesOperator);

            foreach (DataRow row in dtPartners.Rows)
            {
                int mon = row.IsNull("Mon") ? 0 : Convert.ToInt32(row["Mon"]);
                string monthName = mon >= 1 && mon <= 12
                    ? CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(mon)
                    : mon.ToString();

                double hCount = row.IsNull("HotelCount") ? 0 : Convert.ToDouble(row["HotelCount"]);
                double oCount = row.IsNull("OperatorCount") ? 0 : Convert.ToDouble(row["OperatorCount"]);

                seriesHotel.Points.AddXY(monthName, hCount);
                seriesOperator.Points.AddXY(monthName, oCount);
            }
        }

        private void LoadRegionalExpansion()
        {
            chartDestinations.Series.Clear();
            var series = new Series("Destinations") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true };
            chartDestinations.Series.Add(series);

            const string sql = @"
SELECT
    DATENAME(MONTH, CreationDate)    AS MonthName,
    COUNT(DISTINCT DestinationID)     AS DestinationCount,
    MONTH(CreationDate)               AS Mon
FROM TRIP
GROUP BY
    DATENAME(MONTH, CreationDate),
    MONTH(CreationDate)
ORDER BY
    MONTH(CreationDate)";

            dtDestinations = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtDestinations);

            foreach (DataRow row in dtDestinations.Rows)
            {
                string monthName = row["MonthName"].ToString();
                double count = row.IsNull("DestinationCount")
                                  ? 0
                                  : Convert.ToDouble(row["DestinationCount"]);
                series.Points.AddXY(monthName, count);
            }
        }


        private void ExportCsvFiles()
        {
            using (var dlg = new FolderBrowserDialog { Description = "Select folder to save CSV reports" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                string folder = dlg.SelectedPath;
                try
                {
                    WriteDataTableToCsv(dtNewUsers, Path.Combine(folder, "NewRegistrations.csv"));
                    WriteDataTableToCsv(dtActiveUsers, Path.Combine(folder, "ActiveUsers.csv"));
                    WriteDataTableToCsv(dtPartners, Path.Combine(folder, "PartnershipGrowth.csv"));
                    WriteDataTableToCsv(dtDestinations, Path.Combine(folder, "RegionalExpansion.csv"));
                    MessageBox.Show("CSV files exported successfully.", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting CSV: " + ex.Message,
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void WriteDataTableToCsv(DataTable table, string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                // Header
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i > 0) sw.Write(",");
                    sw.Write(table.Columns[i].ColumnName);
                }
                sw.WriteLine();

                // Rows
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i > 0) sw.Write(",");
                        string val = row[i]?.ToString().Replace(',', ' ');
                        sw.Write(val);
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
