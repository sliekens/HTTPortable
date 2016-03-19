namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    using Uri.Grammar;

    public class AbsolutePathLexerFactory : ILexerFactory<AbsolutePath>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly IConcatenationLexerFactory ConcatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsolutePathLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IConcatenationLexerFactory ConcatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (ConcatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ConcatenationLexerFactory));
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
            this.ConcatenationLexerFactory = ConcatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
        }

        public ILexer<AbsolutePath> Create()
        {
            var a = terminalLexerFactory.Create(@"/", StringComparer.Ordinal);
            var b = segmentLexerFactory.Create();
            var c = ConcatenationLexerFactory.Create(a, b);
            var innerLexer = repetitionLexerFactory.Create(c, 1, int.MaxValue);
            return new AbsolutePathLexer(innerLexer);
        }
    }
}