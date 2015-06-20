namespace Http.Grammar
{
    using TextFx.ABNF;

    public class QuotedString : Sequence
    {
        public QuotedString(Sequence sequence)
            : base(sequence)
        {
        }
    }
}