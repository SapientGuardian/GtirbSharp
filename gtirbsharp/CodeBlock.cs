#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class CodeBlock : Block
    {
        private readonly proto.CodeBlock protoObj;

        public ulong Size { get { return protoObj.Size; } set { protoObj.Size = value; } }
        public ulong DecodeMode { get { return protoObj.DecodeMode; } set { protoObj.DecodeMode = value; } }
        internal CodeBlock(proto.Block block) : base(block)
        {
            this.protoObj = block.Code ?? throw new ArgumentException($"Block was not a {nameof(proto.CodeBlock)}", nameof(block));
            var myUuid = protoObj.Uuid == null ? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoObj.Uuid);
            base.SetUuid(myUuid);
        }
    }
}
#nullable restore