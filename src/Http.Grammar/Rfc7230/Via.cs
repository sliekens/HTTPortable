namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class Via : ElementList3<Sequence<ReceivedProtocol, RequiredWhiteSpace, ReceivedBy, Option<Sequence<RequiredWhiteSpace, Comment>>>>
    {
        public Via(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, Sequence<ReceivedProtocol, RequiredWhiteSpace, ReceivedBy, Option<Sequence<RequiredWhiteSpace, Comment>>> element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Sequence<ReceivedProtocol, RequiredWhiteSpace, ReceivedBy, Option<Sequence<RequiredWhiteSpace, Comment>>>>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}
