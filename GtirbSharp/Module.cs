#nullable enable
using GtirbSharp.Interfaces;
using GtirbSharp.DataStructures;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A Module represents a single binary (library or executable).
    /// </summary>
    public sealed class Module : Node
    {
        internal readonly proto.Module protoObj;
        private readonly Lazy<IDictionary<Guid, ulong>> alignment;
        private readonly Lazy<IDictionary<Guid, string>> types;
        private readonly Lazy<IDictionary<Guid, Guid>> symbolForwarding;
        private readonly Lazy<IDictionary<ulong, ulong>> padding;
        private readonly Lazy<IDictionary<Offset, string>> comments;
        private readonly Lazy<IDictionary<Guid, ObservableCollection<Guid>>> functionBlocks;
        private readonly Lazy<IDictionary<Guid, ObservableCollection<Guid>>> functionEntries;
        private readonly Lazy<IDictionary<Guid, Guid>> functionNames;
        private IR? ir;

        public string? BinaryPath { get { return protoObj.BinaryPath; } set { protoObj.BinaryPath = value; } }
        public ulong PreferredAddr { get { return protoObj.PreferredAddr; } set { protoObj.PreferredAddr = value; } }
        public long RebaseDelta { get { return protoObj.RebaseDelta; } set { protoObj.RebaseDelta = value; } }
        public FileFormat FileFormat { get { return (FileFormat)protoObj.FileFormat; } set { protoObj.FileFormat = (proto.FileFormat)value; } }
        public Isa ISA { get { return (Isa)protoObj.Isa; } set { protoObj.Isa = (proto.Isa)value; } }
        public string? Name { get { return protoObj.Name; } set { protoObj.Name = value; } }
        public Guid? EntryPointUuid { get { return protoObj.EntryPoint == null ? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.EntryPoint); } set { protoObj.EntryPoint = value == null ? null : value.Value.ToBigEndianByteArray(); } }
        public IList<Section> Sections { get; private set; }
        public IList<Symbol>? Symbols { get; private set; }
        public IList<ProxyBlock>? ProxyBlocks { get; private set; }
        public CodeBlock? EntryPoint
        {
            get
            {
                if (this.EntryPointUuid == null) return null;
                var cb = NodeContext?.GetByUuid(this.EntryPointUuid.Value);
                return cb as CodeBlock;
            }
        }
        public AuxData AuxData { get; private set; }

        // AuxData schemas


        /// <summary>
        /// A map from the id of a Block, DataObject, or Section to its alignment requirements
        /// </summary>
        public IDictionary<Guid, ulong> Alignment => alignment.Value;

        /// <summary>
        /// A map from the id of a DataObject to the type of the data, expressed as a string containing a C++ type specifier.
        /// </summary>
        public IDictionary<Guid, string> Types => types.Value;

        /// <summary>
        /// A map from the id of a symbol to the id of a symbol to which it is forwarded
        /// </summary>
        public IDictionary<Guid, Guid> SymbolForwarding => symbolForwarding.Value;

        /// <summary>
        /// A map from an address to a length of padding which has been inserted at the address
        /// </summary>
        public IDictionary<ulong, ulong> Padding => padding.Value;

        /// <summary>
        /// A map from an offset to a comment string relevant to it
        /// </summary>
        public IDictionary<Offset, string> Comments => comments.Value;

        /// <summary>
        /// A map from the id of a function to the set of ids of blocks in the function
        /// </summary>
        public IDictionary<Guid, ObservableCollection<Guid>> FunctionBlocks => functionBlocks.Value;

        /// <summary>
        /// A map from the id of a function to the set of ids of entry points for the function
        /// </summary>
        public IDictionary<Guid, ObservableCollection<Guid>>? FunctionEntries => functionEntries.Value;

        /// <summary>
        /// A map from the id of a function to the id of a symbol whose name field contains the name of the function.
        /// </summary>
        public IDictionary<Guid, Guid> FunctionNames => functionNames.Value;

        public IR? IR { 
            get => ir; 
            set 
            {
                if (value != ir)
                {
                    ir?.Modules.Remove(this);
                    ir = value;
                    NodeContext = value?.NodeContext;
                    if (value?.Modules != null && !value.Modules.Contains(this))
                    {
                        value.Modules.Add(this);
                    }
                }
            } 
        }

        public Module(IR? ir) : this(ir, ir?.NodeContext, new proto.Module() { Uuid = Guid.NewGuid().ToBigEndianByteArray() })
        {

        }
        public Module(INodeContext? nodeContext) : this(null, nodeContext, new proto.Module() { Uuid = Guid.NewGuid().ToBigEndianByteArray() })
        {

        }
        internal Module(IR? ir, INodeContext? nodeContext, proto.Module protoModule)
        {
            this.protoObj = protoModule;
            this.IR = ir;

            Sections = new ProtoList<Section, proto.Section>(protoModule.Sections, proto => new Section(this, NodeContext, proto), section => section.protoObj);
            Symbols = new ProtoList<Symbol, proto.Symbol>(protoModule.Symbols, proto => new Symbol(this, NodeContext, proto), symbol => symbol.protoObj);
            ProxyBlocks = new ProtoList<ProxyBlock, proto.ProxyBlock>(protoModule.Proxies, proto => new ProxyBlock(this, NodeContext, proto), proxyBlock => proxyBlock.protoObj);
            AuxData = new AuxData(protoModule.AuxDatas);

            alignment = new Lazy<IDictionary<Guid, ulong>>(AuxData.Alignment, true);
            types = new Lazy<IDictionary<Guid, string>>(AuxData.Types, true);
            symbolForwarding = new Lazy<IDictionary<Guid, Guid>>(AuxData.SymbolForwarding, true);
            padding = new Lazy<IDictionary<ulong, ulong>>(AuxData.Padding, true);
            comments = new Lazy<IDictionary<Offset, string>>(AuxData.Comments, true);
            functionBlocks = new Lazy<IDictionary<Guid, ObservableCollection<Guid>>>(AuxData.FunctionBlocks, true);
            functionEntries = new Lazy<IDictionary<Guid, ObservableCollection<Guid>>>(AuxData.FunctionEntries, true);
            functionNames = new Lazy<IDictionary<Guid, Guid>>(AuxData.FunctionNames, true);
            this.NodeContext = nodeContext;
        }

        protected override Guid GetUuid() => GuidFactory.FromBigEndianByteArray(protoObj.Uuid);
    }
}
#nullable restore