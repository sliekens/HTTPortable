namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferCoding : Alternative
    {
        public TransferCoding(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
