using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.HTTP_name
{
    public class HttpName : Terminal
    {
        public HttpName([NotNull] Terminal terminal)
            : base(terminal)
        {
        }
    }
}
