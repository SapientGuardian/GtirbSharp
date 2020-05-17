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
        internal readonly proto.ProxyBlock protoProxyBlock;

        internal ProxyBlock(proto.ProxyBlock protoProxyBlock)
        {
            base.SetUuid(protoProxyBlock.Uuid == null? Guid.NewGuid() : protoProxyBlock.Uuid.BigEndianByteArrayToGuid());
            this.protoProxyBlock = protoProxyBlock;
        }
    }
}
#nullable restore