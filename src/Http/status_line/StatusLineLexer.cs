using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.status_line
{
    public sealed class StatusLineLexer : CompositeLexer<Concatenation, StatusLine>
    {
        public StatusLineLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
