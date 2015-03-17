namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class Trailer : ElementList3<FieldName>
    {
        public Trailer(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, FieldName element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, FieldName>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}
