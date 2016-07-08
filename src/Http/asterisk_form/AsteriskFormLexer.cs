using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.asterisk_form
{
    public sealed class AsteriskFormLexer : CompositeLexer<Terminal, AsteriskForm>
    {
        public AsteriskFormLexer([NotNull] ILexer<Terminal> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
