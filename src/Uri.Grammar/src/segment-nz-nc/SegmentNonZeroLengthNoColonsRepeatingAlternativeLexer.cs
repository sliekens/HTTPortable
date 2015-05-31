namespace Uri.Grammar.segment_nz_nc
{
    using SLANG;

    public class SegmentNonZeroLengthNoColonsRepeatingAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">unreserved / pct-encoded / sub-delims / "@"</param>
        public SegmentNonZeroLengthNoColonsRepeatingAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
