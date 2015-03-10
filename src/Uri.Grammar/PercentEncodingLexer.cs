namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class PercentEncodingLexer : Lexer<PercentEncoding>
    {
        private readonly ILexer<HexadecimalDigit> hexDigLexer;

        public PercentEncodingLexer()
            : this(new HexadecimalDigitLexer())
        {
        }

        public PercentEncodingLexer(ILexer<HexadecimalDigit> hexDigLexer)
            : base("pct-encoded")
        {
            Contract.Requires(hexDigLexer != null);
            this.hexDigLexer = hexDigLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PercentEncoding element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PercentEncoding);
                return false;
            }

            var context = scanner.GetContext();
            if (!scanner.TryMatch('%'))
            {
                element = default(PercentEncoding);
                return false;
            }

            HexadecimalDigit hexadecimalDigit1;
            if (!this.hexDigLexer.TryRead(scanner, out hexadecimalDigit1))
            {
                scanner.PutBack('%');
                element = default(PercentEncoding);
                return false;
            }


            HexadecimalDigit hexadecimalDigit2;
            if (!this.hexDigLexer.TryRead(scanner, out hexadecimalDigit2))
            {
                scanner.PutBack(hexadecimalDigit1.Data);
                scanner.PutBack('%');
                element = default(PercentEncoding);
                return false;
            }

            element = new PercentEncoding(hexadecimalDigit1, hexadecimalDigit2, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexDigLexer != null);
        }
    }
}