namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class TransferCodingListItem : Alternative<Element, Sequence<TransferCoding, Option<TransferCodingRanking>>>
    {
        public TransferCodingListItem(Element element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public TransferCodingListItem(Sequence<TransferCoding, Option<TransferCodingRanking>> element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}