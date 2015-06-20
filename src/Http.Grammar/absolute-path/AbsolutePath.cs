namespace Http.Grammar
{
    using TextFx.ABNF;

    public class AbsolutePath : Repetition
    {
        public AbsolutePath(Repetition repetition)
            : base(repetition)
        {
        }
    }
}