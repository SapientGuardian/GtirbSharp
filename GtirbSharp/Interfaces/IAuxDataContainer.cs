using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp.Interfaces
{
    internal interface IAuxDataContainer
    {
        IDictionary<string, proto.AuxData> AuxData { get; }
    }
}
