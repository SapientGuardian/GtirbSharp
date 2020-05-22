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
    public class ModuleTests
    {
      
        [Fact]
        void RegistersWithOwner()
        {
            var ir = new IR();
            var module = new Module(ir);
            ir.Modules.Single().Should().Be(module);            
        }

        [Fact]
        void DeregistersWithOwner()
        {
            var ir = new IR();
            var module = new Module(ir);
            module.IR = null;
            ir.Modules.Should().BeEmpty();
        }

        [Fact]
        void GeneratesUUID()
        {
            var module = new Module((IR)null);
            module.UUID.Should().NotBe(default);
        }
    }
}
