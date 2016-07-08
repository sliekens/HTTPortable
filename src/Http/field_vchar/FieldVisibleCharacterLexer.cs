using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_vchar
{
    public sealed class FieldVisibleCharacterLexer : CompositeLexer<Alternation, FieldVisibleCharacter>
    {
        public FieldVisibleCharacterLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
