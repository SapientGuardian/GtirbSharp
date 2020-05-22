using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using GtirbSharp;
using GtirbSharp.Interfaces;

namespace GitrbSharp.Tests
{
    public class ByteIntervalTests
    {
        [Fact]
        void SizeCanBeLargerThanContents()
        {
            var bi = new ByteInterval((INodeContext)null);
            bi.Contents = new byte[3];
            bi.Size = 5;
            bi.Contents.Length.Should().Be(3);
            bi.Size.Should().Be(5);
        }

        [Fact]
        void ContentsTruncatedToSize()
        {
            var bi = new ByteInterval((INodeContext)null);
            bi.Contents = new byte[5];
            bi.Size = 3;
            bi.Contents.Length.Should().Be(3);
        }
    }
}
