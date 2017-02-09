using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_content
{
    public class FieldContent : Concatenation
    {
        public FieldContent([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
