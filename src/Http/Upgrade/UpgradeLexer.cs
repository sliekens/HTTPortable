using JetBrains.Annotations;
using Txt.Core;

namespace Http.Upgrade
{
    public sealed class UpgradeLexer : CompositeLexer<RequiredDelimitedList, Upgrade>
    {
        public UpgradeLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
