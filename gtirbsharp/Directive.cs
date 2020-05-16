#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    // If this becomes mutable, add a change hook to SerializedDictionaryOffsetDirectives
    public sealed class Directive
    {
        public string DirectiveString { get; private set; }
        public IEnumerable<long> DirectiveValues { get; private set; }
        public Guid DirectiveUuid { get; private set; }

        public Directive(string directiveString, IEnumerable<long> directiveValues,
                     Guid directiveUuid)
        {
            this.DirectiveString = directiveString;
            this.DirectiveValues = directiveValues;
            this.DirectiveUuid = directiveUuid;
        }
    }
}
#nullable restore