using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    using Text.Scanning.Core;

    public class OWSToken : Element
    {
        public OWSToken(IList<WhiteSpace> data, ITextContext context)
            : base(string.Concat(data.Select(mutex => mutex.Data)), context)
        {
            Contract.Requires(data != null);
        }
    }
}