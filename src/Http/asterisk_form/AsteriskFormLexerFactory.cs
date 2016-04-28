using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.asterisk_form
{
    public class AsteriskFormLexerFactory : ILexerFactory<AsteriskForm>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AsteriskFormLexerFactory([NotNull] ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<AsteriskForm> Create()
        {
            var innerLexer = terminalLexerFactory.Create(@"*", StringComparer.Ordinal);
            return new AsteriskFormLexer(innerLexer);
        }
    }
}
