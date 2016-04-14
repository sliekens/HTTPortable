using Txt.ABNF;

namespace Http.Host
{
    public class Host : Concatenation
    {
        public Host(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}