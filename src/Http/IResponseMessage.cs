using System;
using System.Net;

namespace Http
{
    public interface IResponseMessage : IMessage
    {
        Version Version { get; set; }

        int Status { get; set; }

        string Reason { get; set; }
    }
}