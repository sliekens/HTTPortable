namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class IPLiteralLexer : Lexer<IPLiteral>
    {
        private readonly ILexer<IPv6Address> ipv6AddressLexer;

        private readonly ILexer<IPvFuture> ipvFutureLexer; 

        public IPLiteralLexer(ILexer<IPv6Address> ipv6AddressLexer, ILexer<IPvFuture> ipvFutureLexer)
            : base("IP-literal")
        {
            Contract.Requires(ipv6AddressLexer != null);
            Contract.Requires(ipvFutureLexer != null);
            this.ipv6AddressLexer = ipv6AddressLexer;
            this.ipvFutureLexer = ipvFutureLexer;
        }

        public override bool TryRead(ITextScanner scanner, out IPLiteral element)
        {
            if (scanner.EndOfInput)
            {
                element = default(IPLiteral);
                return false;
            }

            var context = scanner.GetContext();
            Element openingBracket;
            if (!this.TryReadOpeningBracket(scanner, out openingBracket))
            {
                element = default(IPLiteral);
                return false;
            }

            IPv6Address ipv6Address;
            if (this.ipv6AddressLexer.TryRead(scanner, out ipv6Address))
            {
                Element closingBracket;
                if (!this.TryReadClosingBracket(scanner, out closingBracket))
                {
                    scanner.PutBack(ipv6Address.Data);
                    scanner.PutBack(openingBracket.Data);
                    element = default(IPLiteral);
                    return false;
                }

                element = new IPLiteral(openingBracket, ipv6Address, closingBracket, context);
                return true;
            }
            
            IPvFuture ipvFutureAddress;
            if (this.ipvFutureLexer.TryRead(scanner, out ipvFutureAddress))
            {
                Element closingBracket;
                if (!this.TryReadClosingBracket(scanner, out closingBracket))
                {
                    scanner.PutBack(ipvFutureAddress.Data);
                    scanner.PutBack(openingBracket.Data);
                    element = default(IPLiteral);
                    return false;
                }

                element = new IPLiteral(openingBracket, ipvFutureAddress, closingBracket, context);
                return true;
            }

            scanner.PutBack(openingBracket.Data);
            element = default(IPLiteral);
            return false;
        }

        private bool TryReadOpeningBracket(ITextScanner scanner, out Element element)
        {
            if (scanner == null)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('['))
            {
                element = new Element("[", context);
                return true;
            }

            element = default(Element);
            return false;
        }

        private bool TryReadClosingBracket(ITextScanner scanner, out Element element)
        {
            if (scanner == null)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch(']'))
            {
                element = new Element("]", context);
                return true;
            }

            element = default(Element);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ipv6AddressLexer != null);
            Contract.Invariant(this.ipvFutureLexer != null);
        }
    }
}
