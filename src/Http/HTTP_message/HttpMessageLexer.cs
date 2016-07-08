using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_message
{
    public sealed class HttpMessageLexer : CompositeLexer<Concatenation, HttpMessage>
    {
        public HttpMessageLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
