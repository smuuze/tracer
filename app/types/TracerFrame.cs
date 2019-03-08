using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.types
{
    class TracerFrame
    {
        /// <summary>
        /// The trace r_ foote r_ dat a_ byte
        /// </summary>
        public const int FOOTER_DATA_BYTE = (int)'\n';
        
        /// <summary>
        /// The foote r_ dat a_ length
        /// </summary>
        public const int FOOTER_DATA_LENGTH = 1;

        /// <summary>
        /// The trace r_ heade r_ prefi x_ dat a_ byte
        /// </summary>
        public const int HEADER_PREFIX_DATA_BYTE = 0xFA;

        /// <summary>
        /// The trace r_ heade r_ prefi x_ dat a_ length
        /// </summary>
        public const int HEADER_PREFIX_DATA_LENGTH = 3;
    }
}
