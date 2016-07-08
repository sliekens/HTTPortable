using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_value
{
    public sealed class FieldValueLexer : CompositeLexer<Repetition, FieldValue>
    {
        public FieldValueLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
