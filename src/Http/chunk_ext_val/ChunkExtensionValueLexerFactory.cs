using System;
using Http.quoted_string;
using Http.token;
using Txt;
using Txt.ABNF;

namespace Http.chunk_ext_val
{
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
            var innerLexer = alternativeLexerFactory.Create(
                tokenLexerFactory.Create(),
                quotedStringLexerFactory.Create());
            return new ChunkExtensionValueLexer(innerLexer);
        }
    }
}