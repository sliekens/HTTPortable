using Txt.ABNF;

namespace Http.start_line
{
    public class StartLine : Alternation
    {
        public StartLine(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
