namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class TransferCodingRanking : Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element, Rank>
    {
        public TransferCodingRanking(OptionalWhiteSpace element1, Element element2, OptionalWhiteSpace element3, Element element4, Rank element5, ITextContext context)
            : base(element1, element2, element3, element4, element5, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(element4 != null);
            Contract.Requires(element5 != null);
            Contract.Requires(context != null);
        }
    }
}