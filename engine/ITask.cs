using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEngineModule
{
    interface ITask
    {
        /// <summary>
        /// Determines whether this instance is runnable.
        /// </summary>
        /// <returns></returns>
        bool isRunnable();

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns></returns>
        bool isRunning();

        /// <summary>
        /// Executes this instance.
        /// </summary>
        void execute();

        /// <summary>
        /// Initiates this instance.
        /// </summary>
        void initiate();

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        void terminate();
    }
}
