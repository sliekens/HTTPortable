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
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<Fragment> Create()
        {
            var alternativeLexer = alternativeLexerFactory.Create(
                pathCharacterLexerFactory.Create(),
                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                terminalLexerFactory.Create(@"?", StringComparer.Ordinal));
            var fragmentRepetitionLexer = repetitionLexerFactory.Create(alternativeLexer, 0, int.MaxValue);
            return new FragmentLexer(fragmentRepetitionLexer);
        }
    }
}