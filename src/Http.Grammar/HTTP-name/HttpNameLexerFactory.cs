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
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<HttpName> Create()
        {
            var innerLexer = this.terminalLexerFactory.Create("\x48\x54\x54\x50");
            return new HttpNameLexer(innerLexer);
        }
    }
}