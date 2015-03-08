using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;

    

    public class PathRootlessLexer : Lexer<PathRootless>
    {
        private readonly ILexer<SegmentNonZero> segmentNonZeroLexer;

        private readonly ILexer<Segment> segmentLexer;

        public PathRootlessLexer()
            : this(new SegmentNonZeroLexer(), new SegmentLexer())
        {
        }

        public PathRootlessLexer(ILexer<SegmentNonZero> segmentNonZeroLexer, ILexer<Segment> segmentLexer)
            : base("path-rootless")
        {
            this.segmentNonZeroLexer = segmentNonZeroLexer;
            this.segmentLexer = segmentLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathRootless element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PathRootless);
                return false;
            }

            var context = scanner.GetContext();
            SegmentNonZero segmentNonZero;
            if (!this.segmentNonZeroLexer.TryRead(scanner, out segmentNonZero))
            {
                element = default(PathRootless);
                return false;
            }

            var segments = new List<Sequence<Element, Segment>>();
            var innerContext = scanner.GetContext();
            while (!scanner.EndOfInput && scanner.TryMatch('/'))
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

            element = new PathRootless(segmentNonZero, segments, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.segmentNonZeroLexer != null);
            Contract.Invariant(this.segmentLexer != null);
        }
    }
}
