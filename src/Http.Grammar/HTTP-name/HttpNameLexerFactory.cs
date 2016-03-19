namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public class HttpNameLexerFactory : ILexerFactory<HttpName>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpNameLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<HttpName> Create()
        {
            var innerLexer = terminalLexerFactory.Create("HTTP", StringComparer.OrdinalIgnoreCase);
            return new HttpNameLexer(innerLexer);
        }
    }
}
