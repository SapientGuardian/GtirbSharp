using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public enum FileFormat
    {
        FormatUndefined = 0,
        Coff = 1,
        Elf = 2,
        Pe = 3,
        IdaProDb32 = 4,
        IdaProDb64 = 5,
        Xcoff = 6,
        Macho = 7,
        Raw = 8,
    }
}
