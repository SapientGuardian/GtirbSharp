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
    public class ByteIntervalTests
    {
        [Fact]
        void SizeCanBeLargerThanContents()
        {
            var bi = new ByteInterval((Section)null);
            bi.Contents = new byte[3];
            bi.Size = 5;
            bi.Contents.Length.Should().Be(3);
            bi.Size.Should().Be(5);
        }

        [Fact]
        void ContentsTruncatedToSize()
        {
            var bi = new ByteInterval((Section)null);
            bi.Contents = new byte[5];
            bi.Size = 3;
            bi.Contents.Length.Should().Be(3);
        }

        [Fact]
        void RegistersWithOwner()
        {
            var section = new Section((Module)null);
            var bi = new ByteInterval(section);
            section.ByteIntervals.Single().Should().Be(bi);            
        }

        [Fact]
        void DeregistersWithOwner()
        {
            var section = new Section((Module)null);
            var bi = new ByteInterval(section);
            bi.Section = null;
            section.ByteIntervals.Should().BeEmpty();
        }

        [Fact]
        void GeneratesUUID()
        {
            var bi = new ByteInterval((Section)null);
            bi.UUID.Should().NotBe(default);
        }
    }
}
