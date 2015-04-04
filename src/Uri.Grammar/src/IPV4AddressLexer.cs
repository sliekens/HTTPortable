namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class IPv4AddressLexer : Lexer<IPv4Address>
    {
        private readonly ILexer<DecimalOctet> decimalOctetLexer;

        public IPv4AddressLexer()
            : this(new DecimalOctetLexer())
        {
        }

        public IPv4AddressLexer(ILexer<DecimalOctet> decimalOctetLexer)
            : base("IPv4address")
        {
            Contract.Requires(decimalOctetLexer != null);
            this.decimalOctetLexer = decimalOctetLexer;
        }

        public override bool TryRead(ITextScanner scanner, out IPv4Address element)
        {
            if (scanner.EndOfInput)
            {
                element = default(IPv4Address);
                return false;
            }

            var context = scanner.GetContext();
            DecimalOctet octet1, octet2, octet3, octet4;
            if (!this.decimalOctetLexer.TryRead(scanner, out octet1))
            {
                element = default(IPv4Address);
                return false;
            }

            if (scanner.EndOfInput || !scanner.TryMatch('.'))
            {
                scanner.PutBack(octet1.Data);
                element = default(IPv4Address);
                return false;
            }

            if (!this.decimalOctetLexer.TryRead(scanner, out octet2))
            {
                scanner.PutBack('.');
                scanner.PutBack(octet1.Data);
                element = default(IPv4Address);
                return false;
            }

            if (scanner.EndOfInput || !scanner.TryMatch('.'))
            {
                scanner.PutBack(octet2.Data);
                scanner.PutBack('.');
                scanner.PutBack(octet1.Data);
                element = default(IPv4Address);
                return false;
            }

            if (!this.decimalOctetLexer.TryRead(scanner, out octet3))
            {
                scanner.PutBack('.');
                scanner.PutBack(octet2.Data);
                scanner.PutBack('.');
                scanner.PutBack(octet1.Data);
                element = default(IPv4Address);
                return false;
            }

            if (scanner.EndOfInput || !scanner.TryMatch('.'))
            {
                scanner.PutBack(octet3.Data);
                scanner.PutBack('.');
                scanner.PutBack(octet2.Data);
                scanner.PutBack('.');
                scanner.PutBack(octet1.Data);
                element = default(IPv4Address);
                return false;
            }

            if (!this.decimalOctetLexer.TryRead(scanner, out octet4))
            {
                scanner.PutBack('.');
                scanner.PutBack(octet3.Data);
                scanner.PutBack('.');
                scanner.PutBack(octet2.Data);
                scanner.PutBack('.');
                scanner.PutBack(octet1.Data);
                element = default(IPv4Address);
                return false;
            }

            element = new IPv4Address(octet1, octet2, octet3, octet4, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.decimalOctetLexer != null);
        }
    }
}