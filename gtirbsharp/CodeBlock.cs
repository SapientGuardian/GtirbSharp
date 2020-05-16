#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class CodeBlock : Node
    {
        private readonly GtirbSharp.proto.CodeBlock protoCodeBlock;

        public ulong Size { get { return protoCodeBlock.Size; } set { protoCodeBlock.Size = value; } }
        public ulong Offset { get; private set; }
        public ulong DecodeMode { get { return protoCodeBlock.DecodeMode; } set { protoCodeBlock.DecodeMode = value; } }
        public Guid ByteIntervalUuid { get; private set; }
        internal CodeBlock(GtirbSharp.proto.CodeBlock protoCodeBlock, ulong offset, Guid byteIntervalUuid)
        {
            this.protoCodeBlock = protoCodeBlock;
            var myUuid = protoCodeBlock.Uuid == null? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoCodeBlock.Uuid);
            base.SetUuid(myUuid);
            this.Offset = offset;
            this.ByteIntervalUuid = byteIntervalUuid;
        }
    }
}
#nullable restore