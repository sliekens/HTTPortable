using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HttpNameLexer : Lexer<HttpNameToken>
    {
        public override HttpNameToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HttpNameToken element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected HTTP-name");
        }

        public override bool TryRead(ITextScanner scanner, out HttpNameToken element)
        {
            var context = scanner.GetContext();

            // H
            if (!scanner.TryMatch('\u0048'))
            {
                element = default(HttpNameToken);
                return false;
            }

            // T
            if (!scanner.TryMatch('\u0054'))
            {
                scanner.PutBack('\u0048');
                element = default(HttpNameToken);
                return false;
            }

            // T
            if (!scanner.TryMatch('\u0054'))
            {
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0048');
                element = default(HttpNameToken);
                return false;
            }

            // P
            if (!scanner.TryMatch('\u0050'))
            {
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0048');
                element = default(HttpNameToken);
                return false;
            }

            element = new HttpNameToken(context);
            return true;
        }
    }
}