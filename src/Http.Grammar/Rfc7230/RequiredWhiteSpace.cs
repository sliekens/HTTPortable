namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using SLANG;
    using SLANG.Core;



    public class RequiredWhiteSpace : Element
    {
        public RequiredWhiteSpace(IList<WhiteSpace> whiteSpace, ITextContext context)
            : base(string.Concat(whiteSpace.Select(space => space.Data)), context)
        {
            Contract.Requires(whiteSpace != null);
            Contract.Requires(whiteSpace.Count > 0);
            Contract.Requires(Contract.ForAll(whiteSpace, space => space != null));
            Contract.Requires(context != null);
        }
    }
}
