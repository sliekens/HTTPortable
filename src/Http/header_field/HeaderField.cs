using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.header_field
{
    public class HeaderField : Concatenation
    {
        public HeaderField([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
