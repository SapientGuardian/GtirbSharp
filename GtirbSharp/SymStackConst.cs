using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A SymStackConst represents a symbolic operand of the form "Sym + Offset", representing an offset from a stack variable.
    /// </summary>
    public class SymStackConst : SymbolicExpression
    {
        internal readonly proto.SymStackConst protoObj;

        /// <summary>
        /// UUID of a Symbol representing a stack variable
        /// </summary>
        public Guid? SymbolUuid { get => protoObj.SymbolUuid == null? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.SymbolUuid); set => protoObj.SymbolUuid = value == null? null : value.Value.ToBigEndianByteArray(); }

        /// <summary>
        /// Constant offset
        /// </summary>
        public int Offset { get => protoObj.Offset; set => protoObj.Offset = value; }

        /// <summary>
        /// Construct a new SymStackConst
        /// </summary>
        public SymStackConst(int offset) : this(new proto.SymStackConst() { Offset = offset })
        {

        }
        internal SymStackConst(proto.SymStackConst protoObj)
        {
            this.protoObj = protoObj;
            
        }
    }
}
