using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Http
{
    using System.Diagnostics.Contracts;

    public class RequestMessage : IRequestMessage
    {
        private readonly IHeaderCollection headers;
        private readonly string method;

        public RequestMessage(string method, string requestUri, Version httpVersion)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(method));
            this.method = method;
            this.RequestUri = requestUri;
            this.HttpVersion = httpVersion;
            this.headers = new HeaderCollection();
        }

        public IHeaderCollection Headers
        {
            get
            {
                return this.headers;
            }
        }

        public string Method
        {
            get
            {
                return this.method;
            }
        }

        public string RequestUri { get; set; }

        public Version HttpVersion { get; set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(this.method));
            Contract.Invariant(this.headers != null);
        }

    }
}
