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
        internal readonly proto.Section protoObj;

        public string Name { get { return protoObj.Name; } set { protoObj.Name = value; } }
        public IList<ByteInterval> ByteIntervals { get; private set; }
        public List<SectionFlag> SectionFlags => protoObj.SectionFlags;
        internal Section(proto.Section protoSection)
        {
            this.protoObj = protoSection;
            var myUuid = protoSection.Uuid == null ? Guid.NewGuid() : protoSection.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);
            this.ByteIntervals = new ProtoList<ByteInterval, proto.ByteInterval>(protoSection.ByteIntervals, proto => new ByteInterval(proto), byteInterval => byteInterval.protoObj);
        }

        protected override Guid GetUuid() => protoObj.Uuid.BigEndianByteArrayToGuid();

        protected override void SetUuidInternal(Guid uuid)
        {
            protoObj.Uuid = uuid.ToBigEndian().ToByteArray();
        }
    }
}
#nullable restore