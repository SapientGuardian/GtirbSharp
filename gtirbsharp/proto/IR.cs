// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: IR.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace GtirbSharp.proto
{

    [global::ProtoBuf.ProtoContract(Name = @"IR")]
    internal partial class Ir : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public byte[]? Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"modules")]
        public global::System.Collections.Generic.List<Module> Modules { get; } = new global::System.Collections.Generic.List<Module>();

        [global::ProtoBuf.ProtoMember(5, Name = @"aux_data")]
        [global::ProtoBuf.ProtoMap]
        public global::System.Collections.Generic.Dictionary<string, AuxData> AuxDatas { get; } = new global::System.Collections.Generic.Dictionary<string, AuxData>();

        [global::ProtoBuf.ProtoMember(6, Name = @"version")]
        public uint Version { get; set; }

        [global::ProtoBuf.ProtoMember(7, Name = @"cfg")]
        public Cfg? Cfg { get; set; }

    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
