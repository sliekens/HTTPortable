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
            this.Headers = new GeneralHeaderCollection();
            this.RequestHeaders = new RequestHeaderCollection();
            this.ContentHeaders = new EntityHeaderCollection();
        }

        public IGeneralHeaderCollection Headers { get; private set; }

        public IEntityHeaderCollection ContentHeaders { get; set; }

        public string Method { get; set; }

        public string RequestUri { get; set; }

        public Version HttpVersion { get; set; }

        public IRequestHeaderCollection RequestHeaders { get; set; }
    }
}
