using System.Collections.Generic;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldValueToken : Element
    {
        public FieldValueToken(IList<Alternative<FieldContentToken, ObsFoldToken>> data, ITextContext context)
            : base(string.Concat(data.Select(mutex => mutex.Element.Data)), context)
        {
        }
    }
}
