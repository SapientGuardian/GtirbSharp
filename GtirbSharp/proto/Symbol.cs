// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: Symbol.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace GtirbSharp.proto
{

    [global::ProtoBuf.ProtoContract()]
    internal partial class Symbol : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension? __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public byte[]? Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"value")]
        public ulong Value
        {
            get { return __pbn__optional_payload.Is(2) ? __pbn__optional_payload.UInt64 : default; }
            set { __pbn__optional_payload = new global::ProtoBuf.DiscriminatedUnion64Object(2, value); }
        }
        public bool ShouldSerializeValue() => __pbn__optional_payload.Is(2);
        public void ResetValue() => global::ProtoBuf.DiscriminatedUnion64Object.Reset(ref __pbn__optional_payload, 2);

        private global::ProtoBuf.DiscriminatedUnion64Object __pbn__optional_payload;

        [global::ProtoBuf.ProtoMember(5, Name = @"referent_uuid")]
        public byte[]? ReferentUuid
        {
            get { return __pbn__optional_payload.Is(5) ? ((byte[])__pbn__optional_payload.Object) : default; }
            set { __pbn__optional_payload = new global::ProtoBuf.DiscriminatedUnion64Object(5, value); }
        }
        public bool ShouldSerializeReferentUuid() => __pbn__optional_payload.Is(5);
        public void ResetReferentUuid() => global::ProtoBuf.DiscriminatedUnion64Object.Reset(ref __pbn__optional_payload, 5);

        [global::ProtoBuf.ProtoMember(3, Name = @"name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string Name { get; set; } = "";

        [global::ProtoBuf.ProtoMember(6, Name = @"at_end")]
        public bool AtEnd { get; set; }

    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192