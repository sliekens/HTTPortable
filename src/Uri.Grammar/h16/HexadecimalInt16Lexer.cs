namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class HexadecimalInt16Lexer : Lexer<HexadecimalInt16>
    {
        private readonly ILexer<HexadecimalDigit> hexadecimalDigitLexer;

        public HexadecimalInt16Lexer()
            : this(new HexadecimalDigitLexer())
        {
        }

        public HexadecimalInt16Lexer(ILexer<HexadecimalDigit> hexadecimalDigitLexer)
            : base("h16")
        {
            Contract.Requires(hexadecimalDigitLexer != null);
            this.hexadecimalDigitLexer = hexadecimalDigitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HexadecimalInt16 element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HexadecimalInt16);
                return false;
            }

            var context = scanner.GetContext();
            HexadecimalDigit digit1, digit2, digit3, digit4;
            if (!this.hexadecimalDigitLexer.TryRead(scanner, out digit1))
            {
                element = default(HexadecimalInt16);
                return false;
            }

            if (!this.hexadecimalDigitLexer.TryRead(scanner, out digit2))
            {
                element = new HexadecimalInt16(digit1, context);
                return true;
            }

            if (!this.hexadecimalDigitLexer.TryRead(scanner, out digit3))
            {
                element = new HexadecimalInt16(digit1, digit2, context);
                return true;
            }

            if (!this.hexadecimalDigitLexer.TryRead(scanner, out digit4))
            {
                element = new HexadecimalInt16(digit1, digit2, digit3, context);
                return true;
            }

            element = new HexadecimalInt16(digit1, digit2, digit3, digit4, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexadecimalDigitLexer != null);
        }
    }
}