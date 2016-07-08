using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.comment
{
    public sealed class CommentLexer : CompositeLexer<Concatenation, Comment>
    {
        public CommentLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
