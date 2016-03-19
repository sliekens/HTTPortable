namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RegisteredNameLexerFactory : ILexerFactory<RegisteredName>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public RegisteredNameLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(unreservedLexerFactory));
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(percentEncodingLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        public ILexer<RegisteredName> Create()
        {
            ILexer[] a =
                {
                    unreservedLexerFactory.Create(), percentEncodingLexerFactory.Create(),
                    subcomponentsDelimiterLexerFactory.Create()
                };

            var b = alternativeLexerFactory.Create(a);

            var c = repetitionLexerFactory.Create(b, 0, int.MaxValue);

            return new RegisteredNameLexer(c);
        }
    }
}