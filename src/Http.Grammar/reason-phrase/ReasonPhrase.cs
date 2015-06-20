namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ReasonPhrase : Repetition
    {
        public ReasonPhrase(Repetition repetition)
            : base(repetition)
        {
        }
    }
}