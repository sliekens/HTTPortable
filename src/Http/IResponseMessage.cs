using System;

namespace Http
{
    /// <summary>Provides the interface for response messages.</summary>
    public interface IResponseMessage : IMessage
    {
        Version HttpVersion { get; set; }

        int Status { get; set; }

        string Reason { get; set; }
    }
}