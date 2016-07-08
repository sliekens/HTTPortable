using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.obs_fold
{
    public sealed class ObsoleteFoldLexer : CompositeLexer<Concatenation, ObsoleteFold>
    {
        public ObsoleteFoldLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
