namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ReceivedProtocol : Sequence
    {
        public ReceivedProtocol(Sequence sequence)
            : base(sequence)
        {
        }
    }
}
