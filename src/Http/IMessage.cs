using System.IO;

namespace Http
{
    public interface IMessage
    {
        IGeneralHeaderCollection Headers { get; }

        IEntity Entity { get; set; }
    }
}
