namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class PathNoSchemeLexer : Lexer<PathNoScheme>
    {
         private readonly ILexer<SegmentNonZeroNoColons> segmentNonZeroNoColonsLexer;

        private readonly ILexer<Segment> segmentLexer;

        public PathNoSchemeLexer()
            : this(new SegmentNonZeroNoColonsLexer(), new SegmentLexer())
        {
        }

        public PathNoSchemeLexer(ILexer<SegmentNonZeroNoColons> segmentNonZeroNoColonsLexer, ILexer<Segment> segmentLexer)
            : base("path-noscheme")
        {
            this.segmentNonZeroNoColonsLexer = segmentNonZeroNoColonsLexer;
            this.segmentLexer = segmentLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathNoScheme element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PathNoScheme);
                return false;
            }

            var context = scanner.GetContext();
            SegmentNonZeroNoColons segmentNonZeroNoColons;
            if (!this.segmentNonZeroNoColonsLexer.TryRead(scanner, out segmentNonZeroNoColons))
            {
                element = default(PathNoScheme);
                return false;
            }

            var segments = new List<Sequence<Element, Segment>>();
            var innerContext = scanner.GetContext();
            while (scanner.TryMatch('/'))
            {
                var terminal = new Element('/', innerContext);
                Segment segment;
                if (this.segmentLexer.TryRead(scanner, out segment))
                {
                    var sequence = new Sequence<Element, Segment>(terminal, segment, innerContext);
                    segments.Add(sequence);
                }
                else
                {
                    scanner.PutBack('/');
                    break;
                }

                innerContext = scanner.GetContext();
            }

            element = new PathNoScheme(segmentNonZeroNoColons, segments, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.segmentNonZeroNoColonsLexer != null);
            Contract.Invariant(this.segmentLexer != null);
        }
    }
}
