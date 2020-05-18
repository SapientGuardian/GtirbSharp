#nullable enable
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A ProxyBlock is a placeholder that serves as the endpoint (source or target) of an Edge.
    /// </summary>
    public sealed class ProxyBlock : Node
    {
        internal readonly proto.ProxyBlock protoObj;

        internal ProxyBlock(proto.ProxyBlock protoProxyBlock)
        {
            base.SetUuid(protoProxyBlock.Uuid == null? Guid.NewGuid() : protoProxyBlock.Uuid.BigEndianByteArrayToGuid());
            this.protoObj = protoProxyBlock;
        }
        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();

        protected override void SetUuidInternal(Guid uuid)
        {
            protoObj.Uuid = uuid.ToBigEndian().ToByteArray();
        }
    }
}
#nullable restore