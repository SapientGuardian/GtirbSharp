﻿#nullable enable
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
        private proto.Ir protoIr;
        private CFG? cfg;

        public IList<Module> Modules { get; private set; }
        public CFG? Cfg
        {
            get => cfg;
            set
            {
                cfg = value; protoIr.Cfg = value?.protoCfg;
            }
        }

        public AuxData AuxData { get; private set; }

        public uint ProtoVersion { get => protoIr.Version; set => protoIr.Version = value; }

        public IR()
        {
            this.protoIr = new proto.Ir();
            var myUuid = Guid.NewGuid();
            base.SetUuid(myUuid);
            protoIr.Uuid = myUuid.ToBigEndian().ToByteArray();
            Modules = new ProtoList<Module, proto.Module>(this.protoIr.Modules, proto => new Module(proto), module => module.protoModule);
            protoIr.Cfg = protoIr.Cfg ?? new proto.Cfg();
            Cfg = new CFG(protoIr.Cfg);
            AuxData = new AuxData(protoIr.AuxDatas);
        }

        public static IR LoadFromStream(Stream source)
        {
            var ir = new IR();
            ir.Load(source);
            return ir;
        }

        public void SaveToStream(Stream target)
        {
            Serializer.Serialize(target, protoIr);
        }

        private void Load(Stream source)
        {
            this.protoIr = Serializer.Deserialize<proto.Ir>(source);
            if (protoIr.Uuid != null)
            {
                base.SetUuid(protoIr.Uuid.BigEndianByteArrayToGuid());
            }
            Modules = new ProtoList<Module, proto.Module>(protoIr.Modules, proto => new Module(proto), module => module.protoModule);
            this.Cfg = protoIr.Cfg == null ? null : new CFG(protoIr.Cfg);
            this.AuxData = new AuxData(protoIr.AuxDatas);
        }
    }
}
#nullable restore