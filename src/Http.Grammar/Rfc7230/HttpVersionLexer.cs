using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class HttpVersionLexer : Lexer<HttpVersion>
    {
        private readonly ILexer<HttpName> httpNameLexer;
        private readonly ILexer<Digit> digitLexer;

        public HttpVersionLexer()
            : this(new HttpNameLexer(), new DigitLexer())
        {
        }

        public HttpVersionLexer(ILexer<HttpName> httpNameLexer, ILexer<Digit> digitLexer)
            : base("HTTP-version")
        {
            this.httpNameLexer = httpNameLexer;
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HttpVersion element)
        {
            HttpName httpName;
            Digit digit1;
            Digit digit2;
            var context = scanner.GetContext();
            if (!this.httpNameLexer.TryRead(scanner, out httpName))
            {
                element = default(HttpVersion);
                return false;
            }

            if (!scanner.TryMatch('/'))
            {
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit1))
            {
                scanner.PutBack('/');
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            if (!scanner.TryMatch('.'))
            {
                scanner.PutBack(digit1.Data);
                scanner.PutBack('/');
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit2))
            {
                scanner.PutBack('.');
                scanner.PutBack(digit1.Data);
                scanner.PutBack('/');
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            element = new HttpVersion(httpName, digit1, digit2, context);
            return true;
        }
    }
}
