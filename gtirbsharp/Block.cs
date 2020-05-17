#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// Block represents a base class for blocks. Symbol objects may have references to any kind of Block.
    /// </summary>
    public abstract class Block : Node
    {
        internal readonly proto.Block protoBlock;

        public ulong Offset { get => protoBlock.Offset; set => protoBlock.Offset = value; }
        public Guid? ByteIntervalUuid { get; set; }
        internal Block(proto.Block protoBlock)
        {
            this.protoBlock = protoBlock;
        }

        internal static Block FromProto(proto.Block protoBlock)
        {
            if (protoBlock.Code != null) return new CodeBlock(protoBlock);
            if (protoBlock.Data != null) return new DataBlock(protoBlock);
            throw new ArgumentException("Block was neither Code nor Data", nameof(protoBlock));
        }
    }
}
#nullable restore