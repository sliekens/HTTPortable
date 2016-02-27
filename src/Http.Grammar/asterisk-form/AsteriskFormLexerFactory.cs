namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public class AsteriskFormLexerFactory : ILexerFactory<AsteriskForm>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AsteriskFormLexerFactory(ITerminalLexerFactory terminalLexerFactory)
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
