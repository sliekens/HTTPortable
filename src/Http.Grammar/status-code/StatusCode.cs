namespace Http.Grammar
{
    using TextFx.ABNF;

    public class StatusCode : Repetition
    {
        public StatusCode(Repetition repetition)
            : base(repetition)
        {
        }
    }
}