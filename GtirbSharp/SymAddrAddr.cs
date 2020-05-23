using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A SymAddrAddr represents a symbolic operand of the form "(Sym1 - Sym2) / Scale + Offset".
    /// </summary>
    public sealed class SymAddrAddr : SymbolicExpression
    {
        internal readonly proto.SymAddrAddr protoObj;

        /// <summary>
        /// Constant scale factor
        /// </summary>
        public long Scale { get => protoObj.Scale; set => protoObj.Scale = value; }
        protected override ulong GetOffset() => (ulong)protoObj.Offset;
        protected override void SetOffset(ulong oldValue, ulong newValue) { protoObj.Offset = (long)newValue; base.SetOffset(oldValue, newValue); }
        /// <summary>
        /// Symbol representing the base address
        /// </summary>
        public Guid? Symbol1Uuid { get => protoObj.Symbol1Uuid == null ? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.Symbol1Uuid); set => protoObj.Symbol1Uuid = value == null ? null : value.Value.ToBigEndianByteArray(); }
        /// <summary>
        /// Symbol to subtract from Symbol1
        /// </summary>
        public Guid? Symbol2Uuid { get => protoObj.Symbol2Uuid == null ? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.Symbol2Uuid); set => protoObj.Symbol2Uuid = value == null ? null : value.Value.ToBigEndianByteArray(); }

        /// <summary>
        /// Construct a new SymAddrAddr
        /// </summary>
        public SymAddrAddr() : this(new proto.SymAddrAddr())
        {

        }
        internal SymAddrAddr(proto.SymAddrAddr protoObj)
        {
            this.protoObj = protoObj;

        }
    }
}
