namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class HttpVersionLexer : Lexer<HttpVersion>
    {
        private readonly ILexer<Digit> digitLexer;
        private readonly ILexer<HttpName> httpNameLexer;

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
            if (scanner.EndOfInput)
            {
                element = default(HttpVersion);
                return false;
            }

            var context = scanner.GetContext();
            HttpName httpName;
            if (!this.httpNameLexer.TryRead(scanner, out httpName))
            {
                element = default(HttpVersion);
                return false;
            }

            Element slash;
            if (!TryReadTerminal(scanner, '/', out slash))
            {
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            Digit versionMajor;
            if (!this.digitLexer.TryRead(scanner, out versionMajor))
            {
                scanner.PutBack(slash.Data);
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            Element decimalSeparator;
            if (!TryReadTerminal(scanner, '.', out decimalSeparator))
            {
                scanner.PutBack(versionMajor.Data);
                scanner.PutBack(slash.Data);
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            Digit versionMinor;
            if (!this.digitLexer.TryRead(scanner, out versionMinor))
            {
                scanner.PutBack(decimalSeparator.Data);
                scanner.PutBack(versionMajor.Data);
                scanner.PutBack(slash.Data);
                scanner.PutBack(httpName.Data);
                element = default(HttpVersion);
                return false;
            }

            element = new HttpVersion(httpName, slash, versionMajor, decimalSeparator, versionMinor, context);
            return true;
        }
    }
}