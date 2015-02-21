using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ObsFoldToken : Element
    {
        // TODO: refactor whitespace as RWS
        public ObsFoldToken(EndOfLine endOfLine, IList<WhiteSpace> whitespace, ITextContext context)
            : base("\r\n" + string.Concat(whitespace.Select(space => space.Data)), context)
        {
            Contract.Requires(endOfLine != null);
            Contract.Requires(whitespace != null);
            Contract.Requires(whitespace.Count > 0);
            Contract.Requires(Contract.ForAll(whitespace, mutex => mutex != null));
        }
    }
}
