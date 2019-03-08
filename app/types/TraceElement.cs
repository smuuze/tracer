using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.app.types;

namespace Tracer.app.types
{
    public class TraceElement
    {
        /// <summary>
        /// The type
        /// </summary>
        private TraceType type;

        /// <summary>
        /// The data
        /// </summary>
        private byte[] data;

        /// <summary>
        /// The line number
        /// </summary>
        private UInt16 lineNumber;

        /// <summary>
        /// The file name
        /// </summary>
        private string fileName;

        /// <summary>
        /// The code line
        /// </summary>
        private string codeLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceElement"/> class.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="line">The line.</param>
        /// <param name="d">The d.</param>
        /// <param name="f">The f.</param>
        public TraceElement()
        {
            type = TraceType.UNKNOWN;
            lineNumber = 0;
            data = new byte[0];
            fileName = "";
        }

        public TraceElement(TraceType t, UInt16 line, byte[] d, string f)
        {
            type = t;
            lineNumber = line;
            data = d;
            fileName = f;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TraceType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <returns></returns>
        public byte getByte()
        {
            if (type != TraceType.BYTE)
            {
                return 0;
            }

            return data[0];
        }

        /// <summary>
        /// Gets the word.
        /// </summary>
        /// <returns></returns>
        public UInt16 getWord()
        {
            if (type != TraceType.WORD)
            {
                return 0;
            }

            UInt16 word = 0;
            word += (UInt16)data[0];
            word <<= 8;
            word += (UInt16)data[1];

            return word;
        }

        /// <summary>
        /// Gets the long.
        /// </summary>
        /// <returns></returns>
        public UInt32 getLong()
        {
            if (type != TraceType.LONG)
            {
                return 0;
            }

            UInt32 integer = 0;
            integer += (UInt32)data[0];
            integer <<= 8;
            integer += (UInt32)data[1];
            integer <<= 8;
            integer += (UInt32)data[2];
            integer <<= 8;
            integer += (UInt32)data[3];

            return integer;
        }

        /// <summary>
        /// Gets the array.
        /// </summary>
        /// <returns></returns>
        public byte[] getArray()
        {
            return data;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        public string CodeLine
        {
            get
            {
                return codeLine;
            }

            set
            {
                codeLine = value;
            }
        }

        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public UInt16 LineNumber
        {
            get
            {
                return lineNumber;
            }
        }
    }
}
