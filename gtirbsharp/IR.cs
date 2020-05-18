#nullable enable
using GtirbSharp.DataStructures;
using GtirbSharp.Helpers;
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
    public sealed class IR : Node
    {
        private proto.Ir protoObj;
        private CFG? cfg;

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

        public IR()
        {
            this.protoObj = new proto.Ir();
            var myUuid = Guid.NewGuid();
            base.SetUuid(myUuid);
            Modules = new ProtoList<Module, proto.Module>(this.protoObj.Modules, proto => new Module(proto), module => module.protoObj);
            protoObj.Cfg = protoObj.Cfg ?? new proto.Cfg();
            Cfg = new CFG(protoObj.Cfg);
            AuxData = new AuxData(protoObj.AuxDatas);
        }

        public static IR LoadFromStream(Stream source)
        {
            var ir = new IR();
            ir.Load(source);
            return ir;
        }

        public void SaveToStream(Stream target)
        {
            Serializer.Serialize(target, protoObj);
        }

        private void Load(Stream source)
        {
            this.protoObj = Serializer.Deserialize<proto.Ir>(source);
            var myUuid = protoObj.Uuid == null ? Guid.NewGuid() : protoObj.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);
            Modules = new ProtoList<Module, proto.Module>(protoObj.Modules, proto => new Module(proto), module => module.protoObj);
            this.Cfg = protoObj.Cfg == null ? null : new CFG(protoObj.Cfg);
            this.AuxData = new AuxData(protoObj.AuxDatas);
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();

        protected override void SetUuidInternal(Guid uuid)
        {
            protoObj.Uuid = uuid.ToBigEndian().ToByteArray();
        }
    }
}
#nullable restore