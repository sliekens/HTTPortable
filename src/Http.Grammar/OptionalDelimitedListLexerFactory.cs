namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class OptionalDelimitedListLexerFactory : IOptionalDelimitedListLexerFactory
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IConcatenationLexerFactory ConcatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public OptionalDelimitedListLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            IConcatenationLexerFactory ConcatenationLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (ConcatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ConcatenationLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            this.optionLexerFactory = optionLexerFactory;
            this.ConcatenationLexerFactory = ConcatenationLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
        }

        public ILexer<OptionalDelimitedList> Create(ILexer lexer)
        {
            var delim = terminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = optionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                optionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        alternativeLexerFactory.Create(delim, lexer),
                        repetitionLexerFactory.Create(
                            ConcatenationLexerFactory.Create(
                                ows,
                                delim,
                                optionLexerFactory.Create(ConcatenationLexerFactory.Create(ows, lexer))),
                            0,
                            int.MaxValue)));
            return new OptionalDelimitedListLexer(innerLexer);
        }
    }
}