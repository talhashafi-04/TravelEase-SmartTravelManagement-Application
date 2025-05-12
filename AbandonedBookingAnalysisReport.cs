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
        private readonly SqlConnection con = new SqlConnection(
            @"Data Source=TALHA-SHAFI\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Encrypt=False;");

        // DataTables for CSV export
        private DataTable dtAbandonment, dtReasons, dtRecovery, dtLossTrend;

        public AbandonedBookingReportForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Abandoned Booking Analysis";
            ClientSize = new Size(1000, 850);
            StartPosition = FormStartPosition.CenterScreen;

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

            chartAbandonment = CreateChart("Abandonment Rate", new Point(20, 70));
            chartReasons = CreateChart("Top Cancellation Reasons", new Point(520, 70));
            chartRecovery = CreateChart("Recovery Rate", new Point(20, 400));
            chartLossTrend = CreateChart("Potential Revenue Loss (Monthly)", new Point(520, 400));

            Controls.AddRange(new Control[] {
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
  SUM(CASE WHEN Status='Cancelled' THEN 1 ELSE 0 END)    AS Abandoned,
  SUM(CASE WHEN Status<>'Cancelled' THEN 1 ELSE 0 END)   AS Completed
FROM Booking";
            dtAbandonment = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtAbandonment);

            if (dtAbandonment.Rows.Count > 0)
            {
                var row = dtAbandonment.Rows[0];
                int abandoned = row.IsNull("Abandoned") ? 0 : Convert.ToInt32(row["Abandoned"]);
                int completed = row.IsNull("Completed") ? 0 : Convert.ToInt32(row["Completed"]);
                double total = abandoned + completed;
                if (total > 0)
                {
                    series.Points.AddXY("Abandoned", abandoned / total);
                    series.Points.AddXY("Completed", completed / total);
                    series["PieLabelStyle"] = "Outside";
                    series.Label = "#PERCENT";
                }
            }
        }

        private void LoadReasonsChart()
        {
            chartReasons.Series.Clear();
            var series = new Series("Reasons")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Label = "#VALY"
            };
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
            {
                string reason = r["Reason"].ToString();
                int count = r.IsNull("Count") ? 0 : Convert.ToInt32(r["Count"]);
                series.Points.AddXY(reason, count);
            }
        }

        private void LoadRecoveryChart()
        {
            chartRecovery.Series.Clear();
            var series = new Series("Recovery") { ChartType = SeriesChartType.Pie };
            chartRecovery.Series.Add(series);

            const string sql = @"
SELECT
  -- How many distinct travelers who abandoned later completed
  (SELECT COUNT(DISTINCT b2.TravelerID)
     FROM Booking b2
    WHERE b2.Status = 'Completed'
      AND b2.TravelerID IN (
          SELECT DISTINCT b1.TravelerID
            FROM Booking b1
           WHERE b1.Status = 'Cancelled'
      )
  ) AS Recovered,
  -- Total distinct travelers who abandoned
  (SELECT COUNT(DISTINCT b1.TravelerID)
     FROM Booking b1
    WHERE b1.Status = 'Cancelled'
  )
  -- Subtract recovered to get those not recovered
  -
  (SELECT COUNT(DISTINCT b2.TravelerID)
     FROM Booking b2
    WHERE b2.Status = 'Completed'
      AND b2.TravelerID IN (
          SELECT DISTINCT b1.TravelerID
            FROM Booking b1
           WHERE b1.Status = 'Cancelled'
      )
  ) AS NotRecovered
";

            dtRecovery = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtRecovery);

            if (dtRecovery.Rows.Count > 0)
            {
                DataRow row = dtRecovery.Rows[0];
                int recovered = row.IsNull("Recovered") ? 0 : Convert.ToInt32(row["Recovered"]);
                int notRecovered = row.IsNull("NotRecovered") ? 0 : Convert.ToInt32(row["NotRecovered"]);
                double total = recovered + notRecovered;
                if (total > 0)
                {
                    // Plot as percentages
                    series.Points.AddXY("Recovered", recovered / total);
                    series.Points.AddXY("Not Recovered", notRecovered / total);
                    series["PieLabelStyle"] = "Outside";
                    series.Label = "#PERCENT";
                }
            }
        }

        private void LoadLossTrendChart()
        {
            chartLossTrend.Series.Clear();
            var series = new Series("Loss")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                IsValueShownAsLabel = true,
                Label = "#VALY{C0}",
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6
            };
            chartLossTrend.Series.Add(series);

            const string sql = @"
SELECT
    DATENAME(MONTH, [Date]) AS MonthName,
    MONTH([Date])           AS Mon,
    SUM(TotalAmount)        AS LostAmount
FROM Booking
WHERE Status = 'Cancelled'
GROUP BY
    DATENAME(MONTH, [Date]),
    MONTH([Date])
ORDER BY
    MONTH([Date])";

            dtLossTrend = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtLossTrend);

            foreach (DataRow r in dtLossTrend.Rows)
            {
                string month = r["MonthName"].ToString();
                double loss = r.IsNull("LostAmount") ? 0 : Convert.ToDouble(r["LostAmount"]);
                series.Points.AddXY(month, loss);
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
                        string val = row[i]?.ToString().Replace(',', ' ');
                        sw.Write(val);
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
