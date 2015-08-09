namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthLexer : Lexer<SegmentNonZeroLength>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary></summary>
        /// <param name="innerLexer">1*pchar</param>
        public SegmentNonZeroLengthLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out SegmentNonZeroLength element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new SegmentNonZeroLength(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(SegmentNonZeroLength);
            return false;
        }
    }
}