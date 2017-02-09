using System;
using Http.quoted_string;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext_val
{
    public sealed class ChunkExtensionValueLexerFactory : RuleLexerFactory<ChunkExtensionValue>
    {
        static ChunkExtensionValueLexerFactory()
        {
            Default = new ChunkExtensionValueLexerFactory(
                TokenLexerFactory.Default.Singleton(),
                QuotedStringLexerFactory.Default.Singleton());
        }

        public ChunkExtensionValueLexerFactory(
            [NotNull] ILexerFactory<Token> token,
            [NotNull] ILexerFactory<QuotedString> quotedString)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            if (quotedString == null)
            {
                throw new ArgumentNullException(nameof(quotedString));
            }
            Token = token;
            QuotedString = quotedString;
        }

        [NotNull]
        public static ChunkExtensionValueLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<QuotedString> QuotedString { get; set; }

        [NotNull]
        public ILexerFactory<Token> Token { get; set; }

        public override ILexer<ChunkExtensionValue> Create()
        {
            var innerLexer = Alternation.Create(
                Token.Create(),
                QuotedString.Create());
            return new ChunkExtensionValueLexer(innerLexer);
        }
    }
}
