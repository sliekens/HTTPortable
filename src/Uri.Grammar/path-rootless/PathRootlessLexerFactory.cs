namespace Uri.Grammar.path_rootless
{
    using System;

    using SLANG;

    public class PathRootlessLexerFactory : ILexerFactory<PathRootless>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexer;

        public PathRootlessLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IStringLexerFactory stringLexer,
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

            if (stringLexer == null)
            {
                throw new ArgumentNullException("stringLexer");
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
            this.stringLexer = stringLexer;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public ILexer<PathRootless> Create()
        {
            // "/"
            var a = this.stringLexer.Create(@"/");

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