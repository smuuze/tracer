using File_Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEngineModule;
using Tracer.app.types;

namespace Tracer.app.task
{
    class TracerCommonTask : CommonTask
    {
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
    }
}
