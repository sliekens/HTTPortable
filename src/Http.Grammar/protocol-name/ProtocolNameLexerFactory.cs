namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolNameLexerFactory : ILexerFactory<ProtocolName>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ProtocolNameLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<ProtocolName> Create()
        {
            var innerLexer = tokenLexerFactory.Create();
            return new ProtocolNameLexer(innerLexer);
        }
    }
}