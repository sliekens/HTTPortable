namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<Alternative> reservedAlterativeLexer;

        public ReservedLexer(ILexer<Alternative> reservedAlterativeLexer)
        {
            if (reservedAlterativeLexer == null)
            {
                throw new ArgumentNullException("reservedAlterativeLexer", "Precondition: reservedAlterativeLexer != null");
            }

            this.reservedAlterativeLexer = reservedAlterativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Reserved element)
        {
            Element result;
            if (this.reservedAlterativeLexer.TryReadElement(scanner, out result))
            {
                element = new Reserved(result);
                return true;
            }

            element = default(Reserved);
            return false;
        }
    }
}