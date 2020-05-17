using System;
using System.Collections.Generic;
using System.Text;

namespace GtirbSharp
{
    /// <summary>
    /// A SymbolicExpression represents a data value or instruction operand which should be interpreted as referring to a symbol.
    /// </summary>
    public abstract class SymbolicExpression
    {
        public ulong Offset
        {
            get => GetOffset(); set => SetOffset(GetOffset(), value);
        }
        internal event EventHandler<ulong>? OffsetChanged;

        protected abstract ulong GetOffset();
        protected virtual void SetOffset(ulong oldValue, ulong newValue)
        {
            var handler = OffsetChanged;
            handler?.Invoke(this, oldValue);
        }
        internal SymbolicExpression()
        {

        }
    }
}
