using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.trailer_part
{
    public sealed class TrailerPartLexer : CompositeLexer<Repetition, TrailerPart>
    {
        public TrailerPartLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
