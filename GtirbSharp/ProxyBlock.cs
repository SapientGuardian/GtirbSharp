#nullable enable
using gtirbsharp.Interfaces;
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
        private Module? module;

        public Module? Module
        {
            get => module; set
            {
                module = value;
                if (value?.NodeContext != null)
                {
                    NodeContext = value.NodeContext;
                }
            }
        }

        public ProxyBlock(Module? module) : this(module, module?.NodeContext, new proto.ProxyBlock() { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() }) { }
        public ProxyBlock(INodeContext nodeContext) : this(null, nodeContext, new proto.ProxyBlock() { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() }) { }
        internal ProxyBlock(Module? module, INodeContext? nodeContext, proto.ProxyBlock protoProxyBlock)
        {
            this.protoObj = protoProxyBlock;
            this.Module = module;
            this.NodeContext = nodeContext;
        }
        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();


    }
}
#nullable restore