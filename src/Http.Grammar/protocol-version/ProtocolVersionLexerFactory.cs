namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolVersionLexerFactory : ILexerFactory<ProtocolVersion>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ProtocolVersionLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<ProtocolVersion> Create()
        {
            var innerLexer = tokenLexerFactory.Create();
            return new ProtocolVersionLexer(innerLexer);
        }
    }
}