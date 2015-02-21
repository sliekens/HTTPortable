using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    public class StatusCode : Element
    {
        public StatusCode(Digit digit1, Digit digit2, Digit digit3, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data, digit3.Data), context)
        {
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
            Contract.Requires(digit3 != null);
        }

        public int ToInt()
        {
            return int.Parse(this.Data);
        }
    }
}
