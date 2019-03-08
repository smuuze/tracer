using Debug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEngineModule;
using Tracer.app.task;
using Tracer.app.types;
using Tracer.gui;

namespace Tracer.app.task
{
    class TracerFileLoadTask : TracerCommonTask, ITask
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
        /// Initializes a new instance of the <see cref="TracerDataCatcherTask"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="timeout">The timeout.</param>
        public TracerFileLoadTask(TaskEngine engine, long timeout)
            : base(engine, timeout)
        {
            debugMode = Debug.DEBUG_MODE.FILE;
            debug("TracerFileLoadTask() - Created");
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

                    if (getTraceTableRaw().getSize() == 0)
                    {
                        break;
                    }

                    debug("TracerFileLoadTask.execute() - Starting File loading");
                    actualTaskState = BasicTaskStates.TASK_STATE_INIT;

                    break;

                case BasicTaskStates.TASK_STATE_INIT:

                    actualTaskState = BasicTaskStates.TASK_STATE_RUNNING;
                    break;

                case BasicTaskStates.TASK_STATE_RUNNING:

                    if (!getContext().TraceActive)
                    {
                        actualTaskState = BasicTaskStates.TASK_STATE_FINISH;
                        break;
                    }

                    while (getTraceTableRaw().getSize() != 0)
                    {
                        TraceElement traceElement = getTraceTableRaw().getNext();

                        if (traceElement.Type == TraceType.UNKNOWN)
                        {
                            continue;
                        }

                        string filePath = (getContext().BasicFilePath + traceElement.FileName).Replace("/","\\");
                        debug("TracerFileLoadTask.execute() - Loading File: " + filePath);

                        string[] fileContent = getFileFactory().getFileContentAsLineArray(filePath);

                        if (traceElement.LineNumber > fileContent.Length - 1)
                        {
                            debug(DEBUG_LEVEL.ERROR, "TracerFileLoadTask.execute() - File not found !!! --- (" + filePath + ")");
                            continue;
                        }

                        traceElement.CodeLine = fileContent[traceElement.LineNumber];
                        getTraceTable().addElement(traceElement);
                    }

                    this.invokeEvent(TracerEventType.NEW_TRACE_RECORD);

                    break;

                case BasicTaskStates.TASK_STATE_FINISH:
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
    }
}
