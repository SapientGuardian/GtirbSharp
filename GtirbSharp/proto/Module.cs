// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: Module.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace GtirbSharp.proto
{

    [global::ProtoBuf.ProtoContract()]
    internal partial class Module : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public byte[]? Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"binary_path")]
        [global::System.ComponentModel.DefaultValue("")]
        public string? BinaryPath { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3, Name = @"preferred_addr")]
        public ulong PreferredAddr { get; set; }

        [global::ProtoBuf.ProtoMember(4, Name = @"rebase_delta")]
        public long RebaseDelta { get; set; }

        [global::ProtoBuf.ProtoMember(5, Name = @"file_format")]
        public FileFormat FileFormat { get; set; }

        [global::ProtoBuf.ProtoMember(6, Name = @"isa")]
        public Isa Isa { get; set; }

        [global::ProtoBuf.ProtoMember(7, Name = @"name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string? Name { get; set; } = "";

        [global::ProtoBuf.ProtoMember(9, Name = @"symbols")]
        public global::System.Collections.Generic.List<Symbol> Symbols { get; } = new global::System.Collections.Generic.List<Symbol>();

        [global::ProtoBuf.ProtoMember(16, Name = @"proxies")]
        public global::System.Collections.Generic.List<ProxyBlock> Proxies { get; } = new global::System.Collections.Generic.List<ProxyBlock>();

        [global::ProtoBuf.ProtoMember(12, Name = @"sections")]
        public global::System.Collections.Generic.List<Section> Sections { get; } = new global::System.Collections.Generic.List<Section>();

        [global::ProtoBuf.ProtoMember(17, Name = @"aux_data")]
        [global::ProtoBuf.ProtoMap]
        public global::System.Collections.Generic.Dictionary<string, AuxData> AuxDatas { get; } = new global::System.Collections.Generic.Dictionary<string, AuxData>();

        [global::ProtoBuf.ProtoMember(18, Name = @"entry_point")]
        public byte[]? EntryPoint { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    internal enum FileFormat
    {
        [global::ProtoBuf.ProtoEnum(Name = @"Format_Undefined")]
        FormatUndefined = 0,
        [global::ProtoBuf.ProtoEnum(Name = @"COFF")]
        Coff = 1,
        [global::ProtoBuf.ProtoEnum(Name = @"ELF")]
        Elf = 2,
        [global::ProtoBuf.ProtoEnum(Name = @"PE")]
        Pe = 3,
        IdaProDb32 = 4,
        IdaProDb64 = 5,
        [global::ProtoBuf.ProtoEnum(Name = @"XCOFF")]
        Xcoff = 6,
        [global::ProtoBuf.ProtoEnum(Name = @"MACHO")]
        Macho = 7,
        [global::ProtoBuf.ProtoEnum(Name = @"RAW")]
        Raw = 8,
    }

    [global::ProtoBuf.ProtoContract(Name = @"ISA")]
    internal enum Isa
    {
        [global::ProtoBuf.ProtoEnum(Name = @"ISA_Undefined")]
        ISAUndefined = 0,
        [global::ProtoBuf.ProtoEnum(Name = @"IA32")]
        Ia32 = 1,
        [global::ProtoBuf.ProtoEnum(Name = @"PPC32")]
        Ppc32 = 2,
        X64 = 3,
        [global::ProtoBuf.ProtoEnum(Name = @"ARM")]
        Arm = 4,
        ValidButUnsupported = 5,
        [global::ProtoBuf.ProtoEnum(Name = @"PPC64")]
        Ppc64 = 6,
        [global::ProtoBuf.ProtoEnum(Name = @"ARM64")]
        Arm64 = 7,
        [global::ProtoBuf.ProtoEnum(Name = @"MIPS32")]
        Mips32 = 8,
        [global::ProtoBuf.ProtoEnum(Name = @"MIPS64")]
        Mips64 = 9,
    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
