using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A SymAddrConst represents a symbolic operand of the form "Sym + Offset".
    /// </summary>
    public sealed class SymAddrConst : SymbolicExpression
    {
        internal readonly proto.SymAddrConst protoObj;

        protected override ulong GetOffset() => (ulong)protoObj.Offset;
        protected override void SetOffset(ulong oldValue, ulong newValue) { protoObj.Offset = (long)newValue; base.SetOffset(oldValue, newValue); }
        /// <summary>
        /// UUID of a Symbol representing an address
        /// </summary>
        public Guid? SymbolUuid { get => protoObj.SymbolUuid == null? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.SymbolUuid); set => protoObj.SymbolUuid = value == null? null : value.Value.ToBigEndianByteArray(); }

        /// <summary>
        /// Construct a new SymAddrConst
        /// </summary>
        public SymAddrConst() : this(new proto.SymAddrConst())
        {

        }

        internal SymAddrConst(proto.SymAddrConst protoObj)
        {
            this.protoObj = protoObj;
            
        }
    }
}
