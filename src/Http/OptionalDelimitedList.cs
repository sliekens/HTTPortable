using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public class OptionalDelimitedList : Repetition
    {
        public OptionalDelimitedList([NotNull] OptionalDelimitedList delimitedList)
            : base(delimitedList)
        {
        }

        public OptionalDelimitedList([NotNull] Repetition repetition)
            : base(repetition)
        {
        }

        public IList<Element> GetItems()
        {
            var items = new List<Element>();
            foreach (var seq1 in this.Cast<Concatenation>())
            {
                var alt1 = (Alternation)seq1[0];
                if (alt1.Ordinal == 2)
                {
                    items.Add(alt1.Element);
                }
                var rep1 = (Repetition)seq1[1];
                foreach (var opt1 in rep1.Cast<Concatenation>().Select(seq2 => (Repetition)seq2[2]))
                {
                    items.AddRange(opt1.Cast<Concatenation>().Select(seq3 => seq3[1]));
                }
            }
            return items;
        }
    }
}
