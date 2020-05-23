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
        /// Retrieve all keys currently in the map
        /// </summary>
        public IEnumerable<string> AuxDataNames => protoAuxDataMap.Keys;

        /// <summary>
        /// Get the number of items currently in the map
        /// </summary>
        public int Count => protoAuxDataMap.Count;

        internal AuxData(Dictionary<string, proto.AuxData> protoAuxDataMap)
        {
            this.protoAuxDataMap = protoAuxDataMap;
            
        }

        /// <summary>
        /// Get an aux data item by name
        /// </summary>
        /// <returns>True if the key was found, false if not</returns>
        public bool TryGet(string name, out AuxDataItem? auxData)
        {
            if (protoAuxDataMap.TryGetValue(name, out var protoData))
            {
                auxData = new AuxDataItem(protoData.TypeName, protoData.Data);
                return true;
            }
            auxData = null;
            return false;
        }

        /// <summary>
        /// Set the raw data associated with a name.
        /// </summary>
        public void Set(string name, AuxDataItem auxData)
        {
            if (protoAuxDataMap.TryGetValue(name, out var protoData))
            {
                protoData.Data = auxData.Data;
                protoData.TypeName = auxData.TypeName;
            }
            else
            {
                protoAuxDataMap[name] = new proto.AuxData { Data = auxData.Data, TypeName = auxData.TypeName };
            }
        }   

        /// <summary>
        /// Remove an entry by name
        /// </summary>
        /// <param name="name">Name of the entry to remove</param>
        public void Remove(string name)
        {
            protoAuxDataMap.Remove(name);
        }
    }
}
#nullable restore