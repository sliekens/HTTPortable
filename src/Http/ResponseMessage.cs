namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    public class ResponseMessage : IResponseMessage
    {
        private readonly IHeaderCollection headers;
        private readonly Version httpVersion;

        public ResponseMessage(Version httpVersion)
            : this(httpVersion, new HeaderCollection())
        {
            Contract.Requires(httpVersion != null);
        }

        public ResponseMessage(Version httpVersion, IHeaderCollection headers)
        {
            Contract.Requires(httpVersion != null);
            Contract.Requires(headers != null);
            this.httpVersion = httpVersion;
            this.headers = headers;
        }

        public IHeaderCollection Headers
        {
            get
            {
                return this.headers;
            }
        }

        public string Reason { get; set; }
        public int Status { get; set; }

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
        }
    }
}