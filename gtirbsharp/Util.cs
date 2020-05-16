using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    internal class Util
    {
        public static Guid BigEndianByteArrayToGuid(byte[] bytes)
        {
            if (bytes.Length != 16)
            {
                return Guid.Empty;
            }

            return new Guid(bytes).ToLittleEndian();
        }
    }
}
