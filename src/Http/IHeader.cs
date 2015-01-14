using System.Collections.Generic;

namespace Http
{
    public interface IHeader : IList<string>
    {
        string Name { get; }

        bool Optional { get; }
    }
}