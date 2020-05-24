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
    public sealed class CodeBlock : Block, IByteBlock
    {
        private readonly proto.CodeBlock protoObj;
        private ByteInterval? byteInterval;

        /// <summary>
        /// The ByteInterval to which this CodeBlock belongs
        /// </summary>
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

        /// <summary>
        /// The size of the block in bytes
        /// </summary>
        public ulong Size { get { return protoObj.Size; } set { protoObj.Size = value; } }
        /// <summary>
        /// The decode mode of the block,
        /// used in some ISAs to differentiate between sub-ISAs
        /// (e.g.differentiating blocks written in ARM and Thumb).
        /// </summary>
        public ulong DecodeMode { get { return protoObj.DecodeMode; } set { protoObj.DecodeMode = value; } }
        /// <summary>
        /// Construct a new CodeBlock in the specified NodeContext
        /// </summary>
        /// <param name="nodeContext"></param>
        public CodeBlock(INodeContext nodeContext) : this(null, nodeContext, new proto.Block() { Code = new proto.CodeBlock { Uuid = Guid.NewGuid().ToBigEndianByteArray() } })
        {

        }

        /// <summary>
        /// Construct a new CodeBlock owned by the specified ByteInterval
        /// </summary>
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