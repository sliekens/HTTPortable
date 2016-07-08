using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.RWS
{
    public sealed class RequiredWhiteSpaceLexer : CompositeLexer<Repetition, RequiredWhiteSpace>
    {
        public RequiredWhiteSpaceLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
