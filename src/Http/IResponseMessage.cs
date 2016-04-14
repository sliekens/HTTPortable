namespace Http
{
    /// <summary>Provides the interface for response messages.</summary>
    public interface IResponseMessage : IMessage
    {
        string Reason { get; }
        int Status { get; }
    }
}