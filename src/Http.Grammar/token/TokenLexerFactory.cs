namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class TokenLexerFactory : ILexerFactory<Token>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<TokenCharacter> tokenCharacterLexerFactory;

        public TokenLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<TokenCharacter> tokenCharacterLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (tokenCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenCharacterLexerFactory));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.tokenCharacterLexerFactory = tokenCharacterLexerFactory;
        }

        public ILexer<Token> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(
                tokenCharacterLexerFactory.Create(),
                1,
                int.MaxValue);
            return new TokenLexer(innerLexer);
        }
    }
}