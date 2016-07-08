using JetBrains.Annotations;
using Txt.Core;

namespace Http.Via
{
    public sealed class ViaLexer : CompositeLexer<RequiredDelimitedList, Via>
    {
        public ViaLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
