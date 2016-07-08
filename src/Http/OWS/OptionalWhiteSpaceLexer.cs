using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.OWS
{
    public sealed class OptionalWhiteSpaceLexer : CompositeLexer<Repetition, OptionalWhiteSpace>
    {
        public OptionalWhiteSpaceLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
