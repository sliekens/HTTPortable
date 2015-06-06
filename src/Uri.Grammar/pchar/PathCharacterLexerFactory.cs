namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PathCharacterLexerFactory : ILexerFactory<PathCharacter>
    {
        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public PathCharacterLexerFactory(ILexerFactory<Unreserved> unreservedLexerFactory, ILexerFactory<PercentEncoding> percentEncodingLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, IStringLexerFactory stringLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
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

            if (stringLexerFactory == null)
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
            this.stringLexerFactory = stringLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<PathCharacter> Create()
        {
            var pathCharacterAlternativeLexer = this.alternativeLexerFactory.Create(
                this.unreservedLexerFactory.Create(),
                this.percentEncodingLexerFactory.Create(),
                this.subcomponentsDelimiterLexerFactory.Create(),
                this.stringLexerFactory.Create(@":"),
                this.stringLexerFactory.Create(@"@"));
            return new PathCharacterLexer(pathCharacterAlternativeLexer);
        }
    }
}
