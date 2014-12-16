using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammars.Rfc7230
{
    public class HttpVersionLexer : Lexer<HttpVersionToken>
    {
        private readonly ILexer<HttpNameToken> httpNameLexer;
        private readonly ILexer<DigitToken> digitLexer;

        public HttpVersionLexer(ITextScanner scanner)
            : this(scanner, new HttpNameLexer(scanner), new DigitLexer(scanner))
        {
        }

        public HttpVersionLexer(ITextScanner scanner, ILexer<HttpNameToken> httpNameLexer, ILexer<DigitToken> digitLexer)
            : base(scanner)
        {
            this.httpNameLexer = httpNameLexer;
            this.digitLexer = digitLexer;
        }

        public override HttpVersionToken Read()
        {
            HttpNameToken httpName;
            DigitToken digit1;
            DigitToken digit2;
            var context = this.Scanner.GetContext();
            try
            {
                httpName = this.httpNameLexer.Read();
                var slashContext = this.Scanner.GetContext();
                if (!this.Scanner.TryMatch('/'))
                {
                    throw new SyntaxErrorException("Expected '/'", slashContext);
                }

                digit1 = this.digitLexer.Read();
                var dotContext = this.Scanner.GetContext();
                if (!this.Scanner.TryMatch('.'))
                {
                    throw new SyntaxErrorException("Expected '.'", slashContext);
                }

                digit2 = this.digitLexer.Read();
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException("Expected 'HTTP-version'", syntaxErrorException, context);
            }

            return new HttpVersionToken(httpName, digit1, digit2, context);
        }

        public override bool TryRead(out HttpVersionToken token)
        {
            HttpNameToken httpName;
            DigitToken digit1;
            DigitToken digit2;
            var context = this.Scanner.GetContext();
            if (!this.httpNameLexer.TryRead(out httpName))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!this.Scanner.TryMatch('/'))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!this.digitLexer.TryRead(out digit1))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!this.Scanner.TryMatch('.'))
            {
                token = default(HttpVersionToken);
                return false;
            }

            if (!this.digitLexer.TryRead(out digit2))
            {
                token = default(HttpVersionToken);
                return false;
            }

            token = new HttpVersionToken(httpName, digit1, digit2, context);
            return true;
        }
    }
}
