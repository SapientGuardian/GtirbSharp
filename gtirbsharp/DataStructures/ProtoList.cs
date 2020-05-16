#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class ProtoList<TFriendly, TProto> : IList<TFriendly>
    {
        private readonly List<TFriendly> innerList;
        private readonly IList<TProto> protoList;
        private readonly Func<TFriendly, TProto> protoFromFriendly;

        public ProtoList(IList<TProto> protoList, Func<TProto, TFriendly> friendlyFromProto, Func<TFriendly, TProto> protoFromFriendly)
        {
            this.innerList = protoList.Select(friendlyFromProto).ToList();
            this.protoList = protoList;
            this.protoFromFriendly = protoFromFriendly;
        }

        public TFriendly this[int index] { get => innerList[index]; set => innerList[index] = value; }

        public int Count => innerList.Count;

        public bool IsReadOnly => ((IList<TFriendly>)innerList).IsReadOnly;

        public void Add(TFriendly item)
        {
            innerList.Add(item);
            protoList.Add(protoFromFriendly(item));
        }

        public void Clear()
        {
            innerList.Clear();
            protoList.Clear();
        }

        public bool Contains(TFriendly item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(TFriendly[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TFriendly> GetEnumerator()
        {
            return ((IList<TFriendly>)innerList).GetEnumerator();
        }

        public int IndexOf(TFriendly item)
        {
            return innerList.IndexOf(item);
        }

        public void Insert(int index, TFriendly item)
        {
            innerList.Insert(index, item);
            protoList.Insert(index, protoFromFriendly(item));
        }

        public bool Remove(TFriendly item)
        {
            if (innerList.Remove(item))
            {
                return protoList.Remove(protoFromFriendly(item));                
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            innerList.RemoveAt(index);
            protoList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<TFriendly>)innerList).GetEnumerator();
        }
    }
}
#nullable restore