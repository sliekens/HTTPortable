using System;
using Http.obs_text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.VCHAR;
using Txt.Core;

namespace Http.quoted_pair
{
    public sealed class QuotedPairLexerFactory : RuleLexerFactory<QuotedPair>
    {
        static QuotedPairLexerFactory()
        {
            Default = new QuotedPairLexerFactory(
                Txt.ABNF.Core.HTAB.HorizontalTabLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.VCHAR.VisibleCharacterLexerFactory.Default.Singleton(),
                obs_text.ObsoleteTextLexerFactory.Default.Singleton());
        }

        public QuotedPairLexerFactory(
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory,
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<VisibleCharacter> visibleCharacterLexerFactory,
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
            if (visibleCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(visibleCharacterLexerFactory));
            }
            if (obsoleteTextLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(obsoleteTextLexerFactory));
            }
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
            VisibleCharacterLexerFactory = visibleCharacterLexerFactory;
            ObsoleteTextLexerFactory = obsoleteTextLexerFactory;
        }

        [NotNull]
        public static QuotedPairLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<ObsoleteText> ObsoleteTextLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<VisibleCharacter> VisibleCharacterLexerFactory { get; set; }

        public override ILexer<QuotedPair> Create()
        {
            var htab = HorizontalTabLexerFactory.Create();
            var sp = SpaceLexerFactory.Create();
            var vchar = VisibleCharacterLexerFactory.Create();
            var obsText = ObsoleteTextLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                Terminal.Create(@"\", StringComparer.Ordinal),
                Alternation.Create(htab, sp, vchar, obsText));
            return new QuotedPairLexer(innerLexer);
        }
    }
}
