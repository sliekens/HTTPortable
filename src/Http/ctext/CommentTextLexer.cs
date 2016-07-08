using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.ctext
{
    public sealed class CommentTextLexer : CompositeLexer<Alternation, CommentText>
    {
        public CommentTextLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
