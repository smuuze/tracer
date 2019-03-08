using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Text;

using Connection;
using Connection.SerialIO;

namespace Connection.SerialIO
{
    /// <summary>
    /// Implements a Serial-Connection by using the SerialIO-Interface
    /// </summary>
    /// <remarks>Sebastian Lesse, 12.05.2015</remarks>
    public class SerialIO : ISerialIOInterface
    {

        /// <summary>
        /// Maximum size of the RxD FIFO.
        /// </summary>
        protected const int MAX_RxD_QUEUE_SIZE = 10240;

        /// <summary>
        /// Maximum size of the TxD FIFO.
        /// </summary>
        protected const int MAX_TxD_QUEUE_SIZE = 10240;

        /// <summary>
        /// Specifies time time the RxD/TxD Threads are sleeping of no data is available.
        /// </summary>
        protected const int THREAD_SLEEP_TIME_MS = 100;

        /// <summary>
        /// True if this connection is closed, otherwise false.
        /// </summary>
        protected bool closed;

        /// <summary>
        /// Object to communicate with the serial-connection.
        /// </summary>
        protected SerialPort sio;

        /// <summary>
        /// Default constructor to initialize a new instance of the <see cref="SerialIO"/> class.
        /// </summary>
        public SerialIO()
        {
            sio = new SerialPort();
            sio.ReadBufferSize = MAX_RxD_QUEUE_SIZE;
            sio.WriteBufferSize = MAX_TxD_QUEUE_SIZE;

            setClosed(true);
        }

        /// <summary>
        /// Defines this connection as closed or open.
        /// </summary>
        /// <param name="c">Status of this connection, <c>true</c> = closed, <c>false</c> = opened.</param>
        protected void setClosed(bool c)
        {
            closed = c;
        }

        /// <summary>
        /// Sets basic parameters of the Serial-Connection.
        /// </summary>
        /// <param name="c">Number of the COM-Port on which this connection will be opened.</param>
        /// <param name="b">The Baudrate for the speed of this connection.</param>
        /// <param name="d">The number of databits transfered within block.</param>
        /// <param name="p">Defines the type of parity to use with this connection</param>
        /// <param name="s">Defines the number of stopbits to use with this connection</param>
        /// <see cref="ISerialInterface"/>
        public void configure(string c = "COM1", BAUDRATE b = BAUDRATE.B9600, DATABITS d = DATABITS.DATABITS_8, PARITY p = PARITY.NONE, STOPBIT s = STOPBIT.ONE)
        {
            sio.PortName = c;
            sio.BaudRate = (int)b;
            sio.DataBits = (int)d;
            //sio.Handshake = Handshake.XOnXOff; // do not use this, because we don't have a real rs232 interface. its emulated over rs485
            sio.Parity = (Parity) p;

            switch (s)
            {
                default:
                case STOPBIT.ONE: sio.StopBits = StopBits.One; break;
                case STOPBIT.TWO: sio.StopBits = StopBits.Two; break;
            }

            switch (p)
            {
                default:
                case PARITY.NONE: sio.Parity = Parity.None; break;
                case PARITY.EVEN: sio.Parity = Parity.Even; break;
                case PARITY.ODD: sio.Parity = Parity.Odd; break;
            }
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        /// <summary>
        /// Opens the the connection after configuring it.
        /// </summary>
        /// <returns></returns>
        public ERR_CODES open()
        {
            if (!isCLosed())
            {
                return ERR_CODES.ALREADY_OPEN;
            }

            System.Console.WriteLine("SerialIO.open() - Comport: " + sio.PortName + " / Baudrate: " + sio.BaudRate);

            try
            {
                sio.Open();
            }
            catch (InvalidOperationException)
            {
                return ERR_CODES.PORT_BLOCKED;
            }
            catch (UnauthorizedAccessException)
            {
                return ERR_CODES.PORT_NOT_AVAILABLE;
            }
            catch (IOException)
            {
                return ERR_CODES.PORT_NOT_AVAILABLE;
            }
            catch (ArgumentOutOfRangeException)
            {
                return ERR_CODES.WRONG_CONFIGURATION;
            }
            catch (ArgumentException)
            {
                return ERR_CODES.WRONG_CONFIGURATION;
            }

            setClosed(false);

            return ERR_CODES.OK;
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        public ERR_CODES close()
        {
            setClosed(true);

            try
            {
                sio.Close();
            }
            catch (IOException)
            {
                return ERR_CODES.ERROR_ON_CLOSE;
            }

            return ERR_CODES.OK;
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        public void flush()
        {
            if (!sio.IsOpen)
            {
                return;
            }

            try
            {
                if (sio.BytesToRead > 0)
                {
                    sio.DiscardInBuffer();
                }
            }
            catch (IOException)
            {
                System.Console.WriteLine("SerialIO.flush() - InBuffer - IOException");
            }
            catch (InvalidOperationException)
            {
                System.Console.WriteLine("SerialIO.flush() - InBuffer - InvalidOperationException");
            }

            try
            {
                if (sio.BytesToWrite > 0)
                {
                    sio.DiscardOutBuffer();
                }
            }
            catch (IOException)
            {
                System.Console.WriteLine("SerialIO.flush() - OutBuffer - IOException");
            }
            catch (InvalidOperationException)
            {
                System.Console.WriteLine("SerialIO.flush() - OutBuffer - InvalidOperationException");
            }
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        public bool isCLosed()
        {
            return closed;
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        public bool isReadyToSend()
        {
            return !isCLosed() && sio.IsOpen && (sio.BytesToWrite < MAX_TxD_QUEUE_SIZE);
        }

        /// <summary>
        /// Sends several byte's over the serial connection. But only if this connection is configured and open.
        /// </summary>
        /// <param name="b">Data that will be send over the serial connection</param>
        /// <param name="n">The number of bytes to transmit.</param>
        /// <exception cref="SerialConnection.SenderNotReadyException"></exception>
        public ERR_CODES write(byte[] b, int n, int ms)
        {
            if (!sio.IsOpen)
            {
                return ERR_CODES.PORT_CLOSED;
            }

            try
            {
                sio.WriteTimeout = ms;
                sio.Write(b, 0, n);
            }
            catch (IOException)
            {
                System.Console.WriteLine("SerialIO.write() - IOException");
                return ERR_CODES.IO_WRITE_ERROR;
            }
            catch (ArgumentNullException)
            {
                System.Console.WriteLine("SerialIO.write() - ArgumentNullException");
                return ERR_CODES.PORT_NOT_AVAILABLE;
            }
            catch (InvalidOperationException)
            {
                System.Console.WriteLine("SerialIO.write() - InvalidOperationException");
                return ERR_CODES.PORT_NOT_AVAILABLE;
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Console.WriteLine("SerialIO.write() - ArgumentOutOfRangeException");
                return ERR_CODES.PORT_NOT_AVAILABLE;
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("SerialIO.write() - ArgumentException");
                return ERR_CODES.PORT_NOT_AVAILABLE;
            }
            catch (TimeoutException)
            {
                System.Console.WriteLine("SerialIO.write() - TimeoutException");
                return ERR_CODES.WRITE_TIMEOUT;
            }

            return ERR_CODES.OK;
        }

        /// <summary>
        /// </summary>
        /// <param name="n">The number of bytes to be available.</param>
        /// <returns>
        /// True if there is at leas one block available, otherwise false.
        /// </returns>
        public int numBytesAvailable()
        {
            int numberOfBytes = 0;

            try
            {
                numberOfBytes = sio.BytesToRead;
            }
            catch (InvalidOperationException)
            {
                numberOfBytes = 0;
            }

            return numberOfBytes;
        }

        /// <summary>
        /// Reads n bytes from the serial connection.
        /// This method blocks until all all bytes have been read
        /// or a timeout occurs.
        /// </summary>
        /// <param name="n">The number of bytes to read.</param>
        /// <param name="ms">Timeout in milliseconds to wait until bytes received.</param>
        /// <returns>
        /// The requested number of bytes in the received order.
        /// </returns>
        /// <exception cref="NoBytesAvailableException"></exception>
        public byte[] read(int n, int ms)
        {
            byte[] buffer = new byte[n];

            if (!sio.IsOpen)
            {
                return buffer;
            }

            try
            {
                int bytesLeft = n;
                int bytesReceived = 0;
                int readLen = bytesLeft;
                int timeout = 0;

                sio.ReadTimeout = ms;

                do
                {
                    if (!sio.IsOpen)
                    {
                        break;
                    }

                    if (sio.BytesToRead <= 0)
                    {
                        Thread.Sleep(10);
                        timeout += 10;

                        if (timeout < ms)
                        {
                            continue;
                        }
                        else
                        {
                            throw new TimeoutException();
                        }
                    }

                    if (sio.BytesToRead < bytesLeft)
                    {
                        readLen = sio.BytesToRead;
                    }
                    else
                    {
                        readLen = bytesLeft;
                    }

                    bytesReceived += sio.Read(buffer, bytesReceived, readLen);
                    bytesLeft -= bytesReceived;

                } while (bytesLeft > 0);
            }
            catch (IOException)
            {
                System.Console.WriteLine("SerialIO.read() - IOException");
            }
            catch (ArgumentNullException)
            {
                System.Console.WriteLine("SerialIO.read() - ArgumentNullException");
            }
            catch (InvalidOperationException)
            {
                System.Console.WriteLine("SerialIO.read() - InvalidOperationException");
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Console.WriteLine("SerialIO.read() - ArgumentOutOfRangeException");
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("SerialIO.read() - ArgumentException");
            }
            catch (TimeoutException)
            {
                System.Console.WriteLine("SerialIO.read() - TimeoutException");
            }

            return buffer;
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        public string[] getAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        /**
         * <summary>Implementation of the SerialIO Interface</summary>
         * <see cref="ISerialInterface"/>
         **/
        public string[] getAvailableBaudrates()
        {
            string[] options = new string[4];
            options[0] = string.Format("{0}", (int)BAUDRATE.B4800);
            options[1] = string.Format("{0}", (int)BAUDRATE.B9600);
            options[2] = string.Format("{0}", (int)BAUDRATE.B38400);
            options[3] = string.Format("{0}", (int)BAUDRATE.B115200);

            return options;
        }

        /// <summary>
        /// Bytes the array2 hexadecimal string.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <returns></returns>
        private string byteArray2HexString(byte[] byteArray)
        {
            StringBuilder strB = new StringBuilder();

            foreach (byte b in byteArray)
            {
                byte nibbleL = (byte)(b & 0x0F);
                byte nibbleH = (byte)((b >> 4) & 0x0F);

                if (nibbleH < 10)
                {
                    strB.Append((char)('0' + nibbleH));
                }
                else
                {
                    strB.Append((char)('A' + (nibbleH-10)));
                }

                if (nibbleL < 10)
                {
                    strB.Append((char)('0' + nibbleL));
                }
                else
                {
                    strB.Append((char)('A' + (nibbleL-10)));
                }

                strB.Append(" ");
            }

            return strB.ToString();
        }
    }
}

