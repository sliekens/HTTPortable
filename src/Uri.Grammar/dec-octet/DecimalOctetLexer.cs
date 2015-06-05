namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class DecimalOctetLexer : Lexer<DecimalOctet>
    {
        private readonly ILexer<Alternative> innerLexer;

        public DecimalOctetLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out DecimalOctet element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new DecimalOctet(result);
                return true;
            }

            element = default(DecimalOctet);
            return false;
        }
    }
}