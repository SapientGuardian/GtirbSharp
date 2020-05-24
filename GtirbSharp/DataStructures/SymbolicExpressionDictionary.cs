using GtirbSharp.proto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SymbolicExpressionDictionary : ProtoDictionary<ulong, SymbolicExpression, proto.SymbolicExpression>
    {
        public SymbolicExpressionDictionary(Dictionary<ulong, proto.SymbolicExpression> protoDict) : base(protoDict, SymbolicExpressionFromProto, ProtoFromSymbolicExpression)
        {
        }
                       

        private static SymbolicExpression SymbolicExpressionFromProto(proto.SymbolicExpression proto)
        {
            if (proto.AddrAddr != null) return new SymAddrAddr(proto.AddrAddr);
            if (proto.AddrConst != null) return new SymAddrConst(proto.AddrConst);
            if (proto.StackConst != null) return new SymStackConst(proto.StackConst);
            throw new ArgumentException("Symbolic Expression was not of a known type.", nameof(proto));
        }

        private static proto.SymbolicExpression ProtoFromSymbolicExpression(SymbolicExpression symbolicExpression)
        {
            return symbolicExpression switch
            {
                SymAddrAddr exp => new proto.SymbolicExpression { AddrAddr = exp.protoObj },
                SymAddrConst exp => new proto.SymbolicExpression { AddrConst = exp.protoObj },
                SymStackConst exp => new proto.SymbolicExpression { StackConst = exp.protoObj },
                _ => throw new ArgumentException("Unexpected Symbolic Expression type"),
            };
        }
    
    }
}
