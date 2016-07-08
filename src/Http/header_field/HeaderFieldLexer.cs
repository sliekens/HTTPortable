using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.header_field
{
    public sealed class HeaderFieldLexer : CompositeLexer<Concatenation, HeaderField>
    {
        public HeaderFieldLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
