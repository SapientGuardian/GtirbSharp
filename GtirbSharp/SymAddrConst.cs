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

        /// <summary>
        /// UUID of a Symbol representing an address
        /// </summary>
        public Guid? SymbolUuid { get => protoObj.SymbolUuid == null? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.SymbolUuid); set => protoObj.SymbolUuid = value == null? null : value.Value.ToBigEndianByteArray(); }

        /// <summary>
        /// Constant offset
        /// </summary>
        public long Offset { get => protoObj.Offset; set => protoObj.Offset = value; }

        /// <summary>
        /// Construct a new SymAddrConst
        /// </summary>
        public SymAddrConst(long offset) : this(new proto.SymAddrConst() { Offset = offset })
        {

        }

        internal SymAddrConst(proto.SymAddrConst protoObj)
        {
            this.protoObj = protoObj;
            
        }
    }
}
