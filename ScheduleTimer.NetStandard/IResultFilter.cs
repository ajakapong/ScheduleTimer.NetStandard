using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Schedule.NetStandard
{
    /// <summary>
    /// The IResultFilter interface represents filters that either sort the events for an interval or
    /// remove duplicate events either selecting the first or the last event.
    /// </summary>
    public interface IResultFilter
    {
        void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List);
    }
}
