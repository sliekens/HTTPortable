using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_version
{
    public sealed class HttpVersionLexer : CompositeLexer<Concatenation, HttpVersion>
    {
        public HttpVersionLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
