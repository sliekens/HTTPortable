using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext_name
{
    public class ChunkExtensionName : Token
    {
        public ChunkExtensionName([NotNull] Token token)
            : base(token)
        {
        }
    }
}
