namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class Upgrade : ElementList3<Protocol>
    {
        public Upgrade(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, Protocol element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Protocol>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}