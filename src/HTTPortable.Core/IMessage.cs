namespace Http
{
    using System;

    /// <summary>Provides the interface for types that represent request or response messages.</summary>
    public interface IMessage
    {
        /// <summary>Gets a collection of message headers.</summary>
        IHeaderCollection Headers { get; }

        Version HttpVersion { get; }
    }
}