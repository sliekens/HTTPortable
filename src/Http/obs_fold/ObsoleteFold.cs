using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.obs_fold
{
    public class ObsoleteFold : Concatenation
    {
        public ObsoleteFold([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
