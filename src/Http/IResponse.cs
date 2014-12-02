using System;
using System.Net;

namespace Http
{
    public interface IResponse : IMessage
    {
        Version Version { get; set; }

        HttpStatusCode Status { get; set; }

        string Reason { get; set; }
    }
}