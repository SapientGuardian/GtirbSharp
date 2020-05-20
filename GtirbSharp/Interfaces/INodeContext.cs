using GtirbSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp.Interfaces
{
    /// <summary>
    /// Container for Nodes, providing lookup services
    /// </summary>
    public interface INodeContext
    {        
        internal void RegisterNode(Node node);
        internal void DeregisterNode(Node node);
        /// <summary>
        /// Retrieve a Node by its UUID
        /// </summary>
        /// <param name="uuid">UUID of the node to retrieve</param>
        /// <returns>The Node if found, or null if not</returns>
        public Node? GetByUuid(Guid uuid);
        public int NodeCount();
    }
}
