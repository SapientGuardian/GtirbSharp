﻿#nullable enable
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
        internal CodeBlock(proto.Block block) : base(block)
        {
            this.protoObj = block.Code ?? throw new ArgumentException($"Block was not a {nameof(proto.CodeBlock)}", nameof(block));
            var myUuid = protoObj.Uuid == null ? Guid.NewGuid() : protoObj.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();

        protected override void SetUuidInternal(Guid uuid)
        {
            protoObj.Uuid = uuid.ToBigEndian().ToByteArray();
        }
    }
}
#nullable restore