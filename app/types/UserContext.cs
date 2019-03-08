using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.types
{
    class UserContext
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static UserContext instance = null;

        /// <summary>
        /// The _lock instance
        /// </summary>
        private static object _lockInstance = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceParser" /> class from being created.
        /// </summary>
        private UserContext()
        {

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static UserContext getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new UserContext();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// The tracer user home
        /// </summary>
        private string tracerUserHome = "";

        /// <summary>
        /// The tracer configuration file
        /// </summary>
        private string tracerConfigFile = "";

        /// <summary>
        /// The comport selection
        /// </summary>
        private string comportSelection = "";

        /// <summary>
        /// The tracer debug file
        /// </summary>
        private string tracerDebugFile = "";

        /// <summary>
        /// The baudrate selection
        /// </summary>
        private BAUDRATE baudrateSelection;

        /// <summary>
        /// Gets or sets the tracer user home.
        /// </summary>
        /// <value>
        /// The tracer user home.
        /// </value>
        public string TracerUserHome
        {
            get
            {
                return tracerUserHome;
            }

            set
            {
                tracerUserHome = value;
            }
        }

        /// <summary>
        /// Gets or sets the tracer configuration file.
        /// </summary>
        /// <value>
        /// The tracer configuration file.
        /// </value>
        public string TracerConfigFile
        {
            get
            {
                return tracerConfigFile;
            }

            set
            {
                tracerConfigFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the comport selection.
        /// </summary>
        /// <value>
        /// The comport selection.
        /// </value>
        public string ComportSelection
        {
            get
            {
                return comportSelection;
            }

            set
            {
                comportSelection = value;
            }
        }

        /// <summary>
        /// Gets or sets the baudrate selection.
        /// </summary>
        /// <value>
        /// The baudrate selection.
        /// </value>
        public BAUDRATE BaudrateSelection
        {
            get
            {
                return baudrateSelection;
            }

            set
            {
                baudrateSelection = value;
            }
        }

        /// <summary>
        /// Gets or sets the tracer debug file.
        /// </summary>
        /// <value>
        /// The tracer debug file.
        /// </value>
        public string TracerDebugFile
        {
            get
            {
                return tracerDebugFile;
            }

            set
            {
                tracerDebugFile = value;
            }
        }
    }
}
