using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp.DataStructures
{
    internal class ProtoDictionary<TKey, TFriendlyValue, TProtoValue> : IDictionary<TKey, TFriendlyValue>
    {
        private readonly Dictionary<TKey, TFriendlyValue> friendlyDict;
        private readonly Dictionary<TKey, TProtoValue> protoDict;
        private readonly Func<TFriendlyValue, TProtoValue> protoFromFriendly;

        public ProtoDictionary(Dictionary<TKey, TProtoValue> protoDict, Func<TProtoValue, TFriendlyValue> friendlyFromProto, Func<TFriendlyValue, TProtoValue> protoFromFriendly)
        {
            friendlyDict = new Dictionary<TKey, TFriendlyValue>();
            foreach (var kvp in protoDict)
            {
                friendlyDict[kvp.Key] = friendlyFromProto(kvp.Value);
            }

            this.protoDict = protoDict;
            this.protoFromFriendly = protoFromFriendly;
        }

        public TFriendlyValue this[TKey key]
        {
            get => friendlyDict[key];
            set
            {
                friendlyDict[key] = value;
                protoDict[key] = protoFromFriendly(value);
            }
        }

        public ICollection<TKey> Keys => ((IDictionary<TKey, TFriendlyValue>)friendlyDict).Keys;

        public ICollection<TFriendlyValue> Values => ((IDictionary<TKey, TFriendlyValue>)friendlyDict).Values;

        public int Count => friendlyDict.Count;

        public bool IsReadOnly => ((IDictionary<TKey, TFriendlyValue>)friendlyDict).IsReadOnly;

        public void Add(TKey key, TFriendlyValue value)
        {
            friendlyDict.Add(key, value);
            protoDict.Add(key, protoFromFriendly(value));
        }

        public void Add(KeyValuePair<TKey, TFriendlyValue> item)
        {
            ((IDictionary<TKey, TFriendlyValue>)friendlyDict).Add(item);
            ((IDictionary<TKey, TProtoValue>)protoDict).Add(new KeyValuePair<TKey, TProtoValue>(item.Key, protoFromFriendly(item.Value)));
        }

        public void Clear()
        {
            friendlyDict.Clear();
            protoDict.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TFriendlyValue> item)
        {
            return ((IDictionary<TKey, TFriendlyValue>)friendlyDict).Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return friendlyDict.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TFriendlyValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TFriendlyValue>)friendlyDict).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TFriendlyValue>> GetEnumerator()
        {
            return ((IDictionary<TKey, TFriendlyValue>)friendlyDict).GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (friendlyDict.Remove(key))
            {
                protoDict.Remove(key);
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TFriendlyValue> item)
        {
            if (((IDictionary<TKey, TFriendlyValue>)friendlyDict).Remove(item))
            {
                ((IDictionary<TKey, TProtoValue>)protoDict).Remove(new KeyValuePair<TKey, TProtoValue>(item.Key, protoFromFriendly(item.Value)));
                return false;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TFriendlyValue value)
        {
            return friendlyDict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<TKey, TFriendlyValue>)friendlyDict).GetEnumerator();
        }
    }
}
