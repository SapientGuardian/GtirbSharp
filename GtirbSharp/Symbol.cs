#nullable enable
using gtirbsharp.Interfaces;
using GtirbSharp.Helpers;
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
            get => module; 
            set
            {
                module = value;
                if (value?.NodeContext != null)
                {
                    NodeContext = value.NodeContext;
                }
            }
        }
        public string Name { get { return protoObj.Name; } set { protoObj.Name = value ?? string.Empty; } }
        public Node? ReferentUuid => ReferentByUuid.HasValue ? NodeContext?.GetByUuid(ReferentByUuid.Value) : null;
        public Guid? ReferentByUuid => protoObj.ReferentUuid == null ? (Guid?)null : protoObj.ReferentUuid.BigEndianByteArrayToGuid();
        public ulong Value => protoObj.Value;
        public bool HasValue => !HasReferent;
        public bool HasReferent => protoObj.ReferentUuid != default;


        public Symbol(Module? module) : this(module, module?.NodeContext, new proto.Symbol() { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() }) { }
        public Symbol(INodeContext nodeContext) : this(null, nodeContext, new proto.Symbol() { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() }) { }
        internal Symbol(Module? module, INodeContext? nodeContext, proto.Symbol protoSymbol)
        {
            this.protoObj = protoSymbol;
            this.Module = module;
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();
    }
}
#nullable restore