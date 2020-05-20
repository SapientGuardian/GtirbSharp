#nullable enable
using gtirbsharp.Interfaces;
using Nito.Guids;
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
        private Module? module;

        public Module? Module
        {
            get => module; set
            {
                if (value != module)
                {
                    module?.ProxyBlocks?.Remove(this);
                    module = value;
                    if (value?.NodeContext != null)
                    {
                        NodeContext = value.NodeContext;
                    }
                    if (value?.ProxyBlocks != null && !value.ProxyBlocks.Contains(this))
                    {
                        value.ProxyBlocks.Add(this);
                    }
                }
            }
        }

        public ProxyBlock(Module? module) : this(module, module?.NodeContext, new proto.ProxyBlock() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        public ProxyBlock(INodeContext nodeContext) : this(null, nodeContext, new proto.ProxyBlock() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        internal ProxyBlock(Module? module, INodeContext? nodeContext, proto.ProxyBlock protoProxyBlock)
        {
            this.protoObj = protoProxyBlock;
            this.Module = module;
            this.NodeContext = nodeContext;
        }
        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);


    }
}
#nullable restore