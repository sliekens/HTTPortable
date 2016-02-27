namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferExtension : Concatenation
    {
        public TransferExtension(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}