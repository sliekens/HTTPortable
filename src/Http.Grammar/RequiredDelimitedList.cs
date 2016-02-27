namespace Http.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

    using TextFx;
    using TextFx.ABNF;

    public class RequiredDelimitedList : Concatenation
    {
        public RequiredDelimitedList(RequiredDelimitedList delimitedList)
            : base(delimitedList)
        {
        }

        public RequiredDelimitedList(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public IList<Element> GetItems()
        {
            var e1 = this.Elements[1];
            var items = new List<Element> { e1 };
            var rep1 = (Repetition)this.Elements[2];
            foreach (var opt1 in rep1.Elements.Cast<Concatenation>().Select(seq1 => (Repetition)seq1.Elements[2]))
            {
                items.AddRange(opt1.Elements.Cast<Concatenation>().Select(seq2 => seq2.Elements[1]));
            }

            return items;
        }

        public override string GetWellFormedText()
        {
            return string.Join(", ", this.GetItems().Select(e => e.GetWellFormedText()));
        }
    }
}