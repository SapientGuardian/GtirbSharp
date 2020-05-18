#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    public sealed class Offset
    {
        // If this becomes immutable, add a change hook to SerializedDictionaryOffsetString and SerializedDictionaryOffsetDirectives
        public Guid ElementId { get; private set; }
        public long Displacement { get; private set; }

        public Offset(Guid elementId, long displacement)
        {
            this.ElementId = elementId;
            this.Displacement = displacement;
        }
    }
}
#nullable disable