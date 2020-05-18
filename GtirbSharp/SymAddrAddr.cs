﻿using GtirbSharp.Helpers;
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

        public long Scale { get => protoObj.Scale; set => protoObj.Scale = value; }
        protected override ulong GetOffset() => (ulong)protoObj.Offset;
        protected override void SetOffset(ulong oldValue, ulong newValue) { protoObj.Offset = (long)newValue; base.SetOffset(oldValue, newValue); }
        public Guid? Symbol1Uuid { get => protoObj.Symbol1Uuid == null ? (Guid?)null : protoObj.Symbol1Uuid.BigEndianByteArrayToGuid(); set => protoObj.Symbol1Uuid = value == null ? null : value.Value.ToBigEndian().ToByteArray(); }
        public Guid? Symbol2Uuid { get => protoObj.Symbol2Uuid == null ? (Guid?)null : protoObj.Symbol2Uuid.BigEndianByteArrayToGuid(); set => protoObj.Symbol2Uuid = value == null ? null : value.Value.ToBigEndian().ToByteArray(); }

        internal SymAddrAddr(proto.SymAddrAddr protoObj)
        {
            this.protoObj = protoObj;

        }
    }
}