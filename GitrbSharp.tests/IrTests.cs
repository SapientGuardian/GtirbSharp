using GtirbSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace gitrbsharp.tests
{
    public class IrTests
    {
        [Fact]
        public void LoadFileWithoutException()
        {
            using (var fs = new FileStream(@"Resources\testIr.gtirb", FileMode.Open))
            {
                var ir = IR.LoadFromStream(fs);
                Assert.True(true);
            }
        }

        [Fact]
        public void DeepCopy()
        {
            IR oldIR;
            using (var fs = new FileStream(@"Resources\testIr.gtirb", FileMode.Open))
            {
                oldIR = IR.LoadFromStream(fs);                
            }

            var newIR = new IR();

            newIR.ProtoVersion = oldIR.ProtoVersion;

            var uuidTranslationTable = new Dictionary<Guid, Guid>(); // old -> new

            foreach (var oldModule in oldIR.Modules)
            {
                var newModule = new Module();
                uuidTranslationTable[oldModule.UUID] = newModule.UUID;
            }
        }
    }
}
