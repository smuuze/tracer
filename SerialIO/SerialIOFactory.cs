using System;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using System.IO;

using Connection.SerialIO;

namespace Connection.SerialIO
{
    /**
     * <summary>Implements a Serial-Connection by using the SerialIO-Interface</summary>
     * <remarks>Sebastian Lesse, 12.05.2015</remarks>
     **/
    public class SerialIOFactory
    {
        private static SerialIOFactory instance = null;

        private ISerialIOInterface sioInterface = null;

        /** be sure to really generate only one singleton */
        private static object _lockInstance = new object();
        private static object _lockInterface = new object();

        private SerialIOFactory()
        {

        }

        public static SerialIOFactory getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new SerialIOFactory();
                    }
                }
            }

            return instance;
        }

        public ISerialIOInterface getSerialConnection()
        {
            if (sioInterface == null)
            {
                lock (_lockInterface)
                {
                    if (sioInterface == null)
                    {
                        sioInterface = new SerialIO();
                    }
                }
            }

            return sioInterface;
        }
    }
}

