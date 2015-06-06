namespace Uri.Grammar
{
    using System;

    using SLANG;

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

        public override bool TryRead(ITextScanner scanner, out IPv6Address element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new IPv6Address(result);
                return true;
            }

            element = default(IPv6Address);
            return false;
        }
    }
}