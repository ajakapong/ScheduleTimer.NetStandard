using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.NetStandard
{
    public class ReportEventArgs : EventArgs
    {
        public ReportEventArgs(DateTime Time, int reportNo) { EventTime = Time; ReportNo = reportNo; }
        public int ReportNo;
        public DateTime EventTime;
    }

    public delegate void ReportEventHandler(object sender, ReportEventArgs e);

    /// <summary>
    /// Summary description for ReportTimer.
    /// </summary>
    public class ReportTimer : ScheduleTimerBase
    {
        public void AddReportEvent(IScheduledItem Schedule, int reportNo)
        {
            if (Elapsed == null)
                throw new Exception("You must set elapsed before adding Events");
            AddJob(new TimerJob(Schedule, new DelegateMethodCall(Handler, Elapsed, reportNo)));
        }

        public void AddAsyncReportEvent(IScheduledItem Schedule, int reportNo)
        {
            if (Elapsed == null)
                throw new Exception("You must set elapsed before adding Events");
            TimerJob Event = new TimerJob(Schedule, new DelegateMethodCall(Handler, Elapsed, reportNo));
            Event.SyncronizedEvent = false;
            AddJob(Event);
        }

        public event ReportEventHandler Elapsed;

        delegate void ConvertHandler(ReportEventHandler Handler, int ReportNo, object sender, DateTime time);
        static ConvertHandler Handler = new ConvertHandler(Converter);
        static void Converter(ReportEventHandler Handler, int ReportNo, object sender, DateTime time)
        {
            if (Handler == null)
                throw new ArgumentNullException("Handler");
            if (sender == null)
                throw new ArgumentNullException("sender");
            Handler(sender, new ReportEventArgs(time, ReportNo));
        }
    }
}
