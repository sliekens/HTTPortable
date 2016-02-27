namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferParameter : Concatenation
    {
        public TransferParameter(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}