namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class SegmentNonZeroLengthNoColonsLexer : Lexer<SegmentNonZeroLengthNoColons>
    {
        private readonly ILexer<Repetition> segmentNonZeroLengthNoColonsRepetitionLexer;

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