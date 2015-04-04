namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>Provides the interface for types that represent request or response messages.</summary>
    [ContractClass(typeof(ContractClassForIMessage))]
    public interface IMessage
    {
        /// <summary>Gets a collection of message headers.</summary>
        IHeaderCollection Headers { get; }

        Version HttpVersion { get; }
    }
}