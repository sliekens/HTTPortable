using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.comment
{
    public class Comment : Concatenation
    {
        public Comment([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
