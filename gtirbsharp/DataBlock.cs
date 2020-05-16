#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class DataBlock : Node
    {
        private readonly GtirbSharp.proto.DataBlock protoDataBlock;

        public ulong Size { get { return protoDataBlock.Size; } set { protoDataBlock.Size = value; } }
        public ulong Offset { get; private set; }
        public Guid ByteIntervalUuid { get; private set; }
        internal DataBlock(GtirbSharp.proto.DataBlock protoDataBlock, ulong offset, Guid byteIntervalUuid)
        {
            this.protoDataBlock = protoDataBlock;
            var myUuid = protoDataBlock.Uuid == null ? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoDataBlock.Uuid);
            base.SetUuid(myUuid);
            this.Offset = offset;
            this.ByteIntervalUuid = byteIntervalUuid;
        }
    }
}
#nullable restore