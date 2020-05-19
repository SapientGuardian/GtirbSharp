#nullable enable
using GtirbSharp.proto;
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GtirbSharp.DataStructures;
using gtirbsharp.Interfaces;

namespace GtirbSharp
{
    /// <summary>
    /// A Section represents a named section of a binary.
    /// </summary>
    public sealed class Section : Node
    {
        internal readonly proto.Section protoObj;
        private Module? module;

        public Module? Module
        {
            get => module; set
            {
                module = value;
                if (value?.NodeContext != null)
                {
                    NodeContext = value.NodeContext;
                }
            }
        }

        public string Name { get { return protoObj.Name; } set { protoObj.Name = value; } }
        public IList<ByteInterval> ByteIntervals { get; private set; }
        public List<SectionFlag> SectionFlags => protoObj.SectionFlags;
        public Section(Module? module) : this(module, module?.NodeContext, new proto.Section() { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() }) { }
        public Section(INodeContext nodeContext) : this(null, nodeContext, new proto.Section() { Uuid = Guid.NewGuid().ToBigEndian().ToByteArray() }) { }
        internal Section(Module? module, INodeContext? nodeContext, proto.Section protoSection)
        {
            this.protoObj = protoSection;
            this.Module = module;
            this.ByteIntervals = new ProtoList<ByteInterval, proto.ByteInterval>(protoSection.ByteIntervals, proto => new ByteInterval(this, NodeContext, proto), byteInterval => byteInterval.protoObj);
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();
    }
}
#nullable restore