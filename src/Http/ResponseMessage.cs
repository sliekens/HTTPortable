namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    public class ResponseMessage : IResponseMessage
    {
        private readonly IHeaderCollection headers;
        private readonly Version httpVersion;
        private readonly int status;

        public ResponseMessage(Version httpVersion, int status)
            : this(httpVersion, status, new HeaderCollection())
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(status >= 100 && status <= 999);
        }

        public ResponseMessage(Version httpVersion, int status, IHeaderCollection headers)
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(headers != null);
            Contract.Requires(status >= 100 && status <= 999);
            this.httpVersion = httpVersion;
            this.headers = headers;
            this.status = status;
        }

        public IHeaderCollection Headers
        {
            get
            {
                return this.headers;
            }
        }

        public string Reason { get; set; }

        /// <inheritdoc />
        public int Status
        {
            get
            {
                return this.status;
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

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.httpVersion != null);
            Contract.Invariant(this.headers != null);
            Contract.Invariant(this.status >= 100 && this.status <= 999);
        }
    }
}