using System;
using Http.tchar;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.token
{
    public class TokenLexerFactory : ILexerFactory<Token>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<TokenCharacter> tokenCharacterLexer;

        public TokenLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<TokenCharacter> tokenCharacterLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (tokenCharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenCharacterLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.tokenCharacterLexer = tokenCharacterLexer;
        }

        public ILexer<Token> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(
                tokenCharacterLexer,
                1,
                int.MaxValue);
            return new TokenLexer(innerLexer);
        }
    }
}
