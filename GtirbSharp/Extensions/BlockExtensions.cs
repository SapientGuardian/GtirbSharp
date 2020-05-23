using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp.Extensions
{
    /// <summary>
    /// Extension methods for Blocks
    /// </summary>
    public static class BlockExtensions
    {
        /// <summary>
        /// Calculate the address of the block
        /// </summary>
        public static ulong Address(this CodeBlock block)
        {
            if (block.ByteInterval == null) throw new InvalidOperationException("Address can only be calculated if the block belongs to a ByteInterval.");
            return (block.ByteInterval.Address ?? throw new InvalidOperationException("ByteInterval does not have an address")) + block.Offset;
        }

        /// <summary>
        /// Calculate the address of the block
        /// </summary>
        public static ulong Address(this DataBlock block)
        {
            if (block.ByteInterval == null) throw new InvalidOperationException("Address can only be calculated if the block belongs to a ByteInterval.");
            return (block.ByteInterval.Address ?? throw new InvalidOperationException("ByteInterval does not have an address")) + block.Offset;
        }
    }
}
