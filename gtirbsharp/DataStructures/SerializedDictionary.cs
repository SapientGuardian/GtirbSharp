using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal abstract class SerializedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        protected readonly Dictionary<TKey, TValue> innerDictionary;
        protected readonly Action<byte[]> setData;

        public SerializedDictionary(Action<byte[]> setData, IEnumerable<KeyValuePair<TKey, TValue>> initialContents)
        {
            innerDictionary = new Dictionary<TKey, TValue>();
            foreach (var item in initialContents)
            {
                innerDictionary[item.Key] = item.Value;
            }

            this.setData = setData;
        }

        public virtual TValue this[TKey key]
        {
            get => innerDictionary[key]; set
            {
                innerDictionary[key] = value;
                Save();
            }
        }

        public virtual ICollection<TKey> Keys => ((IDictionary<TKey, TValue>)innerDictionary).Keys;

        public virtual ICollection<TValue> Values => ((IDictionary<TKey, TValue>)innerDictionary).Values;

        public virtual int Count => innerDictionary.Count;

        public virtual bool IsReadOnly => ((IDictionary<TKey, TValue>)innerDictionary).IsReadOnly;

        public virtual void Add(TKey key, TValue value)
        {
            innerDictionary.Add(key, value);
            Save();
        }

        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)innerDictionary).Add(item);
            Save();
        }

        public virtual void Clear()
        {
            innerDictionary.Clear();
            Save();
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)innerDictionary).Contains(item);
        }

        public virtual bool ContainsKey(TKey key)
        {
            return innerDictionary.ContainsKey(key);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)innerDictionary).CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)innerDictionary).GetEnumerator();
        }

        public virtual bool Remove(TKey key)
        {
            if (innerDictionary.Remove(key))
            {
                Save();
                return true;
            }
            return false;
        }

        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (((IDictionary<TKey, TValue>)innerDictionary).Remove(item))
            {
                Save();
                return true;
            }
            return false;
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)innerDictionary).GetEnumerator();
        }

        protected abstract void Save();
    }
}
