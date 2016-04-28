using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Http.RWS
{
    public class RequiredWhiteSpaceLexerFactory : ILexerFactory<RequiredWhiteSpace>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<HorizontalTab> horizontalTabLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Space> spaceLexerFactory;

        public RequiredWhiteSpaceLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<Space> spaceLexerFactory,
            ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
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
            this.alternationLexerFactory = alternationLexerFactory;
            this.spaceLexerFactory = spaceLexerFactory;
            this.horizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        public ILexer<RequiredWhiteSpace> Create()
        {
            var sp = spaceLexerFactory.Create();
            var htab = horizontalTabLexerFactory.Create();
            var a = alternationLexerFactory.Create(sp, htab);
            var innerLexer = repetitionLexerFactory.Create(a, 1, int.MaxValue);
            return new RequiredWhiteSpaceLexer(innerLexer);
        }
    }
}