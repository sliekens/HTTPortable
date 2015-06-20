namespace Http.Grammar
{
    using TextFx.ABNF;

    public class QuotedPair : Sequence
    {
        public QuotedPair(Sequence sequence)
            : base(sequence)
        {
        }
    }
}