// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: ByteInterval.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace GtirbSharp.proto
{

    [global::ProtoBuf.ProtoContract()]
    internal partial class Block : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"offset")]
        public ulong Offset { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"code")]
        public CodeBlock? Code
        {
            get { return __pbn__value.Is(2) ? ((CodeBlock)__pbn__value.Object) : default; }
            set { __pbn__value = new global::ProtoBuf.DiscriminatedUnionObject(2, value); }
        }
        public bool ShouldSerializeCode() => __pbn__value.Is(2);
        public void ResetCode() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__value, 2);

        private global::ProtoBuf.DiscriminatedUnionObject __pbn__value;

        [global::ProtoBuf.ProtoMember(3, Name = @"data")]
        public DataBlock? Data
        {
            get { return __pbn__value.Is(3) ? ((DataBlock)__pbn__value.Object) : default; }
            set { __pbn__value = new global::ProtoBuf.DiscriminatedUnionObject(3, value); }
        }
        public bool ShouldSerializeData() => __pbn__value.Is(3);
        public void ResetData() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__value, 3);

    }

    [global::ProtoBuf.ProtoContract()]
    internal partial class ByteInterval : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public byte[]? Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"blocks")]
        public global::System.Collections.Generic.List<Block> Blocks { get; } = new global::System.Collections.Generic.List<Block>();

        [global::ProtoBuf.ProtoMember(3, Name = @"symbolic_expressions")]
        [global::ProtoBuf.ProtoMap]
        public global::System.Collections.Generic.Dictionary<ulong, SymbolicExpression> SymbolicExpressions { get; } = new global::System.Collections.Generic.Dictionary<ulong, SymbolicExpression>();

        [global::ProtoBuf.ProtoMember(4, Name = @"has_address")]
        public bool HasAddress { get; set; }

        [global::ProtoBuf.ProtoMember(5, Name = @"address")]
        public ulong Address { get; set; }

        [global::ProtoBuf.ProtoMember(6, Name = @"size")]
        public ulong Size { get; set; }

        [global::ProtoBuf.ProtoMember(7, Name = @"contents")]
        public byte[]? Contents { get; set; }

    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192