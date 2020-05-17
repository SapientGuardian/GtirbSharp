#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class DataBlock : Block
    {
        private readonly proto.DataBlock protoObj;

        public ulong Size { get { return protoObj.Size; } set { protoObj.Size = value; } }
        internal DataBlock(proto.Block block) : base(block)
        {
            this.protoObj = block.Data ?? throw new ArgumentException($"Block was not a {nameof(proto.DataBlock)}", nameof(block));
            var myUuid = protoObj.Uuid == null ? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoObj.Uuid);
            base.SetUuid(myUuid);
        }
    }
}
#nullable restore