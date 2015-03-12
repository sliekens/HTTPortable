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
            if (scanner.EndOfInput)
            {
                element = default(HttpName);
                return false;
            }

            var context = scanner.GetContext();
            Element terminal;
            if (TryReadTerminal(scanner, new[] { '\x48', '\x54', '\x54', '\x50' }, out terminal))
            {
                element = new HttpName(terminal, context);
                return true;
            }

            element = default(HttpName);
            return true;
        }
    }
}