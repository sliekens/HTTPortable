using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Content_Length
{
    public sealed class ContentLengthLexer : CompositeLexer<Repetition, ContentLength>
    {
        public ContentLengthLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
