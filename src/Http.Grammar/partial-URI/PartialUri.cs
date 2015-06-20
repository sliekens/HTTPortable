namespace Http.Grammar
{
    using TextFx.ABNF;

    public class PartialUri : Sequence
    {
        public PartialUri(Sequence sequence)
            : base(sequence)
        {
        }
    }
}