namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HttpNameLexerFactory : ILexerFactory<HttpName>
    {
        private readonly IStringLexerFactory caseSensitiveStringLexerFactory;

        public HttpNameLexerFactory(IStringLexerFactory caseSensitiveStringLexerFactory)
        {
            if (caseSensitiveStringLexerFactory == null)
            {
                throw new ArgumentNullException("caseSensitiveStringLexerFactory");
            }

            this.caseSensitiveStringLexerFactory = caseSensitiveStringLexerFactory;
        }

        public ILexer<HttpName> Create()
        {
            var innerLexer = this.caseSensitiveStringLexerFactory.Create(new[] { '\x48', '\x54', '\x54', '\x50' });
            return new HttpNameLexer(innerLexer);
        }
    }
}