namespace Http.Grammar
{
    using System;

    using TextFx;

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
            var innerLexer = this.terminalLexerFactory.Create(@"*", StringComparer.Ordinal);
            return new AsteriskFormLexer(innerLexer);
        }
    }
}