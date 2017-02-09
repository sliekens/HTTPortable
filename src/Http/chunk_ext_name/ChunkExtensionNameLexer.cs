using System;
using System.Collections.Generic;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext_name
{
    public sealed class ChunkExtensionNameLexer : Lexer<ChunkExtensionName>
    {
        public ChunkExtensionNameLexer([NotNull] ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Token> InnerLexer { get; }

        protected override IEnumerable<ChunkExtensionName> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var token in InnerLexer.Read(scanner, context))
            {
                yield return new ChunkExtensionName(token);
            }
        }
    }
}
