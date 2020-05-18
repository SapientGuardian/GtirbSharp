using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GtirbSharp.Helpers
{
    // https://stackoverflow.com/questions/49372626/convert-guid-to-2-longs-and-2-longs-to-guid-in-c-sharp
    [StructLayout(LayoutKind.Explicit)]
    internal struct GuidInt64
    {
        [FieldOffset(0)]
        private Guid _guid;
        [FieldOffset(0)]
        private long _x;
        [FieldOffset(8)]
        private long _y;

        public Guid Guid => _guid;
        public long X => _x;
        public long Y => _y;

        public GuidInt64(Guid guid)
        {
            _x = _y = 0; // to make the compiler happy
            _guid = guid;
        }
        public GuidInt64(long x, long y)
        {
            _guid = Guid.Empty;// to make the compiler happy
            _x = x;
            _y = y;
        }
    }
}
