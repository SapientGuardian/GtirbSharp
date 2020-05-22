#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// An Offset describes a location inside a CodeBlock or gtirb.DataBlock.
    /// </summary>
    public sealed class Offset
    {
        // If this becomes immutable, add a change hook to SerializedDictionaryOffsetString and SerializedDictionaryOffsetDirectives

        /// <summary>
        /// The UUID of a Block containing the location of interest
        /// </summary>
        public Guid ElementId { get; private set; }
        /// <summary>
        /// The offset inside the Node to point to
        /// </summary>
        public long Displacement { get; private set; }

        /// <summary>
        /// Construct a new Offset with the specified elementId and displacement
        /// </summary>
        public Offset(Guid elementId, long displacement)
        {
            this.ElementId = elementId;
            this.Displacement = displacement;
        }
    }
}
#nullable disable