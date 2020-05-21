using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public enum Isa
    {
        ISAUndefined = 0,
        Ia32 = 1,
        Ppc32 = 2,
        X64 = 3,
        Arm = 4,
        ValidButUnsupported = 5,
        Ppc64 = 6,
        Arm64 = 7,
        Mips32 = 8,
        Mips64 = 9,
    }
}
