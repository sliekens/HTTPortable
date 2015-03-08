using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
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
            TokenCharacter tokenCharacter;
            if (!this.tokenCharacterLexer.TryRead(scanner, out tokenCharacter))
            {
                token = default(Token);
                return false;
            }

            List<TokenCharacter> list = new List<TokenCharacter> { tokenCharacter };
            while (this.tokenCharacterLexer.TryRead(scanner, out tokenCharacter))
            {
                list.Add(tokenCharacter);
            }

            token = new Token(new ReadOnlyCollection<TokenCharacter>(list), context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenCharacterLexer != null);
        }

    }
}
