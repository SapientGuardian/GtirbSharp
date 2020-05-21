using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public enum SectionFlag
    {
        SectionUndefined = 0,
        Readable = 1,
        Writable = 2,
        Executable = 3,
        Loaded = 4,
        Initialized = 5,
        ThreadLocal = 6,
    }
}
