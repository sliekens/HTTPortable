using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_name
{
    public class HttpNameLexerFactory : ILexerFactory<HttpName>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpNameLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
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
