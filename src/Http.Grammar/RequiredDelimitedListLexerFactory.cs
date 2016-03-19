namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RequiredDelimitedListLexerFactory : IRequiredDelimitedListLexerFactory
    {
        private readonly ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IConcatenationLexerFactory ConcatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RequiredDelimitedListLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IConcatenationLexerFactory ConcatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (ConcatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ConcatenationLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.ConcatenationLexerFactory = ConcatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
        }

        public ILexer<RequiredDelimitedList> Create(ILexer lexer)
        {
            var delim = this.terminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = this.optionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                this.ConcatenationLexerFactory.Create(
                    this.repetitionLexerFactory.Create(this.ConcatenationLexerFactory.Create(delim, ows), 0, int.MaxValue),
                    lexer,
                    this.repetitionLexerFactory.Create(
                        this.ConcatenationLexerFactory.Create(
                            ows,
                            delim,
                            this.optionLexerFactory.Create(this.ConcatenationLexerFactory.Create(ows, lexer))),
                        0,
                        int.MaxValue));

            return new RequiredDelimitedListLexer(innerLexer);
        }
    }
}