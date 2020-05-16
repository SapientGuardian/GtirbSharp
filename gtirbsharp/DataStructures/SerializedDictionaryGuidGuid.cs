using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryGuidGuid : SerializedDictionary<Guid, Guid>
    {
        public SerializedDictionaryGuidGuid(Action<byte[]> setData, IEnumerable<KeyValuePair<Guid, Guid>> initialContents) : base(setData, initialContents)
        {
        }

        public SerializedDictionaryGuidGuid(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Guid, Guid>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerDictionary.Count * (16/*guid*/ + 16/*Guid*/));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key.ToBigEndian().ToByteArray());
                    bw.Write(kvp.Value.ToBigEndian().ToByteArray());
                }
            }
            setData(ms.ToArray());
        }
    }
}
