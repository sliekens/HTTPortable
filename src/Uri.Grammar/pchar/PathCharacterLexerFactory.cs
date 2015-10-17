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
                throw new ArgumentNullException("unreservedLexerFactory", "Precondition: unreservedLexerFactory != null");
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException("percentEncodingLexerFactory", "Precondition: percentEncodingLexerFactory != null");
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException("subcomponentsDelimiterLexerFactory", "Precondition: subcomponentsDelimiterLexerFactory != null");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<PathCharacter> Create()
        {
            var pathCharacterAlternativeLexer = this.alternativeLexerFactory.Create(
                this.unreservedLexerFactory.Create(),
                this.percentEncodingLexerFactory.Create(),
                this.subcomponentsDelimiterLexerFactory.Create(),
                this.terminalLexerFactory.Create(@":"),
                this.terminalLexerFactory.Create(@"@"));
            return new PathCharacterLexer(pathCharacterAlternativeLexer);
        }
    }
}
