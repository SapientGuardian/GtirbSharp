using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class SerializedDictionaryOffsetDirectives : SerializedDictionaryTObservable<Offset, Directive>
    {
        public SerializedDictionaryOffsetDirectives(Action<byte[]> setData, IEnumerable<KeyValuePair<Offset, ObservableCollection<Directive>>> initialContents) : base(setData, initialContents)
        {           
        }
        public SerializedDictionaryOffsetDirectives(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<Offset, ObservableCollection<Directive>>>())
        {
        }     

        protected override void Save()
        {
            var ms = new MemoryStream(/*Too annoying to calculate in advance*/);
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((long)innerDictionary.Count);
                foreach (var kvp in innerDictionary)
                {
                    bw.Write(kvp.Key.ElementId.ToBigEndianByteArray());
                    bw.Write(kvp.Key.Displacement);

                    bw.Write((byte)kvp.Value.Count);
                    foreach (var directive in kvp.Value)
                    {
                        bw.Write((long)directive.DirectiveString.Length);
                        bw.Write(Encoding.UTF8.GetBytes(directive.DirectiveString).ToArray());
                        bw.Write((long)directive.DirectiveValues.Count());
                        foreach (var directiveValue in directive.DirectiveValues)
                        {
                            bw.Write(directiveValue);
                        }
                        bw.Write(directive.DirectiveUuid.ToBigEndianByteArray());
                    }
                }
            }
            setData(ms.ToArray());
        }
    }
}
