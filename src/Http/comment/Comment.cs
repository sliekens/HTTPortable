using Txt.ABNF;

namespace Http.comment
{
    public class Comment : Concatenation
    {
        public Comment(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}