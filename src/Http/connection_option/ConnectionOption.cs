using Http.token;
using JetBrains.Annotations;

namespace Http.connection_option
{
    public class ConnectionOption : Token
    {
        public ConnectionOption([NotNull] Token token)
            : base(token)
        {
        }
    }
}
