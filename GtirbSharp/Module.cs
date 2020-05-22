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

        /// <summary>
        /// The path to the loadable binary object
        /// represented by this module.An empty string if not specified.
        /// The file represented by this path is indicitave of what file
        /// this Module was initially created from; it is not guaranteed to
        /// currently exist or have the same contents.
        /// </summary>
        public string? BinaryPath { get { return protoObj.BinaryPath; } set { protoObj.BinaryPath = value; } }
        /// <summary>
        /// The preferred loading address of the binary
        /// </summary>
        public ulong PreferredAddr { get { return protoObj.PreferredAddr; } set { protoObj.PreferredAddr = value; } }
        /// <summary>
        /// The rebase delta of the binary
        /// </summary>
        public long RebaseDelta { get { return protoObj.RebaseDelta; } set { protoObj.RebaseDelta = value; } }
        /// <summary>
        /// The file format of the binary
        /// </summary>
        public FileFormat FileFormat { get { return (FileFormat)protoObj.FileFormat; } set { protoObj.FileFormat = (proto.FileFormat)value; } }
        /// <summary>
        /// The ISA of the binary
        /// </summary>
        public Isa ISA { get { return (Isa)protoObj.Isa; } set { protoObj.Isa = (proto.Isa)value; } }
        /// <summary>
        /// The name given to the binary. Some file formats use this
        /// for linking and/or symbol resolution purposes.An empty string if
        /// not specified by the format.
        /// </summary>
        public string? Name { get { return protoObj.Name; } set { protoObj.Name = value; } }
        /// <summary>
        /// The UUID of the CodeBlock representing where control flow of this module begins
        /// </summary>
        public Guid? EntryPointUuid { get { return protoObj.EntryPoint == null ? (Guid?)null : GuidFactory.FromBigEndianByteArray(protoObj.EntryPoint); } set { protoObj.EntryPoint = value == null ? null : value.Value.ToBigEndianByteArray(); } }
        /// <summary>
        /// The set of Sections in the binary
        /// </summary>
        public IList<Section> Sections { get; private set; }
        /// <summary>
        /// The set of Symbols in the binary
        /// </summary>
        public IList<Symbol>? Symbols { get; private set; }
        /// <summary>
        /// The set of ProxyBlocks in the binary
        /// </summary>
        public IList<ProxyBlock>? ProxyBlocks { get; private set; }
        /// <summary>
        /// The CodeBlock representing where control flow of this module begins
        /// </summary>
        public CodeBlock? EntryPoint
        {
            get
            {
                if (this.EntryPointUuid == null) return null;
                var cb = NodeContext?.GetByUuid(this.EntryPointUuid.Value);
                return cb as CodeBlock;
            }
            set
            {
                this.EntryPointUuid = value?.UUID;
            }
        }
        
        /// <summary>
        /// The AuxData attached to the binary
        /// </summary>
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

        /// <summary>
        /// The IR to which this module belongs
        /// </summary>
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

        /// <summary>
        /// Construct a new Module with the specified owning IR
        /// </summary>
        public Module(IR? ir) : this(ir, ir?.NodeContext, new proto.Module() { Uuid = Guid.NewGuid().ToBigEndianByteArray() })
        {

        }

        /// <summary>
        /// Constrruct a new Module with the specified NodeContext
        /// </summary>
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