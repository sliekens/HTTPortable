namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class OptionalWhiteSpace : Repetition<WhiteSpace>
    {
        public OptionalWhiteSpace(IList<WhiteSpace> elements, ITextContext context)
            : base(elements, context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(context != null);
        }
    }
}