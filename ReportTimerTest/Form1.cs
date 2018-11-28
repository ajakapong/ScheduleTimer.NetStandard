using Schedule.NetStandard;
using System;
using System.Windows.Forms;

namespace ReportTimerTest
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Summary description for Form1.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        ReportTimer _Timer = new ReportTimer();

        private void button1_Click(object sender, System.EventArgs e)
        {
            _Timer.Start();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            _Timer.Stop();
        }

        bool _Sync = true;

        private void Form1_Load(object sender, System.EventArgs e)
        {
            _LabelHandler = new setLabelHandler(this.setLabel);
            _Timer.Elapsed += new ReportEventHandler(_Timer_Elapsed);
            _Timer.Error += new ExceptionEventHandler(Error);

            if (_Sync)
            {
                for (int i = 0; i < 60; ++i)
                {
                    _Timer.AddReportEvent(new ScheduledTime("ByMinute", "60,0"), 120 - i);
                    _Timer.AddReportEvent(new ScheduledTime("ByMinute", "60,0"), i);
                }
            }
            else
            {
                for (int i = 0; i < 60; ++i)
                {
                    _Timer.AddAsyncReportEvent(new ScheduledTime("ByMinute", "60,0"), 120 - i);
                    _Timer.AddAsyncReportEvent(new ScheduledTime("ByMinute", "60,0"), i);
                }
            }
            _Timer.AddReportEvent(new ScheduledTime("ByMinute", "30,0"), 60);
            _Timer.AddReportEvent(new ScheduledTime("ByMinute", "30,0"), 0);
        }
        private void _Timer_Elapsed(object sender, ReportEventArgs e)
        {
            label1.Invoke(_LabelHandler, new object[] { e.ReportNo });
        }

        delegate void setLabelHandler(int ReportNo);
        setLabelHandler _LabelHandler;
        private void setLabel(int ReportNo)
        {
            if (ReportNo < 60)
                label1.Text = ReportNo.ToString();
            else
                label2.Text = (ReportNo - 60).ToString();
        }

        private void Error(object Sender, ExceptionEventArgs Args)
        {
            string StrError = "";
            Exception e = Args.Error;
            while (e != null)
            {
                StrError += e.Message + "\r\n" + e.StackTrace + "\r\n-----------------------------\r\n";
                e = e.InnerException;
            }
            MessageBox.Show(StrError);
            Close();
        }
    }
}
