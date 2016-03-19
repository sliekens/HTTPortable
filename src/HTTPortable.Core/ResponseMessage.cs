namespace Http
{
    using System;

    public class ResponseMessage : IResponseMessage
    {
        private readonly IHeaderCollection headers;
        private readonly Version httpVersion;
        private readonly string reason;
        private readonly int status;


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
            if (status < 100 || status > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(status));
            }
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("Argument is null or whitespace", nameof(reason));
            }
            this.httpVersion = httpVersion;
            this.headers = headers;
            this.reason = reason;
            this.status = status;
        }

        public IHeaderCollection Headers
        {
            get
            {
                return headers;
            }
        }

        /// <inheritdoc />
        public Version HttpVersion
        {
            get
            {
                return httpVersion;
            }
        }

        /// <inheritdoc />
        public string Reason
        {
            get
            {
                return reason;
            }
        }

        /// <inheritdoc />
        public int Status
        {
            get
            {
                return status;
            }
        }
    }
}