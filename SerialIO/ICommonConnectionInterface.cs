using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{

    /**
     * <summary>Values for the Baudrate that are supported by this Serial-Connection</summary>
     **/
    public enum BAUDRATE
    {
        B4800 = 4800,
        B9600 = 9600,
        B38400 = 38400,
        B115200 = 115200,
        B230400 = 230400
    };

    /**
     * <summary>Number of databits for one block that are supported by this Serial-Connection.</summary>
     **/
    public enum DATABITS
    {
        DATABITS_1 = 1,
        DATABITS_2,
        DATABITS_3,
        DATABITS_4,
        DATABITS_5,
        DATABITS_6,
        DATABITS_7,
        DATABITS_8,
        DATABITS_9,
        DATABITS_10,
        DATABITS_11,
        DATABITS_12,
        DATABITS_13
    };

    /**
     * <summary>Number of stopbits for one block that are supported by this Serial-Connection.</summary>
     **/
    public enum STOPBIT
    {
        ONE = 1,
        TWO = 2
    }

    /**
     * <summary>Modes for the parity bits supported by this Serial-Connection.</summary>
     **/
    public enum PARITY
    {
        EVEN = 2,
        ODD = 1,
        NONE = 0
    };

    /**
     * <summary>Error-Codes that can be produces while opening a connection</summary>
     **/
    public enum ERR_CODES
    {
        OK,
        ALREADY_OPEN,
        PORT_BLOCKED,
        PORT_CLOSED,
        PORT_NOT_AVAILABLE,
        WRONG_CONFIGURATION,
        ERROR_ON_CLOSE,
        WRITE_TIMEOUT,
        READ_TIMEOUT,
        IO_WRITE_ERROR,
        IO_READ_ERROR
    }

    /**
     * Interface Standard Exception for an empty input
     **/
    public class NoBytesAvailableException : Exception { };

    /**
     * Interface Standard Exception for a buys sender
     **/
    public class SenderNotReadyException : Exception { };

    public interface ICommonConnectionInterface
    {
        /**
         * <summary>Sets basic parameters of the Serial-Connection.</summary>
         * <param name="c">Number of the COM-Port on which this connection will be opened.</param>
         * <param name="b">The Baudrate for the speed of this connection.</param>
         * <param name="d">The number of databits transfered within block.</param>
         * <param name="p">Defines the type of parity to use with this connection</param>
         * <param name="s">Defines the number of stopbits to use with this connection</param>
         **/
        void configure(string c, BAUDRATE b, DATABITS d, PARITY p, STOPBIT s);

        /**
         * <summary>Opens the the connection after configuring it.</summary>
         **/
        ERR_CODES open();

        /// <summary>
        /// Closes this connection in every case.
        /// </summary>
        /// <returns>The ERROR_CODE of this operation.</returns>
        ERR_CODES close();

        /**
         * <summary>Forces this connection to be cleared. Actual processed operations will be finisehd. Pending data will be lost.</summar>
         **/
        void flush();

        /**
         * <summary>Checks if this connection is closed or not.</summary>
         * <returns>True if this connection is closed, otherwise false</returns>
         **/
        bool isCLosed();

        /**
         * <summary>Gives feedback if this connection is able to send data.</summary>
         * <returns>True if this connection can send one more block, otherweise false</returns>
         **/
        bool isReadyToSend();

        /// <summary>
        /// Sends several byte's over the serial connection. But only if this connection is configured and open.
        /// </summary>
        /// <param name="b">Data that will be send over the serial connection</param>
        /// <param name="n">The number of bytes to transmit.</param>
        /// <param name="ms">Timeout in milliseconds to transmit the buffered bytes</param>
        /// <returns></returns>
        ERR_CODES write(byte[] b, int n, int ms);

        /// <summary>
        /// Tells the host if there is at minimum n bytes available..
        /// </summary>
        /// 
        /// <returns></returns>
        int numBytesAvailable();

        /// <summary>
        /// Reads n bytes from the serial connection.
        /// </summary>
        /// <param name="n">The number of bytes to read.</param>
        /// <param name="ms">Timeout in milliseconds to wait until bytes received.</param>
        /// <returns>
        /// The requested number of bytes in the received order.
        /// </returns>
        /// <exception cref="NoBytesAvailableException"></exception>
        byte[] read(int n, int ms);

        /**
         * <summary>Returns a list with all available Serial COM-Ports on the host-system</summary>
         **/
        string[] getAvailablePorts();

        /**
         * <summary>Returns a list with all available baudrate values supported by this serial connection</summary>
         **/
        string[] getAvailableBaudrates();
    }
}
