using Nito.Guids;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryGuidULong : SerializedDictionary<Guid, ulong>
    {
        public SerializedDictionaryGuidULong(Action<byte[]> setData, IEnumerable<KeyValuePair<Guid, ulong>> initialContents) : base(setData, initialContents)
        {
        }

        public SerializedDictionaryGuidULong(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Guid, ulong>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as ulong*/ + innerDictionary.Count * (16/*guid*/ + 8/*ulong*/));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((ulong)innerDictionary.Count);
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
