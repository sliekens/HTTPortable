using System;
using Txt;
using Txt.ABNF;

namespace Http.HTTP_name
{
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
