using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.types
{
    class TracerContext
    {

        /// <summary>
        /// The comport
        /// </summary>
        private string comport;

        /// <summary>
        /// The trace active
        /// </summary>
        private bool traceActive;

        /// <summary>
        /// The baudrate
        /// </summary>
        private BAUDRATE baudrate;

        /// <summary>
        /// The basic file path
        /// </summary>
        private string basicFilePath;

        /// <summary>
        /// The instance
        /// </summary>
        private static TracerContext instance = null;

        /// <summary>
        /// The _lock instance
        /// </summary>
        private static object _lockInstance = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceParser" /> class from being created.
        /// </summary>
        private TracerContext()
        {
            traceActive = false;
            basicFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args != null && args.Length != 0)
                {
                    basicFilePath = args[0];
                }
            }
            catch (NotSupportedException e)
            {

            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static TracerContext getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new TracerContext();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [trace active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [trace active]; otherwise, <c>false</c>.
        /// </value>
        public bool TraceActive
        {
            get
            {
                return traceActive;
            }

            set
            {
                traceActive = value;
            }
        }

        /// <summary>
        /// Gets or sets the comport.
        /// </summary>
        /// <value>
        /// The comport.
        /// </value>
        public string Comport
        {
            get
            {
                return comport;
            }

            set
            {
                comport = value;
            }
        }

        /// <summary>
        /// The baudrate
        /// </summary>
        public BAUDRATE Baudrate
        {
            get
            {
                return baudrate;
            }

            set
            {
                baudrate = value;
            }
        }

        /// <summary>
        /// Gets or sets the basic file path.
        /// </summary>
        /// <value>
        /// The basic file path.
        /// </value>
        public string BasicFilePath
        {
            get
            {
                return basicFilePath;
            }

            set
            {
                basicFilePath = value;
            }
        }
    }
}
