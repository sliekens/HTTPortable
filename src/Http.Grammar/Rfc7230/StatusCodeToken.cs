using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class StatusCodeToken : Element
    {
        private readonly Digit digit1;
        private readonly Digit digit2;
        private readonly Digit digit3;

        public StatusCodeToken(Digit digit1, Digit digit2, Digit digit3, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data, digit3.Data), context)
        {
            this.digit1 = digit1;
            this.digit2 = digit2;
            this.digit3 = digit3;
        }

        public Digit Digit1
        {
            get
            {
                return this.digit1;
            }
        }

        public Digit Digit2
        {
            get
            {
                return this.digit2;
            }
        }

        public Digit Digit3
        {
            get
            {
                return this.digit3;
            }
        }

        public int ToInt()
        {
            return int.Parse(this.Data);
        }
    }
}
