namespace TravelEase
{
    partial class TravelerDemographicsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNationality;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAge;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTripPreferences;
        private System.Windows.Forms.DataGridView dgvSpending;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chartNationality = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAge = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTripPreferences = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvSpending = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.chartNationality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTripPreferences)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpending)).BeginInit();
            this.SuspendLayout();

            this.chartNationality.Location = new System.Drawing.Point(12, 12);
            this.chartNationality.Size = new System.Drawing.Size(400, 300);
            this.chartNationality.Name = "chartNationality";

            this.chartAge.Location = new System.Drawing.Point(430, 12);
            this.chartAge.Size = new System.Drawing.Size(400, 300);
            this.chartAge.Name = "chartAge";

            this.chartTripPreferences.Location = new System.Drawing.Point(12, 330);
            this.chartTripPreferences.Size = new System.Drawing.Size(818, 300);
            this.chartTripPreferences.Name = "chartTripPreferences";

            this.dgvSpending.Location = new System.Drawing.Point(12, 650);
            this.dgvSpending.Size = new System.Drawing.Size(818, 200);
            this.dgvSpending.Name = "dgvSpending";
            this.dgvSpending.ReadOnly = true;

            this.ClientSize = new System.Drawing.Size(850, 870);
            this.Controls.Add(this.chartNationality);
            this.Controls.Add(this.chartAge);
            this.Controls.Add(this.chartTripPreferences);
            this.Controls.Add(this.dgvSpending);
            this.Name = "TravelerDemographicsForm";
            this.Text = "Traveler Demographics and Preferences";

            ((System.ComponentModel.ISupportInitialize)(this.chartNationality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTripPreferences)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpending)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
