#nullable enable
using gtirbsharp.Interfaces;
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

        public DataBlock(INodeContext? nodeContext) : this(nodeContext, new proto.Block() { Data = new proto.DataBlock { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() } })
        {

        }
        internal DataBlock(INodeContext? nodeContext, proto.Block block) : base(block)
        {
            this.protoObj = block.Data ?? throw new ArgumentException($"Block was not a {nameof(proto.DataBlock)}", nameof(block));
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();
   
    }
}
#nullable restore