namespace Http
{
    using System;
    using System.Linq;

    /// <summary>Provides extension methods for the <see cref="T:Http.IHeaderCollection" /> interface.</summary>
    public static class ExtensionsForIHeaderCollection
    {
        /// <summary>
        /// Gets and converts the string representation of the 'Content-Length' header to its 64-bit signed integer
        /// equivalent. A return value indicates whether the header was present and that the conversion succeeded or failed.
        /// </summary>
        /// <param name="instance">The instance to extend.</param>
        /// <param name="result">
        /// When this method returns, contains the 64-bit signed integer value equivalent of the
        /// 'Content-Length' header, if the operation succeeded, or zero if the conversion failed.
        /// </param>
        /// <returns><c>true</c> if the operation was successful; otherwise, <c>false</c>.</returns>
        public static bool TryGetContentLength(this IHeaderCollection instance, out long result)
        {
            var header = instance.SingleOrDefault(h => string.Equals(h.Name, "Content-Length", StringComparison.Ordinal));
            if (header != null && long.TryParse(header.FirstOrDefault(), out result))
            {
                return true;
            }

            result = default (long);
            return false;
        }
    }
}