namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out DecimalOctet element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new DecimalOctet(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(DecimalOctet);
            return false;
        }
    }
}