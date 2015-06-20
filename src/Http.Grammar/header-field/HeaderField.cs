namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HeaderField : Sequence
    {
        public HeaderField(Sequence sequence)
            : base(sequence)
        {
        }
    }
}
