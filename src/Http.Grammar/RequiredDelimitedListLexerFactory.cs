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

        private readonly IStringLexerFactory stringLexerFactory;

        public RequiredDelimitedListLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            IStringLexerFactory stringLexerFactory,
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

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException("optionalWhiteSpaceLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
        }

        public ILexer<RequiredDelimitedList> Create(ILexer lexer)
        {
            var delim = this.stringLexerFactory.Create(@",");
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