namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPv6AddressLexer : Lexer<IPv6Address>
    {
        private readonly ILexer<Alternative> innerLexer;

        public IPv6AddressLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out IPv6Address element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new IPv6Address(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(IPv6Address);
            return false;
        }
    }
}