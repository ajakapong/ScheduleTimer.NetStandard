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
    public partial class ClockForm : Form
    {
        private ScheduleTimer _TickTimer = new ScheduleTimer();
        private ScheduleTimer _AlarmTimer = new ScheduleTimer();

        public ClockForm()
        {
            _Config = new Config("..\\..\\App.config");
            BackColor = NormalBackColor;
            _LastBackColor = AlarmColor;
            _TickTimer.Error += new ExceptionEventHandler(_TickTimer_Error);
            _AlarmTimer.Error += new ExceptionEventHandler(_TickTimer_Error);
            InitializeComponent();
        }

        private void ClockForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                SetLocation();
                SetFont();
                SetTickTimer();
                SetAlarmTimer();
            }
            catch (Exception ex)
            {
                Program.HandleException(ex);
            }
        }

        public string StrTime
        {
            get { return _StrTime; }
            set { _StrTime = value; }
        }

        public Color NormalBackColor
        {
            get { return Color.FromName(GetSettingDefault("back-color", "White")); }
        }

        public Color AlarmColor
        {
            get { return Color.FromName(GetSettingDefault("alarm-color", "Red")); }
        }

        public ScheduledTime AlarmTime
        {
            get
            {
                string StrAlarm = GetSettingDefault("alarm", "Daily|4:30 PM");
                string[] ArrAlarm = StrAlarm.Split('|');
                if (ArrAlarm.Length != 2)
                    throw new Exception("Invalid alarm format.");
                return new ScheduledTime(ArrAlarm[0], ArrAlarm[1]);
            }
        }

        private void SetTickTimer()
        {
            StrTime = DateTime.Now.ToString("T");
            _TickTimer.AddJob(
                new ScheduledTime(EventTimeBase.BySecond, TimeSpan.Zero),
                new TickHandler(_TickTimer_Elapsed)
            );
            _TickTimer.Start();
        }

        private void _TickTimer_Elapsed(DateTime EventTime)
        {
            lock (this)
            {
                StrTime = EventTime.ToString("T");
                if (_Flashing)
                {
                    Color Temp = BackColor;
                    BackColor = _LastBackColor;
                    _LastBackColor = Temp;
                }

            }
            this.Invoke(new System.Threading.ThreadStart(this.Invalidate));
        }

        delegate void TickHandler(DateTime tick);
        private void SetAlarmTimer()
        {
            _AlarmTimer.Stop();
            _AlarmTimer.ClearJobs();
            _AlarmTimer.AddJob(AlarmTime, new TickHandler(_AlarmTimer_Elapsed));
            _AlarmTimer.Start();
        }

        private void _AlarmTimer_Elapsed(DateTime time)
        {
            _Flashing = true;
            BackColor = NormalBackColor;
            _LastBackColor = AlarmColor;
        }

        private void ClockForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            lock (this)
            {
                SizeF sizeF = e.Graphics.MeasureString(StrTime, _Font);
                sizeF.Height += 6; sizeF.Width += 6;
                Size S = new Size((int)sizeF.Width, (int)sizeF.Height);
                if (S != this.ClientSize)
                    this.ClientSize = S;

                e.Graphics.DrawString(StrTime, _Font, new SolidBrush(_Color), 3, 3);
            }
        }

        bool _Flashing = false;
        bool _Drag = false;
        int _X, _Y;
        private void ClockForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_Flashing)
            {
                _Flashing = false;
                BackColor = NormalBackColor;
                _LastBackColor = AlarmColor;
            }
            _Drag = true;
            _X = e.X;
            _Y = e.Y;
        }

        private void ClockForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _Drag = false;
        }

        private void ClockForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_Drag == false)
                return;
            Point pCurent = Location;
            Location = new Point(Location.X + e.X - _X, Location.Y + e.Y - _Y);
        }

        private void itmClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        AlarmDialog _AlarmDialog = new AlarmDialog();
        private void ItmAlarm_Click(object sender, System.EventArgs e)
        {
            try
            {
                _AlarmDialog.SetSchedule(GetSettingDefault("alarm", "Daily|4:30 PM"));
                if (_AlarmDialog.ShowDialog(this) == DialogResult.OK)
                {
                    IScheduledItem Item = _AlarmDialog.GetSchedule();
                    SetSetting("alarm", _AlarmDialog.GetScheduleString());
                    _AlarmTimer.Stop();
                    _AlarmTimer.ClearJobs();
                    _AlarmTimer.AddJob(Item, new TickHandler(_AlarmTimer_Elapsed));
                    _AlarmTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Program.HandleException(ex);
            }
        }

        private void ClockForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetSetting("location", string.Format("{0},{1}", Location.X, Location.Y));
        }

        private string GetSettingDefault(string StrKey, string StrDefault)
        {
            return _Config.GetSettingDefault(StrKey, StrDefault);
        }

        private void SetSetting(string StrKey, string StrValue)
        {
            _Config.SetSetting(StrKey, StrValue);
        }

        private void SetLocation()
        {
            string StrLocation = GetSettingDefault("location", "10,10");
            string[] ArrLocation = StrLocation.Split(',');
            if (ArrLocation.Length != 2)
                return;
            Location = new Point(int.Parse(ArrLocation[0]), int.Parse(ArrLocation[1]));
        }

        private void SetFont()
        {
            string StrFont = GetSettingDefault("font", "Digital Readout Upright");
            float Size = float.Parse(GetSettingDefault("font-size", "28"));
            _Font = new Font(StrFont, Size);

            _Color = Color.FromName(GetSettingDefault("color", "Red"));
        }

        private Config _Config;
        Color _Color;
        Color _LastBackColor;
        Font _Font;
        string _StrTime;

        private void _TickTimer_Error(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.Error.Message + "\r\n" + e.Error.StackTrace);
            Close();
        }
    }
}

