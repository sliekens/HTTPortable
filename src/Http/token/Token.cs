using Txt.ABNF;

namespace Http.token
{
    public class Token : Repetition
    {
        public Token(Repetition repetition)
            : base(repetition)
        {
        }
    }
}