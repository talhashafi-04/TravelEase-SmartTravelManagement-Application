using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DatabaseProject
{
    public partial class PlatformGrowthReportForm : Form
    {
        private Chart chartNewUsers, chartActiveUsers, chartPartners, chartDestinations;
        private Button btnGenerate, btnExportCsv;
        private SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;TrustServerCertificate=True");

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

            btnGenerate = new Button { Text = "Generate Report", Location = new Point(20, 20), Size = new Size(150, 30) };
            btnGenerate.Click += (s, e) => LoadReportData();

            btnExportCsv = new Button { Text = "Export to CSV", Location = new Point(200, 20), Size = new Size(150, 30), Enabled = false };
            btnExportCsv.Click += (s, e) => ExportCsvFiles();

            chartNewUsers = CreateChart("New Registrations", new Point(20, 70));
            chartActiveUsers = CreateChart("Active Users", new Point(520, 70));
            chartPartners = CreateChart("Partnership Growth", new Point(20, 400));
            chartDestinations = CreateChart("Regional Expansion", new Point(520, 400));

            this.Controls.AddRange(new Control[] { btnGenerate, btnExportCsv, chartNewUsers, chartActiveUsers, chartPartners, chartDestinations });
        }

        private Chart CreateChart(string title, Point loc)
        {
            var c = new Chart { Location = loc, Size = new Size(450, 300) };
            c.ChartAreas.Add(new ChartArea());
            c.Titles.Add(title);
            return c;
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
            var s = new Series("Users") { ChartType = SeriesChartType.Column };
            chartNewUsers.Series.Add(s);

            const string sql = @"
SELECT MONTH(RegistrationDate) AS Mon, COUNT(*) AS Count
FROM [USER]
GROUP BY MONTH(RegistrationDate)
ORDER BY Mon";
            dtNewUsers = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtNewUsers);
            foreach (DataRow r in dtNewUsers.Rows)
                s.Points.AddXY(r["Mon"], Convert.ToDouble(r["Count"]));
        }

        private void LoadActiveUsers()
        {
            chartActiveUsers.Series.Clear();
            var s = new Series("Active") { ChartType = SeriesChartType.Line };
            chartActiveUsers.Series.Add(s);

            const string sql = @"
SELECT MONTH(LastLogIn) AS Mon, COUNT(DISTINCT UserID) AS Count
FROM [USER]
WHERE LastLogIn IS NOT NULL
GROUP BY MONTH(LastLogIn)
ORDER BY Mon";
            dtActiveUsers = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtActiveUsers);
            foreach (DataRow r in dtActiveUsers.Rows)
                s.Points.AddXY(r["Mon"], Convert.ToDouble(r["Count"]));
        }

        private void LoadPartnershipGrowth()
        {
            chartPartners.Series.Clear();
            var s = new Series("Partners") { ChartType = SeriesChartType.Column };
            chartPartners.Series.Add(s);

            string sql = @"
SELECT 
  ISNULL(h.Mon, ISNULL(g.Mon, t.Mon)) AS Mon,
  ISNULL(h.HCount, 0) + ISNULL(g.GCount, 0) + ISNULL(t.TCount, 0) AS Count
FROM 
  (SELECT MONTH(StartDate) AS Mon, COUNT(*) AS HCount FROM HOTEL GROUP BY MONTH(StartDate)) h
  FULL OUTER JOIN (SELECT MONTH(StartDate) AS Mon, COUNT(*) AS GCount FROM GUIDE GROUP BY MONTH(StartDate)) g
    ON h.Mon = g.Mon
  FULL OUTER JOIN (SELECT MONTH(StartDate) AS Mon, COUNT(*) AS TCount FROM TRANSPORT_PROVIDER GROUP BY MONTH(StartDate)) t
    ON ISNULL(h.Mon, g.Mon) = t.Mon
ORDER BY Mon";
            dtPartners = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtPartners);
            foreach (DataRow r in dtPartners.Rows)
                s.Points.AddXY(r["Mon"], Convert.ToDouble(r["Count"]));
        }

        private void LoadRegionalExpansion()
        {
            chartDestinations.Series.Clear();
            var s = new Series("Dest") { ChartType = SeriesChartType.Line };
            chartDestinations.Series.Add(s);

            const string sql = @"
SELECT MONTH(CreationDate) AS Mon, COUNT(*) AS Count
FROM DESTINATION
GROUP BY MONTH(CreationDate)
ORDER BY Mon";
            dtDestinations = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtDestinations);
            foreach (DataRow r in dtDestinations.Rows)
                s.Points.AddXY(r["Mon"], Convert.ToDouble(r["Count"]));
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
                    MessageBox.Show("CSV files exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting CSV: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
