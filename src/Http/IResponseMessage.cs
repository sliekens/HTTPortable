namespace Http
{
    /// <summary>Provides the interface for response messages.</summary>
    public interface IResponseMessage : IMessage
    {
        int Status { get; set; }

        string Reason { get; set; }
    }
}