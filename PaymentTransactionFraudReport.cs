using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DatabaseProject
{
    public partial class PaymentFraudReportForm : Form
    {
        private Chart chartSuccessFailure;
        private Chart chartChargebackRate;
        private Button btnGenerateReport;
        private Button btnExportCsv;
        private readonly SqlConnection con = new SqlConnection(
            "Data Source=Shehryar\\SQLEXPRESS;Initial Catalog=TravelEase;Integrated Security=True;Trust Server Certificate=True");

        // DataTables to hold report data
        private DataTable dtSuccessFailure;
        private DataTable dtChargeback;

        public PaymentFraudReportForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Payment Transaction & Fraud Report";
            this.ClientSize = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnGenerateReport = new Button
            {
                Text = "Generate Report",
                Location = new Point(20, 20),
                Size = new Size(150, 30)
            };
            btnGenerateReport.Click += (s, e) => LoadReportData();

            btnExportCsv = new Button
            {
                Text = "Export to CSV",
                Location = new Point(200, 20),
                Size = new Size(150, 30),
                Enabled = false
            };
            btnExportCsv.Click += (s, e) => ExportCsvFiles();

            chartSuccessFailure = CreateChart("Success vs Failure Rate", new Point(20, 70));
            chartChargebackRate = CreateChart("Monthly Chargeback Rate", new Point(460, 70));

            this.Controls.AddRange(new Control[]
            {
                btnGenerateReport,
                btnExportCsv,
                chartSuccessFailure,
                chartChargebackRate
            });
        }

        private Chart CreateChart(string title, Point location)
        {
            var chart = new Chart { Location = location, Size = new Size(400, 350) };
            chart.ChartAreas.Add(new ChartArea());
            chart.Titles.Add(title);
            return chart;
        }

        private void LoadReportData()
        {
            LoadSuccessFailureChart();
            LoadChargebackRateChart();
            btnExportCsv.Enabled = true;
        }

        private void LoadSuccessFailureChart()
        {
            chartSuccessFailure.Series.Clear();
            var series = new Series("Transactions") { ChartType = SeriesChartType.Pie };
            chartSuccessFailure.Series.Add(series);

            const string sql = @"
SELECT
  SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) AS SuccessCount,
  SUM(CASE WHEN Status != 'Completed' THEN 1 ELSE 0 END) AS FailureCount
FROM PAYMENT";
            dtSuccessFailure = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtSuccessFailure);

            if (dtSuccessFailure.Rows.Count > 0)
            {
                var row = dtSuccessFailure.Rows[0];
                int success = Convert.ToInt32(row["SuccessCount"]);
                int failure = Convert.ToInt32(row["FailureCount"]);
                series.Points.AddXY("Success", success);
                series.Points.AddXY("Failure", failure);
            }
        }

        private void LoadChargebackRateChart()
        {
            chartChargebackRate.Series.Clear();
            var series = new Series("ChargebackRate") { ChartType = SeriesChartType.Column };
            chartChargebackRate.Series.Add(series);

            const string sql = @"
SELECT
  DATENAME(MONTH, Date) AS MonthName,
  CAST(SUM(CASE WHEN Status = 'Refunded' THEN 1 ELSE 0 END) * 1.0 / NULLIF(COUNT(*),0) AS DECIMAL(5,2)) AS Rate
FROM PAYMENT
GROUP BY DATENAME(MONTH, Date), MONTH(Date)
ORDER BY MONTH(Date)";
            dtChargeback = new DataTable();
            new SqlDataAdapter(sql, con).Fill(dtChargeback);

            foreach (DataRow row in dtChargeback.Rows)
            {
                string month = row["MonthName"].ToString();
                double rate = Convert.ToDouble(row["Rate"]);
                series.Points.AddXY(month, rate);
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
                    WriteDataTableToCsv(dtSuccessFailure, Path.Combine(folder, "PaymentSuccessFailureRate.csv"));
                    WriteDataTableToCsv(dtChargeback, Path.Combine(folder, "MonthlyChargebackRate.csv"));
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
