namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferParameter : Sequence
    {
        public TransferParameter(Sequence sequence)
            : base(sequence)
        {
        }
    }
}