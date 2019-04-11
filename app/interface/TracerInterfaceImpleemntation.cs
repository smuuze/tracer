using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEngineModule;
using Tracer.app.qeue;
using Tracer.app.task;
using Tracer.app.types;
using Tracer.src.app;

namespace Tracer.app.intern
{
    class TracerInterfaceImplementation : ITracer
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static TracerInterfaceImplementation instance = null;

        /** be sure to really generate only one singleton */
        private static object _lockInstance = new object();

        /// <summary>
        /// The engine
        /// </summary>
        private TaskEngine engine;

        /// <summary>
        /// The update handler
        /// </summary>
        protected TracerUpdateNotification updateHandler;

        private TracerInterfaceImplementation()
        {
            engine = new TaskEngine();
            updateHandler = new TracerUpdateNotification();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void init()
        {
            engine.register(new TracerDataCatcherTask(engine, TracerDataCatcherTask.INTERVAL_TIMEOUT_MS));
            engine.register(new TracerFileLoadTask(engine, TracerFileLoadTask.INTERVAL_TIMEOUT_MS));
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static TracerInterfaceImplementation getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new TracerInterfaceImplementation();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Gets the update handler.
        /// </summary>
        /// <returns></returns>
        public TracerUpdateNotification getUpdateHandler()
        {
            return updateHandler;
        }

        public void setConnectionInterface(ICommonConnectionInterface iConnection)
        {
            TracerContext.getInstance().Connection = iConnection;
        }

        /// <summary>
        /// Gets the next element.
        /// </summary>
        /// <returns></returns>
        public TraceElement getNextElement()
        {
            return TraceElementTable.getInstance().getNext();
        }

        /// <summary>
        /// Starts the tracing.
        /// </summary>
        public void startTracing()
        {
            TracerContext.getInstance().TraceActive = true;
            engine.start(50);
        }

        /// <summary>
        /// Stops the tracing.
        /// </summary>
        public void stopTracing()
        {
            TracerContext.getInstance().TraceActive = false;
            engine.stop();
        }

        /// <summary>
        /// Invokes the event.
        /// </summary>
        public void invokeEvent(TracerEventType newEvent)
        {
            if (updateHandler.Event_UpdateNotification != null)
            {
                updateHandler.Event_UpdateNotification.Invoke();
            }
        }

        /// <summary>
        /// Debugs the specified debug MSG.
        /// </summary>
        /// <param name="debugMsg">The debug MSG.</param>
        private void debug(string debugMsg)
        {
            Debug.DebugFactory.getInstance().debug(Debug.DEBUG_MODE.CONSOLE, debugMsg);
        }
    }
}
