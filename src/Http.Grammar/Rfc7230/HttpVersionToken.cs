using System;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class HttpVersionToken : Element
    {
        private readonly HttpNameToken httpName;
        private readonly Digit digit1;
        private readonly Digit digit2;

        public HttpVersionToken(HttpNameToken httpName, Digit digit1, Digit digit2, ITextContext context)
            : base(string.Concat(httpName.Data, "/", digit1.Data, ".", digit2.Data), context)
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

        public Version ToVersion()
        {
            var major = int.Parse(this.Digit1.Data);
            var minor = int.Parse(this.Digit2.Data);
            return new Version(major, minor);
        }
    }
}
