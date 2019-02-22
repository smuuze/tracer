using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.app.intern;

namespace Tracer.app
{
    public class TracerFactory
    {

        private static TracerFactory instance = null;

        /** be sure to really generate only one singleton */
        private static object _lockInstance = new object();

        private TracerFactory()
        {

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static TracerFactory getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new TracerFactory();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Gets the legic advant4000.
        /// </summary>
        /// <returns></returns>
        public ITracer getInterface()
        {
            return TracerInterfaceImplementation.getInstance();
        }
    }
}
