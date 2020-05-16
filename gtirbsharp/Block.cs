#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// Block represents a base class for blocks. Symbol objects may have references to any kind of Block.
    /// </summary>
    public sealed class Block : Node
    {
        internal readonly GtirbSharp.proto.Block protoBlock;

        public ulong Offset { get; private set; }
        public CodeBlock? CodeBlock { get; private set; }
        public DataBlock? DataBlock { get; private set; }
        internal Block(GtirbSharp.proto.Block protoBlock, Guid byteIntervalUuid)
        {
            this.Offset = protoBlock.Offset;
            this.CodeBlock = protoBlock.Code == null ? null : new CodeBlock(protoBlock.Code, Offset, byteIntervalUuid);
            this.DataBlock = protoBlock.Data == null ? null : new DataBlock(protoBlock.Data, Offset, byteIntervalUuid);
            this.protoBlock = protoBlock;
        }
    }
}
#nullable restore