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
    /// <summary>
    /// A Section represents a named section of a binary.
    /// </summary>
    public sealed class Section : Node
    {
        internal readonly proto.Section protoSection;

        public string Name { get { return protoSection.Name; } set { protoSection.Name = value; } }
        public IList<ByteInterval> ByteIntervals { get; private set; }
        public List<SectionFlag> SectionFlags => protoSection.SectionFlags;
        internal Section(proto.Section protoSection)
        {
            this.protoSection = protoSection;
            var myUuid = protoSection.Uuid == null ? Guid.NewGuid() : protoSection.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);
            this.ByteIntervals = new ProtoList<ByteInterval, proto.ByteInterval>(protoSection.ByteIntervals, proto => new ByteInterval(proto), byteInterval => byteInterval.protoByteInterval);
        }
    }
}
#nullable restore