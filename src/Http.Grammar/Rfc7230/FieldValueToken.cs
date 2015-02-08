using System.Collections.Generic;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldValueToken : Token
    {
        public FieldValueToken(IList<TokenMutex<FieldContentToken, ObsFoldToken>> data, ITextContext context)
            : base(string.Concat(data.Select(mutex => mutex.Token.Data)), context)
        {
        }
    }
}
