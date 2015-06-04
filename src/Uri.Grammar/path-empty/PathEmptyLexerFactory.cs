namespace Uri.Grammar.path_empty
{
    using System;

    using SLANG;

    public class PathEmptyLexerFactory : ILexerFactory<PathEmpty>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        public PathEmptyLexerFactory(IRepetitionLexerFactory repetitionLexerFactory, ILexerFactory<PathCharacter> pathCharacterLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory", "Precondition: repetitionLexerFactory != null");
            }

            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException("pathCharacterLexerFactory", "Precondition: pathCharacterLexerFactory != null");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
        }

        public ILexer<PathEmpty> Create()
        {
            var pathCharacterLexer = this.pathCharacterLexerFactory.Create();
            var repetitionLexer = this.repetitionLexerFactory.Create(pathCharacterLexer, 0, 0);
            return new PathEmptyLexer(repetitionLexer);
        }
    }
}
