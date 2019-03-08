using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.app.types;
using Tracer.src.app;

namespace Tracer.app
{
    public interface ITracer
    {

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void init();
        
        /// <summary>
        /// Gets the update handler.
        /// </summary>
        /// <returns></returns>
        TracerUpdateNotification getUpdateHandler();

        /// <summary>
        /// Sets the connection interface.
        /// </summary>
        /// <param name="iConnection">The i connection.</param>
        void setConnectionInterface(ICommonConnectionInterface iConnection);

        /// <summary>
        /// Gets the next element.
        /// </summary>
        /// <returns></returns>
        TraceElement getNextElement();

        /// <summary>
        /// Starts the tracing.
        /// </summary>
        void startTracing();

        /// <summary>
        /// Stops the tracing.
        /// </summary>
        void stopTracing();
    }
}
