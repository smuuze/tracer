using Debug;
using File_Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEngineModule;
using Tracer.app.intern;
using Tracer.app.qeue;
using Tracer.app.types;

namespace Tracer.app.task
{
    class TracerCommonTask : CommonTask
    {
        /// <summary>
        /// The debug mode
        /// </summary>
        protected DEBUG_MODE debugMode = DEBUG_MODE.DISABLED;

        public TracerCommonTask(TaskEngine engine, long timeout)
            : base(engine, timeout)
        {

        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        protected TracerContext getContext()
        {
            return TracerContext.getInstance();
        }

        /// <summary>
        /// Gets the trace table.
        /// </summary>
        /// <returns></returns>
        protected TraceRawElementTable getTraceTableRaw()
        {
            return TraceRawElementTable.getInstance();
        }

        /// <summary>
        /// Gets the trace table.
        /// </summary>
        /// <returns></returns>
        protected TraceElementTable getTraceTable()
        {
            return TraceElementTable.getInstance();
        }

        /// <summary>
        /// Gets the file factory.
        /// </summary>
        /// <returns></returns>
        protected FileFactory getFileFactory()
        {
            return FileFactory.getInstance();
        }

        /// <summary>
        /// Invokes the event.
        /// </summary>
        protected void invokeEvent(TracerEventType newEvent)
        {
            TracerInterfaceImplementation.getInstance().invokeEvent(newEvent);
        }

        /// <summary>
        /// Debugs the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        protected void debug(string s)
        {
            DebugFactory.getInstance().debug(debugMode, s);
        }

        protected void debug(DEBUG_LEVEL level, string s)
        {
            DebugFactory.getInstance().debug(level, debugMode, s);
        }

        /// <summary>
        /// Sets the debug enabled.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public void setDebugEnabled(bool enabled)
        {
            debugMode = enabled == true ? DEBUG_MODE.CONSOLE : DEBUG_MODE.DISABLED;
        }
    }
}
