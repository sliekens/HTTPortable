namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferExtension : Sequence
    {
        public TransferExtension(Sequence sequence)
            : base(sequence)
        {
        }
    }
}