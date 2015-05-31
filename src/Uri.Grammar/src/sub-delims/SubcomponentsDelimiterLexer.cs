namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class SubcomponentsDelimiterLexer : Lexer<SubcomponentsDelimiter>
    {
        private readonly ILexer<Alternative> subcomponentsDelimiterAlternativeLexer; 

        public SubcomponentsDelimiterLexer(ILexer<Alternative> subcomponentsDelimiterAlternativeLexer)
            : base("sub-delims")
        {
            if (subcomponentsDelimiterAlternativeLexer == null)
            {
                throw new ArgumentNullException("subcomponentsDelimiterAlternativeLexer", "Precondition: subcomponentsDelimiterAlternativeLexer != null");
            }

            this.subcomponentsDelimiterAlternativeLexer = subcomponentsDelimiterAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out SubcomponentsDelimiter element)
        {
            Element result;
            if (this.subcomponentsDelimiterAlternativeLexer.TryReadElement(scanner, out result))
            {
                element = new SubcomponentsDelimiter(result);
                return true;
            }

            element = default(SubcomponentsDelimiter);
            return false;
        }
    }
}