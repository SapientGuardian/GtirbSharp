using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal abstract class SerializedDictionaryTObservable<TKey,TValue> : SerializedDictionary<TKey, ObservableCollection<TValue>>
    {
        public SerializedDictionaryTObservable(Action<byte[]> setData, IEnumerable<KeyValuePair<TKey, ObservableCollection<TValue>>> initialContents) : base(setData, initialContents)
        {
            foreach (var item in initialContents)
            {
                item.Value.CollectionChanged += Value_CollectionChanged;
            }
        }
        public SerializedDictionaryTObservable(Action<byte[]> setData) : base(setData, Enumerable.Empty<KeyValuePair<TKey, ObservableCollection<TValue>>>())
        {
        }

        public override ObservableCollection<TValue> this[TKey key]
        {
            get => innerDictionary[key]; set
            {
                innerDictionary[key] = value;
                value.CollectionChanged += Value_CollectionChanged;
                Save();
            }
        }

        public override void Add(TKey key, ObservableCollection<TValue> value)
        {
            innerDictionary.Add(key, value);
            value.CollectionChanged += Value_CollectionChanged;
            Save();
        }

        public override void Add(KeyValuePair<TKey, ObservableCollection<TValue>> item)
        {
            ((IDictionary<TKey, ObservableCollection<TValue>>)innerDictionary).Add(item);
            item.Value.CollectionChanged += Value_CollectionChanged;
            Save();
        }

        public override void Clear()
        {
            foreach (var value in innerDictionary.Values)
            {
                value.CollectionChanged -= Value_CollectionChanged;
            }
            innerDictionary.Clear();
            Save();
        }


        public override bool Remove(TKey key)
        {
            if (innerDictionary.ContainsKey(key))            
            {
                innerDictionary[key].CollectionChanged -= Value_CollectionChanged;
                innerDictionary.Remove(key);
                Save();
                return true;
            }
            return false;
        }

        public override bool Remove(KeyValuePair<TKey, ObservableCollection<TValue>> item)
        {
            if (((IDictionary<TKey, ObservableCollection<TValue>>)innerDictionary).Contains(item))
            {
                innerDictionary[item.Key].CollectionChanged -= Value_CollectionChanged;
                ((IDictionary<TKey, ObservableCollection<TValue>>)innerDictionary).Remove(item);
                Save();
                return true;
            }
            return false;
        }

        private void Value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Save();
        }
 
    }
}
