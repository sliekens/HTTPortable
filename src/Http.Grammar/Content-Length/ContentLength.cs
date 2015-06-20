namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ContentLength : Repetition
    {
        public ContentLength(Repetition repetition)
            : base(repetition)
        {
        }
    }
}