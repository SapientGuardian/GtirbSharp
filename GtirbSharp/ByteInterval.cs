#nullable enable
using GtirbSharp.Interfaces;
using GtirbSharp.DataStructures;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A ByteInterval represents a named section of a binary.
    /// </summary>
    public sealed class ByteInterval : Node
    {
        internal readonly proto.ByteInterval protoObj;
        private Section? section;

        /// <summary>
        /// The Section that owns this ByteInterval
        /// </summary>
        public Section? Section
        {
            get => section;
            set
            {
                if (value != section)
                {
                    section?.ByteIntervals?.Remove(this);
                    section = value;
                    if (value?.NodeContext != null)
                    {
                        NodeContext = value.NodeContext;
                    }
                    if (value?.ByteIntervals != null && !value.ByteIntervals.Contains(this))
                    {
                        value.ByteIntervals.Add(this);
                    }
                }
            }
        }

        /// <summary>
        /// The fixed address of this interval, if present. If this
        /// field is present, it may indicate the original address at which this
        /// interval was located at in memory, or it may indicate that this block's
        /// address is fixed and must not be changed. If this field is not present,
        /// it indicates that the interval is free to be moved around in memory
        /// while preserving program semantics.
        /// </summary>
        public ulong? Address
        {
            get => protoObj.HasAddress ? protoObj.Address : (ulong?)null; set
            {
                if (value.HasValue)
                {
                    protoObj.Address = value.Value;
                    protoObj.HasAddress = true;
                }
                else
                {
                    protoObj.Address = 0;
                    protoObj.HasAddress = false;
                }
            }
        }
        /// <summary>
        /// The size of this interval in bytes
        /// </summary>
        public ulong Size
        {
            get
            {
                return protoObj.Size;
            }
            set
            {
                protoObj.Size = value;
                if (Contents != null && (int)value < Contents.Length)
                {
                    Contents = Contents.AsMemory().Slice(0, (int)value).ToArray();
                }

            }
        }

        /// <summary>
        /// The bytes stored in this interval
        /// </summary>
        public byte[]? Contents
        {
            get
            {
                return protoObj.Contents;
            }
            set
            {
                protoObj.Contents = value;
                if (value != null && (int)Size < value.Length)
                {
                    Size = (ulong)value.Length;
                }
            }
        }
        /// <summary>
        /// The set of all Blocks in this interval
        /// </summary>
        public IList<Block> Blocks { get; private set; }
        /// <summary>
        /// A mapping from an offset in the interval to a SymbolicExpression in the interval
        /// </summary>
        public ICollection<SymbolicExpression> SymbolicExpressions { get; private set; }

        /// <summary>
        /// Construct a new ByteInterval with an owning Section
        /// </summary>
        /// <param name="section"></param>
        public ByteInterval(Section? section) : this(section, section?.NodeContext, new proto.ByteInterval() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        /// <summary>
        /// Construct a new ByteInterval within a specified NodeContext
        /// </summary>
        /// <param name="nodeContext"></param>
        public ByteInterval(INodeContext? nodeContext) : this(null, nodeContext, new proto.ByteInterval() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        internal ByteInterval(Section? section, INodeContext? nodeContext, proto.ByteInterval protoByteInterval)
        {
            this.protoObj = protoByteInterval;
            this.Section = section;
            this.Blocks = new ProtoList<Block, proto.Block>(protoByteInterval.Blocks, proto => Block.FromProto(this, NodeContext, proto), block => block.protoBlock);
            this.SymbolicExpressions = new SymbolicExpressionDictionary(protoByteInterval.SymbolicExpressions);
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);

    }
}
#nullable disable