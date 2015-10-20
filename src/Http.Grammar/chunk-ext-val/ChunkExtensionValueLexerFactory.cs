namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkExtensionValueLexerFactory : ILexerFactory<ChunkExtensionValue>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<QuotedString> quotedStringLexerFactory;

        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ChunkExtensionValueLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Token> tokenLexerFactory,
            ILexerFactory<QuotedString> quotedStringLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            if (quotedStringLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(quotedStringLexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.tokenLexerFactory = tokenLexerFactory;
            this.quotedStringLexerFactory = quotedStringLexerFactory;
        }

        public ILexer<ChunkExtensionValue> Create()
        {
            var innerLexer = this.alternativeLexerFactory.Create(
                this.tokenLexerFactory.Create(),
                this.quotedStringLexerFactory.Create());
            return new ChunkExtensionValueLexer(innerLexer);
        }
    }
}