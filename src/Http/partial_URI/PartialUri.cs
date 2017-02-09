using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.partial_URI
{
    public class PartialUri : Concatenation
    {
        public PartialUri([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}