using System;
using Http.obs_text;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.VCHAR;

namespace Http.quoted_pair
{
    public class QuotedPairLexerFactory : ILexerFactory<QuotedPair>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<ObsoleteText> obsoleteTextLexer;

        private readonly ILexer<Space> spaceLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<VisibleCharacter> visibleCharacterLexer;

        public QuotedPairLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<HorizontalTab> horizontalTabLexer,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<VisibleCharacter> visibleCharacterLexer,
            [NotNull] ILexer<ObsoleteText> obsoleteTextLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (horizontalTabLexer == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexer));
            }
            if (spaceLexer == null)
            {
                throw new ArgumentNullException(nameof(spaceLexer));
            }
            if (visibleCharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(visibleCharacterLexer));
            }
            if (obsoleteTextLexer == null)
            {
                throw new ArgumentNullException(nameof(obsoleteTextLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.horizontalTabLexer = horizontalTabLexer;
            this.spaceLexer = spaceLexer;
            this.visibleCharacterLexer = visibleCharacterLexer;
            this.obsoleteTextLexer = obsoleteTextLexer;
        }

        public ILexer<QuotedPair> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                terminalLexerFactory.Create(@"\", StringComparer.Ordinal),
                alternationLexerFactory.Create(horizontalTabLexer, spaceLexer, visibleCharacterLexer, obsoleteTextLexer));
            return new QuotedPairLexer(innerLexer);
        }
    }
}
