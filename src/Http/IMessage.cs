namespace Http
{
    /// <summary>Provides the interface for types that represent request or response messages.</summary>
    public interface IMessage
    {
        /// <summary>Gets a collection of message headers.</summary>
        IHeaderCollection Headers { get; }
    }
}