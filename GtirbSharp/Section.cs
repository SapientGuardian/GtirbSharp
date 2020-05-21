#nullable enable
using GtirbSharp.proto;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GtirbSharp.DataStructures;
using GtirbSharp.Interfaces;

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
                if (value != module)
                {
                    module?.Sections?.Remove(this);
                    module = value;
                    if (value?.NodeContext != null)
                    {
                        NodeContext = value.NodeContext;
                    }
                    if (value?.Sections != null && !value.Sections.Contains(this))
                    {
                        value.Sections.Add(this);
                    }
                }
            }
        }

        public string Name { get { return protoObj.Name; } set { protoObj.Name = value; } }
        public IList<ByteInterval> ByteIntervals { get; private set; }
        public IList<SectionFlag> SectionFlags { get; private set; }
        public Section(Module? module) : this(module, module?.NodeContext, new proto.Section() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        public Section(INodeContext nodeContext) : this(null, nodeContext, new proto.Section() { Uuid = Guid.NewGuid().ToBigEndianByteArray() }) { }
        internal Section(Module? module, INodeContext? nodeContext, proto.Section protoSection)
        {
            this.protoObj = protoSection;
            this.Module = module;
            this.ByteIntervals = new ProtoList<ByteInterval, proto.ByteInterval>(protoSection.ByteIntervals, proto => new ByteInterval(this, NodeContext, proto), byteInterval => byteInterval.protoObj);
            this.SectionFlags = new ProtoList<SectionFlag, proto.SectionFlag>(protoObj.SectionFlags, proto => (SectionFlag)proto, friendly => (proto.SectionFlag)friendly);
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);
    }
}
#nullable restore