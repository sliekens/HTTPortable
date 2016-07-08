using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public sealed class RequiredDelimitedListLexer : CompositeLexer<Concatenation, RequiredDelimitedList>
    {
        public RequiredDelimitedListLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
