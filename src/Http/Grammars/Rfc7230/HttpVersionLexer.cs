using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammars.Rfc7230
{
    public class HttpVersionLexer : Lexer<HttpVersionToken>
    {
        private readonly ILexer<HttpNameToken> httpNameLexer;
        private readonly ILexer<DigitToken> digitLexer;

        public HttpVersionLexer()
            : this(new HttpNameLexer(), new DigitLexer())
        {
        }

        public HttpVersionLexer(ILexer<HttpNameToken> httpNameLexer, ILexer<DigitToken> digitLexer)
        {
            this.httpNameLexer = httpNameLexer;
            this.digitLexer = digitLexer;
        }

        public override HttpVersionToken Read(ITextScanner scanner)
        {
            HttpNameToken httpName;
            DigitToken digit1;
            DigitToken digit2;
            var context = scanner.GetContext();
            try
            {
                httpName = this.httpNameLexer.Read(scanner);
                var slashContext = scanner.GetContext();
                if (!scanner.TryMatch('/'))
                {
                    throw new SyntaxErrorException(slashContext, "Expected '/'");
                }

                digit1 = this.digitLexer.Read(scanner);
                var dotContext = scanner.GetContext();
                if (!scanner.TryMatch('.'))
                {
                    throw new SyntaxErrorException(dotContext, "Expected '.'");
                }

                digit2 = this.digitLexer.Read(scanner);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'HTTP-version'", syntaxErrorException);
            }

            return new HttpVersionToken(httpName, digit1, digit2, context);
        }

        public override bool TryRead(ITextScanner scanner, out HttpVersionToken token)
        {
            HttpNameToken httpName;
            DigitToken digit1;
            DigitToken digit2;
            var context = scanner.GetContext();
            if (!this.httpNameLexer.TryRead(scanner, out httpName))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!scanner.TryMatch('/'))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit1))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!scanner.TryMatch('.'))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit2))
            {
                token = default(HttpVersionToken);
                return false;
            }

            token = new HttpVersionToken(httpName, digit1, digit2, context);
            return true;
        }
    }
}
