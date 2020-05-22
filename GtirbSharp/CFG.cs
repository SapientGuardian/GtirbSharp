#nullable enable
using GtirbSharp.proto;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GtirbSharp.DataStructures;

namespace GtirbSharp
{
    /// <summary>
    /// A CFG represents the interprocedural control flow graph.
    /// </summary>
    public sealed class CFG
    {
        internal readonly Cfg protoCfg;

        /// <summary>
        /// The set of Edges in the control flow graph
        /// </summary>
        public IList<Edge>? Edges { get; private set; }
        
        /// <summary>
        /// The set of vertices in the control flow graph
        /// </summary>
        public IList<Guid> Vertices { get; private set; }
        /// <summary>
        /// Construct a new CFG
        /// </summary>
        public CFG() : this(new Cfg())
        {

        }
        internal CFG(Cfg protoObj)
        {
            this.protoCfg = protoObj;
            this.Edges = new ProtoList<Edge, proto.Edge>(protoObj.Edges, proto => new Edge(proto), edge => edge.protoEdge);
            this.Vertices = new ProtoList<Guid, byte[]>(protoObj.Vertices, GuidFactory.FromBigEndianByteArray, guid => guid.ToBigEndianByteArray());
        }
    }
}
#nullable restore