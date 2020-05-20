using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryGuidGuids : SerializedDictionaryTObservable<Guid, Guid>
    {
        public SerializedDictionaryGuidGuids(Action<byte[]> setData, IEnumerable<KeyValuePair<Guid, ObservableCollection<Guid>>> initialContents) : base(setData, initialContents)
        {
        }
        public SerializedDictionaryGuidGuids(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Guid, ObservableCollection<Guid>>>())
        {
        }     

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerDictionary.Count * (16/*guid*/ + 8/*Right Padded Size Byte*/) + innerDictionary.Values.Sum(c => c.Count) * 16 /*guid*/);
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key.ToBigEndianByteArray());
                    bw.Write((byte)kvp.Value.Count);
                    for (int i = 1; i < 8; i++)
                        bw.Write((byte)0);
                    foreach (var guid in kvp.Value)
                        bw.Write(guid.ToBigEndianByteArray());
                }
            }
            setData(ms.ToArray());
        }
    }
}
