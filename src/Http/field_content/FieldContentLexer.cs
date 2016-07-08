using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_content
{
    public sealed class FieldContentLexer : CompositeLexer<Concatenation, FieldContent>
    {
        public FieldContentLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
