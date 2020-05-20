#nullable enable
using GtirbSharp.Interfaces;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A Symbol maps a name to an object in the IR.
    /// </summary>
    public sealed class Symbol : Node
    {
        internal readonly proto.Symbol protoObj;
        private Module? module;

        public Module? Module
        {
            get => module; set
            {
                if (value != module)
                {
                    module?.Symbols?.Remove(this);
                    module = value;
                    if (value?.NodeContext != null)
                    {
                        NodeContext = value.NodeContext;
                    }
                    if (value?.Symbols != null && !value.Symbols.Contains(this))
                    {
                        value.Symbols.Add(this);
                    }
                }
            }
        }

        public string Name { get { return protoObj.Name; } set { protoObj.Name = value ?? string.Empty; } }
        public Guid? ReferentUuid
        {
            get
            {
                return protoObj.ShouldSerializeReferentUuid() && protoObj.ReferentUuid != null ? GuidFactory.FromBigEndianByteArray(protoObj.ReferentUuid) : (Guid?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    protoObj.ReferentUuid = value.Value.ToBigEndianByteArray();
                }
                else
                {
                    protoObj.ResetReferentUuid();
                }

            }
            
        }        
        public ulong? Value 
        {
            get
            {
                return protoObj.ShouldSerializeValue() ? protoObj.Value : (ulong?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    protoObj.Value = value.Value;
                }
                else
                {
                    protoObj.ResetValue();
                }
            }
        }


        public Symbol(Module? module) : this(module, module?.NodeContext, new proto.Symbol() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        public Symbol(INodeContext nodeContext) : this(null, nodeContext, new proto.Symbol() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        internal Symbol(Module? module, INodeContext? nodeContext, proto.Symbol protoSymbol)
        {
            this.protoObj = protoSymbol;
            this.Module = module;
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);
    }
}
#nullable restore