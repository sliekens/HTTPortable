using System;
using Http.token;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.method
{
    public class MethodLexerFactory : ILexerFactory<Method>
    {
        private readonly ILexer<Token> tokenLexer;

        public MethodLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<Method> Create()
        {
            return new MethodLexer(tokenLexer);
        }
    }
}
