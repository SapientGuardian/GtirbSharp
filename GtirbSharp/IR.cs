#nullable enable
using GtirbSharp.Interfaces;
using GtirbSharp.DataStructures;
using Nito.Guids;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// IR describes the internal representation of a software artifact.
    /// </summary>
    public sealed class IR : Node, INodeContext
    {
        private proto.Ir protoObj;
        private CFG? cfg;
        private readonly Dictionary<Guid, WeakReference<Node>> uuidCache = new Dictionary<Guid, WeakReference<Node>>();

        public IList<Module> Modules { get; private set; }
        public CFG? Cfg
        {
            get => cfg;
            set
            {
                cfg = value; protoObj.Cfg = value?.protoCfg;
            }
        }

        public AuxData AuxData { get; private set; }

        public uint ProtoVersion { get => protoObj.Version; set => protoObj.Version = value; }

        public IR() : this(new proto.Ir() { Uuid = Guid.NewGuid().ToBigEndianByteArray() })
        {
            
        }

        private IR(proto.Ir protoObj)
        {
            this.protoObj = protoObj;
            this.NodeContext = this;
            Modules = new ProtoList<Module, proto.Module>(this.protoObj.Modules, proto => new Module(this, this.NodeContext, proto), module => module.protoObj);
            protoObj.Cfg = protoObj.Cfg ?? new proto.Cfg();
            Cfg = new CFG(protoObj.Cfg);
            AuxData = new AuxData(protoObj.AuxDatas);
            
        }

        public static IR LoadFromStream(Stream source)
        {
            var protoObj = Serializer.Deserialize<proto.Ir>(source);
            var ir = new IR(protoObj);
            
            return ir;
        }

        public void SaveToStream(Stream target)
        {
            Serializer.Serialize(target, protoObj);
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);


        void INodeContext.RegisterNode(Node node)
        {
            uuidCache[node.UUID] = new WeakReference<Node>(node);
        }

        void INodeContext.DeregisterNode(Node node)
        {
            uuidCache.Remove(node.UUID);
        }

        public Node? GetByUuid(Guid uuid)
        {
            if (uuidCache.TryGetValue(uuid, out var weakReference) && weakReference.TryGetTarget(out var target))
            {
                return target;
            }
            return null;
        }

        public int NodeCount()
        {
            return uuidCache.Count;
        }
    }
}
#nullable restore