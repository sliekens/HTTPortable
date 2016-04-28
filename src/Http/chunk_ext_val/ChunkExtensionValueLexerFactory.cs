using System;
using Http.quoted_string;
using Http.token;
using Txt;
using Txt.ABNF;

namespace Http.chunk_ext_val
{
    public class ChunkExtensionValueLexerFactory : ILexerFactory<ChunkExtensionValue>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<QuotedString> quotedStringLexerFactory;

        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ChunkExtensionValueLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<Token> tokenLexerFactory,
            ILexerFactory<QuotedString> quotedStringLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            if (quotedStringLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(quotedStringLexerFactory));
            }

            this.alternationLexerFactory = alternationLexerFactory;
            this.tokenLexerFactory = tokenLexerFactory;
            this.quotedStringLexerFactory = quotedStringLexerFactory;
        }

        public ILexer<ChunkExtensionValue> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                tokenLexerFactory.Create(),
                quotedStringLexerFactory.Create());
            return new ChunkExtensionValueLexer(innerLexer);
        }
    }
}