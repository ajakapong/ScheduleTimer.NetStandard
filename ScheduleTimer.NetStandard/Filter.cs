using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Schedule.NetStandard
{
    /// <summary>
    /// This is an empty filter that does not filter any of the events.
    /// </summary>
    public class Filter : IResultFilter
    {
        public static IResultFilter Empty = new Filter();
        private Filter() { }

        public void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List)
        {
            if (List == null)
                return;
            List.Sort();
        }
    }

    /// <summary>
    /// This causes only the first event of the interval to be counted.
    /// </summary>
    public class FirstEventFilter : IResultFilter
    {
        public static IResultFilter Filter = new FirstEventFilter();
        private FirstEventFilter() { }

        public void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List)
        {
            if (List == null)
                return;
            if (List.Count < 2)
                return;
            List.Sort();
            List.RemoveRange(1, List.Count - 1);
        }
    }

    /// <summary>
    /// This causes only the last event of the interval to be counted.
    /// </summary>
    public class LastEventFilter : IResultFilter
    {
        public static IResultFilter Filter = new LastEventFilter();
        private LastEventFilter() { }

        public void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List)
        {
            if (List == null)
                return;
            if (List.Count < 2)
                return;
            List.Sort();
            List.RemoveRange(0, List.Count - 1);
        }
    }
}
