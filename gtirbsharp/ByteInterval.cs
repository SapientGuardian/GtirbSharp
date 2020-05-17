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
        internal readonly GtirbSharp.proto.ByteInterval protoByteInterval;

        public ulong? Address
        {
            get => protoByteInterval.HasAddress ? protoByteInterval.Address : (ulong?)null; set
            {
                if (value.HasValue)
                {
                    protoByteInterval.Address = value.Value;
                    protoByteInterval.HasAddress = true;
                }
                else
                {
                    protoByteInterval.Address = 0;
                    protoByteInterval.HasAddress = false;
                }
            }
        }
        public ulong Size
        {
            get
            {
                return protoByteInterval.Size;
            }
            set
            {
                if (value != protoByteInterval.Size || Bytes == null || (int)value != Bytes.Length)
                {
                    protoByteInterval.Size = value;
                    if (Bytes == null)
                    {
                        Bytes = new byte[(int)value];
                    }
                    else if ((int)value < Bytes.Length)
                    {
                        Bytes = Bytes.AsMemory().Slice(0, (int)value).ToArray();
                    }
                    else
                    {
                        var newBytes = new byte[(int)value];
                        Array.Copy(Bytes, newBytes, Bytes.Length);
                        Bytes = newBytes;
                    }
                }

            }
        }

        public byte[]? Bytes
        {
            get
            {
                return protoByteInterval.Contents;
            }
            set
            {
                protoByteInterval.Contents = value;
                protoByteInterval.Size = value == null ? 0 : (ulong)value.Length;
            }
        }
        public IList<Block> Blocks { get; private set; }
        public ICollection<SymbolicExpression> SymbolicExpressions { get; private set; }
        // TODO: Parent section reference

        internal ByteInterval(proto.ByteInterval protoByteInterval)
        {
            this.protoByteInterval = protoByteInterval;
            var myUuid = protoByteInterval.Uuid == null ? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoByteInterval.Uuid);
            base.SetUuid(myUuid);
            this.Blocks = new ProtoList<Block, proto.Block>(protoByteInterval.Blocks, proto => Block.FromProto(proto), block => block.protoBlock);
            this.SymbolicExpressions = new SymbolicExpressionDictionary(protoByteInterval.SymbolicExpressions);
        }
    }
}
#nullable disable