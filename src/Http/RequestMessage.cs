using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Http
{
    public class RequestMessage : IRequestMessage
    {
        public RequestMessage(string method, string requestUri, Version httpVersion)
        {
            this.Method = method;
            this.RequestUri = requestUri;
            this.HttpVersion = httpVersion;
            this.Headers = new HeaderCollection();
        }

        public IHeaderCollection Headers { get; private set; }

        public string Method { get; set; }

        public string RequestUri { get; set; }

        public Version HttpVersion { get; set; }
    }
}
