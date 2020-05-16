#nullable enable
using GtirbSharp.DataStructures;
using GtirbSharp.Helpers;
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
        private const string binaryType = "binaryType";
        private const string libraries = "libraries";
        private const string libraryPaths = "libraryPaths";
        private const string sccs = "SCCs";
        private const string encodings = "encodings";        
        
        private const string elfSectionProperties = "elfSectionProperties";
        private const string cfiDirectives = "cfiDirectives";

        private readonly Dictionary<string, proto.AuxData> protoAuxDataMap;

        public IEnumerable<string> AuxDataTypes => protoAuxDataMap.Keys;
        public IList<string>? BinaryType { get; private set; }
        public IList<string>? Libraries { get; private set; }
        public IList<string>? LibraryPaths { get; private set; }

        public IDictionary<Guid, long>? SCCs { get; private set; }
        public IDictionary<Guid, string>? Encodings { get; private set; }
        
        public IDictionary<Guid, ObservableCollection<long>>? ElfSectionProperties { get; private set; }
        public IDictionary<Offset, ObservableCollection<Directive>>? CfiDirectives { get; private set; }
        // TODO: functionNames

        internal AuxData(Dictionary<string, proto.AuxData> protoAuxDataMap)
        {
            this.protoAuxDataMap = protoAuxDataMap;
            AttachBinaryType();
            AttachLibraries();
            AttachLibraryPaths();
            AttachSCCs();
            AttachEncodings();
            AttachElfSectionProperties();
            AttachCfiDirectives();
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

        public void SetRaw(string typeName, byte[] data)
        {
            if (protoAuxDataMap.TryGetValue(typeName, out var protoData))
            {
                protoData.Data = data;                
            }
            else
            {
                protoAuxDataMap[typeName] = new proto.AuxData { Data = data, TypeName = typeName };
            }
        }

        private void AttachBinaryType()
        {
            if (this.protoAuxDataMap.TryGetValue(binaryType, out var protoObj) && protoObj.Data != null)
            {
                var valueList = new List<string>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numTypes = serialization.GetLong();

                for (int i = 0; i < numTypes; i++)
                {
                    string aType = serialization.GetString();
                    valueList.Add(aType);
                }
                BinaryType = new SerializedStringList(d => protoObj.Data = d, valueList);
            }
            else
            {
                BinaryType = new SerializedStringList(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(binaryType, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[binaryType] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = binaryType,
                            Data = d
                        };
                    }
                });
            }
        }

        private void AttachLibraries()
        {
            if (this.protoAuxDataMap.TryGetValue(libraries, out var protoObj) && protoObj.Data != null)
            {
                var valueList = new List<string>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numTypes = serialization.GetLong();

                for (int i = 0; i < numTypes; i++)
                {
                    string aType = serialization.GetString();
                    valueList.Add(aType);
                }
                Libraries = new SerializedStringList(d => protoObj.Data = d, valueList);
            }
            else
            {
                Libraries = new SerializedStringList(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(libraries, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[libraries] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = libraries,
                            Data = d
                        };
                    }
                });
            }
        }

        private void AttachLibraryPaths()
        {
            if (this.protoAuxDataMap.TryGetValue(libraryPaths, out var protoObj) && protoObj.Data != null)
            {
                var valueList = new List<string>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numTypes = serialization.GetLong();

                for (int i = 0; i < numTypes; i++)
                {
                    string aType = serialization.GetString();
                    valueList.Add(aType);
                }
                LibraryPaths = new SerializedStringList(d => protoObj.Data = d, valueList);
            }
            else
            {
                LibraryPaths = new SerializedStringList(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(libraryPaths, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[libraryPaths] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = libraryPaths,
                            Data = d
                        };
                    }
                });
            }
        }

        private void AttachSCCs()
        {
            if (this.protoAuxDataMap.TryGetValue(sccs, out var protoObj) && protoObj.Data != null)
            {
                var valueDict = new Dictionary<Guid, long>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    Guid key = serialization.GetUuid();
                    long value = serialization.GetLong();
                    valueDict[key] = value;
                }

                SCCs = new SerializedDictionaryGuidLong(d => protoObj.Data = d, valueDict);
            }
            else
            {
                SCCs = new SerializedDictionaryGuidLong(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(sccs, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[sccs] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = sccs,
                            Data = d
                        };
                    }
                });
            }
        }

        private void AttachEncodings()
        {
            if (this.protoAuxDataMap.TryGetValue(encodings, out var protoObj) && protoObj.Data != null)
            {
                var valueDict = new Dictionary<Guid, string>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    Guid key = serialization.GetUuid();
                    string value = serialization.GetString();
                    valueDict[key] = value;
                }

                Encodings = new SerializedDictionaryGuidString(d => protoObj.Data = d, valueDict);
            }
            else
            {
                Encodings = new SerializedDictionaryGuidString(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(encodings, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[encodings] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = encodings,
                            Data = d
                        };
                    }
                });
            }
        }

        private void AttachElfSectionProperties()
        {
            if (this.protoAuxDataMap.TryGetValue(elfSectionProperties, out var protoObj) && protoObj.Data != null)
            {
                var valueDict = new Dictionary<Guid, ObservableCollection<long>>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numPairs = serialization.GetLong();

                for (int i = 0; i < numPairs; i++)
                {
                    Guid key = serialization.GetUuid();
                    ObservableCollection<long> values = new ObservableCollection<long>();
                    values.Add(serialization.GetLong()); // SectionType
                    values.Add(serialization.GetLong()); // SectionFlags                    
                    valueDict[key] = values;
                }

                ElfSectionProperties = new SerializedDictionaryGuidSectionProperties(d => protoObj.Data = d, valueDict);
            }
            else
            {
                ElfSectionProperties = new SerializedDictionaryGuidSectionProperties(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(elfSectionProperties, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[elfSectionProperties] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = elfSectionProperties,
                            Data = d
                        };
                    }
                });
            }
        }

        private void AttachCfiDirectives()
        {
            if (this.protoAuxDataMap.TryGetValue(cfiDirectives, out var protoObj) && protoObj.Data != null)
            {
                var valueDict = new Dictionary<Offset, ObservableCollection<Directive>>();
                Serialization serialization = new Serialization(protoObj.Data);
                long numPairs = serialization.GetLong();

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
                    valueDict[offset] = directiveList;
                }

                CfiDirectives = new SerializedDictionaryOffsetDirectives(d => protoObj.Data = d, valueDict);
            }
            else
            {
                CfiDirectives = new SerializedDictionaryOffsetDirectives(d =>
                {
                    if (this.protoAuxDataMap.TryGetValue(cfiDirectives, out var protoObj))
                    {
                        protoObj.Data = d;
                    }
                    else
                    {
                        this.protoAuxDataMap[cfiDirectives] = new GtirbSharp.proto.AuxData
                        {
                            TypeName = cfiDirectives,
                            Data = d
                        };
                    }
                });
            }
        }

    }
}
#nullable restore