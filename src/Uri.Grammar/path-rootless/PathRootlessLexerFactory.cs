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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathRootlessLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory,
            ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException("segmentLexerFactory");
            }

            if (segmentNonZeroLengthLexerFactory == null)
            {
                throw new ArgumentNullException("segmentNonZeroLengthLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public ILexer<PathRootless> Create()
        {
            // "/"
            var a = this.terminalLexerFactory.Create(@"/", StringComparer.Ordinal);

            // segment
            var b = this.segmentLexerFactory.Create();

            // "/" segment
            var c = this.sequenceLexerFactory.Create(a, b);

            // *( "/" segment )
            var d = this.repetitionLexerFactory.Create(c, 0, int.MaxValue);

            // segment-nz
            var e = this.segmentNonZeroLengthLexerFactory.Create();

            // segment-nz *( "/" segment )
            var f = this.sequenceLexerFactory.Create(e, d);

            // path-rootless
            return new PathRootlessLexer(f);
        }
    }
}