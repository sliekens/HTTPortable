namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathAbsoluteOrEmptyLexerFactory : ILexerFactory<PathAbsoluteOrEmpty>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathAbsoluteOrEmptyLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(segmentLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
        }

        public ILexer<PathAbsoluteOrEmpty> Create()
        {
            // "/"
            var a = terminalLexerFactory.Create(@"/", StringComparer.Ordinal);

            // segment
            var b = segmentLexerFactory.Create();

            // "/" segment
            var c = concatenationLexerFactory.Create(a, b);

            // *( "/" segment )
            var d = repetitionLexerFactory.Create(c, 0, int.MaxValue);

            // path-abempty
            return new PathAbsoluteOrEmptyLexer(d);
        }
    }
}