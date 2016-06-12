using Http.obs_text;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.VCHAR;
using Txt.Core;

namespace Http.reason_phrase
{
    public class ReasonPhraseLexerFactory : ILexerFactory<ReasonPhrase>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<ObsoleteText> obsoleteTextLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Space> spaceLexer;

        private readonly ILexer<VisibleCharacter> visibleCharacterLexer;

        public ReasonPhraseLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ILexer<HorizontalTab> horizontalTabLexer,
            ILexer<Space> spaceLexer,
            ILexer<VisibleCharacter> visibleCharacterLexer,
            ILexer<ObsoleteText> obsoleteTextLexer)
        {
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.horizontalTabLexer = horizontalTabLexer;
            this.spaceLexer = spaceLexer;
            this.visibleCharacterLexer = visibleCharacterLexer;
            this.obsoleteTextLexer = obsoleteTextLexer;
        }

        public ILexer<ReasonPhrase> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(
                alternationLexerFactory.Create(
                    horizontalTabLexer,
                    spaceLexer,
                    visibleCharacterLexer,
                    obsoleteTextLexer),
                0,
                int.MaxValue);
            return new ReasonPhraseLexer(innerLexer);
        }
    }
}
