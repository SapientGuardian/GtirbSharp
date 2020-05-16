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
    public sealed class Section : Node
    {
        internal readonly GtirbSharp.proto.Section protoSection;

        public string Name { get { return protoSection.Name; } set { protoSection.Name = value; } }
        public IList<ByteInterval> ByteIntervals { get; private set; }
        public List<SectionFlag> SectionFlags => protoSection.SectionFlags;
        // public ulong ElfSectionType { get; set; }
        // public ulong ElfSectionFlags { get; set; }
        // TODO: Parent Module reference
        internal Section(proto.Section protoSection)
        {
            this.protoSection = protoSection;
            var myUuid = protoSection.Uuid == null? Guid.NewGuid() : Util.BigEndianByteArrayToGuid(protoSection.Uuid);
            base.SetUuid(myUuid);
            this.ByteIntervals = new ProtoList<ByteInterval,GtirbSharp.proto.ByteInterval>(protoSection.ByteIntervals, proto => new ByteInterval(proto), byteInterval => byteInterval.protoByteInterval);
        }
    }
}
#nullable restore