namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Scheme : Sequence
    {
        public Scheme(Sequence sequence)
            : base(sequence)
        {
        }
    }
}