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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsolutePathLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(sequenceLexerFactory));
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
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
        }

        public ILexer<AbsolutePath> Create()
        {
            var a = this.terminalLexerFactory.Create(@"/", StringComparer.Ordinal);
            var b = this.segmentLexerFactory.Create();
            var c = this.sequenceLexerFactory.Create(a, b);
            var innerLexer = this.repetitionLexerFactory.Create(c, 1, int.MaxValue);
            return new AbsolutePathLexer(innerLexer);
        }
    }
}