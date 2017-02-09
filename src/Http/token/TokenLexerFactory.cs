using System;
using Http.tchar;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.token
{
    public class TokenLexerFactory : RuleLexerFactory<Token>
    {
        static TokenLexerFactory()
        {
            Default = new TokenLexerFactory(tchar.TokenCharacterLexerFactory.Default.Singleton());
        }

        public TokenLexerFactory([NotNull] ILexerFactory<TokenCharacter> tokenCharacterLexerFactory)
        {
            if (tokenCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenCharacterLexerFactory));
            }
            TokenCharacterLexerFactory = tokenCharacterLexerFactory;
        }

        [NotNull]
        public static TokenLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<TokenCharacter> TokenCharacterLexerFactory { get; }

        public override ILexer<Token> Create()
        {
            var innerLexer = Repetition.Create(TokenCharacterLexerFactory.Create(), 1, int.MaxValue);
            return new TokenLexer(innerLexer);
        }
    }
}
