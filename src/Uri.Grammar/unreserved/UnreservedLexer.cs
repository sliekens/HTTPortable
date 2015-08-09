namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">ALPHA / DIGIT / "-" / "." / "_" / "~"</param>
        public UnreservedLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Unreserved element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Unreserved(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Unreserved);
            return false;
        }
    }
}