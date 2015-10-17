namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathNoSchemeLexerFactory : ILexerFactory<PathNoScheme>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly ILexerFactory<SegmentNonZeroLengthNoColons> segmentNonZeroLengthNoColonsLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathNoSchemeLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
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

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
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
            this.terminalLexerFactory = terminalLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthNoColonsLexerFactory = segmentNonZeroLengthNoColonsLexerFactory;
        }

        public ILexer<PathNoScheme> Create()
        {
            // "/"
            var a = this.terminalLexerFactory.Create(@"/", StringComparer.Ordinal);

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