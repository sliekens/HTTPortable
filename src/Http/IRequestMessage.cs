using System;
using System.Net.Http;

namespace Http
{
    public interface IRequestMessage : IMessage
    {
        string Method { get; set; }

        string RequestUri { get; set; }

        Version HttpVersion { get; set; }

        IRequestHeaderCollection RequestHeaders { get; set; }
    }
}