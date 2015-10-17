namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HttpNameLexerFactory : ILexerFactory<HttpName>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpNameLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<HttpName> Create()
        {
            var innerLexer = this.terminalLexerFactory.Create("HTTP", StringComparer.OrdinalIgnoreCase);
            return new HttpNameLexer(innerLexer);
        }
    }
}