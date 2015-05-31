namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class SegmentNonZeroLengthLexer : Lexer<SegmentNonZeroLength>
    {
        private readonly ILexer<Repetition> segmentNonZeroLengthRepetitionLexer;

        /// <summary></summary>
        /// <param name="segmentNonZeroLengthRepetitionLexer">1*pchar</param>
        public SegmentNonZeroLengthLexer(ILexer<Repetition> segmentNonZeroLengthRepetitionLexer)
        {
            if (segmentNonZeroLengthRepetitionLexer == null)
            {
                throw new ArgumentNullException("segmentNonZeroLengthRepetitionLexer", "Precondition: segmentNonZeroLengthRepetitionLexer != null");
            }

            this.segmentNonZeroLengthRepetitionLexer = segmentNonZeroLengthRepetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out SegmentNonZeroLength element)
        {
            Repetition result;
            if (this.segmentNonZeroLengthRepetitionLexer.TryRead(scanner, out result))
            {
                element = new SegmentNonZeroLength(result);
                return true;
            }

            element = default(SegmentNonZeroLength);
            return false;
        }
    }
}