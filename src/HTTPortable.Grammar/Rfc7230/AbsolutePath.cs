namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;

    using SegmentPart = SLANG.Sequence<SLANG.Element, Uri.Grammar.Segment>;

    public class AbsolutePath : Repetition<SegmentPart>
    {
        public AbsolutePath(IList<SegmentPart> elements, ITextContext context)
            : base(elements, 1, context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
            Contract.Requires(context != null);
        }
    }
}