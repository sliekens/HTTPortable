namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class FragmentLexerFactory : ILexerFactory<Fragment>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public FragmentLexerFactory(IAlternativeLexerFactory alternativeLexerFactory, ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory, ITerminalLexerFactory terminalLexerFactory)
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

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Fragment> Create()
        {
            var alternativeLexer = this.alternativeLexerFactory.Create(
                this.pathCharacterLexerFactory.Create(),
                this.terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                this.terminalLexerFactory.Create(@"?", StringComparer.Ordinal));
            var fragmentRepetitionLexer = this.repetitionLexerFactory.Create(alternativeLexer, 0, int.MaxValue);
            return new FragmentLexer(fragmentRepetitionLexer);
        }
    }
}