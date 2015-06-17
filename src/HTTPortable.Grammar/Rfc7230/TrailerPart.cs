namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class TrailerPart : Repetition<Sequence<HeaderField, EndOfLine>>
    {
        public TrailerPart(IList<Sequence<HeaderField, EndOfLine>> elements, ITextContext context)
            : base(elements, context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(context != null);
        }
    }
}