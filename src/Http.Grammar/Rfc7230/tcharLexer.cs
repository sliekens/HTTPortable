using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class tcharLexer : Lexer<tchar>
    {
        private readonly ILexer<AlphaToken> alphaLexer;
        private readonly ILexer<DigitToken> digitLexer;

        public tcharLexer()
            : this(new AlphaLexer(), new DigitLexer())
        {
        }

        public tcharLexer(ILexer<AlphaToken> alphaLexer, ILexer<DigitToken> digitLexer)
        {
            Contract.Requires(alphaLexer != null);
            Contract.Requires(digitLexer != null);
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }
        public override tchar Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            tchar token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'tchar'");
        }

        public override bool TryRead(ITextScanner scanner, out tchar token)
        {
            var context = scanner.GetContext();
            foreach (var c in new[] { '!', '#', '$', '%', '&', '\'', '*', '+', '-', '.', '^', '_', '`', '|', '~' })
            {
                if (scanner.TryMatch(c))
                {
                    token = new tchar(c, context);
                    return true;
                }
            }

            DigitToken digit;
            if (this.digitLexer.TryRead(scanner, out digit))
            {
                token = new tchar(digit, context);
                return true;
            }

            AlphaToken alpha;
            if (this.alphaLexer.TryRead(scanner, out alpha))
            {
                token = new tchar(alpha, context);
                return true;
            }

            token = default(tchar);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.alphaLexer != null);
            Contract.Invariant(this.digitLexer != null);
        }
    }
}
