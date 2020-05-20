using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryGuidSectionProperties : SerializedDictionaryTObservable<Guid, long>
    {
        public SerializedDictionaryGuidSectionProperties(Action<byte[]> setData, IEnumerable<KeyValuePair<Guid, ObservableCollection<long>>> initialContents) : base(setData, initialContents)
        {
        }
        public SerializedDictionaryGuidSectionProperties(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Guid, ObservableCollection<long>>>())
        {
        }

        protected override void Save()
        {
            var ms = new MemoryStream(8/*length as long*/ + innerDictionary.Count * (16/*guid*/ + 8/*SectionType*/ + 8/*SectionFlags*/));
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key.ToBigEndianByteArray());
                    bw.Write(kvp.Value[0]);
                    bw.Write(kvp.Value[1]);
                }
            }
            setData(ms.ToArray());
        }
    }
}
