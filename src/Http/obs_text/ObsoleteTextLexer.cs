using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.obs_text
{
    public sealed class ObsoleteTextLexer : CompositeLexer<Terminal, ObsoleteText>
    {
        public ObsoleteTextLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
