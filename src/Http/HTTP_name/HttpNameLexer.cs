using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_name
{
    public sealed class HttpNameLexer : CompositeLexer<Terminal, HttpName>
    {
        public HttpNameLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
