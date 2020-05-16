using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public class SymStackConst : SymbolicExpression
    {
        internal readonly proto.SymStackConst protoObj;

        protected override ulong GetOffset() => (ulong)protoObj.Offset;
        protected override void SetOffset(ulong oldValue, ulong newValue) { protoObj.Offset = (int)newValue; base.SetOffset(oldValue, newValue); }
        public Guid? SymbolUuid { get => protoObj.SymbolUuid == null? (Guid?)null : Util.BigEndianByteArrayToGuid(protoObj.SymbolUuid) ; set => protoObj.SymbolUuid = value == null? null : value.Value.ToBigEndian().ToByteArray(); }

        internal SymStackConst(proto.SymStackConst protoObj)
        {
            this.protoObj = protoObj;
            
        }
    }
}
