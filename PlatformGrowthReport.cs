using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DatabaseProject
{
    public partial class PlatformGrowthReportForm : Form
    {
        private Chart chartNewUsers, chartActiveUsers, chartPartners, chartDestinations;
        private ComboBox cboTimeRange;
        private Label lblTimeRange;
        private Button btnGenerate, btnExportCsv;
        private SqlConnection con = new SqlConnection(
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

            lblTimeRange = new Label { Text = "Time Range:", Location = new Point(20, 20), Size = new Size(80, 20) };
            cboTimeRange = new ComboBox
            {
                Location = new Point(100, 20),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTimeRange.Items.AddRange(new object[] { "Last 3 Months", "Last 6 Months", "Last Year", "All Time" });
            cboTimeRange.SelectedIndex = 2; // Default to Last Year

            btnGenerate = new Button { Text = "Generate Report", Location = new Point(270, 20), Size = new Size(150, 30) };
            btnGenerate.Click += (s, e) => LoadReportData();

            btnExportCsv = new Button { Text = "Export to CSV", Location = new Point(440, 20), Size = new Size(150, 30), Enabled = false };
            btnExportCsv.Click += (s, e) => ExportCsvFiles();

            chartNewUsers = CreateChart("New User Registrations", new Point(20, 70));
            chartActiveUsers = CreateChart("Active Users", new Point(520, 70));
            chartPartners = CreateChart("Partnership Growth", new Point(20, 400));
            chartDestinations = CreateChart("Regional Expansion", new Point(520, 400));

            this.Controls.AddRange(new Control[] {
                lblTimeRange, cboTimeRange, btnGenerate, btnExportCsv,
                chartNewUsers, chartActiveUsers, chartPartners, chartDestinations
            });
        }

        private Chart CreateChart(string title, Point loc)
        {
            var c = new Chart { Location = loc, Size = new Size(450, 300) };
            c.ChartAreas.Add(new ChartArea());
            c.Titles.Add(new Title(title, Docking.Top, new Font("Arial", 12, FontStyle.Bold), Color.Black));
            c.ChartAreas[0].AxisX.Title = "Month";
            c.ChartAreas[0].AxisY.Title = "Count";
            c.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            c.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            c.ChartAreas[0].BackColor = Color.White;
            c.BorderlineDashStyle = ChartDashStyle.Solid;
            c.BorderlineColor = Color.LightGray;
            c.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            return c;
        }

        private void LoadReportData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string dateFilter = GetDateFilterClause();

                LoadNewUserRegistrations(dateFilter);
                LoadActiveUsers(dateFilter);
                LoadPartnershipGrowth(dateFilter);
                LoadRegionalExpansion(dateFilter);

                btnExportCsv.Enabled = true;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Error loading report data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDateFilterClause()
        {
            string clause;
            DateTime now = DateTime.Now;

            switch (cboTimeRange.SelectedIndex)
            {
                case 0: // Last 3 Months
                    clause = $"AND DateColumn >= '{now.AddMonths(-3).ToString("yyyy-MM-dd")}'";
                    break;
                case 1: // Last 6 Months
                    clause = $"AND DateColumn >= '{now.AddMonths(-6).ToString("yyyy-MM-dd")}'";
                    break;
                case 2: // Last Year
                    clause = $"AND DateColumn >= '{now.AddYears(-1).ToString("yyyy-MM-dd")}'";
                    break;
                default: // All Time
                    clause = "";
                    break;
            }
            return clause;
        }

        private void LoadNewUserRegistrations(string dateFilter)
        {
            chartNewUsers.Series.Clear();

            // Create series for each user type
            var travelerSeries = new Series("Travelers") { ChartType = SeriesChartType.Column };
            var providerSeries = new Series("Service Providers") { ChartType = SeriesChartType.Column };
            var operatorSeries = new Series("Tour Operators") { ChartType = SeriesChartType.Column };

            chartNewUsers.Series.Add(travelerSeries);
            chartNewUsers.Series.Add(providerSeries);
            chartNewUsers.Series.Add(operatorSeries);

            // Configure chart appearance
            chartNewUsers.ChartAreas[0].AxisX.Interval = 1;
            chartNewUsers.Legends.Add(new Legend("UserTypes"));

            // Modified SQL to group by month and year and user role
            string sql = @"
SELECT 
    MONTH(RegistrationDate) AS MonthNum,
    YEAR(RegistrationDate) AS YearNum,
    FORMAT(RegistrationDate, 'MMM yyyy') AS MonthYear,
    UserRole,
    COUNT(*) AS UserCount
FROM 
    [USER]
WHERE 
    1=1 " + dateFilter.Replace("DateColumn", "RegistrationDate") + @"
GROUP BY 
    MONTH(RegistrationDate),
    YEAR(RegistrationDate),
    FORMAT(RegistrationDate, 'MMM yyyy'),
    UserRole
ORDER BY 
    YearNum, MonthNum";

            dtNewUsers = new DataTable();
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtNewUsers);
                    }
                }
            }

            // Create a pivot view of the data for easier plotting
            DataTable pivotTable = CreatePivotTable(dtNewUsers, "MonthYear", "UserRole", "UserCount");

            foreach (DataRow row in pivotTable.Rows)
            {
                string monthYear = row["MonthYear"].ToString();

                // Add data points for each user type
                double travelerCount = row["TRAVELER"] != DBNull.Value ? Convert.ToDouble(row["TRAVELER"]) : 0;
                double providerCount = row["SERVICE_PROVIDER"] != DBNull.Value ? Convert.ToDouble(row["SERVICE_PROVIDER"]) : 0;
                double operatorCount = row["TOUR_OPERATOR"] != DBNull.Value ? Convert.ToDouble(row["TOUR_OPERATOR"]) : 0;

                travelerSeries.Points.AddXY(monthYear, travelerCount);
                providerSeries.Points.AddXY(monthYear, providerCount);
                operatorSeries.Points.AddXY(monthYear, operatorCount);
            }
        }

        private void LoadActiveUsers(string dateFilter)
        {
            chartActiveUsers.Series.Clear();
            var activeTravelerSeries = new Series("Active Travelers") { ChartType = SeriesChartType.Line, BorderWidth = 3 };
            var activeOperatorSeries = new Series("Active Operators") { ChartType = SeriesChartType.Line, BorderWidth = 3 };

            chartActiveUsers.Series.Add(activeTravelerSeries);
            chartActiveUsers.Series.Add(activeOperatorSeries);

            chartActiveUsers.ChartAreas[0].AxisX.Interval = 1;
            chartActiveUsers.Legends.Add(new Legend("UserTypes"));

            string sql = @"
SELECT 
    MONTH(LastLogIn) AS MonthNum,
    YEAR(LastLogIn) AS YearNum,
    FORMAT(LastLogIn, 'MMM yyyy') AS MonthYear,
    UserRole,
    COUNT(DISTINCT UserID) AS ActiveCount
FROM 
    [USER]
WHERE 
    LastLogIn IS NOT NULL " + dateFilter.Replace("DateColumn", "LastLogIn") + @"
GROUP BY 
    MONTH(LastLogIn),
    YEAR(LastLogIn),
    FORMAT(LastLogIn, 'MMM yyyy'),
    UserRole
HAVING 
    UserRole IN ('TRAVELER', 'TOUR_OPERATOR')
ORDER BY 
    YearNum, MonthNum";

            dtActiveUsers = new DataTable();
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtActiveUsers);
                    }
                }
            }

            // Create a pivot view
            DataTable pivotTable = CreatePivotTable(dtActiveUsers, "MonthYear", "UserRole", "ActiveCount");

            foreach (DataRow row in pivotTable.Rows)
            {
                string monthYear = row["MonthYear"].ToString();

                double travelerCount = row["TRAVELER"] != DBNull.Value ? Convert.ToDouble(row["TRAVELER"]) : 0;
                double operatorCount = row["TOUR_OPERATOR"] != DBNull.Value ? Convert.ToDouble(row["TOUR_OPERATOR"]) : 0;

                activeTravelerSeries.Points.AddXY(monthYear, travelerCount);
                activeOperatorSeries.Points.AddXY(monthYear, operatorCount);
            }
        }

        private void LoadPartnershipGrowth(string dateFilter)
        {
            chartPartners.Series.Clear();
            var hotelSeries = new Series("Hotels") { ChartType = SeriesChartType.Column };
            var guideSeries = new Series("Guides") { ChartType = SeriesChartType.Column };
            var transportSeries = new Series("Transport") { ChartType = SeriesChartType.Column };

            chartPartners.Series.Add(hotelSeries);
            chartPartners.Series.Add(guideSeries);
            chartPartners.Series.Add(transportSeries);

            chartPartners.ChartAreas[0].AxisX.Interval = 1;
            chartPartners.Legends.Add(new Legend("PartnerTypes"));

            string sql = @"
WITH HotelData AS (
    SELECT 
        MONTH(h.StartDate) AS MonthNum,
        YEAR(h.StartDate) AS YearNum,
        FORMAT(h.StartDate, 'MMM yyyy') AS MonthYear,
        COUNT(*) AS HotelCount
    FROM 
        HOTEL h
        JOIN SERVICES s ON h.HotelID = s.ServiceID
    WHERE 
        1=1 " + dateFilter.Replace("DateColumn", "h.StartDate") + @"
    GROUP BY 
        MONTH(h.StartDate),
        YEAR(h.StartDate),
        FORMAT(h.StartDate, 'MMM yyyy')
),
GuideData AS (
    SELECT 
        MONTH(g.StartDate) AS MonthNum,
        YEAR(g.StartDate) AS YearNum,
        FORMAT(g.StartDate, 'MMM yyyy') AS MonthYear,
        COUNT(*) AS GuideCount
    FROM 
        GUIDE g
        JOIN SERVICES s ON g.GuideID = s.ServiceID
    WHERE 
        1=1 " + dateFilter.Replace("DateColumn", "g.StartDate") + @"
    GROUP BY 
        MONTH(g.StartDate),
        YEAR(g.StartDate),
        FORMAT(g.StartDate, 'MMM yyyy')
),
TransportData AS (
    SELECT 
        MONTH(t.StartDate) AS MonthNum,
        YEAR(t.StartDate) AS YearNum,
        FORMAT(t.StartDate, 'MMM yyyy') AS MonthYear,
        COUNT(*) AS TransportCount
    FROM 
        TRANSPORT_PROVIDER t
        JOIN SERVICES s ON t.TransportID = s.ServiceID
    WHERE 
        1=1 " + dateFilter.Replace("DateColumn", "t.StartDate") + @"
    GROUP BY 
        MONTH(t.StartDate),
        YEAR(t.StartDate),
        FORMAT(t.StartDate, 'MMM yyyy')
),
AllMonths AS (
    SELECT 
        m.MonthNum,
        m.YearNum,
        m.MonthYear
    FROM (
        SELECT MonthNum, YearNum, MonthYear FROM HotelData
        UNION
        SELECT MonthNum, YearNum, MonthYear FROM GuideData
        UNION
        SELECT MonthNum, YearNum, MonthYear FROM TransportData
    ) m
)
SELECT 
    a.MonthYear,
    ISNULL(h.HotelCount, 0) AS HotelCount,
    ISNULL(g.GuideCount, 0) AS GuideCount,
    ISNULL(t.TransportCount, 0) AS TransportCount
FROM 
    AllMonths a
    LEFT JOIN HotelData h ON a.MonthYear = h.MonthYear
    LEFT JOIN GuideData g ON a.MonthYear = g.MonthYear
    LEFT JOIN TransportData t ON a.MonthYear = t.MonthYear
ORDER BY 
    a.YearNum, a.MonthNum";

            dtPartners = new DataTable();
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtPartners);
                    }
                }
            }

            foreach (DataRow row in dtPartners.Rows)
            {
                string monthYear = row["MonthYear"].ToString();
                double hotelCount = Convert.ToDouble(row["HotelCount"]);
                double guideCount = Convert.ToDouble(row["GuideCount"]);
                double transportCount = Convert.ToDouble(row["TransportCount"]);

                hotelSeries.Points.AddXY(monthYear, hotelCount);
                guideSeries.Points.AddXY(monthYear, guideCount);
                transportSeries.Points.AddXY(monthYear, transportCount);
            }
        }

        private void LoadRegionalExpansion(string dateFilter)
        {
            chartDestinations.Series.Clear();
            var destinationSeries = new Series("New Destinations") { ChartType = SeriesChartType.Line, BorderWidth = 3 };
            var cumulativeSeries = new Series("Cumulative Total")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.DarkGreen,
                BorderDashStyle = ChartDashStyle.Dash
            };

            chartDestinations.Series.Add(destinationSeries);
            chartDestinations.Series.Add(cumulativeSeries);

            chartDestinations.ChartAreas[0].AxisX.Interval = 1;
            chartDestinations.Legends.Add(new Legend("DestinationGrowth"));

            string sql = @"
WITH MonthlyData AS (
    SELECT 
        MONTH(CreationDate) AS MonthNum,
        YEAR(CreationDate) AS YearNum,
        FORMAT(CreationDate, 'MMM yyyy') AS MonthYear,
        COUNT(*) AS NewCount
    FROM 
        DESTINATION
    WHERE 
        1=1 " + dateFilter.Replace("DateColumn", "CreationDate") + @"
    GROUP BY 
        MONTH(CreationDate),
        YEAR(CreationDate),
        FORMAT(CreationDate, 'MMM yyyy')
)
SELECT 
    MonthYear,
    NewCount,
    SUM(NewCount) OVER (ORDER BY YearNum, MonthNum) AS CumulativeCount
FROM 
    MonthlyData
ORDER BY 
    YearNum, MonthNum";

            dtDestinations = new DataTable();
            using (SqlConnection connection = new SqlConnection(con.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtDestinations);
                    }
                }
            }

            foreach (DataRow row in dtDestinations.Rows)
            {
                string monthYear = row["MonthYear"].ToString();
                double newCount = Convert.ToDouble(row["NewCount"]);
                double cumulativeCount = Convert.ToDouble(row["CumulativeCount"]);

                destinationSeries.Points.AddXY(monthYear, newCount);
                cumulativeSeries.Points.AddXY(monthYear, cumulativeCount);
            }
        }

        private DataTable CreatePivotTable(DataTable source, string rowField, string columnField, string valueField)
        {
            DataTable result = new DataTable();

            // Add row field column
            result.Columns.Add(rowField, source.Columns[rowField].DataType);

            // Get distinct column values
            var columnValues = source.AsEnumerable()
                .Select(row => row[columnField].ToString())
                .Distinct()
                .ToList();

            // Add columns for each distinct column value
            foreach (var colValue in columnValues)
            {
                result.Columns.Add(colValue, typeof(double));
            }

            // Get distinct row values
            var rowValues = source.AsEnumerable()
                .Select(row => row[rowField].ToString())
                .Distinct()
                .ToList();

            // Populate the pivot table
            foreach (var rowValue in rowValues)
            {
                DataRow newRow = result.NewRow();
                newRow[rowField] = rowValue;

                foreach (var colValue in columnValues)
                {
                    // Find the value for this row and column
                    var matches = source.AsEnumerable()
                        .Where(row => row[rowField].ToString() == rowValue &&
                                    row[columnField].ToString() == colValue);

                    if (matches.Any())
                    {
                        newRow[colValue] = matches.First()[valueField];
                    }
                    else
                    {
                        newRow[colValue] = 0;
                    }
                }

                result.Rows.Add(newRow);
            }

            return result;
        }

        private void ExportCsvFiles()
        {
            using (var dlg = new FolderBrowserDialog { Description = "Select folder to save CSV reports" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                string folder = dlg.SelectedPath;
                try
                {
                    Cursor = Cursors.WaitCursor;
                    WriteDataTableToCsv(dtNewUsers, Path.Combine(folder, "NewRegistrations.csv"));
                    WriteDataTableToCsv(dtActiveUsers, Path.Combine(folder, "ActiveUsers.csv"));
                    WriteDataTableToCsv(dtPartners, Path.Combine(folder, "PartnershipGrowth.csv"));
                    WriteDataTableToCsv(dtDestinations, Path.Combine(folder, "RegionalExpansion.csv"));
                    Cursor = Cursors.Default;
                    MessageBox.Show("CSV files exported successfully.", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Error exporting CSV: " + ex.Message, "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    sw.Write($"\"{table.Columns[i].ColumnName}\"");
                }
                sw.WriteLine();

                // Rows
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i > 0) sw.Write(",");
                        string val = row[i]?.ToString() ?? "";
                        // Escape quotes and special characters
                        val = val.Replace("\"", "\"\"");
                        sw.Write($"\"{val}\"");
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}