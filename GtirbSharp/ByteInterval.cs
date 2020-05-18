#nullable enable
using GtirbSharp.DataStructures;
using GtirbSharp.Helpers;
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
        public ulong Size
        {
            get
            {
                return protoObj.Size;
            }
            set
            {
                if (value != protoObj.Size || Contents == null || (int)value != Contents.Length)
                {
                    protoObj.Size = value;
                    if (Contents == null)
                    {
                        Contents = new byte[(int)value];
                    }
                    else if ((int)value < Contents.Length)
                    {
                        Contents = Contents.AsMemory().Slice(0, (int)value).ToArray();
                    }
                    else
                    {
                        var newBytes = new byte[(int)value];
                        Array.Copy(Contents, newBytes, Contents.Length);
                        Contents = newBytes;
                    }
                }

            }
        }

        public byte[]? Contents
        {
            get
            {
                return protoObj.Contents;
            }
            set
            {
                protoObj.Contents = value;
                protoObj.Size = value == null ? 0 : (ulong)value.Length;
            }
        }
        public IList<Block> Blocks { get; private set; }
        public ICollection<SymbolicExpression> SymbolicExpressions { get; private set; }

        internal ByteInterval(proto.ByteInterval protoByteInterval)
        {
            this.protoObj = protoByteInterval;
            var myUuid = protoByteInterval.Uuid == null ? Guid.NewGuid() : protoByteInterval.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);
            this.Blocks = new ProtoList<Block, proto.Block>(protoByteInterval.Blocks, proto => Block.FromProto(proto), block => block.protoBlock);
            this.SymbolicExpressions = new SymbolicExpressionDictionary(protoByteInterval.SymbolicExpressions);
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();

        protected override void SetUuidInternal(Guid uuid)
        {
            protoObj.Uuid = uuid.ToBigEndian().ToByteArray();
        }
    }
}
#nullable disable