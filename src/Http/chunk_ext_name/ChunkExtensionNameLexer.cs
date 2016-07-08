using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.chunk_ext_name
{
    public sealed class ChunkExtensionNameLexer : CompositeLexer<Token, ChunkExtensionName>
    {
        public ChunkExtensionNameLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
