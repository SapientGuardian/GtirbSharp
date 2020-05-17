#nullable enable
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
        internal readonly GtirbSharp.proto.Symbol protoSymbol;
        public string Name { get { return protoSymbol.Name; } set { protoSymbol.Name = value ?? string.Empty; } }
        public Node? ReferentUuid => ReferentByUuid.HasValue ? GetByUuid(ReferentByUuid.Value) : null;
        public Guid? ReferentByUuid => protoSymbol.ReferentUuid == null ? (Guid?)null : protoSymbol.ReferentUuid.BigEndianByteArrayToGuid();
        public ulong Value => protoSymbol.Value;
        public bool HasValue => !HasReferent;
        public bool HasReferent => protoSymbol.ReferentUuid != default;
        

        internal Symbol(GtirbSharp.proto.Symbol protoSymbol)
        {
            this.protoSymbol = protoSymbol;
            if (protoSymbol.Uuid == null)
            {
                base.SetUuid(Guid.NewGuid());
            }
            else
            {
                base.SetUuid(protoSymbol.Uuid.BigEndianByteArrayToGuid());
            }
        }
    }
}
#nullable restore