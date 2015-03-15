namespace Http.Grammar
{
    using Http.Grammar.Rfc7230;

    using SLANG;

    public class ElementList<T> : Sequence<T, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, T>>>
        where T : Element
    {
        public ElementList(T element1, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, T>> element2, ITextContext context)
            : base(element1, element2, context)
        {
        }
    }
}
