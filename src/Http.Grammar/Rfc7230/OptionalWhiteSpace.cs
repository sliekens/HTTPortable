using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;



    public class OptionalWhiteSpace : Element
    {
        public OptionalWhiteSpace(IList<WhiteSpace> data, ITextContext context)
            : base(string.Concat(data.Select(space => space.Data)), context)
        {
            Contract.Requires(data != null);
        }
    }
}