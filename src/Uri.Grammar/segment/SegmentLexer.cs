namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class SegmentLexer : Lexer<Segment>
    {
        private readonly ILexer<Repetition> segmentRepetitionLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentRepetitionLexer">*pchar</param>
        public SegmentLexer(ILexer<Repetition> segmentRepetitionLexer)
        {
            if (segmentRepetitionLexer == null)
            {
                throw new ArgumentNullException("segmentRepetitionLexer", "Precondition: segmentRepetitionLexer != null");
            }

            this.segmentRepetitionLexer = segmentRepetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Segment element)
        {
            Repetition result;
            if (this.segmentRepetitionLexer.TryRead(scanner, out result))
            {
                element = new Segment(result);
                return true;
            }

            element = default(Segment);
            return false;
        }
    }
}