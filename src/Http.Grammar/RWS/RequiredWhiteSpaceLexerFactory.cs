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
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException("spaceLexerFactory");
            }

            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException("horizontalTabLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.spaceLexerFactory = spaceLexerFactory;
            this.horizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        public ILexer<RequiredWhiteSpace> Create()
        {
            var sp = this.spaceLexerFactory.Create();
            var htab = this.horizontalTabLexerFactory.Create();
            var a = this.alternativeLexerFactory.Create(sp, htab);
            var innerLexer = this.repetitionLexerFactory.Create(a, 1, int.MaxValue);
            return new RequiredWhiteSpaceLexer(innerLexer);
        }
    }
}