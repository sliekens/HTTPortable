using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class TCharToken : Token
    {
        private readonly AlphaToken alpha;
        private readonly DigitToken digit;

        public TCharToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == '!' || data == '#' || data == '$' || data == '%' || data == '&' || data == '\'' ||
                              data == '*' || data == '+' || data == '-' || data == '.' || data == '^' || data == '_' ||
                              data == '`' || data == '|' || data == '~');
        }

        public TCharToken(DigitToken digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
            this.digit = digit;
        }

        public TCharToken(AlphaToken alpha, ITextContext context)
            : base(alpha.Data, context)
        {
            Contract.Requires(alpha != null);
            this.alpha = alpha;
        }

        public AlphaToken Alpha
        {
            get { return alpha; }
        }

        public DigitToken Digit
        {
            get { return digit; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Alpha == null || this.Digit == null);
        }
    }
}