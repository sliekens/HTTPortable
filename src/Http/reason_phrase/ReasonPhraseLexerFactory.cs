using System;
using Http.obs_text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.ABNF.Core.VCHAR;
using Txt.Core;

namespace Http.reason_phrase
{
    public sealed class ReasonPhraseLexerFactory : RuleLexerFactory<ReasonPhrase>
    {
        static ReasonPhraseLexerFactory()
        {
            Default = new ReasonPhraseLexerFactory(
                Txt.ABNF.Core.HTAB.HorizontalTabLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.VCHAR.VisibleCharacterLexerFactory.Default.Singleton(),
                obs_text.ObsoleteTextLexerFactory.Default.Singleton());
        }

        public ReasonPhraseLexerFactory(
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
        public static ReasonPhraseLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; }

        [NotNull]
        public ILexerFactory<ObsoleteText> ObsoleteTextLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; }

        [NotNull]
        public ILexerFactory<VisibleCharacter> VisibleCharacterLexerFactory { get; }

        public override ILexer<ReasonPhrase> Create()
        {
            var innerLexer = Repetition.Create(
                Alternation.Create(
                    HorizontalTabLexerFactory.Create(),
                    SpaceLexerFactory.Create(),
                    VisibleCharacterLexerFactory.Create(),
                    ObsoleteTextLexerFactory.Create()),
                0,
                int.MaxValue);
            return new ReasonPhraseLexer(innerLexer);
        }
    }
}
