using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class TCharLexer : Lexer<TChar>
    {
        private readonly ILexer<Alpha> alphaLexer;
        private readonly ILexer<Digit> digitLexer;

        public TCharLexer()
            : this(new AlphaLexer(), new DigitLexer())
        {
        }

        public TCharLexer(ILexer<Alpha> alphaLexer, ILexer<Digit> digitLexer)
        {
            Contract.Requires(alphaLexer != null);
            Contract.Requires(digitLexer != null);
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }
        public override TChar Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            TChar token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'tchar'");
        }

        public override bool TryRead(ITextScanner scanner, out TChar token)
        {
            if (scanner.EndOfInput)
            {
                token = default (TChar);
                return false;
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { '!', '#', '$', '%', '&', '\'', '*', '+', '-', '.', '^', '_', '`', '|', '~' })
            {
                if (scanner.TryMatch(c))
                {
                    token = new TChar(c, context);
                    return true;
                }
            }

            Digit digit;
            if (this.digitLexer.TryRead(scanner, out digit))
            {
                token = new TChar(digit, context);
                return true;
            }

            Alpha alpha;
            if (this.alphaLexer.TryRead(scanner, out alpha))
            {
                token = new TChar(alpha, context);
                return true;
            }

            token = default(TChar);
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
