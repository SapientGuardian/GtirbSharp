using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public enum EdgeType
    {
        TypeBranch = 0,
        TypeCall = 1,
        TypeFallthrough = 2,
        TypeReturn = 3,
        TypeSyscall = 4,
        TypeSysret = 5,
    }

}
