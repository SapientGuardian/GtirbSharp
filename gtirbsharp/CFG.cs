#nullable enable
using GtirbSharp.proto;
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GtirbSharp.DataStructures;

namespace GtirbSharp
{
    public sealed class CFG
    {
        internal readonly Cfg protoCfg;

        public IList<Edge>? EdgeList { get; private set; }
        public IList<byte[]> VerticeList => protoCfg.Vertices;
        internal CFG(Cfg protoCfg)
        {
            this.protoCfg = protoCfg;
            this.EdgeList = new ProtoList<Edge,proto.Edge>(protoCfg.Edges, proto => new Edge(proto), edge => edge.protoEdge);
        }
    }
}
#nullable restore