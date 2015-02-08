using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class OWSToken : Token
    {
        public OWSToken(IList<WspMutex> data, ITextContext context)
            : base(string.Concat(data.Select(mutex => mutex.Token.Data)), context)
        {
            Contract.Requires(data != null);
        }
    }
}