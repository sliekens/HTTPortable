namespace Http
{
    using System;

    public class ResponseMessage : IResponseMessage
    {
        public ResponseMessage(Version httpVersion, int status, string reason)
            : this(httpVersion, status, new HeaderCollection(), reason)
        {
        }

        public ResponseMessage(Version httpVersion, int status, IHeaderCollection headers, string reason)
        {
            if (httpVersion == null)
            {
                throw new ArgumentNullException(nameof(httpVersion));
            }
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if ((status < 100) || (status > 999))
            {
                throw new ArgumentOutOfRangeException(nameof(status));
            }
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("Argument is null or whitespace", nameof(reason));
            }
            HttpVersion = httpVersion;
            Headers = headers;
            Reason = reason;
            Status = status;
        }

        public IHeaderCollection Headers { get; }

        /// <inheritdoc />
        public Version HttpVersion { get; }

        /// <inheritdoc />
        public string Reason { get; }

        /// <inheritdoc />
        public int Status { get; }
    }
}