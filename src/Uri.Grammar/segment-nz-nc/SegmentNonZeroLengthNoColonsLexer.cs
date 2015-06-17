namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthNoColonsLexer : Lexer<SegmentNonZeroLengthNoColons>
    {
        private readonly ILexer<Repetition> segmentNonZeroLengthNoColonsRepetitionLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentNonZeroLengthNoColonsRepetitionLexer">1*( unreserved / pct-encoded / sub-delims / "@" )</param>
        public SegmentNonZeroLengthNoColonsLexer(ILexer<Repetition> segmentNonZeroLengthNoColonsRepetitionLexer)
        {
            if (segmentNonZeroLengthNoColonsRepetitionLexer == null)
            {
                throw new ArgumentNullException("segmentNonZeroLengthNoColonsRepetitionLexer", "Precondition: segmentNonZeroLengthNoColonsRepetitionLexer != null");
            }

            this.segmentNonZeroLengthNoColonsRepetitionLexer = segmentNonZeroLengthNoColonsRepetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out SegmentNonZeroLengthNoColons element)
        {
            Repetition result;
            if (this.segmentNonZeroLengthNoColonsRepetitionLexer.TryRead(scanner, out result))
            {
                element = new SegmentNonZeroLengthNoColons(result);
                return true;
            }

            element = default(SegmentNonZeroLengthNoColons);
            return false;
        }
    }
}