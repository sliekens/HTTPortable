namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PathNoSchemeLexerFactory : ILexerFactory<PathNoScheme>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly ILexerFactory<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public PathNoSchemeLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IStringLexerFactory stringLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory,
            ILexerFactory<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException("segmentLexerFactory");
            }

            if (segmentNonZeroLengthNoColonsLexerFactory == null)
            {
                throw new ArgumentNullException("segmentNonZeroLengthNoColonsLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthNoColonsLexerFactory = segmentNonZeroLengthNoColonsLexerFactory;
        }

        public ILexer<PathNoScheme> Create()
        {
            // "/"
            var a = this.stringLexerFactory.Create(@"/");

            // segment
            var b = this.segmentLexerFactory.Create();

            // "/" segment
            var c = this.sequenceLexerFactory.Create(a, b);

            // *( "/" segment )
            var d = this.repetitionLexerFactory.Create(c, 0, int.MaxValue);

            // segment-nz-nc
            var e = this.segmentNonZeroLengthNoColonsLexerFactory.Create();

            // segment-nz-nc *( "/" segment )
            var f = this.sequenceLexerFactory.Create(e, d);

            // path-noscheme
            return new PathNoSchemeLexer(f);
        }
    }
}