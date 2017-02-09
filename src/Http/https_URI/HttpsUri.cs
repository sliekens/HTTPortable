using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.https_URI
{
    public class HttpsUri : Concatenation
    {
        public HttpsUri([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
