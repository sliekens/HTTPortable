namespace Http
{
    public interface IMessage
    {
        IGeneralHeaderCollection Headers { get; }

        IEntityHeaderCollection ContentHeaders { get; set; }
    }
}