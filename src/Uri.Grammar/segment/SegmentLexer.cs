namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentLexer : Lexer<Segment>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">*pchar</param>
        public SegmentLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Segment element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Segment(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Segment);
            return false;
        }
    }
}