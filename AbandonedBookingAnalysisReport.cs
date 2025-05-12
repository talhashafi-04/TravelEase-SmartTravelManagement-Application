using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DatabaseProject
{
    public partial class AbandonedBookingReportForm : Form
    {
        private Chart chartAbandonment, chartReasons, chartRecovery, chartLossTrend;
        private Button btnGenerate, btnExportCsv;
        private SqlConnection con = new SqlConnection(
            @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;");

        // DataTables for CSV export
        private DataTable dtAbandonment, dtReasons, dtRecovery, dtLossTrend;

        public AbandonedBookingReportForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Abandoned Booking Analysis";
            this.ClientSize = new Size(1000, 850);
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
                Size = new Size(150, 30)
            };
            btnExportCsv.Click += (s, e) => ExportCsvFiles();
            btnExportCsv.Enabled = false; // only enabled after generating

            chartAbandonment = CreateChart("Abandonment Rate", new Point(20, 70));
            chartReasons = CreateChart("Top Cancellation Reasons", new Point(520, 70));
            chartRecovery = CreateChart("Recovery Rate", new Point(20, 400));
            chartLossTrend = CreateChart("Potential Revenue Loss (Monthly)", new Point(520, 400));

            this.Controls.AddRange(new Control[] {
                btnGenerate, btnExportCsv,
                chartAbandonment, chartReasons,
                chartRecovery, chartLossTrend
            });
        }

        private Chart CreateChart(string title, Point loc)
        {
            var chart = new Chart { Location = loc, Size = new Size(450, 300) };
            chart.ChartAreas.Add(new ChartArea());
            chart.Titles.Add(title);
            return chart;
        }

        private void LoadReportData()
        {
            LoadAbandonmentChart();
            LoadReasonsChart();
            LoadRecoveryChart();
            LoadLossTrendChart();
            btnExportCsv.Enabled = true;
        }

        private void LoadAbandonmentChart()
        {
            chartAbandonment.Series.Clear();
            var series = new Series("Status") { ChartType = SeriesChartType.Pie };
            chartAbandonment.Series.Add(series);

            const string sql = @"
SELECT
  SUM(CASE WHEN Status='Cancelled' THEN 1 ELSE 0 END) AS Abandoned,
  SUM(CASE WHEN Status!='Cancelled' THEN 1 ELSE 0 END) AS Completed
FROM Booking";
            dtAbandonment = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtAbandonment);

            if (dtAbandonment.Rows.Count > 0)
            {
                var row = dtAbandonment.Rows[0];
                series.Points.AddXY("Abandoned", Convert.ToInt32(row["Abandoned"]));
                series.Points.AddXY("Completed", Convert.ToInt32(row["Completed"]));
            }
        }

        private void LoadReasonsChart()
        {
            chartReasons.Series.Clear();
            var series = new Series("Reasons") { ChartType = SeriesChartType.Column };
            chartReasons.Series.Add(series);

            const string sql = @"
SELECT TOP 5 CancellationReason AS Reason, COUNT(*) AS Count
FROM Booking
WHERE Status='Cancelled' AND CancellationReason IS NOT NULL
GROUP BY CancellationReason
ORDER BY Count DESC";
            dtReasons = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtReasons);

            foreach (DataRow r in dtReasons.Rows)
                series.Points.AddXY(r["Reason"].ToString(), Convert.ToInt32(r["Count"]));
        }

        private void LoadRecoveryChart()
        {
            chartRecovery.Series.Clear();
            var series = new Series("Recovery") { ChartType = SeriesChartType.Pie };
            chartRecovery.Series.Add(series);

            const string sql = @"
SELECT 
  (SELECT COUNT(DISTINCT b2.CustomerID) 
     FROM Booking b2 
    WHERE b2.Status='Delivered' 
      AND b2.CustomerID IN 
        (SELECT DISTINCT b1.CustomerID FROM Booking b1 WHERE b1.Status='Cancelled')
  ) AS Recovered,
  (
    SELECT COUNT(DISTINCT b1.CustomerID) FROM Booking b1 WHERE b1.Status='Cancelled'
  )
  -
  (SELECT COUNT(DISTINCT b2.CustomerID) 
     FROM Booking b2 
    WHERE b2.Status='Delivered' 
      AND b2.CustomerID IN 
        (SELECT DISTINCT b1.CustomerID FROM Booking b1 WHERE b1.Status='Cancelled')
  ) AS NotRecovered";
            dtRecovery = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtRecovery);

            if (dtRecovery.Rows.Count > 0)
            {
                var r = dtRecovery.Rows[0];
                series.Points.AddXY("Recovered", Convert.ToInt32(r["Recovered"]));
                series.Points.AddXY("Not Recovered", Convert.ToInt32(r["NotRecovered"]));
            }
        }

        private void LoadLossTrendChart()
        {
            chartLossTrend.Series.Clear();
            var series = new Series("Loss") { ChartType = SeriesChartType.Line };
            chartLossTrend.Series.Add(series);

            const string sql = @"
SELECT MONTH(BookingDate) AS Month, SUM(TotalAmount) AS Lost
FROM Booking
WHERE Status='Cancelled'
GROUP BY MONTH(BookingDate)
ORDER BY Month";
            dtLossTrend = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtLossTrend);

            foreach (DataRow r in dtLossTrend.Rows)
                series.Points.AddXY(Convert.ToInt32(r["Month"]), Convert.ToDouble(r["Lost"]));
        }

        private void ExportCsvFiles()
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select folder to save CSV reports";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                string folder = dlg.SelectedPath;

                try
                {
                    WriteDataTableToCsv(dtAbandonment, Path.Combine(folder, "AbandonmentRate.csv"));
                    WriteDataTableToCsv(dtReasons, Path.Combine(folder, "CancellationReasons.csv"));
                    WriteDataTableToCsv(dtRecovery, Path.Combine(folder, "RecoveryRates.csv"));
                    WriteDataTableToCsv(dtLossTrend, Path.Combine(folder, "PotentialRevenueLoss.csv"));

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
                        var value = row[i]?.ToString().Replace(',', ' ');
                        sw.Write(value);
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
