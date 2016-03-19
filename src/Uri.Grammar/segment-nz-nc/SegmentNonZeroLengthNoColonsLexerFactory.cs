namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthNoColonsLexerFactory : ILexerFactory<SegmentNonZeroLengthNoColons>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public SegmentNonZeroLengthNoColonsLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory,
            ITerminalLexerFactory terminalLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(repetitionLexerFactory),
                    "Precondition: repetitionLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(alternativeLexerFactory),
                    "Precondition: alternativeLexerFactory != null");
            }

            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(unreservedLexerFactory),
                    "Precondition: unreservedLexerFactory != null");
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(percentEncodingLexerFactory),
                    "Precondition: percentEncodingLexerFactory != null");
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(subcomponentsDelimiterLexerFactory),
                    "Precondition: subcomponentsDelimiterLexerFactory != null");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<SegmentNonZeroLengthNoColons> Create()
        {
            var alternativeLexer = alternativeLexerFactory.Create(
                unreservedLexerFactory.Create(),
                percentEncodingLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create(),
                terminalLexerFactory.Create(@"@", StringComparer.Ordinal));
            var segmentNonZeroLengthNoColonsRepetitionLexer = repetitionLexerFactory.Create(alternativeLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthNoColonsLexer(segmentNonZeroLengthNoColonsRepetitionLexer);
        }
    }
}