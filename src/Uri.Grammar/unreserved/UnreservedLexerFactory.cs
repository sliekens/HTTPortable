namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class UnreservedLexerFactory : ILexerFactory<Unreserved>
    {
        private readonly ILexerFactory<Alpha> alphaLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public UnreservedLexerFactory(
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory), "Precondition: alphaLexerFactory != null");
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory), "Precondition: digitLexerFactory != null");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory), "Precondition: terminalLexerFactory != null");
            }

            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory), "Precondition: alphaLexerFactory != null");
            }

            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<Unreserved> Create()
        {
            var unreservedAlternativeLexer = alternativeLexerFactory.Create(
                alphaLexerFactory.Create(),
                digitLexerFactory.Create(),
                terminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                terminalLexerFactory.Create(@".", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"_", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"~", StringComparer.Ordinal));
            return new UnreservedLexer(unreservedAlternativeLexer);
        }
    }
}