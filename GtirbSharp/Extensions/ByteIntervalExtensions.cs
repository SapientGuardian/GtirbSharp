using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GtirbSharp.Extensions;
using GtirbSharp.Interfaces;

namespace GtirbSharp.Extensions
{
    /// <summary>
    /// Extension methods for ByteIntervals
    /// </summary>
    public static class ByteIntervalExtensions
    {

        /// <summary>
        /// Find blocks at a specified offset
        /// </summary>
        public static IEnumerable<Block> BlocksAtOffset(this ByteInterval byteInterval, ulong offset)
        {
            if (byteInterval == null) throw new ArgumentNullException(nameof(byteInterval));
            return byteInterval.Blocks.Where(b => b.Offset == offset);
        }

        /// <summary>
        /// Find blocks of a specified type at the specified address
        /// </summary>
        /// <typeparam name="T">Type of block to find</typeparam>
        public static IEnumerable<T> BlocksAtAddress<T>(this ByteInterval byteInterval, ulong address) where T : Block, IByteBlock
        {
            if (byteInterval == null) throw new ArgumentNullException(nameof(byteInterval));
            return byteInterval.Blocks.OfType<T>().Where(block => block.Address() == address);
        }

        /// <summary>
        /// Find blocks of a specified type at the specified addresses
        /// </summary>
        /// <typeparam name="T">Type of block to find</typeparam>
        public static IEnumerable<T> BlocksAtAddress<T>(this ByteInterval byteInterval, IEnumerable<ulong> addresses) where T : Block, IByteBlock
        {
            if (byteInterval == null) throw new ArgumentNullException(nameof(byteInterval));
            return byteInterval.Blocks.OfType<T>().Where(block => addresses.Contains(block.Address()));
        }

        // TODO: Method to find all CodeBlock objects that intersect a given address or range of addresses.

        // TODO: Method to find all DataBlock objects that intersect a given address or range of addresses.

        // TODO: Find all SymbolicExpression objects that start at a given address or range of addresses;

    }
}
