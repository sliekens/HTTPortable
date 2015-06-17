namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;

    public class TokenLexer : RepetitionLexer<Token, TokenCharacter>
    {
        private readonly ILexer<TokenCharacter> tokenCharacterLexer;

        public TokenLexer()
            : this(new TokenCharacterLexer())
        {
        }

        public TokenLexer(ILexer<TokenCharacter> tokenCharacterLexer)
            : base("token", 1, int.MaxValue)
        {
            Contract.Requires(tokenCharacterLexer != null);
            this.tokenCharacterLexer = tokenCharacterLexer;
        }

        protected override Token CreateInstance(IList<TokenCharacter> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new Token(elements, context);
        }

        protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out TokenCharacter element)
        {
            return this.tokenCharacterLexer.TryRead(scanner, out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenCharacterLexer != null);
        }
    }
}