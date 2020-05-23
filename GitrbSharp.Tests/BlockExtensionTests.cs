using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using GtirbSharp;
using GtirbSharp.Extensions;

namespace GitrbSharp.Tests
{
    public class BlockExtensionTests
    {
        [Fact]
        public void CalculatesAddressOfCodeBlock()
        {
            var interval = new ByteInterval((Section)null);
            var block = new CodeBlock(interval);
            interval.Address = 10;
            block.Offset = 5;
            block.Address().Should().Be(15);
        }

        [Fact]
        public void CalculatesAddressOfDataBlock()
        {
            var interval = new ByteInterval((Section)null);
            var block = new DataBlock(interval);
            interval.Address = 10;
            block.Offset = 5;
            block.Address().Should().Be(15);
        }
    }
}
