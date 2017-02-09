using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_message
{
    public class HttpMessage : Concatenation
    {
        public HttpMessage([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
