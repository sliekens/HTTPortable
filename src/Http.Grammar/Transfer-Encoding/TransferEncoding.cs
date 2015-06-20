namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferEncoding : Sequence
    {
        public TransferEncoding(Sequence sequence)
            : base(sequence)
        {
        }
    }
}