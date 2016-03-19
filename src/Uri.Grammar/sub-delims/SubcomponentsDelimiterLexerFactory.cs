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
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(alternativeLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<SubcomponentsDelimiter> Create()
        {
            ILexer[] a =
                {
                    terminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"$", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"&", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"'", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"(", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@")", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"*", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@",", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@";", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"=", StringComparer.Ordinal)
                };

            // "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
            var b = alternativeLexerFactory.Create(a);

            // sub-delims
            return new SubcomponentsDelimiterLexer(b);
        }
    }
}