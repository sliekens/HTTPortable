using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.HTTP_version
{
    public class HttpVersion : Concatenation
    {
        public HttpVersion([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
