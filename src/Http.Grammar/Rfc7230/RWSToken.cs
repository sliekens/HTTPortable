using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class RWSToken : Token
    {
        public RWSToken(IList<WspMutex> whiteSpace, ITextContext context)
            : base(string.Concat(whiteSpace.Select(mutex => mutex.Token.Data)), context)
        {
            Contract.Requires(whiteSpace != null);
            Contract.Requires(whiteSpace.Count > 0);
            Contract.Requires(Contract.ForAll(whiteSpace, mutex => mutex != null));
        }
    }
}
