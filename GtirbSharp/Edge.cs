#nullable enable
using GtirbSharp.proto;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class Edge
    {
        internal readonly proto.Edge protoEdge;

        public Guid? SourceUuid { get { return protoEdge.SourceUuid == null ? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoEdge.SourceUuid); } set { protoEdge.SourceUuid = value?.ToBigEndianByteArray(); } }
        public Guid? TargetUuid { get { return protoEdge.TargetUuid == null? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoEdge.TargetUuid); } set { protoEdge.TargetUuid = value?.ToBigEndianByteArray(); } }
        public bool EdgeLabelConditional { get { return (protoEdge.Label?.Conditional).GetValueOrDefault(); } set { protoEdge.Label!.Conditional = value; } }
        public bool EdgeLabelDirect { get { return (protoEdge.Label?.Direct).GetValueOrDefault(); } set { protoEdge.Label!.Direct = value; } }
        public EdgeType EdgeType { get { return protoEdge.Label!.Type; } set { protoEdge.Label!.Type = value; } }
        public Edge() : this(new proto.Edge())
        {

        }
        internal Edge(proto.Edge protoEdge)
        {
            this.protoEdge = protoEdge;
            protoEdge.Label ??= new EdgeLabel();
        }
    }
}
#nullable restore