using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Http.RWS
{
    public class RequiredWhiteSpaceLexerFactory : ILexerFactory<RequiredWhiteSpace>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Space> spaceLexer;

        public RequiredWhiteSpaceLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<HorizontalTab> horizontalTabLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (spaceLexer == null)
            {
                throw new ArgumentNullException(nameof(spaceLexer));
            }
            if (horizontalTabLexer == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        public ILexer<RequiredWhiteSpace> Create()
        {
            var innerLexer =
                repetitionLexerFactory.Create(
                    alternationLexerFactory.Create(spaceLexer, horizontalTabLexer),
                    1,
                    int.MaxValue);
            return new RequiredWhiteSpaceLexer(innerLexer);
        }
    }
}
