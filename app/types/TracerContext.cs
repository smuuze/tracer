using Connection;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// The i connection
        /// </summary>
        private ICommonConnectionInterface connection;

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
                if (args != null && args.Length > 1)
                {
                    basicFilePath = args[1];
                }
                else
                {
                    basicFilePath = Directory.GetCurrentDirectory() + "/";
                }
            }
            catch (NotSupportedException e)
            {
                System.Console.WriteLine("TracerContext.TracerContext() - Exception in COntructor : " + e.ToString());
            }
            finally
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

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public ICommonConnectionInterface Connection{
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

        /// <summary>
        /// Debugs the information.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void debugInfo(string msg)
        {
            Debug.DebugFactory.getInstance().debug(Debug.DEBUG_LEVEL.INFO, Debug.DEBUG_MODE.CONSOLE, msg);
        }
    }
}
