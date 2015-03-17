namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class TransferEncoding : ElementList3<TransferCoding>
    {
        public TransferEncoding(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, TransferCoding element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, TransferCoding>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}
