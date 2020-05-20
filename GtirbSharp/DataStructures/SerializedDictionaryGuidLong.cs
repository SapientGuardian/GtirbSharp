using Nito.Guids;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryGuidLong : SerializedDictionary<Guid, long>
    {
        public SerializedDictionaryGuidLong(Action<byte[]> setData, IEnumerable<KeyValuePair<Guid, long>> initialContents) : base(setData, initialContents)
        {
        }

        public SerializedDictionaryGuidLong(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Guid, long>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerDictionary.Count * (16/*guid*/ + 8/*long*/));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key.ToBigEndianByteArray());
                    bw.Write(kvp.Value);
                }
            }
            setData(ms.ToArray());
        }
    }
}
