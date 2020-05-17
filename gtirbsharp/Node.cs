#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public class Node
    {
        private static Dictionary<Guid, WeakReference<Node>> uuid_cache = new Dictionary<Guid, WeakReference<Node>>();        

        public Guid UUID { get; private set; }
        internal Node() // It might be ok for something external to extend Node, but for now let's disallow it.
        {
            SetUuid(Guid.NewGuid());
        }

        public static Node? GetByUuid(Guid uuid)
        {
            if (uuid_cache.TryGetValue(uuid, out var weakReference) && weakReference.TryGetTarget(out var target))
            {
                return target;
            }
            return null;
        }

        public WeakReference<Node> SetUuid(Guid uuid)
        {
            UUID = uuid;
            var weakReference = new WeakReference<Node>(this);
            uuid_cache[uuid] = weakReference;
            return weakReference;
        }
    }
}
#nullable restore