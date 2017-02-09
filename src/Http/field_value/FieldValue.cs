using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_value
{
    public class FieldValue : Repetition
    {
        public FieldValue([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
