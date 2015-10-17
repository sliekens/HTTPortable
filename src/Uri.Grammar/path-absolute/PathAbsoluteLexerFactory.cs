namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathAbsoluteLexerFactory : ILexerFactory<PathAbsolute>
    {
        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly RepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathAbsoluteLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            RepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory,
            ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException("segmentLexerFactory");
            }

            if (segmentNonZeroLengthLexerFactory == null)
            {
                throw new ArgumentNullException("segmentNonZeroLengthLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public ILexer<PathAbsolute> Create()
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

            // [ segment-nz *( "/" segment ) ]
            var g = this.optionLexerFactory.Create(f);

            // "/" [ segment-nz *( "/" segment ) ]
            var h = this.sequenceLexerFactory.Create(a, g);

            // path-absolute
            return new PathAbsoluteLexer(h);
        }
    }
}