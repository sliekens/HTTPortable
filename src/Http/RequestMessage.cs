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

        public RequestMessage(string method, string requestUri, Version httpVersion)
        {
            this.Method = method;
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

        public string Method { get; set; }

        public string RequestUri { get; set; }

        public Version HttpVersion { get; set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.headers != null);
        }

    }
}
