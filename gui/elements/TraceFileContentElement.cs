using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.gui
{
    class TraceFileContentElement
    {
        /// <summary>
        /// The line
        /// </summary>
        private string line;

        /// <summary>
        /// The line number
        /// </summary>
        private int lineNumber;

        public TraceFileContentElement(int l, string s)
        {
            line = s;
            lineNumber = l;
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
        /// Gets the line.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        public string Line
        {
            get
            {
                return line;
            }
        }
    }
}
