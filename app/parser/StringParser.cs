using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.modules
{
    class StringParser
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static StringParser instance = null;

        /// <summary>
        /// The _lock instance
        /// </summary>
        private static object _lockInstance = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceParser"/> class from being created.
        /// </summary>
        private StringParser()
        {

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static StringParser getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new StringParser();
                    }
                }
            }

            return instance;
        }

        public string byte2HexNumber(byte b)
        {
            byte nibbleL = (byte)(b & 0x0F);
            byte nibbleH = (byte)((b >> 4) & 0x0F);
            StringBuilder strB = new StringBuilder();

            if (nibbleH < 10)
            {
                strB.Append((char)('0' + nibbleH));
            }
            else
            {
                strB.Append((char)('A' + (nibbleH - 10)));
            }

            if (nibbleL < 10)
            {
                strB.Append((char)('0' + nibbleL));
            }
            else
            {
                strB.Append((char)('A' + (nibbleL - 10)));
            }

            return strB.ToString();
        }

        /// <summary>
        /// Bytes the array2 hexadecimal string.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <returns></returns>
        public string byteArray2HexString(byte[] byteArray)
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
                    strB.Append((char)('A' + (nibbleH - 10)));
                }

                if (nibbleL < 10)
                {
                    strB.Append((char)('0' + nibbleL));
                }
                else
                {
                    strB.Append((char)('A' + (nibbleL - 10)));
                }
            }

            return strB.ToString();
        }

        /// <summary>
        /// Hexadecimals the string2byte array.
        /// </summary>
        /// <param name="hexString">The hexadecimal string.</param>
        /// <returns></returns>
        public byte[] hexString2byteArray(string hexString)
        {
            int numOfBytes = hexString.Length / 2;
            byte[] byteArray = new byte[numOfBytes];
            int index = 0;
            hexString = hexString.ToUpper();

            for (int i = 0; i < numOfBytes; i++)
            {
                char[] bytes = hexString.ToCharArray(index, 2);
                byte nibbleH = 0;
                byte nibbleL = 0;

                if (bytes[0] >= 'A')
                {
                    nibbleH = (byte)((byte)(bytes[0] - 'A') + 10);
                }
                else
                {
                    nibbleH = (byte)(bytes[0] - '0');
                }

                if (bytes[1] >= 'A')
                {
                    nibbleL = (byte)((byte)(bytes[1] - 'A') + 10);
                }
                else
                {
                    nibbleL = (byte)(bytes[1] - '0');
                }

                byteArray[i] = (byte)((nibbleH << 4) + nibbleL);
                index += 2;
            }

            return byteArray;
        }

        /// <summary>
        /// Hexadecimals the string2 ASCII string.
        /// </summary>
        /// <param name="hex">The hexadecimal.</param>
        /// <returns></returns>
        public string hexString2AsciiString(string hex)
        {
            byte[] byteArray = hexString2byteArray(hex);
            string asciiStr = "";

            for (int i = 0; i < byteArray.Length; i++)
            {
                asciiStr += Convert.ToChar(byteArray[i]);
            }

            return asciiStr;
        }

        /// <summary>
        /// ASCIIs the string2 hexadecimal string.
        /// </summary>
        /// <param name="ascii">The ASCII.</param>
        /// <returns></returns>
        public string asciiString2HexString(string ascii)
        {
            int byteCount = ascii.Length;
            char[] charArray = ascii.ToCharArray();
            byte[] byteArray = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                byteArray[i] = Convert.ToByte(charArray[i]);
            }

            return byteArray2HexString(byteArray);
        }

        /// <summary>
        /// Number2s the hexadecimal string.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="numBytes">The number bytes.</param>
        /// <returns></returns>
        public string number2HexString(ulong number, int numBytes)
        {
            byte[] byteArray = new byte[numBytes];

            for (int i = 0; i < numBytes; i++)
            {
                int shiftWidth = (numBytes - 1 - i) * 8;
                byteArray[i] = (byte)(number >> shiftWidth);
            }

            return byteArray2HexString(byteArray);
        }

        /// <summary>
        /// Hexadecimals the string2 number.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="numBytes">The number bytes.</param>
        /// <returns></returns>
        public ulong hexString2Number(string str, int numBytes)
        {
            byte[] byteArray = hexString2byteArray(str);
            ulong number = 0;
            int start = 0;

            for (int i = 0; i < byteArray.Length; i++, start++)
            {
                if (byteArray[i] != 0x00)
                {
                    break;
                }
            }

            int length = byteArray.Length - start;
            if (length > numBytes)
            {
                start += (length - numBytes);
                length = numBytes;
            }
            for (int i = 0; i < length; i++)
            {
                int shiftWidth = (length - 1 - i) * 8;
                number |= ((ulong)byteArray[start + i] << shiftWidth);
            }


            return number;
        }
    }
}
