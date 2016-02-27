namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TransferCodingRank : Concatenation
    {
        public TransferCodingRank(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}