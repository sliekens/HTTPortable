using Http.OWS;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.BWS
{
    public sealed class BadWhiteSpaceLexer : CompositeLexer<OptionalWhiteSpace, BadWhiteSpace>
    {
        public BadWhiteSpaceLexer([NotNull] ILexer<OptionalWhiteSpace> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
