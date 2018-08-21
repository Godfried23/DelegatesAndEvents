using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public delegate int BizRulesDelegate(int x, int y);
    class Program
    {
        static void Main(string[] args)
        {
            BizRulesDelegate addDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;

            var data = new ProcessData();
            data.Process(2, 3, addDel);
            data.Process(2, 3, multiplyDel);

            Action<int, int> myAction = (x, y) => Console.WriteLine(x + y);
            Action<int, int> myMultiplyAction = (x, y) => Console.WriteLine(x * y);
            data.ProcessAction(2, 3, myAction);
            data.ProcessAction(2, 3, myMultiplyAction);

            var worker = new Worker();

            //WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
            //WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
            //WorkPerformedHandler del3 = new WorkPerformedHandler(WorkPerformed3);

            //// These are hard coded
            ////del1(5, WorkType.Golf);
            ////del2(10, WorkType.GenerateReports);

            ////// This provides an example of a dynamic delegate call
            ////DoWork(del1);
            ////DoWork(del2);

            //// Multiple delegates and the invocation list
            //del1 += del2 + del3; // Combining delegates
            //int finalHours = del1(10, WorkType.GenerateReports);// The last delegate in the invocation list is what will get returned
            //Console.WriteLine(finalHours);

            // This is the manual way of hooking up the event handlers
            //worker.WorkPerformed += new EventHandler<WorkPerformedEventArgs>(Worker_WorkPerformed);
            //worker.WorkCompleted += new EventHandler(Worker_WorkCompleted);

            // This is how its done using Delegate Inference
            //worker.WorkPerformed += Worker_WorkPerformed; // The compiler already knows how the events and they types are set up in the Worker class,
            //worker.WorkCompleted += Worker_WorkCompleted; // so we don't need all the extra code

            // This is how its done using an Anonymous Method.
            //worker.WorkPerformed += delegate (object sender, WorkPerformedEventArgs e)
            //{ Console.WriteLine("Hours worked: " + e.Hours + " " + e.WorkType); };
            //worker.WorkCompleted += delegate(object sender, EventArgs e) { Console.WriteLine("Worker is done"); };

            // Using Lambdas
            worker.WorkPerformed += (s, e) => Console.WriteLine("Hours worked: " + e.Hours + " " + e.WorkType);
            worker.WorkCompleted += (s, e) => Console.WriteLine("Worker is complete");
            worker.DoWork(8, WorkType.GenerateReports);
        }

        //static void Worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        //{
        //    Console.WriteLine("Hours worked: " + e.Hours + " " + e.WorkType);
        //}

        //static void Worker_WorkCompleted(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Worker is done");
        //}

        //static void DoWork(WorkPerformedHandler del)
        //{
        //    del(12, WorkType.GoToMeetings);
        //}

        //static int WorkPerformed1(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkPerformed1 called " + hours.ToString());
        //    return hours + 1;
        //}

        //static int WorkPerformed2(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkPerformed2 called " + hours.ToString());
        //    return hours + 2;
        //}

        //static int WorkPerformed3(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkPerformed3 called " + hours.ToString());
        //    return hours + 3;
        //}
    }
    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateReports
    }
}
