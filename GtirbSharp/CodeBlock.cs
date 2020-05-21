#nullable enable
using GtirbSharp.Interfaces;
using Nito.Guids;
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
        private ByteInterval? byteInterval;

        public ByteInterval? ByteInterval
        {
            get => byteInterval;
            set
            {
                if (value != byteInterval)
                {
                    byteInterval?.Blocks?.Remove(this);
                    byteInterval = value;
                    if (value?.NodeContext != null)
                    {
                        NodeContext = value.NodeContext;
                    }

                    if (value?.Blocks != null && !value.Blocks.Contains(this))
                    {
                        value.Blocks.Add(this);
                    }

                }


            }
        }

        public ulong Size { get { return protoObj.Size; } set { protoObj.Size = value; } }
        public ulong DecodeMode { get { return protoObj.DecodeMode; } set { protoObj.DecodeMode = value; } }
        public CodeBlock(INodeContext? nodeContext) : this(null, nodeContext, new proto.Block() { Code = new proto.CodeBlock { Uuid = Guid.NewGuid().ToBigEndianByteArray() } })
        {

        }
        public CodeBlock(ByteInterval? byteInterval) : this(byteInterval, byteInterval?.NodeContext, new proto.Block() { Code = new proto.CodeBlock { Uuid = Guid.NewGuid().ToBigEndianByteArray() } })
        {

        }

        internal CodeBlock(ByteInterval? byteInterval, INodeContext? nodeContext, proto.Block block) : base(block)
        {
            this.protoObj = block.Code ?? throw new ArgumentException($"Block was not a {nameof(proto.CodeBlock)}", nameof(block));
            this.NodeContext = nodeContext;
            this.ByteInterval = byteInterval;
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);
    }
}
#nullable restore