namespace Http.Grammar
{
    using TextFx.ABNF;

    public class StatusLine : Sequence
    {
        public StatusLine(Sequence sequence)
            : base(sequence)
        {
        }
    }
}
