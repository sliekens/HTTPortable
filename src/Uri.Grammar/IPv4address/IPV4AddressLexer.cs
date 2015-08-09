namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPv4AddressLexer : Lexer<IPv4Address>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">dec-octet "." dec-octet "." dec-octet "." dec-octet</param>
        public IPv4AddressLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out IPv4Address element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new IPv4Address(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(IPv4Address);
            return false;
        }
    }
}