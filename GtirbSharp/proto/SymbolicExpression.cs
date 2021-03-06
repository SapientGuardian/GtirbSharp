// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: SymbolicExpression.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace GtirbSharp.proto
{

    [global::ProtoBuf.ProtoContract()]
    internal partial class SymStackConst : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"offset")]
        public int Offset { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"symbol_uuid")]
        public byte[]? SymbolUuid { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    internal partial class SymAddrConst : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"offset")]
        public long Offset { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"symbol_uuid")]
        public byte[]? SymbolUuid { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    internal partial class SymAddrAddr : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"scale")]
        public long Scale { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"offset")]
        public long Offset { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"symbol1_uuid")]
        public byte[]? Symbol1Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(4, Name = @"symbol2_uuid")]
        public byte[]? Symbol2Uuid { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    internal partial class SymbolicExpression : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"stack_const")]
        public SymStackConst? StackConst
        {
            get { return __pbn__value.Is(1) ? ((SymStackConst)__pbn__value.Object) : default; }
            set { __pbn__value = new global::ProtoBuf.DiscriminatedUnionObject(1, value); }
        }
        public bool ShouldSerializeStackConst() => __pbn__value.Is(1);
        public void ResetStackConst() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__value, 1);

        private global::ProtoBuf.DiscriminatedUnionObject __pbn__value;

        [global::ProtoBuf.ProtoMember(2, Name = @"addr_const")]
        public SymAddrConst? AddrConst
        {
            get { return __pbn__value.Is(2) ? ((SymAddrConst)__pbn__value.Object) : default; }
            set { __pbn__value = new global::ProtoBuf.DiscriminatedUnionObject(2, value); }
        }
        public bool ShouldSerializeAddrConst() => __pbn__value.Is(2);
        public void ResetAddrConst() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__value, 2);

        [global::ProtoBuf.ProtoMember(3, Name = @"addr_addr")]
        public SymAddrAddr? AddrAddr
        {
            get { return __pbn__value.Is(3) ? ((SymAddrAddr)__pbn__value.Object) : default; }
            set { __pbn__value = new global::ProtoBuf.DiscriminatedUnionObject(3, value); }
        }
        public bool ShouldSerializeAddrAddr() => __pbn__value.Is(3);
        public void ResetAddrAddr() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__value, 3);

    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
