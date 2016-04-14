using System;
using Txt;
using Txt.ABNF;
using Uri.segment;

namespace Http.absolute_path
{
    public class AbsolutePathLexerFactory : ILexerFactory<AbsolutePath>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsolutePathLexerFactory(
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

        public ILexer<AbsolutePath> Create()
        {
            var a = terminalLexerFactory.Create(@"/", StringComparer.Ordinal);
            var b = segmentLexerFactory.Create();
            var c = concatenationLexerFactory.Create(a, b);
            var innerLexer = repetitionLexerFactory.Create(c, 1, int.MaxValue);
            return new AbsolutePathLexer(innerLexer);
        }
    }
}