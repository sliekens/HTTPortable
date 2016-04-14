using System;
using Http.token;
using Txt;

namespace Http.method
{
    public class MethodLexerFactory : ILexerFactory<Method>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public MethodLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<Method> Create()
        {
            var innerLexer = tokenLexerFactory.Create();
            return new MethodLexer(innerLexer);
        }
    }
}