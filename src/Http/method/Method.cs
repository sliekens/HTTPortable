using Http.token;
using JetBrains.Annotations;

namespace Http.method
{
    public class Method : Token
    {
        public Method([NotNull] Token token)
            : base(token)
        {
        }
    }
}
