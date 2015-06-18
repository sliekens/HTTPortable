namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class OptionalDelimitedListLexerFactory : ILexerFactory<OptionalDelimitedList>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Element> listItemLexerFactory;

        private readonly ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public OptionalDelimitedListLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            IStringLexerFactory stringLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Element> listItemLexerFactory)
        {
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException("optionalWhiteSpaceLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (listItemLexerFactory == null)
            {
                throw new ArgumentNullException("listItemLexerFactory");
            }

            this.optionLexerFactory = optionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.listItemLexerFactory = listItemLexerFactory;
        }

        public ILexer<OptionalDelimitedList> Create()
        {
            // element
            var element = this.listItemLexerFactory.Create();

            // ","
            var delim = this.stringLexerFactory.Create(@",");

            // OWS
            var ows = this.optionalWhiteSpaceLexerFactory.Create();

            // OWS element
            var innerLexer =
                this.optionLexerFactory.Create(
                    this.sequenceLexerFactory.Create(
                        this.alternativeLexerFactory.Create(delim, element),
                        this.repetitionLexerFactory.Create(
                            this.sequenceLexerFactory.Create(
                                ows,
                                delim,
                                this.optionLexerFactory.Create(this.sequenceLexerFactory.Create(ows, element))),
                            0,
                            int.MaxValue)));
            return new OptionalDelimitedListLexer(innerLexer);
        }
    }
}