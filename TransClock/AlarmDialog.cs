using Schedule.NetStandard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransClock
{
    public partial class AlarmDialog : Form
    {
        public AlarmDialog()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        public void SetSchedule(string data)
        {
            string[] ArrData = data.Split('|');
            comboBox1.Text = ArrData[0];
            txtOffset.Text = ArrData[1];
        }

        public IScheduledItem GetSchedule()
        {
            return new ScheduledTime(comboBox1.Text, txtOffset.Text);
        }

        public string GetScheduleString()
        {
            return comboBox1.Text + "|" + txtOffset.Text;
        }
    }
}
