using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.ctext
{
    public class CommentText : Alternation
    {
        public CommentText([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
