namespace Http.Grammar
{
    using TextFx.ABNF;

    public class RequiredWhiteSpace : Repetition
    {
        public RequiredWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }
    }
}