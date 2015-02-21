using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Uri.Grammar
{
    public class UnreservedToken : Element
    {
        public UnreservedToken(Alpha alpha, ITextContext context)
            : base(alpha.Data, context)
        {
            Contract.Requires(alpha != null);
        }

        public UnreservedToken(Digit digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
        }

        public UnreservedToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '-'
                              || data == '.'
                              || data == '_'
                              || data == '~');
        }
    }
}