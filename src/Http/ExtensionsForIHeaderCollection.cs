namespace Http
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Grammar.Rfc7230;
    using Headers;
    using SLANG;

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
            foreach (var header in instance)
            {
                if (!string.Equals(header.Name, ContentLengthHeader.FieldName, StringComparison.Ordinal))
                {
                    continue;
                }

                var value = header.SingleOrDefault();
                if (value == null)
                {
                    result = default(long);
                    return false; 
                }

                var lexer = new ContentLengthLexer();
                using (var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
                using (var pushbackInputStream = new PushbackInputStream(inputStream))
                using (ITextScanner scanner = new TextScanner(pushbackInputStream))
                {
                    scanner.Read();
                    ContentLength element;
                    if (!lexer.TryRead(scanner, out element))
                    {
                        continue;
                    }

                    if (long.TryParse(element.Data, NumberStyles.None, NumberFormatInfo.InvariantInfo, out result))
                    {
                        return true;
                    }
                }
            }

            result = default(long);
            return false;
        }
    }
}