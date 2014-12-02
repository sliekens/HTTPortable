using System.IO;

namespace Http
{
    public interface IEntity
    {
        IEntityHeaderCollection Headers { get; set; }

        Stream Content { get; }
    }
}
