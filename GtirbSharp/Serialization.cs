#nullable enable
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GtirbSharp
{
    internal sealed class Serialization
    {
        private readonly BinaryReader bb;

        public int Remaining { get; private set; }
        public Serialization(byte[] bytes)
        {
            bb = new BinaryReader(new MemoryStream(bytes));
            Remaining = bytes.Length;
        }

        public int GetSize()
        {
            // Call this at the beginning of a collection to get the
            // number of elements to expect. At that point, the next
            // byte will have the size, followed by 7 empty bytes.
            byte size = bb.ReadByte();
            // move position along a total of 8
            for (int i = 1; i < 8; i++)
            {
                bb.ReadByte();
            }
            Remaining = Remaining - 8;
            return (int)size;
        }

        public byte GetByte()
        {
            byte retval;
            try
            {
                retval = bb.ReadByte();
            }
            catch
            {
                Remaining = 0;
                return 0;
            }
            Remaining = Remaining - 1;
            return retval;
        }

        public short GetShort()
        {
            short retval;
            try
            {
                retval = bb.ReadInt16();
            }
            catch
            {
                Remaining = 0;
                return 0;
            }
            Remaining = Remaining - 2;
            return retval;
        }

        public int GetInt()
        {
            int retval;
            try
            {
                retval = bb.ReadInt32();
            }
            catch
            {
                Remaining = 0;
                return 0;
            }
            Remaining = Remaining - 4;
            return retval;
        }

        public long GetLong()
        {
            long retval;
            try
            {
                retval = bb.ReadInt64();
            }
            catch
            {
                Remaining = 0;
                return 0;
            }
            Remaining = Remaining - 8;
            return retval;
        }

        public string GetString()
        {
            int length = (int)GetLong();
            byte[] strBytes = new byte[length + 1];
            for (int i = 0; i < length; i++)
            {
                strBytes[i] = bb.ReadByte();
                Remaining = Remaining - 1;
            }
            return Encoding.UTF8.GetString(strBytes);
        }

        public Guid GetUuid()
        {
            Remaining -= 16;
            return GuidFactory.FromBigEndianByteArray(bb.ReadBytes(16));
            //long longA = this.GetByteSwappedLong();
            //long longB = this.GetByteSwappedLong();
            //return new GuidInt64(longA, longB).Guid.ToLittleEndian();
        }

        private long GetByteSwappedLong()
        {
            const int longSize = 8;
            byte[] swappedBytes = new byte[longSize];
            for (int i = 0; i < 8; i++)
            {
                swappedBytes[i] = bb.ReadByte();
            }
            Remaining = Remaining - longSize;
            Array.Reverse(swappedBytes); // It's big endian
            return BitConverter.ToInt64(swappedBytes, 0);
        }
    }
}
#nullable restore