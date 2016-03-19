namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class RequiredWhiteSpaceLexerFactory : ILexerFactory<RequiredWhiteSpace>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<HorizontalTab> horizontalTabLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Space> spaceLexerFactory;

        public RequiredWhiteSpaceLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Space> spaceLexerFactory,
            ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }

            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.spaceLexerFactory = spaceLexerFactory;
            this.horizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        public ILexer<RequiredWhiteSpace> Create()
        {
            var sp = spaceLexerFactory.Create();
            var htab = horizontalTabLexerFactory.Create();
            var a = alternativeLexerFactory.Create(sp, htab);
            var innerLexer = repetitionLexerFactory.Create(a, 1, int.MaxValue);
            return new RequiredWhiteSpaceLexer(innerLexer);
        }
    }
}