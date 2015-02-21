using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HttpNameLexer : Lexer<HttpName>
    {
        public override HttpName Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HttpName element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected HTTP-name");
        }

        public override bool TryRead(ITextScanner scanner, out HttpName element)
        {
            var context = scanner.GetContext();

            // H
            if (!scanner.TryMatch('\u0048'))
            {
                element = default(HttpName);
                return false;
            }

            // T
            if (!scanner.TryMatch('\u0054'))
            {
                scanner.PutBack('\u0048');
                element = default(HttpName);
                return false;
            }

            // T
            if (!scanner.TryMatch('\u0054'))
            {
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0048');
                element = default(HttpName);
                return false;
            }

            // P
            if (!scanner.TryMatch('\u0050'))
            {
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0054');
                scanner.PutBack('\u0048');
                element = default(HttpName);
                return false;
            }

            element = new HttpName(context);
            return true;
        }
    }
}