namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class RequiredWhiteSpace : Repetition<WhiteSpace>
    {
        public RequiredWhiteSpace(IList<WhiteSpace> elements, ITextContext context)
            : base(elements, 1, context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(elements.Count != 0);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
            Contract.Requires(context != null);
        }
    }
}