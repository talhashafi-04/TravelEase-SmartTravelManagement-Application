using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseProject;
using TravelEase;
using TripBookingReportApp;

namespace TravelApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ////Application.Run(new LoginForm());
            //Application.Run(new TravelerDashboard("T009"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Uncomment the line below to test the TravelerDemographicsForm
            // Application.Run(new TravelEase.TravelerDemographicsForm());
            
            // Default application run
            //Application.Run(new AdminDashboardForm("A001"));
            //Application.Run(new TravelerDashboard("T001"));
            Application.Run(new Form1("SP001"));

        }
    }
}
