namespace GtirbSharp.Interfaces
{
    public interface IByteBlock
    {
        /// <summary>
        /// The ByteInterval to which this CodeBlock belongs
        /// </summary>
        ByteInterval? ByteInterval { get; set; }
        /// <summary>
        /// The size of the block in bytes
        /// </summary>
        public ulong Size { get; }
        /// <summary>
        /// The offset of this block in the owning ByteInterval
        /// </summary>
        public ulong Offset
        {
            get;
        }
    }
}