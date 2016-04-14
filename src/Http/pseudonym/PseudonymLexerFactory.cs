using System;
using Http.token;
using Txt;

namespace Http.pseudonym
{
    public class PseudonymLexerFactory : ILexerFactory<Pseudonym>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public PseudonymLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<Pseudonym> Create()
        {
            var innerLexer = tokenLexerFactory.Create();
            return new PseudonymLexer(innerLexer);
        }
    }
}