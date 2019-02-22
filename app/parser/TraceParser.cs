using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracer.app.types;

namespace Tracer.app.modules
{
    class TraceParser
    {
        /// <summary>
        /// The ra w_ dat a_ inde x_ o f_ trac e_ type
        /// </summary>
        private const int RAW_DATA_INDEX_OF_TRACE_TYPE = 3;

        /// <summary>
        /// The ra w_ dat a_ inde x_ o f_ trac e_ data
        /// </summary>
        private const int RAW_DATA_INDEX_OF_TRACE_DATA = 4;

        /// <summary>
        /// The instance
        /// </summary>
        private static TraceParser instance = null;

        /// <summary>
        /// The _lock instance
        /// </summary>
        private static object _lockInstance = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceParser"/> class from being created.
        /// </summary>
        private TraceParser()
        {

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static TraceParser getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new TraceParser();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Parses the trace data.
        /// </summary>
        /// <param name="traceRawData">The trace raw data.</param>
        /// <returns></returns>
        public TraceElement parseTraceData(byte[] traceRawData)
        {
            if (traceRawData == null)
            {
                return new TraceElement(TraceType.UNKNOWN, (UInt16)0, new byte[0], "");
            }

            if (traceRawData.Length < 2)
            {
                return new TraceElement(TraceType.UNKNOWN, (UInt16)0, new byte[0], "");
            }

            int byteCount = getByteCount(traceRawData);

            if (traceRawData.Length - 2 != byteCount)
            {
                return new TraceElement(TraceType.UNKNOWN, (UInt16)0, new byte[0], "");
            }

            TraceType type = getTraceType(traceRawData[RAW_DATA_INDEX_OF_TRACE_TYPE]);
            byte[] data = getTraceData(type, traceRawData);
            UInt16 lineNumber = getLineNumber(type, traceRawData);
            string fileName = getFileName(type, traceRawData);

            return new TraceElement(type, lineNumber, data, fileName);
        }

        /// <summary>
        /// Gets the byte count.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns></returns>
        private int getByteCount(byte[] rawData)
        {
            int byteCount = ((int)rawData[1] << 8) + ((int)rawData[0]);
            return byteCount;
        }

        /// <summary>
        /// Gets the type of the trace.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns></returns>
        private TraceType getTraceType(byte rawData)
        {
            switch (rawData)
            {
                default:
                    return TraceType.UNKNOWN;

                case TraceTypeConstants.TRACE_TYPE_PASS:
                    return TraceType.PASS;

                case TraceTypeConstants.TRACE_TYPE_BYTE:
                    return TraceType.BYTE;

                case TraceTypeConstants.TRACE_TYPE_WORD:
                    return TraceType.WORD;

                case TraceTypeConstants.TRACE_TYPE_LONG:
                    return TraceType.LONG;

                case TraceTypeConstants.TRACE_TYPE_ARRAY:
                    return TraceType.ARRAY;
            }
        }

        /// <summary>
        /// Gets the trace data.
        /// </summary>
        /// <param name="traceType">Type of the trace.</param>
        /// <param name="traceRawData">The trace raw data.</param>
        /// <returns></returns>
        private byte[] getTraceData(TraceType traceType, byte[] traceRawData)
        {
            switch (traceType)
            {
                default:
                    return new byte[0];

                case TraceType.PASS:
                    return new byte[0];

                case TraceType.BYTE:

                    if (traceRawData.Length - 3 < 1)
                    {
                        return new byte[0];
                    }

                    return new byte[] { traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA] };

                case TraceType.WORD:

                    if (traceRawData.Length - 3 < 2)
                    {
                        return new byte[0];
                    }

                    return new byte[] { traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 1], traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA] };

                case TraceType.LONG:

                    if (traceRawData.Length - 3 < 4)
                    {
                        return new byte[0];
                    }

                    return new byte[] { traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 3], 
                                        traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 2], 
                                        traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 1], 
                                        traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA] };

                case TraceType.ARRAY:

                    if (traceRawData.Length - 3 < 1)
                    {
                        return new byte[0];
                    }

                    int length = traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA];

                    if (length == 0)
                    {
                        return new byte[0];
                    }

                    byte[] newByte = new byte[length];

                    return newByte;
            }
        }

        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <param name="traceType">Type of the trace.</param>
        /// <param name="traceRawData">The trace raw data.</param>
        /// <returns></returns>
        private UInt16 getLineNumber(TraceType traceType, byte[] traceRawData)
        {
            UInt16 lineNumber = 0;

            switch (traceType)
            {
                default:
                    return 0;

                case TraceType.PASS:

                    lineNumber = (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_TYPE + 2];
                    lineNumber <<= 8;
                    lineNumber += (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_TYPE + 1];
                    break;

                case TraceType.BYTE:

                    lineNumber = (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 3];
                    lineNumber <<= 8;
                    lineNumber += (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 2];
                    break;

                case TraceType.WORD:

                    lineNumber = (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 4];
                    lineNumber <<= 8;
                    lineNumber += (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 3];
                    break;

                case TraceType.LONG:

                    lineNumber = (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 6];
                    lineNumber <<= 8;
                    lineNumber += (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 7];
                    break;

                case TraceType.ARRAY:

                    byte length = traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 1];

                    lineNumber = (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 1 + length + 1];
                    lineNumber <<= 8;
                    lineNumber += (UInt16)traceRawData[RAW_DATA_INDEX_OF_TRACE_DATA + 1 + length];
                    break;
            }

            return lineNumber;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="traceType">Type of the trace.</param>
        /// <param name="traceRawData">The trace raw data.</param>
        /// <returns></returns>
        private string getFileName(TraceType traceType, byte[] traceRawData)
        {
            int offset = 0;
            int length = 0;

            switch (traceType)
            {
                default:
                    return "";

                case TraceType.PASS:

                    offset = RAW_DATA_INDEX_OF_TRACE_TYPE + 2;
                    break;

                case TraceType.BYTE:

                    offset = RAW_DATA_INDEX_OF_TRACE_DATA + 1;
                    break;

                case TraceType.WORD:

                    offset = RAW_DATA_INDEX_OF_TRACE_DATA + 2;
                    break;

                case TraceType.LONG:

                    offset = RAW_DATA_INDEX_OF_TRACE_DATA + 4;
                    break;

                case TraceType.ARRAY:

                   // byte length = traceRawData[RAW_DATA_INDEX_OF_TRACE_TYPE + 1];

                    offset = RAW_DATA_INDEX_OF_TRACE_TYPE + 2;
                    break;
            }

            length = traceRawData.Length - offset;
            byte[] fileNameRaw = new byte[length];

            Array.Copy(traceRawData, offset, fileNameRaw, 0, length);
            return StringParser.getInstance().hexString2AsciiString(StringParser.getInstance().byteArray2HexString(fileNameRaw));
        }
    }
}
