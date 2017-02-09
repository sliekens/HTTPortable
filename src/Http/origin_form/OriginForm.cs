using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.origin_form
{
    public class OriginForm : Concatenation
    {
        public OriginForm([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
