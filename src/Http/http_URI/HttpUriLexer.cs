using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.http_URI
{
    public sealed class HttpUriLexer : CompositeLexer<Concatenation, HttpUri>
    {
        public HttpUriLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
