using Connection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEngineModule
{
    class CommonTask
    {
        /// <summary>
        /// The parent engine
        /// </summary>
        protected TaskEngine parentEngine;

        /// <summary>
        /// The interval execution time
        /// </summary>
        protected Stopwatch taskIntervalTimer;

        /// <summary>
        /// The interval t imeout millis
        /// </summary>
        protected long intervalTImeoutMillis;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonTask"/> class.
        /// </summary>
        public CommonTask(TaskEngine engine, long timeout = 1000)
        {
            parentEngine = engine;
            intervalTImeoutMillis = timeout;
            taskIntervalTimer = new Stopwatch();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            taskIntervalTimer.Start();
        }

        /// <summary>
        /// Marks the execution time.
        /// </summary>
        protected void markExecutionTime()
        {
            taskIntervalTimer.Reset();
            taskIntervalTimer.Start();
        }

        /// <summary>
        /// Determines whether [is interval timeout].
        /// </summary>
        /// <returns></returns>
        protected bool isIntervalTimeout()
        {
            return (taskIntervalTimer.ElapsedMilliseconds > intervalTImeoutMillis);
        }
    }
}
