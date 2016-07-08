using JetBrains.Annotations;
using Txt.Core;

namespace Http.Connection
{
    public sealed class ConnectionLexer : CompositeLexer<RequiredDelimitedList, Connection>
    {
        public ConnectionLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
