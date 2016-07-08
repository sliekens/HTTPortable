using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.https_URI
{
    public sealed class HttpsUriLexer : CompositeLexer<Concatenation, HttpsUri>
    {
        public HttpsUriLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
