#nullable enable
using GtirbSharp.proto;
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class Edge
    {
        internal readonly GtirbSharp.proto.Edge protoEdge;

        public Guid? SourceUuid { get { return protoEdge.SourceUuid == null ? (Guid?)null : Util.BigEndianByteArrayToGuid(protoEdge.SourceUuid); } set { protoEdge.SourceUuid = value?.ToBigEndian().ToByteArray(); } }
        public Guid? TargetUuid { get { return protoEdge.TargetUuid == null? (Guid?)null : Util.BigEndianByteArrayToGuid(protoEdge.TargetUuid); } set { protoEdge.TargetUuid = value?.ToBigEndian().ToByteArray(); } }
        public bool EdgeLabelConditional { get { return (protoEdge.Label?.Conditional).GetValueOrDefault(); } set { protoEdge.Label!.Conditional = value; } }
        public bool EdgeLabelDirect { get { return (protoEdge.Label?.Direct).GetValueOrDefault(); } set { protoEdge.Label!.Direct = value; } }
        public EdgeType EdgeType { get { return protoEdge.Label!.Type; } set { protoEdge.Label!.Type = value; } }
        internal Edge(GtirbSharp.proto.Edge protoEdge)
        {
            this.protoEdge = protoEdge;
            protoEdge.Label ??= new EdgeLabel();
        }
    }
}
#nullable restore