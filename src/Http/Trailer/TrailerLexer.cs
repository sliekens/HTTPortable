using JetBrains.Annotations;
using Txt.Core;

namespace Http.Trailer
{
    public sealed class TrailerLexer : CompositeLexer<RequiredDelimitedList, Trailer>
    {
        public TrailerLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
