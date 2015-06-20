namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TrailerPart : Repetition
    {
        public TrailerPart(Repetition repetition)
            : base(repetition)
        {
        }
    }
}