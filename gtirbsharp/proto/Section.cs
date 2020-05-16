// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: Section.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace GtirbSharp.proto
{

    [global::ProtoBuf.ProtoContract()]
    internal partial class Section : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public byte[]? Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string Name { get; set; } = "";

        [global::ProtoBuf.ProtoMember(5, Name = @"byte_intervals")]
        public global::System.Collections.Generic.List<ByteInterval> ByteIntervals { get; } = new global::System.Collections.Generic.List<ByteInterval>();

        [global::ProtoBuf.ProtoMember(6, Name = @"section_flags", IsPacked = true)]
        public global::System.Collections.Generic.List<SectionFlag> SectionFlags { get; } = new global::System.Collections.Generic.List<SectionFlag>();

    }

    [global::ProtoBuf.ProtoContract()]
    public enum SectionFlag
    {
        [global::ProtoBuf.ProtoEnum(Name = @"Section_Undefined")]
        SectionUndefined = 0,
        Readable = 1,
        Writable = 2,
        Executable = 3,
        Loaded = 4,
        Initialized = 5,
        ThreadLocal = 6,
    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
