using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryULongULong : SerializedDictionary<ulong, ulong>
    {
        public SerializedDictionaryULongULong(Action<byte[]> setData, IEnumerable<KeyValuePair<ulong, ulong>> initialContents) : base(setData, initialContents)
        {
        }

        public SerializedDictionaryULongULong(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<ulong, ulong>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as ulong*/ + innerDictionary.Count * (8/*ulong*/ + 8/*ulong*/));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((ulong)innerDictionary.Count);
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
