namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Protocol : Sequence
    {
        public Protocol(Sequence sequence)
            : base(sequence)
        {
        }
    }
}