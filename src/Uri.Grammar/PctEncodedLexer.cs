using System;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Uri.Grammar
{
    public class PctEncodedLexer : Lexer<PctEncodedToken>
    {
        private readonly ILexer<HexDigToken> hexDigLexer;

        public PctEncodedLexer(ILexer<HexDigToken> hexDigLexer)
        {
            Contract.Requires(hexDigLexer != null);
            this.hexDigLexer = hexDigLexer;
        }

        public override PctEncodedToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            PctEncodedToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'pct-encoded'");
        }

        public override bool TryRead(ITextScanner scanner, out PctEncodedToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(PctEncodedToken);
                return false;
            }

            var context = scanner.GetContext();
            if (!scanner.TryMatch('%'))
            {
                token = default(PctEncodedToken);
                return false;
            }

            HexDigToken hexDig1;
            if (!this.hexDigLexer.TryRead(scanner, out hexDig1))
            {
                scanner.PutBack('%');
                token = default(PctEncodedToken);
                return false;
            }


            HexDigToken hexDig2;
            if (!this.hexDigLexer.TryRead(scanner, out hexDig2))
            {
                this.hexDigLexer.PutBack(scanner, hexDig1);
                scanner.PutBack('%');
                token = default(PctEncodedToken);
                return false;
            }

            token = new PctEncodedToken(hexDig1, hexDig2, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexDigLexer != null);
        }
    }
}