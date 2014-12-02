using System;
using System.Net.Http;

namespace Http
{
    public interface IRequest : IMessage
    {
        HttpMethod Method { get; set; }

        string Uri { get; set; }

        Version Version { get; set; }

        IRequestHeaderCollection RequestHeaders { get; set; }
    }
}