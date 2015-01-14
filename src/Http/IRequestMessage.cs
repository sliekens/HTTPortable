using System;

namespace Http
{
    public interface IRequestMessage : IMessage
    {
        string Method { get; }

        string RequestUri { get; set; }

        Version HttpVersion { get; set; }
    }
}