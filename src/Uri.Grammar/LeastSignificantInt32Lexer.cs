namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class LeastSignificantInt32Lexer : Lexer<LeastSignificantInt32>
    {
        private readonly ILexer<HexadecimalInt16> hexadecimalInt16Lexer;
        private readonly ILexer<IPv4Address> ipv4AddressLexer;

        public LeastSignificantInt32Lexer()
            : this(new HexadecimalInt16Lexer(), new IPv4AddressLexer())
        {
        }

        public LeastSignificantInt32Lexer(ILexer<HexadecimalInt16> hexadecimalInt16Lexer, 
            ILexer<IPv4Address> ipv4AddressLexer)
            : base("ls32")
        {
            Contract.Requires(hexadecimalInt16Lexer != null);
            Contract.Requires(ipv4AddressLexer != null);
            this.hexadecimalInt16Lexer = hexadecimalInt16Lexer;
            this.ipv4AddressLexer = ipv4AddressLexer;
        }

        public override bool TryRead(ITextScanner scanner, out LeastSignificantInt32 element)
        {
            if (scanner.EndOfInput)
            {
                element = default(LeastSignificantInt32);
                return false;
            }

            if (this.TryReadAsHexadecimal(scanner, out element))
            {
                return true;
            }

            if (this.TryReadAsIPv4Address(scanner, out element))
            {
                return true;
            }

            element = default(LeastSignificantInt32);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexadecimalInt16Lexer != null);
            Contract.Invariant(this.ipv4AddressLexer != null);
        }

        private bool TryReadAsHexadecimal(ITextScanner scanner, out LeastSignificantInt32 element)
        {
            HexadecimalInt16 left, right;
            var context = scanner.GetContext();
            if (!this.hexadecimalInt16Lexer.TryRead(scanner, out left))
            {
                element = default(LeastSignificantInt32);
                return false;
            }

            if (scanner.EndOfInput || !scanner.TryMatch(':'))
            {
                scanner.PutBack(left.Data);
                element = default(LeastSignificantInt32);
                return false;
            }

            if (!this.hexadecimalInt16Lexer.TryRead(scanner, out right))
            {
                scanner.PutBack(':');
                scanner.PutBack(left.Data);
                element = default(LeastSignificantInt32);
                return false;
            }

            element = new LeastSignificantInt32(left, right, context);
            return true;
        }

        private bool TryReadAsIPv4Address(ITextScanner scanner, out LeastSignificantInt32 element)
        {
            IPv4Address address;
            var context = scanner.GetContext();
            if (this.ipv4AddressLexer.TryRead(scanner, out address))
            {
                element = new LeastSignificantInt32(address, context);
                return true;
            }

            element = default(LeastSignificantInt32);
            return false;
        }
    }
}