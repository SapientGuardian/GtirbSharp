#nullable enable
using GtirbSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A Node is any GTIRB object which can be referenced by UUID
    /// </summary>
    public abstract class Node
    {
        private INodeContext? nodeContext;
        /// <summary>
        /// The UUID of this Node
        /// </summary>
        public Guid UUID { get => GetUuid(); }
        /// <summary>
        /// The context in which this node exists
        /// </summary>
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