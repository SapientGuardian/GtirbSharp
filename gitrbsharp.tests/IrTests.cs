using GtirbSharp;
using System;
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
    }
}
