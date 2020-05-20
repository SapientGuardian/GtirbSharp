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

        protected override ulong GetOffset() => (ulong)protoObj.Offset;
        protected override void SetOffset(ulong oldValue, ulong newValue) { protoObj.Offset = (int)newValue; base.SetOffset(oldValue, newValue); }
        public Guid? SymbolUuid { get => protoObj.SymbolUuid == null? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.SymbolUuid); set => protoObj.SymbolUuid = value == null? null : value.Value.ToBigEndianByteArray(); }

        internal SymStackConst(proto.SymStackConst protoObj)
        {
            this.protoObj = protoObj;
            
        }
    }
}
