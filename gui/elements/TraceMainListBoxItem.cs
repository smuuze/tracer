using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.app.types;

namespace Tracer.src.gui
{
    class TraceMainListBoxItem
    {
        private TraceType type;

        /// <summary>
        /// The code line
        /// </summary>
        private string codeLine;

        /// <summary>
        /// The file name
        /// </summary>
        private string fileName;

        /// <summary>
        /// The data byte
        /// </summary>
        private byte dataByte;

        /// <summary>
        /// The data word
        /// </summary>
        private UInt16 dataWord;

        /// <summary>
        /// The data long
        /// </summary>
        private UInt32 dataLong;

        /// <summary>
        /// The data array
        /// </summary>
        private byte[] dataArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceMainListBoxItem"/> class.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        public TraceMainListBoxItem(string f, string c, byte d)
        {
            type = TraceType.BYTE;
            codeLine = c;
            fileName = f;

            dataByte = d;
            dataWord = 0;
            dataLong = 0;
            dataArray = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceMainListBoxItem"/> class.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        public TraceMainListBoxItem(string f, string c, UInt16 d)
        {
            type = TraceType.WORD;
            codeLine = c;
            fileName = f;

            dataByte = 0;
            dataWord = d;
            dataLong = 0;
            dataArray = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceMainListBoxItem"/> class.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        public TraceMainListBoxItem(string f, string c, UInt32 d)
        {
            type = TraceType.LONG;
            codeLine = c;
            fileName = f;

            dataByte = 0;
            dataWord = 0;
            dataLong = d;
            dataArray = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceMainListBoxItem"/> class.
        /// </summary>
        /// <param name="f">The f.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        public TraceMainListBoxItem(string f, string c, byte[] d)
        {
            type = TraceType.ARRAY;
            codeLine = c;
            fileName = f;

            dataByte = 0;
            dataWord = 0;
            dataLong = 0;
            dataArray = d;
        }

        /// <summary>
        /// Gets or sets the type.
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
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        private string FileName
        {
            get
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets or sets the code line.
        /// </summary>
        /// <value>
        /// The code line.
        /// </value>
        public string CodeLine
        {
            get
            {
                return codeLine;
            }
        }

        /// <summary>
        /// Gets or sets the data byte.
        /// </summary>
        /// <value>
        /// The data byte.
        /// </value>
        public byte DataByte
        {
            get
            {
                return dataByte;
            }
        }

        /// <summary>
        /// Gets or sets the data word.
        /// </summary>
        /// <value>
        /// The data word.
        /// </value>
        public UInt16 DataWord
        {
            get
            {
                return dataWord;
            }
        }

        /// <summary>
        /// Gets or sets the data long.
        /// </summary>
        /// <value>
        /// The data long.
        /// </value>
        public UInt32 DataLong
        {
            get
            {
                return dataLong;
            }
        }

        /// <summary>
        /// Gets or sets the data array.
        /// </summary>
        /// <value>
        /// The data array.
        /// </value>
        public byte[] DataArray
        {
            get
            {
                return dataArray;
            }
        }
    }
}
