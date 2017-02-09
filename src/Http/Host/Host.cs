using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.Host
{
    public class Host : Concatenation
    {
        public Host([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
