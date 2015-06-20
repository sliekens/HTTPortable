namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferCodingCollection : Alternative
    {
        public TransferCodingCollection(Alternative alternative)
            : base(alternative)
        {
        }
    }
}