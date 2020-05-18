using GtirbSharp.DataStructures;
using GtirbSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GtirbSharp
{
    internal static class AuxDataExtensions
    {
        /// <summary>
        /// A map from the id of a Block, DataObject, or Section to its alignment requirements
        /// </summary>
        public static IDictionary<Guid, ulong> Alignment(this AuxData auxData)
        {
            const string typeName = "alignment";
            return SerializedDictionaryGuidUlong(auxData, typeName);
        }

        /// <summary>
        /// A map from the id of a DataObject to the type of the data, expressed as a string containing a C++ type specifier.
        /// </summary>
        public static IDictionary<Guid, string> Types(this AuxData auxData)
        {
            const string typeName = "types";
            return SerializedDictionaryGuidString(auxData, typeName);

        }

        /// <summary>
        /// A map from the id of a symbol to the id of a symbol to which it is forwarded
        /// </summary>
        public static IDictionary<Guid, Guid> SymbolForwarding(this AuxData auxData)
        {
            const string typeName = "symbolForwarding";
            return SerializedDictionaryGuidGuid(auxData, typeName);
        }


        /// <summary>
        /// A map from an address to a length of padding which has been inserted at the address
        /// </summary>
        public static IDictionary<ulong, ulong> Padding(this AuxData auxData)
        {
            const string typeName = "padding";
            return SerializedDictionaryUlongUlong(auxData, typeName);
        }

        /// <summary>
        /// A map from an offset to a comment string relevant to it
        /// </summary>
        public static IDictionary<Offset, string> Comments(this AuxData auxData)
        {
            const string typeName = "comments";
            return SerializedDictionaryOffsetString(auxData, typeName);
        }

        /// <summary>
        /// A map from the id of a function to the set of ids of blocks in the function
        /// </summary>
        public static IDictionary<Guid, ObservableCollection<Guid>> FunctionBlocks(this AuxData auxData)
        {
            const string typeName = "functionBlocks";
            return SerializedDictionaryGuidGuids(auxData, typeName);
        }

        /// <summary>
        /// A map from the id of a function to the set of ids of entry points for the function
        /// </summary>
        public static IDictionary<Guid, ObservableCollection<Guid>> FunctionEntries(this AuxData auxData)
        {
            const string typeName = "functionEntries";
            return SerializedDictionaryGuidGuids(auxData, typeName);
        }

        /// <summary>
        /// A map from the id of a function to the id of a symbol whose name field contains the name of the function.
        /// </summary>
        public static IDictionary<Guid, Guid> FunctionNames(this AuxData auxData)
        {
            const string typeName = "functionNames";
            return SerializedDictionaryGuidGuid(auxData, typeName);
        }
        public static IDictionary<Guid, ulong> SCCs(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "SCCs";
            return SerializedDictionaryGuidUlong(auxData, typeName);
        }

        public static IDictionary<Guid, string> Encodings(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "encodings";
            return SerializedDictionaryGuidString(auxData, typeName);
        }

        public static IList<string> BinaryType(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "binaryType";
            return SerializedStringList(auxData, typeName);
        }

        public static IList<string> Libraries(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "libraries";
            return SerializedStringList(auxData, typeName);
        }

        public static IList<string> LibraryPaths(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "libraryPaths";
            return SerializedStringList(auxData, typeName);
        }

        public static IDictionary<Guid, ObservableCollection<long>> ElfSectionProperties(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "elfSectionProperties";
            var valueDict = new Dictionary<Guid, ObservableCollection<long>>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    ObservableCollection<long> values = new ObservableCollection<long>();
                    values.Add(serialization.GetLong()); // SectionType
                    values.Add(serialization.GetLong()); // SectionFlags                    
                    valueDict[key] = values;
                }

            }
            return new SerializedDictionaryGuidSectionProperties(d => auxData.SetRaw(typeName, d), valueDict);
        }

        public static IDictionary<Offset, ObservableCollection<Directive>> CfiDirectives(this AuxData auxData)
        {
            // TODO: Add a description and link to a scope
            const string typeName = "cfiDirectives";
            var valueDict = new Dictionary<Offset, ObservableCollection<Directive>>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    // Each CFI DIRECTIVE has an Offset
                    Guid elementId = serialization.GetUuid();
                    long displacement = serialization.GetLong();
                    Offset offset = new Offset(elementId, displacement);

                    // Each CFI DIRECTIVE has a list of Directives
                    long numDirectives = serialization.GetLong();
                    var directiveList = new ObservableCollection<Directive>();

                    for (int j = 0; j < numDirectives; j++)
                    {

                        // Get a directive
                        string directiveString = serialization.GetString();
                        var directiveValues = new List<long>();
                        long numValues = serialization.GetLong();
                        for (int k = 0; k < numValues; k++)
                        {
                            long value = serialization.GetLong();
                            directiveValues.Add(value);
                        }
                        Guid directiveUuid = serialization.GetUuid();

                        // Put in list
                        Directive directive = new Directive(
                            directiveString, directiveValues, directiveUuid);
                        directiveList.Add(directive);
                    }
                }

            }
            return new SerializedDictionaryOffsetDirectives(d => auxData.SetRaw(typeName, d), valueDict);
        }



        private static IDictionary<Guid, ulong> SerializedDictionaryGuidUlong(AuxData auxData, string typeName)
        {
            var valueDict = new Dictionary<Guid, ulong>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (uint i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    var value = (ulong)serialization.GetLong();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryGuidULong(d =>
            {
                auxData.SetRaw(typeName, d);
            }, valueDict);
        }

        private static IDictionary<Guid, string> SerializedDictionaryGuidString(AuxData auxData, string typeName)
        {
            var valueDict = new Dictionary<Guid, string>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    var value = serialization.GetString();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryGuidString(d => auxData.SetRaw(typeName, d), valueDict);
        }

        private static IDictionary<Guid, Guid> SerializedDictionaryGuidGuid(AuxData auxData, string typeName)
        {
            var valueDict = new Dictionary<Guid, Guid>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    var value = serialization.GetUuid();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryGuidGuid(d => auxData.SetRaw(typeName, d), valueDict);
        }

        private static IDictionary<ulong, ulong> SerializedDictionaryUlongUlong(AuxData auxData, string typeName)
        {
            var valueDict = new Dictionary<ulong, ulong>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = (ulong)serialization.GetLong();
                    var value = (ulong)serialization.GetLong();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryULongULong(d => auxData.SetRaw(typeName, d), valueDict);
        }

        private static IDictionary<Offset, string> SerializedDictionaryOffsetString(AuxData auxData, string typeName)
        {
            var valueDict = new Dictionary<Offset, string>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var elementId = serialization.GetUuid();
                    var displacement = serialization.GetLong();
                    var key = new Offset(elementId, displacement);
                    var value = serialization.GetString();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryOffsetString(d => auxData.SetRaw(typeName, d), valueDict);
        }

        private static IDictionary<Guid, ObservableCollection<Guid>> SerializedDictionaryGuidGuids(AuxData auxData, string typeName)
        {
            var valueDict = new Dictionary<Guid, ObservableCollection<Guid>>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    var numVals = serialization.GetSize();
                    ObservableCollection<Guid> values = new ObservableCollection<Guid>();
                    for (int j = 0; j < numVals; j++)
                    {
                        var value = serialization.GetUuid();
                        values.Add(value);
                    }
                    valueDict[key] = values;
                }

            }
            return new SerializedDictionaryGuidGuids(d => auxData.SetRaw(typeName, d), valueDict);
        }        

        private static IList<string> SerializedStringList(AuxData auxData, string typeName)
        {
            var valueList = new List<string>();
            if (auxData.TryGetRaw(typeName, out var protoObjData) && protoObjData != null)
            {
                var serialization = new Serialization(protoObjData);
                var numValues = serialization.GetLong();

                for (int i = 0; i < numValues; i++)
                {
                    var value = serialization.GetString();
                    valueList.Add(value);
                }

            }
            return new SerializedStringList(d => auxData.SetRaw(typeName, d), valueList);
        }

    }
}
