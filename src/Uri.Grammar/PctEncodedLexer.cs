using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Uri.Grammar
{
    public class PctEncodedLexer : Lexer<PctEncoded>
    {
        private readonly ILexer<HexadecimalDigit> hexDigLexer;

        public PctEncodedLexer()
            : this(new HexadecimalDigitLexer())
        {
        }

        public PctEncodedLexer(ILexer<HexadecimalDigit> hexDigLexer)
        {
            Contract.Requires(hexDigLexer != null);
            this.hexDigLexer = hexDigLexer;
        }

        public override PctEncoded Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            PctEncoded element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'pct-encoded'");
        }

        public override bool TryRead(ITextScanner scanner, out PctEncoded element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PctEncoded);
                return false;
            }

            var context = scanner.GetContext();
            if (!scanner.TryMatch('%'))
            {
                element = default(PctEncoded);
                return false;
            }

            HexadecimalDigit hexDig1;
            if (!this.hexDigLexer.TryRead(scanner, out hexDig1))
            {
                scanner.PutBack('%');
                element = default(PctEncoded);
                return false;
            }


            HexadecimalDigit hexDig2;
            if (!this.hexDigLexer.TryRead(scanner, out hexDig2))
            {
                this.hexDigLexer.PutBack(scanner, hexDig1);
                scanner.PutBack('%');
                element = default(PctEncoded);
                return false;
            }

            element = new PctEncoded(hexDig1, hexDig2, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexDigLexer != null);
        }
    }
}