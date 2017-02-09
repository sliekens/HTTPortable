using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.RWS
{
    public sealed class RequiredWhiteSpaceLexerFactory : RuleLexerFactory<RequiredWhiteSpace>
    {
        static RequiredWhiteSpaceLexerFactory()
        {
            Default = new RequiredWhiteSpaceLexerFactory(
                SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.HTAB.HorizontalTabLexerFactory.Default.Singleton());
        }

        public RequiredWhiteSpaceLexerFactory(
            [NotNull] ILexerFactory<Space> spaceFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (spaceFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceFactory));
            }
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            SpaceFactory = spaceFactory;
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        [NotNull]
        public static RequiredWhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Space> SpaceFactory { get; set; }

        public override ILexer<RequiredWhiteSpace> Create()
        {
            var innerLexer = Repetition.Create(
                Alternation.Create(SpaceFactory.Create(), HorizontalTabLexerFactory.Create()),
                1,
                int.MaxValue);
            return new RequiredWhiteSpaceLexer(innerLexer);
        }
    }
}
