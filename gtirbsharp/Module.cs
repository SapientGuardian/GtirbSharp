#nullable enable
using GtirbSharp.DataStructures;
using GtirbSharp.Helpers;
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
        internal readonly proto.Module protoModule;
        private readonly Lazy<IDictionary<Guid, ulong>> alignment;
        private readonly Lazy<IDictionary<Guid, string>> types;
        private readonly Lazy<IDictionary<Guid, Guid>> symbolForwarding;
        private readonly Lazy<IDictionary<ulong, ulong>> padding;
        private readonly Lazy<IDictionary<Offset, string>> comments;
        private readonly Lazy<IDictionary<Guid, ObservableCollection<Guid>>> functionBlocks;
        private readonly Lazy<IDictionary<Guid, ObservableCollection<Guid>>> functionEntries;
        private readonly Lazy<IDictionary<Guid, Guid>> functionNames;

        public string? BinaryPath { get { return protoModule.BinaryPath; } set { protoModule.BinaryPath = value; } }
        public ulong PreferredAddr { get { return protoModule.PreferredAddr; } set { protoModule.PreferredAddr = value; } }
        public long RebaseDelta { get { return protoModule.RebaseDelta; } set { protoModule.RebaseDelta = value; } }
        public proto.FileFormat FileFormat { get { return protoModule.FileFormat; } set { protoModule.FileFormat = value; } }
        public proto.Isa ISA { get { return protoModule.Isa; } set { protoModule.Isa = value; } }
        public string? Name { get { return protoModule.Name; } set { protoModule.Name = value; } }
        public Guid? EntryPointUuid { get { return protoModule.EntryPoint == null? (Guid?)null : protoModule.EntryPoint.BigEndianByteArrayToGuid(); } set { protoModule.EntryPoint = value == null? null : value.Value.ToBigEndian().ToByteArray(); } }
        public IList<Section> Sections { get; private set; }
        public IList<Symbol>? Symbols { get; private set; }
        public IList<ProxyBlock>? ProxyBlocks { get; private set; }
        public CodeBlock? EntryPoint { 
            get
            {
                if (this.EntryPointUuid == null) return null;
                var cb = GetByUuid(this.EntryPointUuid.Value);
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

        internal Module(proto.Module protoModule)
        {
            this.protoModule = protoModule;
            var myUuid = protoModule.Uuid == null? Guid.NewGuid() : protoModule.Uuid.BigEndianByteArrayToGuid();
            base.SetUuid(myUuid);

            Sections = new ProtoList<Section, proto.Section>(protoModule.Sections, proto => new Section(proto), section => section.protoSection);
            Symbols = new ProtoList<Symbol, proto.Symbol>(protoModule.Symbols, proto => new Symbol(proto), symbol => symbol.protoSymbol);
            ProxyBlocks = new ProtoList<ProxyBlock, proto.ProxyBlock>(protoModule.Proxies, proto => new ProxyBlock(proto), proxyBlock => proxyBlock.protoProxyBlock);
            AuxData = new AuxData(protoModule.AuxDatas);

            alignment = new Lazy<IDictionary<Guid, ulong>>(AuxData.Alignment, true);
            types = new Lazy<IDictionary<Guid, string>>(AuxData.Types, true);
            symbolForwarding = new Lazy<IDictionary<Guid, Guid>>(AuxData.SymbolForwarding, true);
            padding = new Lazy<IDictionary<ulong, ulong>>(AuxData.Padding, true);
            comments = new Lazy<IDictionary<Offset, string>>(AuxData.Comments, true);
            functionBlocks = new Lazy<IDictionary<Guid, ObservableCollection<Guid>>>(AuxData.FunctionBlocks, true);
            functionEntries = new Lazy<IDictionary<Guid, ObservableCollection<Guid>>>(AuxData.FunctionEntries, true);
            functionNames = new Lazy<IDictionary<Guid, Guid>>(AuxData.FunctionNames, true);
        }          
    }
}
#nullable restore