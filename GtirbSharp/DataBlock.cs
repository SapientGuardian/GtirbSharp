#nullable enable
using gtirbsharp.Interfaces;
using Nito.Guids;
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

        public DataBlock(INodeContext? nodeContext) : this(null, nodeContext, new proto.Block() { Data = new proto.DataBlock { Uuid = Guid.NewGuid().ToBigEndianByteArray() } })
        {

        }
        public DataBlock(ByteInterval? byteInterval) : this(byteInterval, byteInterval?.NodeContext, new proto.Block() { Data = new proto.DataBlock { Uuid = Guid.NewGuid().ToBigEndianByteArray() } })
        {

        }

        internal DataBlock(ByteInterval? byteInterval, INodeContext? nodeContext, proto.Block block) : base(block)
        {
            this.protoObj = block.Data ?? throw new ArgumentException($"Block was not a {nameof(proto.DataBlock)}", nameof(block));
            this.NodeContext = nodeContext;
            this.ByteInterval = ByteInterval;
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);

    }
}
#nullable restore