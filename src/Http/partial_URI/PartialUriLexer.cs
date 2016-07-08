using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.partial_URI
{
    public sealed class PartialUriLexer : CompositeLexer<Concatenation, PartialUri>
    {
        public PartialUriLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
