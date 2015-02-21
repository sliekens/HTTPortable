using System.Collections.Generic;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldValue : Element
    {
        public FieldValue(IList<Alternative<FieldContent, ObsFold>> data, ITextContext context)
            : base(string.Concat(data.Select(alternative => alternative.Element.Data)), context)
        {
        }
    }
}
