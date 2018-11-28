using Schedule.NetStandard;
using System;
using System.Collections;

namespace TimerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MonthlyTest();
                MethodTest();
                HourlyTest();
                DailyTest();
                BlockTest();
                WeeklyTest();
                SimpleTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("enter q and <enter> to quit.");
                try { while (String.Compare(Console.ReadLine(), "q", true) != 0) ; } catch { };
            }
        }
        static ScheduleTimer _Timer = new ScheduleTimer();

        private delegate void f(DateTime time);
        static f _f = new f(WriteEvent);

        public static void WriteEvent(DateTime time)
        {
            DateTime actual = DateTime.Now;
            Console.WriteLine("{0} - {1} = {2}", actual, time, (actual - time).TotalMilliseconds);
        }

        private static void _Timer_Error(object sender, ExceptionEventArgs Args)
        {
            Console.WriteLine(Args.EventTime);
            Console.WriteLine(Args.Error.Message);
            Console.WriteLine(Args.Error.StackTrace);
        }

        private delegate void vv();
        private delegate void dOne(string p1);
        private delegate void dOutput(string p1, DateTime time);
        private delegate void dTwo(string p1, string p2);
        private delegate void dThree(string p1, string p2, string p3);
        private delegate void Array(object[] objectList);

        private static object monitor = new object();
        private static Random r = new Random();
        private static void Output(string p1, DateTime time)
        {
            double amount;
            lock (r)
                amount = r.NextDouble();

            System.Threading.Thread.Sleep((int)(amount * 5000));

            if (String.Compare(p1, "error", true) == 0)
                throw new Exception("error");

            lock (monitor)
            {
                DateTime actual = DateTime.Now;
                Console.WriteLine("{0} - {1} = {2} Msg:{3}", actual, time, (actual - time).TotalMilliseconds, p1);
            }
        }

        private static void One(string p1)
        {
            if (p1 != "p1")
                throw new Exception("p1 must equal p1");
        }

        private static void Two(string p1, string p2)
        {
            if (p1 != "p1")
                throw new Exception("p1 must equal p1");
            if (p2 != "p2")
                throw new Exception("p2 must equal p2");
        }

        private static void Three(string p1, string p2, string p3)
        {
            if (p1 != "p1")
                throw new Exception("p1 must equal p1");
            if (p2 != "p2")
                throw new Exception("p2 must equal p2");
            if (p3 != "p3")
                throw new Exception("p3 must equal p3");
        }

        private static void Arr(object[] objectList)
        {
        }

        private static void MethodTest()
        {
            IMethodCall call;
            call = new DelegateMethodCall(new dOne(One), new OrderParameterSetter("p1"));
            call.Execute();
            call = new DelegateMethodCall(new dTwo(Two), new OrderParameterSetter("p1", "p2"));
            call.Execute();
            call = new DelegateMethodCall(new dThree(Three), new OrderParameterSetter("p1", "p2", "p3"));
            call.Execute();
            call = new DelegateMethodCall(new Array(Arr), new OrderParameterSetter(new object[] { new object[] { 1, "3", "five" } }));
            call.Execute();
            //			call = new DelegateMethodCall(new Array(Arr), new OrderParameterSetter(new object[] { 1, "3", "five" }));
            //				call.Execute();
        }

        private static void MonthlyTest()
        {
            IScheduledItem item = new ScheduledTime(EventTimeBase.Monthly, new TimeSpan(0));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), false, new DateTime(2004, 2, 1));
            TestItem(item, new DateTime(2004, 1, 31), true, new DateTime(2004, 2, 1));
            TestItem(item, new DateTime(2004, 1, 31), false, new DateTime(2004, 2, 1));
            TestItem(item, new DateTime(2004, 1, 31, 23, 59, 59, 999), false, new DateTime(2004, 2, 1));
            TestItem(item, new DateTime(2004, 1, 15), true, new DateTime(2004, 2, 1));

            TestItem(new ScheduledTime(EventTimeBase.Monthly, new TimeSpan(1, 3, 2, 1, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 2, 3, 2, 1));
            TestItem(new ScheduledTime(EventTimeBase.Monthly, new TimeSpan(14, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 15, 0, 0, 0));
        }

        private static void WeeklyTest()
        {
            IScheduledItem item = new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(1, 0, 0, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 5));
            TestItem(item, new DateTime(2004, 1, 2), true, new DateTime(2004, 1, 5));
            TestItem(item, new DateTime(2003, 12, 30), true, new DateTime(2004, 1, 5));
            TestItem(item, new DateTime(2004, 1, 5), true, new DateTime(2004, 1, 5));
            TestItem(item, new DateTime(2004, 1, 5), false, new DateTime(2004, 1, 12));
            TestItem(item, new DateTime(2004, 1, 6), false, new DateTime(2004, 1, 12));

            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 4));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(1, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 5));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(2, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 6));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(3, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 7));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(4, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(5, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 2));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(6, 0, 0, 0, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 3));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(4, 0, 0, 0, 0)), new DateTime(2004, 1, 1), false, new DateTime(2004, 1, 8));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 3, 0, 0), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 6, 0, 0), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 6, 33, 0), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 6, 34, 0), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 6, 34, 23), true, new DateTime(2004, 1, 4, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 6, 34, 23), false, new DateTime(2004, 1, 11, 6, 34, 23));
            TestItem(new ScheduledTime(EventTimeBase.Weekly, new TimeSpan(0, 6, 34, 23, 0)), new DateTime(2004, 1, 4, 6, 34, 24), true, new DateTime(2004, 1, 11, 6, 34, 23));
        }

        private static void HourlyTest()
        {
            IScheduledItem item = new ScheduledTime(EventTimeBase.Hourly, TimeSpan.FromMinutes(20));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1, 0, 20, 0));
            TestItem(item, new DateTime(2004, 1, 1), false, new DateTime(2004, 1, 1, 0, 20, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 20, 0), true, new DateTime(2004, 1, 1, 0, 20, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 20, 0), false, new DateTime(2004, 1, 1, 1, 20, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 20, 1), true, new DateTime(2004, 1, 1, 1, 20, 0));
        }

        private static void DailyTest()
        {
            IScheduledItem item = new ScheduledTime(EventTimeBase.Daily, new TimeSpan(0, 6, 0, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1, 6, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 0, 0), true, new DateTime(2004, 1, 1, 6, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 1, 0), true, new DateTime(2004, 1, 2, 6, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 0, 1), true, new DateTime(2004, 1, 2, 6, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 0, 0, 1), true, new DateTime(2004, 1, 2, 6, 0, 0));
        }

        private static void BlockTest()
        {
            IScheduledItem item =
                new BlockWrapper(
                new SimpleInterval(DateTime.Parse("1/01/2004"), TimeSpan.FromMinutes(15)),
                "Daily",
                "6:00 AM",
                "5:00 PM");

            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1, 6, 15, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 0, 0), true, new DateTime(2004, 1, 1, 6, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 0, 0), false, new DateTime(2004, 1, 1, 6, 15, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 0, 1), true, new DateTime(2004, 1, 1, 6, 15, 0));
            TestItem(item, new DateTime(2004, 1, 1, 6, 15, 0), true, new DateTime(2004, 1, 1, 6, 15, 0));
            TestItem(item, new DateTime(2004, 1, 1, 16, 45, 0), false, new DateTime(2004, 1, 1, 17, 0, 0));
            TestItem(item, new DateTime(2004, 1, 1, 17, 1, 0), true, new DateTime(2004, 1, 2, 6, 15, 0));

        }

        private static void SimpleTest()
        {
            IScheduledItem item = new SimpleInterval(new DateTime(2004, 1, 1), TimeSpan.FromMinutes(2));
            TestItem(item, new DateTime(2003, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2003, 1, 1), false, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), false, new DateTime(2004, 1, 1, 0, 2, 0));
            TestItem(item, new DateTime(2055, 1, 1), true, new DateTime(2055, 1, 1));

            item = new SimpleInterval(new DateTime(2004, 1, 1), TimeSpan.FromMinutes(2), 1);
            TestItem(item, new DateTime(2003, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2003, 1, 1), false, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), false, DateTime.MaxValue);
            TestItem(item, new DateTime(2004, 1, 1, 0, 0, 1), true, DateTime.MaxValue);

            item = new SimpleInterval(new DateTime(2004, 1, 1), TimeSpan.FromMinutes(2), 2);
            TestItem(item, new DateTime(2003, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2003, 1, 1), false, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), false, new DateTime(2004, 1, 1, 0, 2, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 0, 1), true, new DateTime(2004, 1, 1, 0, 2, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 3, 0), true, DateTime.MaxValue);

            item = new SimpleInterval(new DateTime(2004, 1, 1), TimeSpan.FromMinutes(2), 3);
            TestItem(item, new DateTime(2003, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2003, 1, 1), false, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), true, new DateTime(2004, 1, 1));
            TestItem(item, new DateTime(2004, 1, 1), false, new DateTime(2004, 1, 1, 0, 2, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 0, 1), true, new DateTime(2004, 1, 1, 0, 2, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 3, 0), true, new DateTime(2004, 1, 1, 0, 4, 0));
            TestItem(item, new DateTime(2004, 1, 1, 0, 5, 0), true, DateTime.MaxValue);
        }

        private static void MultipleJobTimer()
        {
            DateTime now = DateTime.Now;
            Delegate d = new dOutput(Output);
            IScheduledItem item;

            //			item = new SimpleInterval(now, new TimeSpan(0, 0, 1));
            //			_Timer.AddJob(item, d, "error");

            item = new SimpleInterval(now, new TimeSpan(0, 0, 3));
            _Timer.AddJob(item, d, "three");

            item = new SimpleInterval(now, new TimeSpan(0, 0, 2));
            _Timer.AddJob(item, d, "two");

            _Timer.Error += new ExceptionEventHandler(_Timer_Error);
            _Timer.Start();
        }

        private static void MultipleAsyncJobTimer()
        {
            DateTime now = DateTime.Now;
            Delegate d = new dOutput(Output);
            IScheduledItem item;

            item = new SimpleInterval(now, new TimeSpan(0, 0, 1));
            _Timer.AddAsyncJob(item, d, "one");

            item = new SimpleInterval(now, new TimeSpan(0, 0, 3));
            _Timer.AddAsyncJob(item, d, "three");

            item = new SimpleInterval(now, new TimeSpan(0, 0, 2));
            _Timer.AddAsyncJob(item, d, "two");

            _Timer.Error += new ExceptionEventHandler(_Timer_Error);
            _Timer.Start();
        }

        private static ArrayList TestList(IScheduledItem item, DateTime Start, DateTime End)
        {
            ArrayList List = new ArrayList();
            item.AddEventsInInterval(Start, End, List);
            return List;
        }

        private static void TestItem(IScheduledItem item, DateTime input, bool AllowExact, DateTime ExpectedOutput)
        {
            DateTime Result = item.NextRunTime(input, AllowExact);
            if (Result == ExpectedOutput)
                Console.WriteLine("Success");
            else
                Console.WriteLine(string.Format("Failure: Received: {0} Expected: {1}", Result, ExpectedOutput));
        }
    }
}
