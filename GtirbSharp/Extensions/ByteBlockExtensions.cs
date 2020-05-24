using GtirbSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp.Extensions
{
    /// <summary>
    /// Extension methods for Blocks
    /// </summary>
    public static class ByteBlockExtensions
    {
        /// <summary>
        /// Calculate the address of the block
        /// </summary>
        public static ulong Address(this IByteBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (block.ByteInterval == null) throw new InvalidOperationException("Address can only be calculated if the block belongs to a ByteInterval.");
            return (block.ByteInterval.Address ?? throw new InvalidOperationException("ByteInterval does not have an address")) + block.Offset;
        }
    }
}
