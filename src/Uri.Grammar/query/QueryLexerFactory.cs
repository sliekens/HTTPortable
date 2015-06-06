namespace Uri.Grammar.query
{
    using System;

    using SLANG;

    public class QueryLexerFactory : ILexerFactory<Query>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public QueryLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory, IStringLexerFactory stringLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException("pathCharacterLexerFactory", "Precondition: pathCharacterLexerFactory != null");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory", "Precondition: repetitionLexerFactory != null");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
        }

        public ILexer<Query> Create()
        {
            var alternativeLexer = this.alternativeLexerFactory.Create(
                this.pathCharacterLexerFactory.Create(),
                this.stringLexerFactory.Create(@"/"),
                this.stringLexerFactory.Create(@"?"));
            var fragmentRepetitionLexer = this.repetitionLexerFactory.Create(alternativeLexer, 0, int.MaxValue);
            return new QueryLexer(fragmentRepetitionLexer);
        }
    }
}