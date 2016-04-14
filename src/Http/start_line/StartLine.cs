using Txt.ABNF;

namespace Http.start_line
{
    public class StartLine : Alternative
    {
        public StartLine(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
