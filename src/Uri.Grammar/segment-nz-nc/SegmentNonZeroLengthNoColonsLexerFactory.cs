namespace Uri.Grammar.segment_nz_nc
{
    using System;

    using SLANG;

    public class SegmentNonZeroLengthNoColonsLexerFactory : ILexerFactory<SegmentNonZeroLengthNoColons>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public SegmentNonZeroLengthNoColonsLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory,
            IStringLexerFactory stringLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "repetitionLexerFactory",
                    "Precondition: repetitionLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "alternativeLexerFactory",
                    "Precondition: alternativeLexerFactory != null");
            }

            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "unreservedLexerFactory",
                    "Precondition: unreservedLexerFactory != null");
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "percentEncodingLexerFactory",
                    "Precondition: percentEncodingLexerFactory != null");
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(
                    "subcomponentsDelimiterLexerFactory",
                    "Precondition: subcomponentsDelimiterLexerFactory != null");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
        }

        public ILexer<SegmentNonZeroLengthNoColons> Create()
        {
            var alternativeLexer = this.alternativeLexerFactory.Create(
                this.unreservedLexerFactory.Create(),
                this.percentEncodingLexerFactory.Create(),
                this.subcomponentsDelimiterLexerFactory.Create(),
                this.stringLexerFactory.Create(@"@"));
            var segmentNonZeroLengthNoColonsRepetitionLexer = this.repetitionLexerFactory.Create(alternativeLexer, 1, Int32.MaxValue);
            return new SegmentNonZeroLengthNoColonsLexer(segmentNonZeroLengthNoColonsRepetitionLexer);
        }
    }
}