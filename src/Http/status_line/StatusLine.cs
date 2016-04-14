using Txt.ABNF;

namespace Http.status_line
{
    public class StatusLine : Concatenation
    {
        public StatusLine(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
