namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthNoColonsLexer : Lexer<SegmentNonZeroLengthNoColons>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">1*( unreserved / pct-encoded / sub-delims / "@" )</param>
        public SegmentNonZeroLengthNoColonsLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out SegmentNonZeroLengthNoColons element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new SegmentNonZeroLengthNoColons(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(SegmentNonZeroLengthNoColons);
            return false;
        }
    }
}