using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.request_target
{
    public sealed class RequestTargetLexer : CompositeLexer<Alternation, RequestTarget>
    {
        public RequestTargetLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
