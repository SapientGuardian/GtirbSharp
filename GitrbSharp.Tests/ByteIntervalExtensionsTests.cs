using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using GtirbSharp;
using GtirbSharp.Extensions;
using System.Linq;

namespace GitrbSharp.Tests
{
    public class ByteIntervalExtensionsTests
    {
        [Fact]
        public void FindsBlocksAtOffset()
        {
            var interval = new ByteInterval((Section)null);
            interval.Address = 10;

            var block1 = new CodeBlock(interval) { Offset = 5 };
            var block2 = new DataBlock(interval) { Offset = 5 };
            var block3 = new CodeBlock(interval) { Offset = 5 };
            var block4 = new CodeBlock(interval) { Offset = 10 };

            interval.BlocksAtOffset(5).Count().Should().Be(3);
            interval.BlocksAtOffset(5).Should().Contain(block1);
            interval.BlocksAtOffset(5).Should().Contain(block2);
            interval.BlocksAtOffset(5).Should().Contain(block3);
            
        }

        [Fact]
        public void FindsBlocksAtAddress()
        {
            var interval = new ByteInterval((Section)null);
            interval.Address = 10;

            var block1 = new CodeBlock(interval) { Offset = 5 };
            var block2 = new DataBlock(interval) { Offset = 5 };
            var block3 = new CodeBlock(interval) { Offset = 5 };

            interval.BlocksAtAddress<CodeBlock>(15).Count().Should().Be(2);
            interval.BlocksAtAddress<CodeBlock>(15).Should().Contain(block1);
            interval.BlocksAtAddress<CodeBlock>(15).Should().Contain(block3);
            interval.BlocksAtAddress<DataBlock>(15).Single().Should().Be(block2);
        }
    }
}
