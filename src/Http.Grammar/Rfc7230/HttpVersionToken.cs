using System;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class HttpVersionToken : Token
    {
        private readonly HttpNameToken httpName;
        private readonly DigitToken digit1;

        private readonly DigitToken digit2;

        public HttpVersionToken(HttpNameToken httpName, DigitToken digit1, DigitToken digit2, ITextContext context)
            : base(string.Concat(httpName, '/', digit1, '.', digit2), context)
        {
            this.httpName = httpName;
            this.digit1 = digit1;
            this.digit2 = digit2;
        }

        public HttpNameToken HttpName
        {
            get
            {
                return this.httpName;
            }
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

        public Version ToVersion()
        {
            var major = int.Parse(this.Digit1.Data);
            var minor = int.Parse(this.Digit2.Data);
            return new Version(major, minor);
        }
    }
}
