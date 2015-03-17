namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class TE : ElementList2<TransferCodingListItem>
    {
        public TE(ITextContext context)
            : base(context)
        {
        }

        public TE(Sequence<Alternative<Element, TransferCodingListItem>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, TransferCodingListItem>>>>> element, ITextContext context)
            : base(element, context)
        {
        }
    }
}
