using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.reason_phrase
{
    public sealed class ReasonPhraseLexer : CompositeLexer<Repetition, ReasonPhrase>
    {
        public ReasonPhraseLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
