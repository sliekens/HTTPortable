namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Token : Repetition
    {
        public Token(Repetition repetition)
            : base(repetition)
        {
        }
    }
}