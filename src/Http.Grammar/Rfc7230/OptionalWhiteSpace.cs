using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    using Text.Scanning.Core;

    public class OptionalWhiteSpace : Element
    {
        public OptionalWhiteSpace(IList<WhiteSpace> data, ITextContext context)
            : base(string.Concat(data.Select(space => space.Data)), context)
        {
            Contract.Requires(data != null);
        }
    }
}