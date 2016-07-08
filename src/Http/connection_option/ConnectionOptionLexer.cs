using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.connection_option
{
    public sealed class ConnectionOptionLexer : CompositeLexer<Token, ConnectionOption>
    {
        public ConnectionOptionLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
