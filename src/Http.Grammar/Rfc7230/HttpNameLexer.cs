namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class HttpNameLexer : Lexer<HttpName>
    {
        public HttpNameLexer()
            : base("HTTP-name")
        {
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