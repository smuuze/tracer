using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Connection;
using System.Threading;
using System.Diagnostics;

namespace TaskEngineModule
{
    /// <summary>
    /// 
    /// </summary>
    class TaskEngine
    {
        /// <summary>
        /// The notification handler
        /// </summary>
        private ChipdesignEventNotificationHandler notificationHandler;
        
        /// <summary>
        /// The engine active
        /// </summary>
        private bool engineActive;

        /// <summary>
        /// The task list
        /// </summary>
        private List<ITask> taskList;

        /// <summary>
        /// The engine interval time ms
        /// </summary>
        private int engineIntervalTimeMS;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskEngine"/> class.
        /// </summary>
        /// <param name="con">The con.</param>
        public TaskEngine()
        {
            notificationHandler = new ChipdesignEventNotificationHandler();
            taskList = new List<ITask>();
            engineActive = false;
        }

        /// <summary>
        /// Registers the specified new task.
        /// </summary>
        /// <param name="newTask">The new task.</param>
        public void register(ITask newTask)
        {
            taskList.Add(newTask);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start(int intervlaTimeMS)
        {
            engineActive = true;
            engineIntervalTimeMS = intervlaTimeMS;

            Thread runThread = new Thread(new ThreadStart(run));
            runThread.IsBackground = true;
            runThread.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void stop()
        {
            engineActive = false;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        protected void run()
        {
            var executionTimer = new Stopwatch();

            foreach (ITask task in taskList)
            {
                task.initiate();
                task.execute();
            } 

            while (engineActive)
            {
                executionTimer.Start();

                foreach (ITask task in taskList)
                {
                    if (task.isRunnable())
                    {
                        task.execute();
                    }
                }              

                long executaionDuration = executionTimer.ElapsedMilliseconds;
                executionTimer.Stop();

                long sleepTime = (long)engineIntervalTimeMS;
                if (executaionDuration < sleepTime)
                {
                    sleepTime -= executaionDuration;
                }
                else
                {
                    sleepTime = 10;
                }

                //System.Console.WriteLine("Engine.run() - going to sleep for " + sleepTime + " ms");

                // reduce CPU load by waiting a few milliseconds
                try
                {
                    Thread.Sleep((int)sleepTime);
                }
                catch (ArgumentOutOfRangeException)
                {
                    // do nohing
                }
            }

            foreach (ITask task in taskList)
            {
                task.terminate();
            } 
        }
    }
}
