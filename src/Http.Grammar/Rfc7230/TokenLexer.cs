namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class TokenLexer : Lexer<Token>
    {
        private readonly ILexer<TokenCharacter> tokenCharacterLexer;

        public TokenLexer()
            : this(new TokenCharacterLexer())
        {
        }

        public TokenLexer(ILexer<TokenCharacter> tokenCharacterLexer)
            : base("token")
        {
            Contract.Requires(tokenCharacterLexer != null);
            this.tokenCharacterLexer = tokenCharacterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Token token)
        {
            if (scanner.EndOfInput)
            {
                token = default(Token);
                return false;
            }

            var context = scanner.GetContext();
            var elements = new List<TokenCharacter>();
            TokenCharacter tokenCharacter;
            while (this.tokenCharacterLexer.TryRead(scanner, out tokenCharacter))
            {
                elements.Add(tokenCharacter);
            }

            if (elements.Count == 0)
            {
                token = default(Token);
                return false;
            }

            token = new Token(new ReadOnlyCollection<TokenCharacter>(elements), context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenCharacterLexer != null);
        }
    }
}