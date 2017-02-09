using System;
using System.Text;
using Http.obs_text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.ctext
{
    public sealed class CommentTextLexerFactory : RuleLexerFactory<CommentText>
    {
        static CommentTextLexerFactory()
        {
            Default = new CommentTextLexerFactory(
                Txt.ABNF.Core.HTAB.HorizontalTabLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                obs_text.ObsoleteTextLexerFactory.Default.Singleton());
        }

        public CommentTextLexerFactory(
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory,
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<ObsoleteText> obsoleteTextLexerFactory)
        {
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (obsoleteTextLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(obsoleteTextLexerFactory));
            }
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
            ObsoleteTextLexerFactory = obsoleteTextLexerFactory;
        }

        [NotNull]
        public static CommentTextLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<ObsoleteText> ObsoleteTextLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; set; }

        public override ILexer<CommentText> Create()
        {
            var innerLexer = Alternation.Create(
                HorizontalTabLexerFactory.Create(),
                SpaceLexerFactory.Create(),
                ValueRange.Create(0x21, 0x27, Encoding.UTF8),
                ValueRange.Create(0x2A, 0x5B, Encoding.UTF8),
                ValueRange.Create(0x5D, 0x7E, Encoding.UTF8),
                ObsoleteTextLexerFactory.Create());
            return new CommentTextLexer(innerLexer);
        }
    }
}
