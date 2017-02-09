using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.method
{
    public sealed class MethodLexerFactory : RuleLexerFactory<Method>
    {
        static MethodLexerFactory()
        {
            Default = new MethodLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public MethodLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static MethodLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; }

        public override ILexer<Method> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new MethodLexer(innerLexer);
        }
    }
}
