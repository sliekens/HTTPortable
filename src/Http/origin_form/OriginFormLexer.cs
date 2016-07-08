using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.origin_form
{
    public sealed class OriginFormLexer : CompositeLexer<Concatenation, OriginForm>
    {
        public OriginFormLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
