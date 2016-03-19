namespace Http
{
    using System;

    public class RequestMessage : IRequestMessage
    {
        private readonly IHeaderCollection headers;
        private readonly Version httpVersion;
        private readonly string method;
        private readonly string requestUri;

        public RequestMessage(string method, string requestUri, Version httpVersion)
            : this(method, requestUri, httpVersion, new HeaderCollection())
        {
        }

        public RequestMessage(string method, string requestUri, Version httpVersion, IHeaderCollection headers)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Argument is null or whitespace", nameof(method));
            }
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Argument is null or whitespace", nameof(requestUri));
            }
            if (httpVersion == null)
            {
                throw new ArgumentNullException(nameof(httpVersion));
            }
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            this.method = method;
            this.requestUri = requestUri;
            this.httpVersion = httpVersion;
            this.headers = headers;
        }

        public IHeaderCollection Headers
        {
            get
            {
                return headers;
            }
        }

        public Version HttpVersion
        {
            get
            {
                return httpVersion;
            }
        }

        public string Method
        {
            get
            {
                return method;
            }
        }

        public string RequestUri
        {
            get
            {
                return requestUri;
            }
        }
    }
}