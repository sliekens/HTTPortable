namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    public class RequestMessage : IRequestMessage
    {
        private readonly IHeaderCollection headers;
        private readonly Version httpVersion;
        private readonly string method;
        private readonly string requestUri;

        public RequestMessage(string method, string requestUri, Version httpVersion)
            : this(method, requestUri, httpVersion, new HeaderCollection())
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(method));
            Contract.Requires(!string.IsNullOrWhiteSpace(requestUri));
            Contract.Requires(httpVersion != null);
        }

        public RequestMessage(string method, string requestUri, Version httpVersion, IHeaderCollection headers)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(method));
            Contract.Requires(!string.IsNullOrWhiteSpace(requestUri));
            Contract.Requires(httpVersion != null);
            Contract.Requires(headers != null);
            this.method = method;
            this.requestUri = requestUri;
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

        public Version HttpVersion
        {
            get
            {
                return this.httpVersion;
            }
        }

        public string Method
        {
            get
            {
                return this.method;
            }
        }

        public string RequestUri
        {
            get
            {
                return this.requestUri;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.method));
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.requestUri));
            Contract.Invariant(this.httpVersion != null);
            Contract.Invariant(this.headers != null);
        }
    }
}