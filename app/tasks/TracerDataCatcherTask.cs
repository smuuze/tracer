using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskEngineModule;
using Connection;
using Connection.SerialIO;
using Tracer.app.types;
using Tracer.app.modules;

namespace Tracer.app.task
{
    /// <summary>
    /// 
    /// </summary>
    class TracerDataCatcherTask : TracerCommonTask, ITask
    {
        /// <summary>
        /// The trace r_ dat a_ cathce r_ tas k_ interva l_ timeou t_ ms
        /// </summary>
        public static long INTERVAL_TIMEOUT_MS = 10;

        /// <summary>
        /// The actual task state
        /// </summary>
        private BasicTaskStates actualTaskState;

        /// <summary>
        /// The connection
        /// </summary>
        private ICommonConnectionInterface connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracerDataCatcherTask"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="timeout">The timeout.</param>
        public TracerDataCatcherTask(TaskEngine engine, long timeout)
            : base(engine, timeout)
        {
            connection = SerialIOFactory.getInstance().getSerialConnection();
            actualTaskState = BasicTaskStates.TASK_STATE_ILDE;
        }

        /// <summary>
        /// Determines whether this instance is runnable.
        /// </summary>
        /// <returns></returns>
        public bool isRunnable()
        {
            if (!isIntervalTimeout())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns></returns>
        public bool isRunning()
        {
            return false;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void execute()
        {
            markExecutionTime();

            if (connection.numBytesAvailable() == 0)
            {
                return;
            }

            switch (actualTaskState)
            {
                default:
                case BasicTaskStates.TASK_STATE_ILDE:

                    if (!getContext().TraceActive)
                    {
                        break;
                    }
                       
                    System.Console.WriteLine("TracerDataCatcherTask.execute() - Starting Trace catching");
                    actualTaskState = BasicTaskStates.TASK_STATE_INIT;

                    break;

                case BasicTaskStates.TASK_STATE_INIT:

                    connection.configure(getContext().Comport, getContext().Baudrate, DATABITS.DATABITS_8, PARITY.NONE, STOPBIT.ONE);
                    if (connection.open() != ERR_CODES.OK)
                    {
                        System.Console.WriteLine("TracerDataCatcherTask.execute() - Opening Comport has FAILED !!! ---");
                        actualTaskState = BasicTaskStates.TASK_STATE_ILDE;
                    }
                    else
                    {
                        System.Console.WriteLine("TracerDataCatcherTask.execute() - Opening Comport succeeded");
                        actualTaskState = BasicTaskStates.TASK_STATE_RUNNING;
                    }

                    break;

                case BasicTaskStates.TASK_STATE_RUNNING:

                    if (!getContext().TraceActive)
                    {
                        System.Console.WriteLine("TracerDataCatcherTask.execute() - Trace catching has been stopped");
                        actualTaskState = BasicTaskStates.TASK_STATE_FINISH;
                        break;
                    }

                    if (connection.numBytesAvailable() == 0)
                    {
                        break;
                    }

                    loadBytesFromInterface();

                    break;

                case BasicTaskStates.TASK_STATE_FINISH:
                    if (connection.close() != ERR_CODES.OK)
                    {
                        System.Console.WriteLine("TracerDataCatcherTask.execute() - Closing Comport has FAILED !!! ---");
                        actualTaskState = BasicTaskStates.TASK_STATE_ILDE;
                    }
                    else
                    {
                        System.Console.WriteLine("TracerDataCatcherTask.execute() - Closing Comport succeeded");
                        actualTaskState = BasicTaskStates.TASK_STATE_RUNNING;
                    }
                    break;
            }
        }

        /// <summary>
        /// Initiates this instance.
        /// </summary>
        public void initiate()
        {

        }

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        public void terminate()
        {

        }

        /// <summary>
        /// Loads the bytes from interface.
        /// </summary>
        private void loadBytesFromInterface()
        {
            while (connection.numBytesAvailable() != 0)
            {
                // get number of following bytes
                byte[] byteCountRaw = connection.read(2, 10);
                int byteCount = (int)(((int)byteCountRaw[1] << 8) + (int)byteCountRaw[0]);

                getTraceTable().addElement(TraceParser.getInstance().parseTraceData(connection.read(byteCount, 10)));
            }
        }
    }
}
