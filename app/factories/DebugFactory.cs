using File_Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.app.types;

namespace Debug
{

    /// <summary>
    /// 
    /// </summary>
    public enum DEBUG_MODE
    {
        DISABLED,
        CONSOLE,
        FILE,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DEBUG_LEVEL
    {
        ALL,
        FATAL,
        ERROR,
        WARNING,
        INFO,
    }

    class DebugFactory
    {
        private DEBUG_LEVEL debugLevel;

        private static DebugFactory instance = null;

        /** be sure to really generate only one singleton */
        private static object _lockInstance = new object();

        private DebugFactory()
        {
            debugLevel = DEBUG_LEVEL.ERROR;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static DebugFactory getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new DebugFactory();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Debugs the specified d string.
        /// </summary>
        /// <param name="dString">The d string.</param>
        public void debug(DEBUG_MODE dMode, string dString)
        {
            debug(DEBUG_LEVEL.INFO, dMode, dString);
        }

        /// <summary>
        /// Debugs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="dMode">The d mode.</param>
        /// <param name="dString">The d string.</param>
        public void debug(DEBUG_LEVEL level, DEBUG_MODE dMode, string dString)
        {
            if (level != debugLevel && debugLevel != DEBUG_LEVEL.ALL)
            {
               // return;
            }

            switch (dMode)
            {
                default:
                case DEBUG_MODE.DISABLED :
                    break;

                case DEBUG_MODE.CONSOLE :
                    System.Console.WriteLine(dString);
                    break;

                case DEBUG_MODE.FILE :
                    writeIntoFile(dString);
                    break;
            }
        }

        /// <summary>
        /// Sets the debug level.
        /// </summary>
        /// <value>
        /// The debug level.
        /// </value>
        public DEBUG_LEVEL DebugLevel
        {
            get
            {
                return debugLevel;
            }

            set
            {
                debugLevel = value;
            }
        }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        /// <returns></returns>
        private UserContext getUserContext()
        {
            return UserContext.getInstance();
        }

        /// <summary>
        /// Gets the file factory.
        /// </summary>
        /// <returns></returns>
        private FileFactory getFileFactory()
        {
            return FileFactory.getInstance();
        }

        /// <summary>
        /// Writes the into file.
        /// </summary>
        private void writeIntoFile(string newLine)
        {
            if (!getFileFactory().fileExists(getUserContext().TracerDebugFile))
            {
                getFileFactory().fileCreate(getUserContext().TracerDebugFile);
            }

            getFileFactory().writeLine(getUserContext().TracerDebugFile, newLine);
        }
    }
}
