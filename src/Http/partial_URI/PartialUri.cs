using Txt.ABNF;

namespace Http.partial_URI
{
    public class PartialUri : Concatenation
    {
        public PartialUri(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}