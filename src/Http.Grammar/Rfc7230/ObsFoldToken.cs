using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ObsFoldToken : Token
    {
        public ObsFoldToken(CrLfToken crLf, IList<WspMutex> whitespace, ITextContext context)
            : base("\r\n" + string.Concat(whitespace.Select(mutex => mutex.Token.Data)), context)
        {
            Contract.Requires(crLf != null);
            Contract.Requires(whitespace != null);
            Contract.Requires(whitespace.Count > 0);
            Contract.Requires(Contract.ForAll(whitespace, mutex => mutex != null));
        }
    }
}
