namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using Uri.Grammar;

    public class PartialUri : Sequence<RelativePart, Option<Sequence<Element, Query>>>
    {
        public PartialUri(RelativePart element1, Option<Sequence<Element, Query>> element2, ITextContext context)
            : base(element1, element2, context)
        {
        }
    }
}