namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class QueryLexerFactory : ILexerFactory<Query>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public QueryLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory, ITerminalLexerFactory terminalLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory), "Precondition: alternativeLexerFactory != null");
            }

            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory), "Precondition: pathCharacterLexerFactory != null");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory), "Precondition: repetitionLexerFactory != null");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Query> Create()
        {
            var alternativeLexer = alternativeLexerFactory.Create(
                pathCharacterLexerFactory.Create(),
                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"?", StringComparer.Ordinal));
            var fragmentRepetitionLexer = repetitionLexerFactory.Create(alternativeLexer, 0, int.MaxValue);
            return new QueryLexer(fragmentRepetitionLexer);
        }
    }
}