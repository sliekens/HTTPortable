using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class TokenCharacterLexer : Lexer<TokenCharacter>
    {
        private readonly ILexer<Alpha> alphaLexer;
        private readonly ILexer<Digit> digitLexer;

        public TokenCharacterLexer()
            : this(new AlphaLexer(), new DigitLexer())
        {
        }

        public TokenCharacterLexer(ILexer<Alpha> alphaLexer, ILexer<Digit> digitLexer)
            : base("tchar")
        {
            Contract.Requires(alphaLexer != null);
            Contract.Requires(digitLexer != null);
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }
  
        public override bool TryRead(ITextScanner scanner, out TokenCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default (TokenCharacter);
                return false;
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { '!', '#', '$', '%', '&', '\'', '*', '+', '-', '.', '^', '_', '`', '|', '~' })
            {
                if (scanner.TryMatch(c))
                {
                    element = new TokenCharacter(c, context);
                    return true;
                }
            }

            Digit digit;
            if (this.digitLexer.TryRead(scanner, out digit))
            {
                element = new TokenCharacter(digit, context);
                return true;
            }

            Alpha alpha;
            if (this.alphaLexer.TryRead(scanner, out alpha))
            {
                element = new TokenCharacter(alpha, context);
                return true;
            }

            element = default(TokenCharacter);
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
