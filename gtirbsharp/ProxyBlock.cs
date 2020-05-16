#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class ProxyBlock : Node
    {
        internal readonly GtirbSharp.proto.ProxyBlock protoProxyBlock;

        internal ProxyBlock(GtirbSharp.proto.ProxyBlock protoProxyBlock)
        {
            base.SetUuid(protoProxyBlock.Uuid == null? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoProxyBlock.Uuid));
            this.protoProxyBlock = protoProxyBlock;
        }
    }
}
#nullable restore