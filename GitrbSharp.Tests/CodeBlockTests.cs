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
    public class CodeBlockTests
    {
      
        [Fact]
        void RegistersWithOwner()
        {            
            var bi = new ByteInterval((Section)null);
            var block = new CodeBlock(bi);
            bi.Blocks.Single().Should().Be(block);            
        }

        [Fact]
        void DeregistersWithOwner()
        {
            var bi = new ByteInterval((Section)null);
            var block = new CodeBlock(bi);
            block.ByteInterval = null;
            bi.Blocks.Should().BeEmpty();
        }

        [Fact]
        void GeneratesUUID()
        {
            var block = new CodeBlock((ByteInterval)null);
            block.UUID.Should().NotBe(default);
        }
    }
}
