using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Host
{
    public sealed class HostLexer : CompositeLexer<Concatenation, Host>
    {
        public HostLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
