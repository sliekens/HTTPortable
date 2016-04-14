using Txt.ABNF;

namespace Http.origin_form
{
    public class OriginForm : Concatenation
    {
        public OriginForm(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}