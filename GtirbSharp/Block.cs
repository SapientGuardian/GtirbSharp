#nullable enable
using GtirbSharp.Interfaces;
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

        /// <summary>
        /// The offset of this block in the owning ByteInterval
        /// </summary>
        public ulong Offset { get => protoBlock.Offset; set => protoBlock.Offset = value; }       

        internal Block(proto.Block protoBlock)
        {
            this.protoBlock = protoBlock;
        }
        
        internal static Block FromProto(ByteInterval? byteInterval, INodeContext? nodeContext, proto.Block protoBlock)
        {
            if (protoBlock.Code != null) return new CodeBlock(byteInterval, nodeContext, protoBlock);
            if (protoBlock.Data != null) return new DataBlock(byteInterval, nodeContext, protoBlock);
            throw new ArgumentException("Block was neither Code nor Data", nameof(protoBlock));
        }
    }
}
#nullable restore