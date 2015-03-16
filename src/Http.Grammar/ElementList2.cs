namespace Http.Grammar
{
    using Http.Grammar.Rfc7230;

    using SLANG;

    public class ElementList2<T> : Option<Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>>
        where T : Element
    {
        public ElementList2(ITextContext context)
            : base(context)
        {
        }

        public ElementList2(Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>> element, ITextContext context)
            : base(element, context)
        {
        }
    }
}
