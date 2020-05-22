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
    public class SymbolTests
    {   
        [Fact]
        void RegistersWithOwner()
        {
            var module = new Module((IR)null);
            var symbol = new Symbol(module);
            module.Symbols.Single().Should().Be(symbol);            
        }

        [Fact]
        void DeregistersWithOwner()
        {
            var module = new Module((IR)null);
            var symbol = new Symbol(module);
            symbol.Module = null;
            module.Symbols.Should().BeEmpty();
        }

        [Fact]
        void GeneratesUUID()
        {
            var symbol = new Symbol((Module)null);
            symbol.UUID.Should().NotBe(default);
        }
    }
}
