using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.absolute_path
{
    public sealed class AbsolutePathLexer : CompositeLexer<Repetition, AbsolutePath>
    {
        public AbsolutePathLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
