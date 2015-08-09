namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">gen-delims / sub-delims</param>
        public ReservedLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Reserved element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Reserved(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Reserved);
            return false;
        }
    }
}