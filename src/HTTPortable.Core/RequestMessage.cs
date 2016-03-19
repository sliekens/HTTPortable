using System;

namespace HTTPortable.Core
{
    public class RequestMessage : IRequestMessage
    {
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
            Method = method;
            RequestUri = requestUri;
            HttpVersion = httpVersion;
            Headers = headers;
        }

        public IHeaderCollection Headers { get; }

        public Version HttpVersion { get; }

        public string Method { get; }

        public string RequestUri { get; }
    }
}