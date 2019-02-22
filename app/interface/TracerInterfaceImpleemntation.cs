using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.intern
{
    class TracerInterfaceImplementation : ITracer
    {
        private static TracerInterfaceImplementation instance = null;

        /** be sure to really generate only one singleton */
        private static object _lockInstance = new object();

        private TracerInterfaceImplementation()
        {

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
    }
}
