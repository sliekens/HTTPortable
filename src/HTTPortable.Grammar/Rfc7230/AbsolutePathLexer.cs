namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;
    using SegmentPart = SLANG.Sequence<SLANG.Element, Uri.Grammar.Segment>;

    public class AbsolutePathLexer : Lexer<AbsolutePath>
    {
        private readonly ILexer<Segment> segmentLexer;

        public AbsolutePathLexer()
            : this(new SegmentLexer())
        {
        }

        public AbsolutePathLexer(ILexer<Segment> segmentLexer)
            : base("absolute-path")
        {
            Contract.Requires(segmentLexer != null);
            this.segmentLexer = segmentLexer;
        }

        public override bool TryRead(ITextScanner scanner, out AbsolutePath element)
        {
            if (scanner.EndOfInput)
            {
                element = default(AbsolutePath);
                return false;
            }

            var context = scanner.GetContext();
            var segments = new List<SegmentPart>();
            SegmentPart segment;
            while (this.TryReadSegmentPart(scanner, out segment))
            {
                segments.Add(segment);
            }

            if (segments.Count == 0)
            {
                element = default(AbsolutePath);
                return false;
            }

            element = new AbsolutePath(segments, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.segmentLexer != null);
        }

        private bool TryReadSegmentPart(ITextScanner scanner, out SegmentPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(SegmentPart);
                return false;
            }

            var context = scanner.GetContext();
            Element slash;
            if (!TryReadTerminal(scanner, "/", out slash))
            {
                element = default(SegmentPart);
                return false;
            }

            Segment segment;
            if (!this.segmentLexer.TryRead(scanner, out segment))
            {
                scanner.PutBack(slash.Data);
                element = default(SegmentPart);
                return false;
            }

            element = new SegmentPart(slash, segment, context);
            return true;
        }
    }
}