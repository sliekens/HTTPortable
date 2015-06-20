namespace Http.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

    using TextFx;
    using TextFx.ABNF;

    public class OptionalDelimitedList : Repetition
    {
        public OptionalDelimitedList(OptionalDelimitedList delimitedList)
            : base(delimitedList)
        {
        }

        public OptionalDelimitedList(Repetition repetition)
            : base(repetition)
        {
        }

        public IList<Element> GetItems()
        {
            var items = new List<Element>();
            foreach (var seq1 in this.Elements.Cast<Sequence>())
            {
                var alt1 = (Alternative)seq1.Elements[0];
                if (alt1.Ordinal == 2)
                {
                    items.Add(alt1.Element);
                }

                var rep1 = (Repetition)seq1.Elements[1];
                foreach (var opt1 in rep1.Elements.Cast<Sequence>().Select(seq2 => (Repetition)seq2.Elements[2]))
                {
                    items.AddRange(opt1.Elements.Cast<Sequence>().Select(seq3 => seq3.Elements[1]));
                }
            }

            return items;
        }

        public override string GetWellFormedText()
        {
            return string.Join(", ", this.GetItems().Select(e => e.GetWellFormedText()));
        }
    }
}