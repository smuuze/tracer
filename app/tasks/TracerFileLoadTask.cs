using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEngineModule;
using Tracer.app.task;
using Tracer.app.types;
using Tracer.src.gui;

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

                    if (getTraceTable().getSize() == 0)
                    {
                        break;
                    }

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

                    while (getTraceTable().getSize() != 0)
                    {
                        TraceElement traceElement = getTraceTable().getNext();

                        if (traceElement.Type == TraceType.UNKNOWN)
                        {
                            continue;
                        }

                        string[] fileContent = getFileFactory().getFileContentAsLineArray(getContext().BasicFilePath + traceElement.FileName);

                        if (traceElement.LineNumber > fileContent.Length - 1)
                        {
                            continue;
                        }
                        
                        TraceMainListBoxItem newListBoxItem = null;

                        switch (traceElement.Type)
                        {
                            case TraceType.BYTE:
                                newListBoxItem = new TraceMainListBoxItem(traceElement.FileName, fileContent[traceElement.LineNumber], traceElement.getByte());
                                break;

                            case TraceType.WORD:
                                newListBoxItem = new TraceMainListBoxItem(traceElement.FileName, fileContent[traceElement.LineNumber], traceElement.getWord());
                                break;

                            case TraceType.LONG:
                                newListBoxItem = new TraceMainListBoxItem(traceElement.FileName, fileContent[traceElement.LineNumber], traceElement.getLong());
                                break;

                            case TraceType.ARRAY:
                                newListBoxItem = new TraceMainListBoxItem(traceElement.FileName, fileContent[traceElement.LineNumber], traceElement.getArray());
                                break;
                        }
                    }

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
