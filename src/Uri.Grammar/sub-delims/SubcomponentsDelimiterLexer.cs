namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class SubcomponentsDelimiterLexer : Lexer<SubcomponentsDelimiter>
    {
        private readonly ILexer<Alternative> subcomponentsDelimiterAlternativeLexer; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subcomponentsDelimiterAlternativeLexer">"!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="</param>
        public SubcomponentsDelimiterLexer(ILexer<Alternative> subcomponentsDelimiterAlternativeLexer)
        {
            if (subcomponentsDelimiterAlternativeLexer == null)
            {
                throw new ArgumentNullException("subcomponentsDelimiterAlternativeLexer", "Precondition: subcomponentsDelimiterAlternativeLexer != null");
            }

            this.subcomponentsDelimiterAlternativeLexer = subcomponentsDelimiterAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out SubcomponentsDelimiter element)
        {
            Alternative result;
            if (this.subcomponentsDelimiterAlternativeLexer.TryRead(scanner, out result))
            {
                element = new SubcomponentsDelimiter(result);
                return true;
            }

            element = default(SubcomponentsDelimiter);
            return false;
        }
    }
}