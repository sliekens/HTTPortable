namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SubcomponentsDelimiterLexer : Lexer<SubcomponentsDelimiter>
    {
        private readonly ILexer<Alternative> innerLexer; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">"!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="</param>
        public SubcomponentsDelimiterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out SubcomponentsDelimiter element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new SubcomponentsDelimiter(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(SubcomponentsDelimiter);
            return false;
        }
    }
}