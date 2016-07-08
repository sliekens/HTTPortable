using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public sealed class OptionalDelimitedListLexer : CompositeLexer<Repetition, OptionalDelimitedList>
    {
        public OptionalDelimitedListLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
