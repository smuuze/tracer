using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.app.types;

namespace Tracer.gui
{
    class TraceGuiListItem
    {
        private TraceType type;

        /// <summary>
        /// The code line
        /// </summary>
        private string codeLine;

        /// <summary>
        /// The line number
        /// </summary>
        private int lineNumber;

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
        /// Traces the GUI element.
        /// </summary>
        /// <param name="traceElement">The trace element.</param>
        public TraceGuiListItem(TraceElement traceElement)
        {
            type = traceElement.Type;
            codeLine = traceElement.CodeLine;
            fileName = traceElement.FileName;
            lineNumber = traceElement.LineNumber;

            dataByte = traceElement.getByte();
            dataWord = traceElement.getWord();
            dataLong = traceElement.getLong();
            dataArray = traceElement.getArray();
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
        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public int LineNumber
        {
            get
            {
                return lineNumber;
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
