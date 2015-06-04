namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using SLANG;

    public class PathNoScheme : Element
    {
        public PathNoScheme(SegmentNonZeroNoColons startSegment, IList<Sequence<Element, Segment>> segments, 
            ITextContext context)
            : base(string.Concat(startSegment.Data, string.Concat(segments.Select(sequence => sequence.Data))), context)
        {
            Contract.Requires(startSegment != null);
            Contract.Requires(segments != null);
            Contract.Requires(Contract.ForAll(segments, sequence => sequence != null));
            Contract.Requires(context != null);
        }
    }
}