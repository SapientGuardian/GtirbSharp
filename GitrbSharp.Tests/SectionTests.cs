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
    public class SectionTests
    {   
        [Fact]
        void RegistersWithOwner()
        {
            var module = new Module((IR)null);
            var section = new Section(module);
            module.Sections.Single().Should().Be(section);            
        }

        [Fact]
        void DeregistersWithOwner()
        {
            var module = new Module((IR)null);
            var section = new Section(module);
            section.Module = null;
            module.Sections.Should().BeEmpty();
        }

        [Fact]
        void GeneratesUUID()
        {
            var section = new Section((Module)null);
            section.UUID.Should().NotBe(default);
        }
    }
}
