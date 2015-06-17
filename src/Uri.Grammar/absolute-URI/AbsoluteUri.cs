namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class AbsoluteUri : Sequence
    {
        public AbsoluteUri(Sequence sequence)
            : base(sequence)
        {
        }
    }
}