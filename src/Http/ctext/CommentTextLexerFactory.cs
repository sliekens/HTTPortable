using System;
using System.Text;
using Http.obs_text;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Http.ctext
{
    public class CommentTextLexerFactory : ILexerFactory<CommentText>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<ObsoleteText> obsoleteTextLexer;

        private readonly ILexer<Space> spaceLexer;

        private readonly IValueRangeLexerFactory valueRangeLexerFactory;

        public CommentTextLexerFactory(
            [NotNull] IValueRangeLexerFactory valueRangeLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<HorizontalTab> horizontalTabLexer,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<ObsoleteText> obsoleteTextLexer)
        {
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
            this.valueRangeLexerFactory = valueRangeLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.horizontalTabLexer = horizontalTabLexer;
            this.spaceLexer = spaceLexer;
            this.obsoleteTextLexer = obsoleteTextLexer;
        }

        public ILexer<CommentText> Create()
        {
            return
                new CommentTextLexer(
                    alternationLexerFactory.Create(
                        horizontalTabLexer,
                        spaceLexer,
                        valueRangeLexerFactory.Create(0x21, 0x27, Encoding.UTF8),
                        valueRangeLexerFactory.Create(0x2A, 0x5B, Encoding.UTF8),
                        valueRangeLexerFactory.Create(0x5D, 0x7E, Encoding.UTF8),
                        obsoleteTextLexer));
        }
    }
}
