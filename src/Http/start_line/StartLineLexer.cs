using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.start_line
{
    public sealed class StartLineLexer : CompositeLexer<Alternation, StartLine>
    {
        public StartLineLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
