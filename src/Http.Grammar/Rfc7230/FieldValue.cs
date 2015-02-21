using System.Collections.Generic;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    public class FieldValue : Element
    {
        public FieldValue(IList<Alternative<FieldContent, ObsoletedFold>> data, ITextContext context)
            : base(string.Concat(data.Select(element => element.Data)), context)
        {
            Contract.Requires(data != null);
        }
    }
}
