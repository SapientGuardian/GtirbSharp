#nullable enable
using GtirbSharp.DataStructures;
using Nito.Guids;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// An AuxData object provides functionality for associating auxiliary data with elements of the representation.
    /// </summary>
    public sealed class AuxData
    {

        private readonly Dictionary<string, proto.AuxData> protoAuxDataMap;

        /// <summary>
        /// Retrieve all keys currently in the AuxData map
        /// </summary>
        public IEnumerable<string> AuxDataTypes => protoAuxDataMap.Keys;

        public int Count => protoAuxDataMap.Count;

        internal AuxData(Dictionary<string, proto.AuxData> protoAuxDataMap)
        {
            this.protoAuxDataMap = protoAuxDataMap;
            
            // Sync the names
            foreach (var kvp in protoAuxDataMap)
            {
                kvp.Value.TypeName = kvp.Key;
            }
        }

        public bool TryGetRaw(string typeName, out byte[]? data)
        {
            if (protoAuxDataMap.TryGetValue(typeName, out var protoData))
            {
                data = protoData.Data;
                return true;
            }
            data = null;
            return false;
        }

        public void SetRaw(string typeName, byte[]? data)
        {
            if (data == null)
            {
                protoAuxDataMap.Remove(typeName);
            }
            else if (protoAuxDataMap.TryGetValue(typeName, out var protoData))
            {
                protoData.Data = data;                
            }
            else
            {
                protoAuxDataMap[typeName] = new proto.AuxData { Data = data, TypeName = typeName };
            }
        }                    
    }
}
#nullable restore