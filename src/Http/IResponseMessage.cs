using System;
using System.Net;

namespace Http
{
    public interface IResponseMessage : IMessage
    {
        Version Version { get; set; }

        HttpStatusCode Status { get; set; }

        string Reason { get; set; }

        IResponseHeaderCollection ResponseHeaders { get; set; }
    }
}