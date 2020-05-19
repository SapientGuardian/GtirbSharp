#nullable enable
using gtirbsharp.Interfaces;
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A CodeBlock represents a basic block in the binary.
    /// </summary>
    public sealed class CodeBlock : Block
    {
        private readonly proto.CodeBlock protoObj;

        public ulong Size { get { return protoObj.Size; } set { protoObj.Size = value; } }
        public ulong DecodeMode { get { return protoObj.DecodeMode; } set { protoObj.DecodeMode = value; } }
        public CodeBlock(INodeContext? nodeContext) : this(nodeContext, new proto.Block() { Code = new proto.CodeBlock { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() } } )
        {

        }
        internal CodeBlock(INodeContext? nodeContext, proto.Block block) : base(block)
        {
            this.protoObj = block.Code ?? throw new ArgumentException($"Block was not a {nameof(proto.CodeBlock)}", nameof(block));
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();    
    }
}
#nullable restore