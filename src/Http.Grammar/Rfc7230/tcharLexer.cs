using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class TCharLexer : Lexer<TCharToken>
    {
        private readonly ILexer<AlphaToken> alphaLexer;
        private readonly ILexer<DigitToken> digitLexer;

        public TCharLexer()
            : this(new AlphaLexer(), new DigitLexer())
        {
        }

        public TCharLexer(ILexer<AlphaToken> alphaLexer, ILexer<DigitToken> digitLexer)
        {
            Contract.Requires(alphaLexer != null);
            Contract.Requires(digitLexer != null);
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }
        public override TCharToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            TCharToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'tchar'");
        }

        public override bool TryRead(ITextScanner scanner, out TCharToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default (TCharToken);
                return false;
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { '!', '#', '$', '%', '&', '\'', '*', '+', '-', '.', '^', '_', '`', '|', '~' })
            {
                if (scanner.TryMatch(c))
                {
                    token = new TCharToken(c, context);
                    return true;
                }
            }

            DigitToken digit;
            if (this.digitLexer.TryRead(scanner, out digit))
            {
                token = new TCharToken(digit, context);
                return true;
            }

            AlphaToken alpha;
            if (this.alphaLexer.TryRead(scanner, out alpha))
            {
                token = new TCharToken(alpha, context);
                return true;
            }

            token = default(TCharToken);
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
