using System;
using System.Linq;

namespace Http
{
    /// <summary>Provides extension methods for the <see cref="T:Http.IHeaderCollection" /> interface.</summary>
    public static class ExtensionsForIHeaderCollection
    {
        public static bool TryGetContentLength(this IHeaderCollection instance, out long result)
        {
            var header = instance.SingleOrDefault(h => h.Name.Equals("Content-Length", StringComparison.Ordinal));
            if (header != null && long.TryParse(header.FirstOrDefault(), out result))
            {
                return true;
            }
            
            result = default (long);
            return false;
        }
    }
}
