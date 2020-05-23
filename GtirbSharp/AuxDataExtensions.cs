using GtirbSharp.DataStructures;
using Nito.Guids;
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
        public static IDictionary<Guid, ulong> Alignment(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "alignment";
            return SerializedDictionaryGuidUlong(auxData, keyName);
        }

        /// <summary>
        /// A map from the id of a DataObject to the type of the data, expressed as a string containing a C++ type specifier.
        /// </summary>
        public static IDictionary<Guid, string> Types(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "types";
            return SerializedDictionaryGuidString(auxData, keyName);

        }

        /// <summary>
        /// A map from the id of a symbol to the id of a symbol to which it is forwarded
        /// </summary>
        public static IDictionary<Guid, Guid> SymbolForwarding(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "symbolForwarding";
            return SerializedDictionaryGuidGuid(auxData, keyName);
        }


        /// <summary>
        /// A map from an address to a length of padding which has been inserted at the address
        /// </summary>
        public static IDictionary<ulong, ulong> Padding(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "padding";
            return SerializedDictionaryUlongUlong(auxData, keyName);
        }

        /// <summary>
        /// A map from an offset to a comment string relevant to it
        /// </summary>
        public static IDictionary<Offset, string> Comments(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "comments";
            return SerializedDictionaryOffsetString(auxData, keyName);
        }

        /// <summary>
        /// A map from the id of a function to the set of ids of blocks in the function
        /// </summary>
        public static IDictionary<Guid, ObservableCollection<Guid>> FunctionBlocks(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "functionBlocks";
            return SerializedDictionaryGuidGuids(auxData, keyName);
        }

        /// <summary>
        /// A map from the id of a function to the set of ids of entry points for the function
        /// </summary>
        public static IDictionary<Guid, ObservableCollection<Guid>> FunctionEntries(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "functionEntries";
            return SerializedDictionaryGuidGuids(auxData, keyName);
        }

        /// <summary>
        /// A map from the id of a function to the id of a symbol whose name field contains the name of the function.
        /// </summary>
        public static IDictionary<Guid, Guid> FunctionNames(this IDictionary<string, AuxDataItem> auxData)
        {
            const string keyName = "functionNames";
            return SerializedDictionaryGuidGuid(auxData, keyName);
        }
        public static IDictionary<Guid, ulong> SCCs(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "SCCs";
            return SerializedDictionaryGuidUlong(auxData, keyName);
        }

        public static IDictionary<Guid, string> Encodings(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "encodings";
            return SerializedDictionaryGuidString(auxData, keyName);
        }

        public static IList<string> BinaryType(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "binaryType";
            return SerializedStringList(auxData, keyName);
        }

        public static IList<string> Libraries(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "libraries";
            return SerializedStringList(auxData, keyName);
        }

        public static IList<string> LibraryPaths(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "libraryPaths";
            return SerializedStringList(auxData, keyName);
        }

        public static IDictionary<Guid, ObservableCollection<long>> ElfSectionProperties(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "elfSectionProperties";
            var valueDict = new Dictionary<Guid, ObservableCollection<long>>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
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
            return new SerializedDictionaryGuidSectionProperties(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }

        public static IDictionary<Offset, ObservableCollection<Directive>> CfiDirectives(this IDictionary<string, AuxDataItem> auxData)
        {
            // TODO: Add a description and link to a scope
            const string keyName = "cfiDirectives";
            var valueDict = new Dictionary<Offset, ObservableCollection<Directive>>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
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
            return new SerializedDictionaryOffsetDirectives(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }



        private static IDictionary<Guid, ulong> SerializedDictionaryGuidUlong(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueDict = new Dictionary<Guid, ulong>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
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
                auxData[keyName] = new AuxDataItem(null, d);
            }, valueDict);
        }

        private static IDictionary<Guid, string> SerializedDictionaryGuidString(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueDict = new Dictionary<Guid, string>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    var value = serialization.GetString();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryGuidString(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }

        private static IDictionary<Guid, Guid> SerializedDictionaryGuidGuid(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueDict = new Dictionary<Guid, Guid>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = serialization.GetUuid();
                    var value = serialization.GetUuid();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryGuidGuid(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }

        private static IDictionary<ulong, ulong> SerializedDictionaryUlongUlong(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueDict = new Dictionary<ulong, ulong>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
                var numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    var key = (ulong)serialization.GetLong();
                    var value = (ulong)serialization.GetLong();
                    valueDict[key] = value;
                }

            }
            return new SerializedDictionaryULongULong(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }

        private static IDictionary<Offset, string> SerializedDictionaryOffsetString(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueDict = new Dictionary<Offset, string>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
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
            return new SerializedDictionaryOffsetString(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }

        private static IDictionary<Guid, ObservableCollection<Guid>> SerializedDictionaryGuidGuids(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueDict = new Dictionary<Guid, ObservableCollection<Guid>>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
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
            return new SerializedDictionaryGuidGuids(d => auxData[keyName] = new AuxDataItem(null, d), valueDict);
        }        

        private static IList<string> SerializedStringList(IDictionary<string, AuxDataItem> auxData, string keyName)
        {
            var valueList = new List<string>();
            if (auxData.TryGetValue(keyName, out var protoObjData) && protoObjData?.Data != null)
            {
                var serialization = new Serialization(protoObjData.Data);
                var numValues = serialization.GetLong();

                for (int i = 0; i < numValues; i++)
                {
                    var value = serialization.GetString();
                    valueList.Add(value);
                }

            }
            return new SerializedStringList(d => auxData[keyName] = new AuxDataItem(null, d), valueList);
        }

    }
}
