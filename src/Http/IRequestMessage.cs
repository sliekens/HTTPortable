namespace Http
{
    public interface IRequestMessage : IMessage
    {
        string Method { get; }

        string RequestUri { get; }
    }
}