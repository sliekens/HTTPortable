namespace Http.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

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

        public IList<T> GetElements()
        {
            var elements = new List<T>();
            foreach (var sequence in this.Elements)
            {
                var element = sequence.Element1 as T;
                if (element != null)
                {
                    elements.Add(element);
                }

                elements.AddRange(sequence.Element2.Elements.SelectMany(s => s.Element3.Elements).Select(o => o.Element2));
            }

            return elements;
        }
    }
}
