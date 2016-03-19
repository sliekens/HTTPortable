namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathCharacterLexerFactory : ILexerFactory<PathCharacter>
    {
        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public PathCharacterLexerFactory(ILexerFactory<Unreserved> unreservedLexerFactory, ILexerFactory<PercentEncoding> percentEncodingLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, ITerminalLexerFactory terminalLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexerFactory));
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(percentEncodingLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<PathCharacter> Create()
        {
            var pathCharacterAlternativeLexer = alternativeLexerFactory.Create(
                unreservedLexerFactory.Create(),
                percentEncodingLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create(),
                terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"@", StringComparer.Ordinal));
            return new PathCharacterLexer(pathCharacterAlternativeLexer);
        }
    }
}
