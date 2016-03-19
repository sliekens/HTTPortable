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

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathAbsoluteLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            RepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory,
            ILexerFactory<SegmentNonZeroLength> segmentNonZeroLengthLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }

            if (segmentNonZeroLengthLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentNonZeroLengthLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
            this.segmentNonZeroLengthLexerFactory = segmentNonZeroLengthLexerFactory;
        }

        public ILexer<PathAbsolute> Create()
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

            // [ segment-nz *( "/" segment ) ]
            var g = optionLexerFactory.Create(f);

            // "/" [ segment-nz *( "/" segment ) ]
            var h = concatenationLexerFactory.Create(a, g);

            // path-absolute
            return new PathAbsoluteLexer(h);
        }
    }
}