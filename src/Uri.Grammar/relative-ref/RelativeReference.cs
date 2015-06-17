namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class RelativeReference : Sequence
    {
        public RelativeReference(Sequence sequence)
            : base(sequence)
        {
        }
    }
}