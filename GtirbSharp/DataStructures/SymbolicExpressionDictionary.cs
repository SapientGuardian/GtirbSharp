using GtirbSharp.proto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SymbolicExpressionDictionary : ICollection<SymbolicExpression>
    {
        private readonly Dictionary<ulong, proto.SymbolicExpression> protoDict;

        public SymbolicExpressionDictionary(Dictionary<ulong, proto.SymbolicExpression> protoDict)
        {
            this.protoDict = protoDict;
        }
        public int Count => protoDict.Count;

        public bool IsReadOnly => false;

        public void Add(SymbolicExpression item)
        {
            if (protoDict.ContainsKey(item.Offset))
            {
                throw new ArgumentException("Item with this offset already exists");
            }
            protoDict[item.Offset] = item switch
            {
                SymAddrAddr exp => new proto.SymbolicExpression { AddrAddr = exp.protoObj },
                SymAddrConst exp => new proto.SymbolicExpression { AddrConst = exp.protoObj },
                SymStackConst exp => new proto.SymbolicExpression { StackConst = exp.protoObj },
                _ => throw new ArgumentException("Unexpected Symbolic Expression type"),
            };
            item.OffsetChanged += Item_OffsetChanged;
        }

        private void Item_OffsetChanged(object sender, ulong e)
        {
            var expSender = (SymbolicExpression)sender;
            protoDict.Remove(e);
            Add(expSender);
        }

        public void Clear()
        {
            protoDict.Clear();
        }

        public bool Contains(SymbolicExpression item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(SymbolicExpression[] array, int arrayIndex)
        {
            Enumerable().ToArray().CopyTo(array, arrayIndex);
        }

        public IEnumerator<SymbolicExpression> GetEnumerator()
        {
            return Enumerable().GetEnumerator();
        }

        public bool Remove(SymbolicExpression item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<SymbolicExpression> Enumerable()
        {
            return protoDict.Select(kvp =>
            {
                if (kvp.Value.AddrAddr != null) return new SymAddrAddr(kvp.Value.AddrAddr) { Offset = kvp.Key } as SymbolicExpression;
                if (kvp.Value.AddrConst != null) return new SymAddrConst(kvp.Value.AddrConst) { Offset = kvp.Key } as SymbolicExpression;
                if (kvp.Value.StackConst != null) return new SymStackConst(kvp.Value.StackConst) { Offset = kvp.Key } as SymbolicExpression;
                return null as SymbolicExpression;
            }).Where(s => s != null).Select(s => (s!));
        }
    }
}
