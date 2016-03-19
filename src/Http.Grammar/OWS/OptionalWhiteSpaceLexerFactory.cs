namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class OptionalWhiteSpaceLexerFactory : ILexerFactory<OptionalWhiteSpace>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<WhiteSpace> whiteSpaceLexerFactory;

        public OptionalWhiteSpaceLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<WhiteSpace> whiteSpaceLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.whiteSpaceLexerFactory = whiteSpaceLexerFactory;
        }

        public ILexer<OptionalWhiteSpace> Create()
        {
            var wsp = whiteSpaceLexerFactory.Create();
            var innerLexer = repetitionLexerFactory.Create(wsp, 0, int.MaxValue);
            return new OptionalWhiteSpaceLexer(innerLexer);
        }
    }
}