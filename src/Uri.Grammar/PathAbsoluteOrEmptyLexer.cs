namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class PathAbsoluteOrEmptyLexer : Lexer<PathAbsoluteOrEmpty>
    {
        private readonly ILexer<Segment> segmentLexer;

        public PathAbsoluteOrEmptyLexer()
            :this(new SegmentLexer())
        {
        }

        public PathAbsoluteOrEmptyLexer(ILexer<Segment> segmentLexer)
            : base("path-abempty")
        {
            Contract.Requires(segmentLexer != null);
            this.segmentLexer = segmentLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathAbsoluteOrEmpty element)
        {
            var context = scanner.GetContext();
            var innerContext = scanner.GetContext();
            var elements = new List<Sequence<Element, Segment>>();
            while (scanner.TryMatch('/'))
            {
                Segment segment;
                if (this.segmentLexer.TryRead(scanner, out segment))
                {
                    var terminal = new Element('/', innerContext);
                    var sequence = new Sequence<Element, Segment>(terminal, segment, innerContext);
                    elements.Add(sequence);
                }
                else
                {
                    scanner.PutBack('/');
                    break;
                }

                innerContext = scanner.GetContext();
            }

            element = new PathAbsoluteOrEmpty(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.segmentLexer != null);
        }
    }
}
