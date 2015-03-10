namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    public class ResponseMessage : IResponseMessage
    {
        private readonly IHeaderCollection headers;
        private readonly Version httpVersion;
        private readonly string reason;
        private readonly int status;

        public ResponseMessage(Version httpVersion, int status, string reason)
            : this(httpVersion, status, new HeaderCollection(), reason)
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(status >= 100 && status <= 999);
            Contract.Requires(!string.IsNullOrWhiteSpace(reason));
        }

        public ResponseMessage(Version httpVersion, int status, IHeaderCollection headers, string reason)
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(headers != null);
            Contract.Requires(status >= 100 && status <= 999);
            Contract.Requires(!string.IsNullOrWhiteSpace(reason));
            this.httpVersion = httpVersion;
            this.headers = headers;
            this.reason = reason;
            this.status = status;
        }

        public IHeaderCollection Headers
        {
            get
            {
                return this.headers;
            }
        }

        /// <inheritdoc />
        public Version HttpVersion
        {
            get
            {
                return this.httpVersion;
            }
        }

        /// <inheritdoc />
        public string Reason
        {
            get
            {
                return this.reason;
            }
        }

        /// <inheritdoc />
        public int Status
        {
            get
            {
                return this.status;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpVersion != null);
            Contract.Invariant(this.headers != null);
            Contract.Invariant(this.status >= 100 && this.status <= 999);
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.reason));
        }
    }
}