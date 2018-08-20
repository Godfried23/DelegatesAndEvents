using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    //public delegate int WorkPerformedHandler(object sender, WorkPerformedEventArgs e);
    public class Worker
    {
        public event EventHandler<WorkPerformedEventArgs> WorkPerformed;
        public event EventHandler WorkCompleted;

        public void DoWork(int hours, WorkType workType)
        {
            for (int i = 0; i < hours; i++)
            {
                // Raise Event

                // Evoking the event like a method (Not the best practice)
                // WorkPerformed(i + 1, workType); // If you don't have any listeners attached to this method, you'll get an exception
                
                // The proper way to raise an event using separate methods
                OnWorkPerformed(i + 1, workType);
            }
            // Raise Event
            OnWorkCompleted();
        }

        protected virtual void OnWorkPerformed(int hours, WorkType workType)
        {
            var del = WorkPerformed as EventHandler<WorkPerformedEventArgs>; // The event cast as the delegate
            if (del != null)
            {
                del(this, new WorkPerformedEventArgs(hours, workType));
            }
        }

        protected virtual void OnWorkCompleted()
        {
            var del = WorkCompleted as EventHandler; // The event cast as the delegate
            if (del != null)
            {
                del(this, EventArgs.Empty);
            }
        }
    }
}
