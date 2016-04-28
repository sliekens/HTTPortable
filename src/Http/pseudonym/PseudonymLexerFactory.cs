using System;
using Http.token;
using JetBrains.Annotations;
using Txt;

namespace Http.pseudonym
{
    public class PseudonymLexerFactory : ILexerFactory<Pseudonym>
    {
        private readonly ILexer<Token> tokenLexer;

        public PseudonymLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<Pseudonym> Create()
        {
            return new PseudonymLexer(tokenLexer);
        }
    }
}
