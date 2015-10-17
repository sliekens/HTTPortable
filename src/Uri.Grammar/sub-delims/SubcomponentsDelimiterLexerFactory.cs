namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SubcomponentsDelimiterLexerFactory : ILexerFactory<SubcomponentsDelimiter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public SubcomponentsDelimiterLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "alternativeLexerFactory",
                    "Precondition: alternativeLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<SubcomponentsDelimiter> Create()
        {
            ILexer[] a =
                {
                    this.terminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"$", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"&", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"'", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"(", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@")", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"*", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@",", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@";", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"=", StringComparer.Ordinal)
                };

            // "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            var b = this.alternativeLexerFactory.Create(a);

            // sub-delims
            return new SubcomponentsDelimiterLexer(b);
        }
    }
}