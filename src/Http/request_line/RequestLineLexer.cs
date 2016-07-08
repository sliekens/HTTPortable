using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.request_line
{
    public sealed class RequestLineLexer : CompositeLexer<Concatenation, RequestLine>
    {
        public RequestLineLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
