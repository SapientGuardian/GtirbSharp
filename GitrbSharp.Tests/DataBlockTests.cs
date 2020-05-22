using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using GtirbSharp;
using GtirbSharp.Interfaces;
using System.Linq;

namespace GitrbSharp.Tests
{
    public class DataBlockTests
    {
      
        [Fact]
        void RegistersWithOwningSection()
        {            
            var bi = new ByteInterval((Section)null);
            var block = new DataBlock(bi);
            bi.Blocks.Single().Should().Be(block);            
        }

        [Fact]
        void DeregistersWithOwningSection()
        {
            var bi = new ByteInterval((Section)null);
            var block = new DataBlock(bi);
            block.ByteInterval = null;
            bi.Blocks.Should().BeEmpty();
        }

        [Fact]
        void GeneratesUUID()
        {
            var block = new DataBlock((ByteInterval)null);
            block.UUID.Should().NotBe(default);
        }
    }
}
