using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.status_code
{
    public sealed class StatusCodeLexer : CompositeLexer<Repetition, StatusCode>
    {
        public StatusCodeLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
