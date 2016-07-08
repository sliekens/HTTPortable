using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.field_name
{
    public sealed class FieldNameLexer : CompositeLexer<Token, FieldName>
    {
        public FieldNameLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
