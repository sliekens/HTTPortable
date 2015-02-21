using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    using Text.Scanning.Core;

    public class RWSToken : Element
    {
        public RWSToken(IList<WhiteSpace> whiteSpace, ITextContext context)
            : base(string.Concat(whiteSpace.Select(space => space.Data)), context)
        {
            Contract.Requires(whiteSpace != null);
            Contract.Requires(whiteSpace.Count > 0);
            Contract.Requires(Contract.ForAll(whiteSpace, space => space != null));
        }
    }
}
