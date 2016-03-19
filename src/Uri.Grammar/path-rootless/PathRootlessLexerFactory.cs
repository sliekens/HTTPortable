namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathRootlessLexerFactory : ILexerFactory<PathRootless>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathRootlessLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory,
            ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }

            if (segmentNonZeroLengthLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthLexerFactory));
            }

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public ILexer<PathRootless> Create()
        {
            // "/"
            var a = terminalLexerFactory.Create(@"/", StringComparer.Ordinal);

            // segment
            var b = segmentLexerFactory.Create();

            // "/" segment
            var c = concatenationLexerFactory.Create(a, b);

            // *( "/" segment )
            var d = repetitionLexerFactory.Create(c, 0, int.MaxValue);

            // segment-nz
            var e = segmentNonZeroLengthLexerFactory.Create();

            // segment-nz *( "/" segment )
            var f = concatenationLexerFactory.Create(e, d);

            // path-rootless
            return new PathRootlessLexer(f);
        }
    }
}