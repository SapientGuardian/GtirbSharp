using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using GtirbSharp;

namespace GitrbSharp.Tests
{
    public class AuxDataTests
    {
        [Fact]
        void CanRemoveEntry()
        {
            var module = new Module(null);
            module.AuxData.SetRaw("test", new byte[] { 1, 2, 3 });
            module.AuxData.Count.Should().Be(1); // Confirm the entry got added
            module.AuxData.SetRaw("test", null);
            module.AuxData.Count.Should().Be(0); // Confirm the entry got removed
        }
    }
}
