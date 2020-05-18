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
        internal readonly proto.Symbol protoObj;
        public string Name { get { return protoObj.Name; } set { protoObj.Name = value ?? string.Empty; } }
        public Node? ReferentUuid => ReferentByUuid.HasValue ? GetByUuid(ReferentByUuid.Value) : null;
        public Guid? ReferentByUuid => protoObj.ReferentUuid == null ? (Guid?)null : protoObj.ReferentUuid.BigEndianByteArrayToGuid();
        public ulong Value => protoObj.Value;
        public bool HasValue => !HasReferent;
        public bool HasReferent => protoObj.ReferentUuid != default;
        

        internal Symbol(GtirbSharp.proto.Symbol protoSymbol)
        {
            this.protoObj = protoSymbol;
            if (protoSymbol.Uuid == null)
            {
                base.SetUuid(Guid.NewGuid());
            }
            else
            {
                base.SetUuid(protoSymbol.Uuid.BigEndianByteArrayToGuid());
            }
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();

        protected override void SetUuidInternal(Guid uuid)
        {
            protoObj.Uuid = uuid.ToBigEndian().ToByteArray();
        }
    }
}
#nullable restore