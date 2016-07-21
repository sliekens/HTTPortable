using System.Collections.Generic;
using System.Linq;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
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
            var e1 = this[1];
            var items = new List<Element> { e1 };
            var rep1 = (Repetition)this[2];
            foreach (var opt1 in rep1.Cast<Concatenation>().Select(seq1 => (Repetition)seq1[2]))
            {
                items.AddRange(opt1.Cast<Concatenation>().Select(seq2 => seq2[1]));
            }

            return items;
        }
    }
}