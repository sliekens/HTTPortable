namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using ConnectionOption = Token;

    public class Connection : ElementList3<ConnectionOption>
    {
        public Connection(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, ConnectionOption element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, ConnectionOption>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}
