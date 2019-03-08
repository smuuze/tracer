using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.types
{
    
    static class TraceTypeConstants
    {
        public const byte TRACE_TYPE_UNKNOWN = 0x00;
        public const byte TRACE_TYPE_PASS = 0x01;
        public const byte TRACE_TYPE_BYTE = 0x02;
        public const byte TRACE_TYPE_WORD = 0x03;
        public const byte TRACE_TYPE_LONG = 0x04;
        public const byte TRACE_TYPE_ARRAY = 0x05;
    }

    public enum TraceType
    {
        UNKNOWN = TraceTypeConstants.TRACE_TYPE_UNKNOWN,
        PASS = TraceTypeConstants.TRACE_TYPE_PASS,
        BYTE = TraceTypeConstants.TRACE_TYPE_BYTE,
        WORD = TraceTypeConstants.TRACE_TYPE_WORD,
        LONG = TraceTypeConstants.TRACE_TYPE_LONG,
        ARRAY = TraceTypeConstants.TRACE_TYPE_ARRAY
    }
}
