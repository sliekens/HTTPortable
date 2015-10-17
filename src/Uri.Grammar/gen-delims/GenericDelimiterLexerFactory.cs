namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class GenericDelimiterLexerFactory : ILexerFactory<GenericDelimiter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public GenericDelimiterLexerFactory(ITerminalLexerFactory terminalLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<GenericDelimiter> Create()
        {
            ILexer[] a =
                {
                    this.terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"[", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"]", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"@", StringComparer.Ordinal)
                };

            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var b = this.alternativeLexerFactory.Create(a);

            // gen-delims
            return new GenericDelimiterLexer(b);
        }
    }
}
