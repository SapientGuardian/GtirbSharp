#nullable enable
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A DataBlock represents a data object, possibly symbolic.
    /// </summary>
    public sealed class DataBlock : Block
    {
        private readonly proto.DataBlock protoObj;

        public ulong Size { get { return protoObj.Size; } set { protoObj.Size = value; } }
        internal DataBlock(proto.Block block) : base(block)
        {
            this.protoObj = block.Data ?? throw new ArgumentException($"Block was not a {nameof(proto.DataBlock)}", nameof(block));
            var myUuid = protoObj.Uuid == null ? Guid.NewGuid() : protoObj.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);
        }
    }
}
#nullable restore