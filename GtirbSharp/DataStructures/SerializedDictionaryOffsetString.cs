using Nito.Guids;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryOffsetString : SerializedDictionary<Offset, string>
    {
        public SerializedDictionaryOffsetString(Action<byte[]> setData, IEnumerable<KeyValuePair<Offset, string>> initialContents) : base(setData, initialContents)
        {
        }

        public SerializedDictionaryOffsetString(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Offset, string>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerDictionary.Count * (16/*guid*/ + 8 /*offset displacement as long*/ + 8 /*length as long*/) + innerDictionary.Values.Sum(s => s.Length));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key.ElementId.ToBigEndianByteArray());
                    bw.Write(kvp.Key.Displacement);
                    bw.Write((long)kvp.Value.Length);
                    bw.Write(Encoding.UTF8.GetBytes(kvp.Value));
                }
            }
            setData(ms.ToArray());
        }
    }
}
