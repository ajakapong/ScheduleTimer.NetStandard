using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransClock
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.Run(new ClockForm()); }
            catch (Exception ex) { HandleException(ex); }
        }

        public static void HandleException(Exception e)
        {
            MessageBox.Show(e.Message + "\r\n" + e.StackTrace);
        }
    }
}
