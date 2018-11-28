using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.NetStandard
{
    /// <summary>
    /// IEventStorage is used to provide persistance of schedule during service shutdowns.
    /// </summary>
    public interface IEventStorage
    {
        void RecordLastTime(DateTime Time);
        DateTime ReadLastTime();
    }
}
