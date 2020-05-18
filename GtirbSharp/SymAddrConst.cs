﻿using GtirbSharp.Helpers;
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
        public Guid? SymbolUuid { get => protoObj.SymbolUuid == null? (Guid?)null : protoObj.SymbolUuid.BigEndianByteArrayToGuid(); set => protoObj.SymbolUuid = value == null? null : value.Value.ToBigEndian().ToByteArray(); }

        internal SymAddrConst(proto.SymAddrConst protoObj)
        {
            this.protoObj = protoObj;
            
        }
    }
}