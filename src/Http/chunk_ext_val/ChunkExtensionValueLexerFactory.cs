using System;
using Http.quoted_string;
using Http.token;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.chunk_ext_val
{
    public class ChunkExtensionValueLexerFactory : ILexerFactory<ChunkExtensionValue>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<QuotedString> quotedStringLexer;

        private readonly ILexer<Token> tokenLexer;

        public ChunkExtensionValueLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<Token> tokenLexer,
            [NotNull] ILexer<QuotedString> quotedStringLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            if (quotedStringLexer == null)
            {
                throw new ArgumentNullException(nameof(quotedStringLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.tokenLexer = tokenLexer;
            this.quotedStringLexer = quotedStringLexer;
        }

        public ILexer<ChunkExtensionValue> Create()
        {
            var innerLexer = alternationLexerFactory.Create(
                tokenLexer,
                quotedStringLexer);
            return new ChunkExtensionValueLexer(innerLexer);
        }
    }
}
