using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_vchar
{
    public class FieldVisibleCharacter : Alternation
    {
        public FieldVisibleCharacter([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
