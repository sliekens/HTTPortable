using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.obs_fold
{
    public sealed class ObsoleteFoldLexerFactory : RuleLexerFactory<ObsoleteFold>
    {
        static ObsoleteFoldLexerFactory()
        {
            Default = new ObsoleteFoldLexerFactory(
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.HTAB.HorizontalTabLexerFactory.Default.Singleton());
        }

        public ObsoleteFoldLexerFactory(
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory,
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            NewLineLexerFactory = newLineLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        [NotNull]
        public static ObsoleteFoldLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; set; }

        public override ILexer<ObsoleteFold> Create()
        {
            var innerLexer = Concatenation.Create(
                NewLineLexerFactory.Create(),
                Repetition.Create(
                    Alternation.Create(SpaceLexerFactory.Create(), HorizontalTabLexerFactory.Create()),
                    1,
                    int.MaxValue));
            return new ObsoleteFoldLexer(innerLexer);
        }
    }
}
