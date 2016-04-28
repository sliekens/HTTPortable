using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.segment;

namespace Http.absolute_path
{
    public class AbsolutePathLexerFactory : ILexerFactory<AbsolutePath>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Segment> segmentLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public AbsolutePathLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Segment> segmentLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (segmentLexer == null)
            {
                throw new ArgumentNullException(nameof(segmentLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.segmentLexer = segmentLexer;
        }

        public ILexer<AbsolutePath> Create()
        {
            var innerLexer =
                repetitionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                        segmentLexer),
                    1,
                    int.MaxValue);
            return new AbsolutePathLexer(innerLexer);
        }
    }
}
