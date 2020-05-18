using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal sealed class SerializedStringList : IList<string>
    {
        private readonly List<string> innerList;
        private readonly Action<byte[]> setData;

        public SerializedStringList(Action<byte[]> setData) : this(setData, Enumerable.Empty<string>())
        {

        }

        public SerializedStringList(Action<byte[]> setData, IEnumerable<string> initialContents)
        {
            innerList = new List<string>(initialContents);
            this.setData = setData;
        }

        public string this[int index] { get => innerList[index]; set { innerList[index] = value; Save(); } }

        public int Count => innerList.Count;

        public bool IsReadOnly => ((IList<string>)innerList).IsReadOnly;

        public void Add(string item)
        {
            innerList.Add(item);
            Save();
        }

        public void Clear()
        {
            innerList.Clear();
            Save();
        }

        public bool Contains(string item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IList<string>)innerList).GetEnumerator();
        }

        public int IndexOf(string item)
        {
            return innerList.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            innerList.Insert(index, item);
            Save();
        }

        public bool Remove(string item)
        {
            if (innerList.Remove(item))
            {
                Save();
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            innerList.RemoveAt(index);
            Save();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<string>)innerList).GetEnumerator();
        }

        private void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerList.Sum(s => 8/*length as long*/ + s.Length));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerList.Count);
                foreach (var str in innerList)
                {
                    bw.Write((long)str.Length);
                    bw.Write(Encoding.UTF8.GetBytes(str));
                }
            }
            setData(ms.ToArray());
        }
    }
}
