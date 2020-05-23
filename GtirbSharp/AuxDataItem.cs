using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class AuxDataItem
    {
        public string? TypeName { get; private set; }
        public byte[]? Data { get; private set; }

        public AuxDataItem(string? typeName, byte[]? data)
        {
            this.TypeName = typeName;
            this.Data = data;
        }
    }
}
