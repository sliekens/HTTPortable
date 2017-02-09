using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.http_URI
{
    public class HttpUri : Concatenation
    {
        public HttpUri([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
