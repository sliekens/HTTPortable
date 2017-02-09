using Http.token;
using JetBrains.Annotations;

namespace Http.protocol_version
{
    public class ProtocolVersion : Token
    {
        public ProtocolVersion([NotNull] Token token)
            : base(token)
        {
        }
    }
}
