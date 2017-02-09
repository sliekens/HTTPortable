using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.pseudonym
{
    public sealed class PseudonymLexerFactory : RuleLexerFactory<Pseudonym>
    {
        static PseudonymLexerFactory()
        {
            Default = new PseudonymLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public PseudonymLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static PseudonymLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; set; }

        public override ILexer<Pseudonym> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new PseudonymLexer(innerLexer);
        }
    }
}
