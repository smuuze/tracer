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
using Debug;

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
        /// The frame start
        /// </summary>
        private byte[] frameStart;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TracerDataCatcherTask"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="timeout">The timeout.</param>
        public TracerDataCatcherTask(TaskEngine engine, long timeout)
            : base(engine, timeout)
        {
            actualTaskState = BasicTaskStates.TASK_STATE_ILDE;
            debugMode = Debug.DEBUG_MODE.CONSOLE;

            frameStart = new byte[TracerFrame.HEADER_PREFIX_DATA_LENGTH];

            debug("TracerDataCatcherTask() - Created");
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

            switch (actualTaskState)
            {
                default:
                case BasicTaskStates.TASK_STATE_ILDE:

                    if (!getContext().TraceActive)
                    {
                        break;
                    }

                    debug("TracerDataCatcherTask.execute() - Starting Trace catching");
                    actualTaskState = BasicTaskStates.TASK_STATE_INIT;

                    break;

                case BasicTaskStates.TASK_STATE_INIT:

                    getContext().Connection.configure(getContext().Comport, getContext().Baudrate, DATABITS.DATABITS_8, PARITY.NONE, STOPBIT.ONE);
                    if (getContext().Connection.open() != ERR_CODES.OK)
                    {
                        debug(DEBUG_LEVEL.ERROR, "TracerDataCatcherTask.execute() - Opening Comport has FAILED !!! ---");
                        actualTaskState = BasicTaskStates.TASK_STATE_ILDE;
                    }
                    else
                    {
                        debug("TracerDataCatcherTask.execute() - Opening Comport succeeded");
                        actualTaskState = BasicTaskStates.TASK_STATE_RUNNING;
                    }

                    break;

                case BasicTaskStates.TASK_STATE_RUNNING:

                    if (!getContext().TraceActive)
                    {
                        debug("TracerDataCatcherTask.execute() - Trace catching has been stopped");
                        actualTaskState = BasicTaskStates.TASK_STATE_FINISH;
                        break;
                    }

                    loadBytesFromInterface();

                    break;

                case BasicTaskStates.TASK_STATE_FINISH:
                    if (getContext().Connection.close() != ERR_CODES.OK)
                    {
                        debug(DEBUG_LEVEL.ERROR, "TracerDataCatcherTask.execute() - Closing Comport has FAILED !!! ---");
                        actualTaskState = BasicTaskStates.TASK_STATE_ILDE;
                    }
                    else
                    {
                        debug("TracerDataCatcherTask.execute() - Closing Comport succeeded");
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
            actualTaskState = BasicTaskStates.TASK_STATE_ILDE;

            if (getContext().Connection.close() != ERR_CODES.OK)
            {
                debug(DEBUG_LEVEL.ERROR, "TracerDataCatcherTask.terminate() - Closing Comport has FAILED !!! ---");
            }
        }

        /// <summary>
        /// Loads the bytes from interface.
        /// </summary>
        private void loadBytesFromInterface()
        {
            if (getContext().Connection.numBytesAvailable() == 0)
            {
                return;
            }

            int headerIndex = 0;

            while (headerIndex < TracerFrame.HEADER_PREFIX_DATA_LENGTH)
            {
                byte headerByte = getContext().Connection.read(1, 10000)[0];

                if (headerByte != TracerFrame.HEADER_PREFIX_DATA_BYTE)
                {
                    headerIndex = 0;
                    continue;
                }

                headerIndex += 1;
            }

            if (headerIndex != TracerFrame.HEADER_PREFIX_DATA_LENGTH)
            {
                return;
            }

            // get number of following bytes
            byte[] byteCountRaw = getContext().Connection.read(2, 10000);
            int byteCount = (int)(((int)byteCountRaw[0] << 8) + (int)byteCountRaw[1]);

            debug("TracerDataCatcherTask.execute() - New Tracedata - Length: " + byteCount);
            getTraceTableRaw().addRawElement(TraceParser.getInstance().parseTraceData(byteCount, getContext().Connection.read(byteCount, 10000)));
        }
    }
}
