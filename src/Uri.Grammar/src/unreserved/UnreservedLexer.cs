namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alternative> unreservedAlternativeLexer;

        public UnreservedLexer(ILexer<Alternative> unreservedAlternativeLexer)
        {
            if (unreservedAlternativeLexer == null)
            {
                throw new ArgumentNullException("unreservedAlternativeLexer", "Precondition: unreservedAlternativeLexer != null");
            }

            this.unreservedAlternativeLexer = unreservedAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Unreserved element)
        {
            Element result;
            if (this.unreservedAlternativeLexer.TryReadElement(scanner, out result))
            {
                element = new Unreserved(result);
                return true;
            }

            element = default(Unreserved);
            return false;
        }
    }
}