namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SegmentPart = SLANG.Sequence<SLANG.Element, Uri.Grammar.Segment>;

    public class AbsolutePath : Element
    {
        public AbsolutePath(IList<SegmentPart> segments, ITextContext context)
            : base(string.Concat(segments), context)
        {
            Contract.Requires(segments != null);
            Contract.Requires(Contract.ForAll(segments, 
                sequence =>
                {
                    if (sequence == null)
                    {
                        return false;
                    }

                    if (sequence.Element1 == null || sequence.Element1.Data != "/")
                    {
                        return false;
                    }

                    if (sequence.Element2 == null)
                    {
                        return false;
                    }

                    return true;
                }));
            Contract.Requires(context != null);
        }
    }
}