#nullable enable
using GtirbSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public abstract class Node
    {
        private INodeContext? nodeContext;
        public Guid UUID { get => GetUuid(); }
        public INodeContext? NodeContext
        {
            get => nodeContext;
            set
            {
                if (value != nodeContext)
                {
                    nodeContext?.DeregisterNode(this);
                    nodeContext = value;
                    nodeContext?.RegisterNode(this);
                }
            }
        }

        internal Node() // It might be ok for something external to extend Node, but for now let's disallow it.
        {
        }


        protected abstract Guid GetUuid();

    }
}
#nullable restore