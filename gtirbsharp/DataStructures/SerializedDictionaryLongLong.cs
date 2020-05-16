using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryLongLong : SerializedDictionary<long, long>
    {
        public SerializedDictionaryLongLong(Action<byte[]> setData, IEnumerable<KeyValuePair<long, long>> initialContents) : base(setData, initialContents)
        {
        }

        public SerializedDictionaryLongLong(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<long, long>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerDictionary.Count * (8/*long*/ + 8/*long*/));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key);
                    bw.Write(kvp.Value);
                }
            }
            setData(ms.ToArray());
        }
    }
}
