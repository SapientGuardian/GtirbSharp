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

        /// <summary>
        /// A map from the id of a DataObject to the type of the data, expressed as a string containing a C++ type specifier.
        /// </summary>
        public static IDictionary<Guid, string> Types(this AuxData auxData)
        {
            const string typeName = "types";
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

        /// <summary>
        /// A map from the id of a symbol to the id of a symbol to which it is forwarded
        /// </summary>
        public static IDictionary<Guid, Guid> SymbolForwarding(this AuxData auxData)
        {
            const string typeName = "symbolForwarding";
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

        /// <summary>
        /// A map from an address to a length of padding which has been inserted at the address
        /// </summary>
        public static IDictionary<ulong, ulong> Padding(this AuxData auxData)
        {
            const string typeName = "padding";
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

        /// <summary>
        /// A map from an offset to a comment string relevant to it
        /// </summary>
        public static IDictionary<Offset, string> Comments(this AuxData auxData)
        {
            const string typeName = "comments";
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

        /// <summary>
        /// A map from the id of a function to the set of ids of blocks in the function
        /// </summary>
        public static IDictionary<Guid, ObservableCollection<Guid>> FunctionBlocks(this AuxData auxData)
        {
            const string typeName = "functionBlocks";
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

        /// <summary>
        /// A map from the id of a function to the set of ids of entry points for the function
        /// </summary>
        public static IDictionary<Guid, ObservableCollection<Guid>> FunctionEntries(this AuxData auxData)
        {
            const string typeName = "functionEntries";
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

        /// <summary>
        /// A map from the id of a function to the id of a symbol whose name field contains the name of the function.
        /// </summary>
        public static IDictionary<Guid, Guid> FunctionNames(this AuxData auxData)
        {
            const string typeName = "functionNames";
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
    }
}
