using FluentAssertions;
using GtirbSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace GitrbSharp.Tests
{
    public class IrTests
    {
        [Fact]
        public void LoadFileWithoutException()
        {
            using (var fs = new FileStream(@"Resources\testIr.gtirb", FileMode.Open))
            {
                var ir = IR.LoadFromStream(fs);
                Assert.True(true);
            }
        }

        [Fact]
        public void DeepCopy()
        {
            IR oldIR;
            using (var fs = new FileStream(@"Resources\test1.gtirb", FileMode.Open))
            {
                oldIR = IR.LoadFromStream(fs);
            }

            var newIR = new IR();

            newIR.ProtoVersion = oldIR.ProtoVersion;

            var uuidTranslationTable = new Dictionary<Guid, Guid>(); // old -> new

            foreach (var key in oldIR.AuxData.AuxDataTypes)
            {
                oldIR.AuxData.TryGetRaw(key, out var data);
                newIR.AuxData.SetRaw(key, data);
            }
            newIR.AuxData.Count.Should().Be(oldIR.AuxData.Count);

            foreach (var oldModule in oldIR.Modules)
            {
                var newModule = new Module(newIR);
                uuidTranslationTable[oldModule.UUID] = newModule.UUID;

                newModule.BinaryPath = oldModule.BinaryPath;
                newModule.PreferredAddr = oldModule.PreferredAddr;
                newModule.RebaseDelta = oldModule.RebaseDelta;
                newModule.FileFormat = oldModule.FileFormat;
                newModule.ISA = oldModule.ISA;
                newModule.Name = oldModule.Name;


                foreach (var key in oldModule.AuxData.AuxDataTypes)
                {
                    oldModule.AuxData.TryGetRaw(key, out var data);
                    newModule.AuxData.SetRaw(key, data);
                }

                newModule.AuxData.Count.Should().Be(oldModule.AuxData.Count);

                foreach (var oldSection in oldModule.Sections)
                {
                    var newSection = new Section(newModule);
                    uuidTranslationTable[oldSection.UUID] = newSection.UUID;

                    foreach (var oldByteInterval in oldSection.ByteIntervals)
                    {
                        var newByteInterval = new ByteInterval(newSection);
                        uuidTranslationTable[oldByteInterval.UUID] = newByteInterval.UUID;

                        newByteInterval.Address = oldByteInterval.Address;
                        foreach (var oldBlock in oldByteInterval.Blocks)
                        {
                            Block newBlock = oldBlock switch
                            {
                                CodeBlock _ => new CodeBlock(newByteInterval),
                                DataBlock _ => new DataBlock(newByteInterval),
                                _ => throw new Exception("Unexpected block type")
                            };
                            uuidTranslationTable[oldBlock.UUID] = newBlock.UUID;

                            if (newBlock is CodeBlock codeBlock)
                            {
                                codeBlock.DecodeMode = ((CodeBlock)oldBlock).DecodeMode;
                                codeBlock.Offset = ((CodeBlock)oldBlock).Offset;
                                codeBlock.Size = ((CodeBlock)oldBlock).Size;
                            }
                            else if (newBlock is DataBlock dataBlock)
                            {
                                dataBlock.Offset = ((DataBlock)oldBlock).Offset;
                                dataBlock.Size = ((DataBlock)oldBlock).Size;
                            }
                        }
                        newByteInterval.Blocks.Count.Should().Be(oldByteInterval.Blocks.Count);

                        newByteInterval.Contents = oldByteInterval?.Contents?.ToArray();
                        newByteInterval.Size = oldByteInterval.Size;
                        foreach (var oldSymbolicExpression in oldByteInterval.SymbolicExpressions)
                        {
                            newByteInterval.SymbolicExpressions.Add(oldSymbolicExpression);
                        }
                        newByteInterval.SymbolicExpressions.Count.Should().Be(oldByteInterval.SymbolicExpressions.Count);
                    }
                    newSection.ByteIntervals.Count.Should().Be(oldSection.ByteIntervals.Count);
                    newSection.Name = oldSection.Name;
                    foreach (var flag in oldSection.SectionFlags)
                    {
                        newSection.SectionFlags.Add(flag);
                    }
                    newSection.SectionFlags.Count.Should().Be(oldSection.SectionFlags.Count);
                }
                newModule.Sections.Count.Should().Be(oldModule.Sections.Count);

                foreach (var oldProxyBlock in oldModule.ProxyBlocks)
                {
                    var newProxyBlock = new ProxyBlock(newModule);
                    uuidTranslationTable[oldProxyBlock.UUID] = newProxyBlock.UUID;
                }
                newModule.ProxyBlocks.Count.Should().Be(oldModule.ProxyBlocks.Count);

                foreach (var oldSymbol in oldModule.Symbols)
                {
                    var newSymbol = new Symbol(newModule);
                    uuidTranslationTable[oldSymbol.UUID] = newSymbol.UUID;
                    newSymbol.Name = oldSymbol.Name;
                    if (oldSymbol.ReferentUuid.HasValue)
                    {
                        newSymbol.ReferentUuid = uuidTranslationTable[oldSymbol.ReferentUuid.Value];
                    }
                    else if (oldSymbol.Value.HasValue)
                    {
                        newSymbol.Value = oldSymbol.Value.Value;
                    }
                    newSymbol.AtEnd = oldSymbol.AtEnd;
                }
                newModule.Symbols.Count.Should().Be(oldModule.Symbols.Count);
            }

            if (oldIR.Cfg != null)
            {
                newIR.Cfg = new CFG();
                foreach (var vertice in oldIR.Cfg.Vertices)
                {
                    newIR.Cfg.Vertices.Add(uuidTranslationTable[vertice]);
                }
                foreach (var oldEdge in oldIR.Cfg.Edges)
                {
                    var newEdge = new Edge();
                    newEdge.EdgeLabelConditional = oldEdge.EdgeLabelConditional;
                    newEdge.EdgeLabelDirect = oldEdge.EdgeLabelDirect;
                    newEdge.EdgeType = oldEdge.EdgeType;
                    if (oldEdge.SourceUuid.HasValue)
                    {
                        newEdge.SourceUuid = uuidTranslationTable[oldEdge.SourceUuid.Value];
                    }
                    if (oldEdge.TargetUuid.HasValue)
                    {
                        newEdge.TargetUuid = uuidTranslationTable[oldEdge.TargetUuid.Value];
                    }
                    newIR.Cfg.Edges.Add(newEdge);
                }
            }

            foreach (var oldModule in oldIR.Modules)
            {
                var newModule = (Module)newIR.GetByUuid(uuidTranslationTable[oldModule.UUID]);
                if (oldModule.EntryPointUuid.HasValue)
                {
                    newModule.EntryPointUuid = uuidTranslationTable[oldModule.EntryPointUuid.Value];
                }
            }

            // ID fixups
            foreach (var newModule in newIR.Modules)
            {


                Guid[] oldKeys;

                oldKeys = newModule.Alignment.Keys.ToArray();
                foreach (var key in oldKeys)
                {
                    newModule.Alignment[uuidTranslationTable[key]] = newModule.Alignment[key];
                    newModule.Alignment.Remove(key);
                }

                oldKeys = newModule.Types.Keys.ToArray();
                foreach (var key in oldKeys)
                {
                    newModule.Types[uuidTranslationTable[key]] = newModule.Types[key];
                    newModule.Types.Remove(key);
                }

                oldKeys = newModule.SymbolForwarding.Keys.ToArray();
                foreach (var key in oldKeys)
                {
                    newModule.SymbolForwarding[uuidTranslationTable[key]] = uuidTranslationTable[newModule.SymbolForwarding[key]];
                    newModule.SymbolForwarding.Remove(key);
                }

                oldKeys = newModule.FunctionNames.Keys.ToArray();
                foreach (var key in oldKeys)
                {
                    newModule.FunctionNames[uuidTranslationTable[key]] = uuidTranslationTable[newModule.FunctionNames[key]];
                    newModule.FunctionNames.Remove(key);
                }

                // These keys don't seem to exist.
                oldKeys = newModule.FunctionBlocks.Keys.ToArray();
                foreach (var key in oldKeys)
                {
                    if (uuidTranslationTable.ContainsKey(key))
                    {
                        newModule.FunctionBlocks[uuidTranslationTable[key]] = new System.Collections.ObjectModel.ObservableCollection<Guid>(newModule.FunctionBlocks[key].Select(oldId => uuidTranslationTable[oldId]));
                        newModule.FunctionBlocks.Remove(key);
                    }
                }

                // These keys don't seem to exist.
                oldKeys = newModule.FunctionEntries.Keys.ToArray();
                foreach (var key in oldKeys)
                {
                    if (uuidTranslationTable.ContainsKey(key))
                    {
                        newModule.FunctionEntries[uuidTranslationTable[key]] = new System.Collections.ObjectModel.ObservableCollection<Guid>(newModule.FunctionEntries[key].Select(oldId => uuidTranslationTable[oldId]));
                        newModule.FunctionEntries.Remove(key);
                    }
                }
            }


            newIR.NodeCount().Should().Be(oldIR.NodeCount());
            newIR.Cfg.Edges.Count.Should().Be(oldIR.Cfg.Edges.Count);
            newIR.Cfg.Vertices.Count.Should().Be(oldIR.Cfg.Vertices.Count);     

            var ms1 = new MemoryStream();
            var ms2 = new MemoryStream();
            oldIR.SaveToStream(ms1);
            newIR.SaveToStream(ms2);

            (ms1.Length - ms2.Length).Should().Be(0);
        }
    }
}
