using System;
using System.Text;
using Http.obs_text;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Http.qdtext
{
    public class QuotedTextLexerFactory : ILexerFactory<QuotedText>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<ObsoleteText> obsoleteTextLexer;

        private readonly ILexer<Space> spaceLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public QuotedTextLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<HorizontalTab> horizontalTabLexer,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<ObsoleteText> obsoleteTextLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (valueRangeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(valueRangeLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (horizontalTabLexer == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexer));
            }
            if (spaceLexer == null)
            {
                throw new ArgumentNullException(nameof(spaceLexer));
            }
            if (obsoleteTextLexer == null)
            {
                throw new ArgumentNullException(nameof(obsoleteTextLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.horizontalTabLexer = horizontalTabLexer;
            this.spaceLexer = spaceLexer;
            this.obsoleteTextLexer = obsoleteTextLexer;
        }

        public ILexer<QuotedText> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                horizontalTabLexer,
                spaceLexer,
                terminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                valueRangeLexerFactory.Create(0x23, 0x5B, Encoding.UTF8),
                valueRangeLexerFactory.Create(0x5D, 0x7E, Encoding.UTF8),
                obsoleteTextLexer);
            return new QuotedTextLexer(innerLexer);
        }
    }
}
