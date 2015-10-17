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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RequiredDelimitedListLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException("optionalWhiteSpaceLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
        }

        public ILexer<RequiredDelimitedList> Create(ILexer lexer)
        {
            var delim = this.terminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = this.optionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                this.sequenceLexerFactory.Create(
                    this.repetitionLexerFactory.Create(this.sequenceLexerFactory.Create(delim, ows), 0, int.MaxValue),
                    lexer,
                    this.repetitionLexerFactory.Create(
                        this.sequenceLexerFactory.Create(
                            ows,
                            delim,
                            this.optionLexerFactory.Create(this.sequenceLexerFactory.Create(ows, lexer))),
                        0,
                        int.MaxValue));

            return new RequiredDelimitedListLexer(innerLexer);
        }
    }
}