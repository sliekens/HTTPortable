namespace Uri.Grammar.segment_nz_nc
{
    using SLANG;

    public class SegmentNonZeroLengthNoColonsRepetitionLexer : RepetitionLexer
    {
        /// <summary></summary>
        /// <param name="repeatingElementLexer">( unreserved / pct-encoded / sub-delims / "@" )</param>
        public SegmentNonZeroLengthNoColonsRepetitionLexer(ILexer<Alternative> repeatingElementLexer)
            : base(repeatingElementLexer, 1, int.MaxValue)
        {
        }
    }
}
