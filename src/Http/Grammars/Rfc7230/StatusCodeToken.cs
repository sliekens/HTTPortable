namespace Http.Grammars.Rfc7230
{
    using Text.Scanning;
    using Text.Scanning.Core;

    public class StatusCodeToken : Token
    {
        private readonly DigitToken digit1;
        private readonly DigitToken digit2;
        private readonly DigitToken digit3;

        public StatusCodeToken(DigitToken digit1, DigitToken digit2, DigitToken digit3, ITextContext context)
            : base(string.Concat(digit1, digit2, digit3), context)
        {
            this.digit1 = digit1;
            this.digit2 = digit2;
            this.digit3 = digit3;
        }

        public DigitToken Digit1
        {
            get
            {
                return this.digit1;
            }
        }

        public DigitToken Digit2
        {
            get
            {
                return this.digit2;
            }
        }

        public DigitToken Digit3
        {
            get
            {
                return this.digit3;
            }
        }

        public int ToInt()
        {
            return int.Parse(this.Digit1.Data) * 100 + int.Parse(this.Digit2.Data) * 10 + int.Parse(this.Digit3.Data);
        }
    }
}
