using Http.token;
using JetBrains.Annotations;

namespace Http.protocol_name
{
    public class ProtocolName : Token
    {
        public ProtocolName([NotNull] Token token)
            : base(token)
        {
        }
    }
}
