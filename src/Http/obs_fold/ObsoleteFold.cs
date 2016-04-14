using Txt.ABNF;

namespace Http.obs_fold
{
    public class ObsoleteFold : Concatenation
    {
        public ObsoleteFold(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}