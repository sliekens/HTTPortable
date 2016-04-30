using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Http.obs_fold
{
    public class ObsoleteFoldLexerFactory : ILexerFactory<ObsoleteFold>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Space> spaceLexer;

        public ObsoleteFoldLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<NewLine> newLineLexer,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<HorizontalTab> horizontalTabLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
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
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.newLineLexer = newLineLexer;
            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        public ILexer<ObsoleteFold> Create()
        {
            return
                new ObsoleteFoldLexer(
                    concatenationLexerFactory.Create(
                        newLineLexer,
                        repetitionLexerFactory.Create(
                            alternationLexerFactory.Create(spaceLexer, horizontalTabLexer),
                            1,
                            int.MaxValue)));
        }
    }
}
