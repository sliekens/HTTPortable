using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Uri.Grammar
{
    public class Unreserved : Element
    {
        public Unreserved(Alpha alpha, ITextContext context)
            : base(alpha.Data, context)
        {
            Contract.Requires(alpha != null);
        }

        public Unreserved(Digit digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
        }

        public Unreserved(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '-'
                              || data == '.'
                              || data == '_'
                              || data == '~');
        }
    }
}