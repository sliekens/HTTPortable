using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.protocol
{
    public class Protocol : Concatenation
    {
        public Protocol([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
