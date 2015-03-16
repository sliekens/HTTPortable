namespace Http.Grammar
{
    using Http.Grammar.Rfc7230;

    using SLANG;

    public class ElementList3<T> : Sequence<Repetition<Sequence<Element, OptionalWhiteSpace>>, T, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>
        where T : Element
    {
        public ElementList3(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, T element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}
